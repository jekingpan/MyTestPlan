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
        private BaseIDOResult BaseResult;
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

        public int GetPropertyInt(string Name)
        {
            try
            {
                return int.Parse(GetPropertyValue(Name, 0));
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
                return bool.Parse(GetPropertyValue(Name, 0));
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
                return bool.Parse(GetPropertyValue(Name, Row));
            }
            catch (Exception Ex)
            {
                return false;
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
    }
    

}