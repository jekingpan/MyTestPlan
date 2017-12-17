using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;
using System;
using Android.Content;

namespace SyteLine.Classes.Business.Purchase
{
    public class IDOPurchaseOrders : BaseBusinessObject
    {
        public IDOPurchaseOrders(SOAPParameters parm, Context con = null) : base(parm, con)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOPurchaseOrders(string Token, Context con = null) : base(Token, con)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLPos";
            parm.PropertyList = "PoNum,OrderDate,Stat,Type,VendNum,VendorName,TermsCode,TermsCodeDesc"
                + ",ShipCode,ShipCodeDesc,PoCost,Whse,BuilderPoOrigSite,BuilderPoNum,DerPchChgNum,DerPchStat"
                + ",VendLcrNum,ShipAddr,FormatedShipToAddress,Buyer,ReqNum";
        }

        public void BuilderFilterByPoNum(string PoNum)
        {
            parm.Filter = string.Format("PoNum Like N'{0}'", PoNum);
        }

        public void BuilderFilterByPoNumOrVendNum(string Value)
        {
            parm.Filter = string.Format("PoNum Like N'{0}' OR VendNum Like N'{0}'", Value);
        }

        public void BuilderFilterByPoNumOrVendNumOrVendorName(string Value)
        {
            parm.Filter = string.Format("PoNum Like N'{0}' OR VendNum Like N'{0}' OR VendorName Like N'{0}'", Value);
        }

        public string GetPoNum( int index = 0)
        {
            return base.GetPropertyValue("PoNum", index);
        }

        public string GetOrderDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(base.GetPropertyValue("OrderDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
                //return Convert.ToDateTime(base.GetPropertyValue("OrderDate", index)).ToShortDateString();
            }catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string GetStat(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("Stat", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else
            {
                if (value == "P")
                {
                    value = context.GetString(Resource.String.Plan);
                }
                else if (value == "O")
                {
                    value = context.GetString(Resource.String.Ordered);
                }
                else if (value == "C")
                {
                    value = context.GetString(Resource.String.Complete);
                }
                else if (value == "H")
                {
                    value = context.GetString(Resource.String.History);
                }
                return value;
            }
        }

        public string GetType(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("Type", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else{
                if (value == "R")
                {
                    value = context.GetString(Resource.String.Regular);
                }
                else if (value == "B")
                {
                    value = context.GetString(Resource.String.Blanket);
                }
                return value;
            }            
        }
        
        public string GetVendNum(int index = 0)
        {
            return base.GetPropertyValue("VendNum", index);
        }
        
        public string GetVendorName(int index = 0)
        {
            return base.GetPropertyValue("VendorName", index);
        }

        public string GetTermsCode(int index = 0)
        {
            return base.GetPropertyValue("TermsCode", index);
        }

        public string GetTermsCodeDesc(int index = 0)
        {
            return base.GetPropertyValue("TermsCodeDesc", index);
        }

        public string GetShipCode(int index = 0)
        {
            return base.GetPropertyValue("ShipCode", index);
        }

        public string GetPoCost(int index = 0,string Format = "{0:###############0.000#####}")
        {
            return string.Format(Format, Convert.ToDecimal(base.GetPropertyValue("PoCost", index)));
        }

        public string GetWhse(int index = 0)
        {
            return base.GetPropertyValue("Whse", index);
        }

        public string GetBuilderPoOrigSite(int index = 0)
        {
            return base.GetPropertyValue("BuilderPoOrigSite", index);
        }

        public string GetBuilderPoNum(int index = 0)
        {
            return base.GetPropertyValue("BuilderPoNum", index);
        }

        public string GetDerPchChgNum(int index = 0)
        {
            return base.GetPropertyValue("DerPchChgNum", index);
        }

        public string GetDerPchStat(int index = 0)
        {
            return base.GetPropertyValue("DerPchStat", index);
        }

        public string GetVendLcrNum(int index = 0)
        {
            return base.GetPropertyValue("VendLcrNum", index);
        }

        public string GetShipAddr(int index = 0)
        {
            return base.GetPropertyValue("ShipAddr", index);
        }

        public string GetFormatedShipToAddress(int index = 0)
        {
            return base.GetPropertyValue("FormatedShipToAddress", index);
        }

        public string GetBuyer(int index = 0)
        {
            return base.GetPropertyValue("Buyer", index);
        }

        public string GetReqNum(int index = 0)
        {
            return base.GetPropertyValue("ReqNum", index);
        }

    }
}