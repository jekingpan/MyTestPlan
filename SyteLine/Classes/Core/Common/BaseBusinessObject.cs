using System;
using System.Collections.Generic;
using System.Linq;
using SyteLine.Classes.Core.CSIWebServices;
using Android.Util;
using System.IO;
using Android.Graphics;
using Android.Content;

namespace SyteLine.Classes.Core.Common
{
    public class BaseBusinessObject
    {
        protected Configure configure;
        public SOAPParameters parm;
        private IDOResult IDOResult;
        protected Context context;
        protected bool IsReading = false;

        public BaseBusinessObject(SOAPParameters parm, Context con = null)
        {
            context = con;
            configure = new Configure();
            DefaultParm();
        }


        public BaseBusinessObject(string Token, Context con = null)
        {
            context = con;
            configure = new Configure();
            parm = new SOAPParameters
            {
                Token = Token
            };
            DefaultParm();
        }

        protected virtual void DefaultParm()
        {            
            parm.RecordCap = int.Parse(new Configure().RecordCap);
            parm.LoadCommand = GetObjectsBySOAPREST.LoadJson;
            parm.SaveCommand = GetObjectsBySOAPREST.SaveJson;
        }

        public void BuilderFilter(string Filter, string Oper = "")
        {
            if (Oper == "")
            {
                parm.Filter = Filter;
            }
            else
            {
                parm.Filter = string.Format("{0} {1} {2}", parm.Filter, Oper, Filter);
            }
        }

        public void BuilderAdditionalFilter(string Filter, string Oper = "AND")
        {
            if (parm.Filter == "")
            {
                parm.Filter = Filter;
            }
            else
            {
                parm.Filter = string.Format("({0}) {1} ({2})", parm.Filter, Oper, Filter);
            }
        }

        public void SetOrderBy(string OrderBy)
        {
            parm.OrderBy = OrderBy;
        }

        public string GetPropertyValue(string Name)
        {
            return GetPropertyValue(Name, 0);
        }

        public int GetRowCount()
        {
            if (IDOResult is null)
            {
                return 0;
            }
            return IDOResult.ObjectItems.Count;
        }

        public Bitmap GetPropertyBitmap(string Name)
        {
            try
            {
                return GetPropertyBitmap(Name, 0);
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }

        public Bitmap GetPropertyBitmap(string Name, int Row)
        {
            try
            {
                string PictureBase64 = GetPropertyValue(Name, Row);
                if (PictureBase64 == null)
                {
                    return null;
                }
                byte[] PictureBytes = System.Convert.FromBase64String(PictureBase64);
                System.IO.Stream stream = new MemoryStream(PictureBytes);
                return BitmapFactory.DecodeStream(stream);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Decimal GetPropertyDecimalValue(string Name)
        {
            try
            {
                return Convert.ToDecimal(GetPropertyValue(Name, 0));
            }catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
        }

        public Decimal GetPropertyDecimalValue(string Name, int Row)
        {
            try
            {
                return Convert.ToDecimal(GetPropertyValue(Name, Row));
            }
            catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
        }

        public string GetPropertyValue(string Name, int Row)
        {
            string value = "";
            if (IDOResult.ResultType == "SOAP")
            {
                int colIndex = IDOResult.ObjectNames.IndexOf(Name);
                if (colIndex >= 0)
                {
                    if (Row >= 0 && Row < IDOResult.ObjectItems.Count())
                    {
                        value = IDOResult.ObjectItems[Row].ObjectItem[colIndex].ItemValue;
                    }
                }
            }
            else if (IDOResult.ResultType == "REST")
            {
                value = IDOResult.ObjectItems[Row].GetItemValue(Name);
            }
            return value;
        }

        public void Read()
        {
            if (IsReading)
            {
                return;
            }
            parm.Command = parm.LoadCommand;
            try
            {
                IsReading = true;
                if (configure.UseRESTForRequest)
                {
                    CallByREST();
                }
                else
                {
                    CallBySOAP();
                }
                IsReading = false;
            }
            catch (Exception Ex)
            {
                IsReading = false;
                throw Ex;
            }
        }
        
        private void PasreSOAPJson()
        {
            IDOResult = new IDOResult("SOAP");
            ReadSOAPJsonHead(new JsonReader(new Java.IO.StringReader(parm.OutPutJsonString)));
        }

        private void ReadSOAPJsonHead(JsonReader jReader)
    {
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("Items"))
                {
                    ReadSOAPItemsArray(jReader);
                }
                else if (name.Equals("PropertyList"))
                {
                    jReader.BeginArray();
                    while (jReader.HasNext)
                    {
                        IDOResult.ObjectNames.Add(jReader.NextString());
                    }
                    jReader.EndArray();
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            jReader.EndObject();
            jReader.Close();
        }

        private void ReadSOAPItemsArray(JsonReader jReader)
        {
            jReader.BeginArray();
            while (jReader.HasNext)
            {
                ReadSOAPItem(jReader);
            }
            jReader.EndArray();
        }

        private void ReadSOAPItem(JsonReader jReader)
        {
            IDOObject col = new IDOObject();
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("EditStatus"))
                {
                    col.EditStatus = jReader.NextInt();
                }
                else if (name.Equals("ID"))
                {
                    col.ID = jReader.NextString();
                }
                else if(name.Equals("Properties"))
                {
                    ReadSOAPPropertiesArray(jReader, ref col);
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            jReader.EndObject();
            IDOResult.ObjectItems.Add(col);
        }

        private void ReadSOAPPropertiesArray(JsonReader jReader, ref IDOObject col)
        {
            jReader.BeginArray();
            
            while (jReader.HasNext)
            {
                IDOObjectItem propertyValues = new IDOObjectItem();
                jReader.BeginObject();
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Property"))
                    {
                        JsonToken peek = jReader.Peek();
                        if (peek == JsonToken.Null)
                        {
                            jReader.SkipValue();
                        }
                        else
                        {
                            propertyValues.ItemValue = jReader.NextString();
                        }
                    }
                    else if (name.Equals("Updated"))
                    {
                        propertyValues.Updated = jReader.NextBoolean();
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
                col.ObjectItem.Add(propertyValues);
            }
            jReader.EndArray();            
        }        

        private void CallBySOAP()
        {
            try
            {
                parm.Url = configure.UpdateUrl();
                GetObjectsBySOAPREST.CallSOAP(ref parm);
                PasreSOAPJson();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CallByREST()
        {
            try
            {
                parm.Url = configure.UpdateUrl();
                GetObjectsBySOAPREST.CallREST(ref parm);
                PasreRESTJson();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void PasreRESTJson()
        {
            try
            {
                IDOResult = new IDOResult("REST");
                ReadRESTJsonHead(new JsonReader(new Java.IO.StringReader(parm.OutPutJsonString)));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void ReadRESTJsonHead(JsonReader jReader)
        {
            jReader.BeginObject();
            try
            {
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Items"))
                    {
                        ReadRESTItemsArray(jReader);
                    }
                    else if (name.Equals("Message"))
                    {
                        string msg = jReader.NextString();
                        if (!msg.Equals("Success"))
                        {
                            throw new Exception(msg);
                        }
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
             }
            catch (Exception Ex)
            {
                throw Ex;
            }
            jReader.Close();
        }

        private void ReadRESTItemsArray(JsonReader jReader)
        {
            try
            {
                jReader.BeginArray();
                while (jReader.HasNext)
                {
                    ReadRESTPropertiesArray(jReader);
                }
                jReader.EndArray();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            //jReader.Close();
        }

        private void ReadRESTPropertiesArray(JsonReader jReader)
        {
            try
            {
                IDOObject col = new IDOObject();
                IDOResult.ObjectItems.Add(col);
                jReader.BeginArray();
                while (jReader.HasNext)
                {
                    ReadRESTProperties(jReader);
                }
                jReader.EndArray();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void ReadRESTProperties(JsonReader jReader)
        {
            try
            {
                IDOObjectItem item = new IDOObjectItem();
                jReader.BeginObject();
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Name"))
                    {
                        item.ItemName = jReader.NextString();
                    }
                    else if (name.Equals("Value"))
                    {
                        JsonToken peek = jReader.Peek();
                        if (peek == JsonToken.Null)
                        {
                            jReader.SkipValue();
                        }
                        else
                        {
                            item.ItemValue = jReader.NextString();
                        }
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
                IDOResult.ObjectItems.Last().ObjectItem.Add(item);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }

    public class IDOResult
    {
        public List<string> ObjectNames = new List<string>();
        public List<IDOObject> ObjectItems = new List<IDOObject>();
        public string ResultType = "SOAP";//SOAP or REST

        public IDOResult(string ResultType)
        {
            this.ResultType = ResultType;
        }
    }

    public class IDOObject
    {
        public int EditStatus;
        public string ID;
        public List<IDOObjectItem> ObjectItem = new List<IDOObjectItem>();


        public string GetItemValue(string Name)
        {
            foreach (IDOObjectItem it in ObjectItem)
            { 
                if (it.ItemName == Name)
                {
                    return it.ItemValue;
                }
            }
            return "";
        }
    }

    public class IDOObjectItem
    {
        public string ItemName { get; set; }
        public string ItemValue { get; set; }
        public bool Updated { get; set; }
    }
    

}