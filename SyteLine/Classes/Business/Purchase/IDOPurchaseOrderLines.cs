using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;
using System;
using Android.Content;

namespace SyteLine.Classes.Business.Purchase
{
    public class IDOPurchaseOrderLines : BaseBusinessObject
    {
        public IDOPurchaseOrderLines(SOAPParameters parm, Context con = null) : base(parm, con)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOPurchaseOrderLines(string Token, Context con = null) : base(Token, con)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLPoItems";
            parm.PropertyList = "PoNum,PoLine,VenadrName,Item,Description,DerItemOverview,QtyOrderedConv,UM"
                + ",Stat,DueDate,PromiseDate,PoVendNum,PoVendorPo,PoStat,PoOrderDate,VendItem,Whse"
                + ",ManufacturerId,ManufacturerName,ManufacturerItem,ManufacturerItemDesc"
                + ",RefType,RefNum,RefLineSuf,RefRelease,ShipAddr,DropShipNo,DropSeq,DerShipToAddr";
        }

        public void BuilderFilterByPoNum(string PoNum)
        {
            parm.Filter = string.Format("PoNum Like N'{0}'", PoNum);
        }
        
        private string GetStat(int index = 0, int rtnCode = 0)
        {
            string value = GetPropertyValue("Stat", index);
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
                else if (value == "F")
                {
                    value = context.GetString(Resource.String.Filled);
                }
                return value;
            }
        }

        private string GetPromiseDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(GetPropertyValue("PromiseDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetDueDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(GetPropertyValue("DueDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetPoOrderDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(GetPropertyValue("PoOrderDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
            } catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetPoStat(int index = 0, int rtnCode = 0)
        {
            string value = GetPropertyValue("PoStat", index);
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

        private string GetQtyOrderedConv(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyOrderedConv", index));
        }

        public override string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                case "PoStat":
                    value = GetPoStat(Row);
                    break;
                case "Stat":
                    value = GetStat(Row);
                    break;
                case "PromiseDate":
                    value = GetPromiseDate(Row);
                    break;
                case "DueDate":
                    value = GetDueDate(Row);
                    break;
                case "PoOrderDate":
                    value = GetPoOrderDate(Row);
                    break;
                case "QtyOrderedConv":
                    value = GetQtyOrderedConv(Row);
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}