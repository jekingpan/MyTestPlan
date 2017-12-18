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

        public string GetPoNum(int index = 0)
        {
            return GetPropertyValue("PoNum", index);
        }

        public string GetPoLine(int index = 0)
        {
            return GetPropertyValue("PoLine", index);
        }

        public string GetVenadrName(int index = 0)
        {
            return GetPropertyValue("VenadrName", index);
        }

        public string GetStat(int index = 0, int rtnCode = 0)
        {
            string value = GetPropertyValue("Type", index);
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

        public string GetPromiseDate(int index = 0)
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

        public string GetDueDate(int index = 0)
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

        public string GetPoOrderDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(GetPropertyValue("PoOrderDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
            } catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string GetPoStat(int index = 0, int rtnCode = 0)
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

        public string GetWhse(int index = 0)
        {
            return GetPropertyValue("Whse", index);
        }

        public string GetItem(int index = 0)
        {
            return GetPropertyValue("Item", index);
        }

        public string GetDescription(int index = 0)
        {
            return GetPropertyValue("Description", index);
        }

        public string GetDerItemOverview(int index = 0)
        {
            return GetPropertyValue("DerItemOverview", index);
        }

        public string GetQtyOrderedConv(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyOrderedConv", index));
        }

        public string GetUM(int index = 0)
        {
            return GetPropertyValue("UM", index);
        }

        public string GetPoVendNum(int index = 0)
        {
            return GetPropertyValue("PoVendNum", index);
        }

        public string GetPoVendorPo(int index = 0)
        {
            return GetPropertyValue("PoVendorPo", index);
        }

        public string GetVendItem(int index = 0)
        {
            return GetPropertyValue("VendItem", index);
        }

        public string GetManufacturerId(int index = 0)
        {
            return GetPropertyValue("ManufacturerId", index);
        }

        public string GetManufacturerName(int index = 0)
        {
            return GetPropertyValue("ManufacturerName", index);
        }

        public string GetManufacturerItem(int index = 0)
        {
            return GetPropertyValue("ManufacturerItem", index);
        }

        public string GetManufacturerItemDesc(int index = 0)
        {
            return GetPropertyValue("ManufacturerItemDesc", index);
        }

        public string GetRefType(int index = 0)
        {
            return GetPropertyValue("RefType", index);
        }

        public string GetRefNum(int index = 0)
        {
            return GetPropertyValue("RefNum", index);
        }

        public string GetRefLineSuf(int index = 0)
        {
            return GetPropertyValue("RefLineSuf", index);
        }

        public string GetRefRelease(int index = 0)
        {
            return GetPropertyValue("PoRefRelease", index);
        }

        public string ShipAddr(int index = 0)
        {
            return GetPropertyValue("ShipAddr", index);
        }
        public string GetDropShipNo(int index = 0)
        {
            return GetPropertyValue("DropShipNo", index);
        }

        public string GetDropSeq(int index = 0)
        {
            return GetPropertyValue("DropSeq", index);
        }

        public string GetDerShipToAddr(int index = 0)
        {
            return GetPropertyValue("DerShipToAddr", index);
        }
    }
}