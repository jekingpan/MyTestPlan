using System;
using System.Collections.Generic;
using System.Linq;
using SyteLine.Classes.Core.CSIWebServices;
using Android.Util;
using System.IO;
using Android.Graphics;
using Android.Content;
using static Android.Graphics.Bitmap;
using Java.IO;

namespace SyteLine.Classes.Core.Common
{
    public class BaseBusinessObject
    {
        protected Configure configure;
        public SOAPParameters parm;
        private BaseIDOResult BaseResult;
        protected Context context;
        protected bool IsReading = false;
        protected List<string> HideDuplicatedList = new List<string>();

        public int CurrentRow { get; set; }

        public BaseBusinessObject(SOAPParameters parm, Context con = null)
        {
            CurrentRow = 0;
            context = con;
            configure = new Configure();
            DefaultParm();
        }

        public BaseBusinessObject(string Token, Context con = null)
        {
            CurrentRow = 0;
            context = con;
            configure = new Configure();
            parm = new SOAPParameters
            {
                Token = Token
            };
            DefaultParm();
        }

        public void HideDuplcatedCol(string Name)
        {
            HideDuplicatedList.Add(Name);
        }

        public bool IsDuplicatedCol(string Name)
        {
            return HideDuplicatedList.Contains(Name);
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
            return GetPropertyValue(Name, CurrentRow);
        }

        public int GetRowCount()
        {
            if (BaseResult is null)
            {
                return 0;
            }
            return BaseResult.Objects.Count;
        }

        public Bitmap GetPropertyBitmap(string Name)
        {
            try
            {
                return GetPropertyBitmap(Name, CurrentRow);
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
                return Convert.ToDecimal(GetPropertyValue(Name, CurrentRow));
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

        public int GetPropertyInt(string Name)
        {
            try
            {
                return int.Parse(GetPropertyValue(Name, CurrentRow));
            }
            catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
        }

        public int GetPropertyInt(string Name, int Row)
        {
            try
            {
                return int.Parse(GetPropertyValue(Name, Row));
            }
            catch (Exception Ex)
            {
                return 0;
                throw Ex;
            }
        }

        public bool GetPropertyBoolean(string Name)
        {
            try
            {
                return bool.Parse(GetPropertyValue(Name, CurrentRow));
            }
            catch (Exception Ex)
            {
                return false;
                throw Ex;
            }
        }

        public bool GetPropertyBoolean(string Name, int Row)
        {
            try
            {
                return (GetPropertyValue(Name, Row) == "1");
            }
            catch (Exception Ex)
            {
                return false;
                throw Ex;
            }
        }

        public virtual string GetPropertyDisplayedValue(string Name)
        {
            try
            {
                return GetPropertyDisplayedValue(Name, CurrentRow);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public virtual string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                default:
                    value = GetPropertyValue(Name, Row);
                    break;
            } 
            return value;
        }

        public string GetPropertyValue(string Name, int Row)
        {
            return BaseResult.GetPropertyValue(Name, Row);
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

        private void CallBySOAP()
        {
            try
            {
                parm.Url = configure.UpdateUrl();
                GetObjectsBySOAPREST.CallSOAP(ref parm);
                BaseResult = new CSIJsonSOAP().PasreJson(parm.OutPutJsonString);
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
                BaseResult = new CSIJsonREST().PasreJson(parm.OutPutJsonString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void New()
        {
            BaseResult.InsertRow();
        }

        public void Delete(int Row)
        {
            BaseResult.DeleteRow(Row);
        }

        public void SetPropertyValue(string Name, string Value)
        {
            BaseResult.SetPropertyValue(Name, CurrentRow, Value);
        }

        public void SetPropertyValue(string Name, int Row, string Value)
        {
            BaseResult.SetPropertyValue(Name, Row, Value);
        }

        public void SetPropertyBitmap(string Name, Bitmap Pic)
        {
            SetPropertyBitmap(Name, CurrentRow, Pic);
        }

        public void SetPropertyBitmap(string Name, int Row, Bitmap Pic)
        {
            try
            {
                MemoryStream output = new MemoryStream();
                Pic.Compress(CompressFormat.Jpeg, 100, output);
                byte[] PictureBytes = output.ToArray();
                string PictureBase64 = System.Convert.ToBase64String(PictureBytes);
                SetPropertyValue(Name, Row, PictureBase64);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void CleanResult()
        {
            BaseResult = null;
        }
    }
    

}