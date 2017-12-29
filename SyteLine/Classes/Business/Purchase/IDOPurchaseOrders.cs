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

        private string GetOrderDate(int index = 0)
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

        private string GetStat(int index = 0, int rtnCode = 0)
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

        private string GetType(int index = 0, int rtnCode = 0)
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

        private string GetPoCost(int index = 0,string Format = "{0:###############0.000#####}")
        {
            return string.Format(Format, Convert.ToDecimal(base.GetPropertyValue("PoCost", index)));
        }

        public override string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                case "Type":
                    value = GetType(Row);
                    break;
                case "Stat":
                    value = GetStat(Row);
                    break;
                case "OrderDate":
                    value = GetOrderDate(Row);
                    break;
                case "PoCost":
                    value = GetPoCost(Row);
                    break; 
                default:
                    break;
            }
            return value;
        }
    }
}