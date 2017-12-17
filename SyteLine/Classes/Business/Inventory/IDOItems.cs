using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;
using Android.Graphics;
using System;
using Android.Content;

namespace SyteLine.Classes.Business.Inventory
{
    public class IDOItems : BaseBusinessObject
    {
        public IDOItems(SOAPParameters parm, Context con = null) : base(parm, con)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOItems(string Token, Context con = null) : base(Token, con)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLItems";
            parm.PropertyList = "Item,Description,UM,MatlType,PMTCode,ProductCode,LotTracked,SerialTracked,Picture"
                + ",DerQtyOnHand,DerQtyAvailable,DerQtyOrdered,DerQtyReorder,DerQtyRsvdCo,DerQtyTrans,DerQtyWip"
                + ",QtyAllocjob,DerQtyAllocCo";
                //+ ",DerQtyAllocTrn,DerSafetyStock,SafetyStockPercent"
                //+ ",DerQtyPurYtd,DerQtySoldYtd,QtyMfgYtd,QtyUsedYtd";
        }

        public void BuilderFilterByItem(string Item)
        {
            parm.Filter = string.Format("Item Like N'{0}'", Item);
        }

        public void BuilderFilterByItemOrDesc(string Item)
        {
            parm.Filter = string.Format("Item Like N'{0}' OR Description Like N'{0}'", Item);
        }

        public string GetItem( int index = 0)
        {
            return base.GetPropertyValue("Item", index);
        }

        public string GetDescription(int index = 0)
        {
            return base.GetPropertyValue("Description", index);
        }

        public string GetUM(int index = 0)
        {
            return base.GetPropertyValue("UM", index);
        }

        public string GetOverview(int index = 0)
        {
            return base.GetPropertyValue("Overview", index);
        }

        public string GetMatlType(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("MatlType", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else{
                if (value == "M")
                {
                    value = context.GetString(Resource.String.Material);
                }
                else if (value == "T")
                {
                    value = context.GetString(Resource.String.Tool);
                }
                else if (value == "F")
                {
                    value = context.GetString(Resource.String.Fixture);
                }
                else if (value == "O")
                {
                    value = context.GetString(Resource.String.Other);
                }
                return value;
            }
            
        }

        public string GetPMTCode(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("PMTCode", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else
            {
                if (value == "M")
                {
                    value = context.GetString(Resource.String.Manufactured);
                }
                else if (value == "P")
                {
                    value = context.GetString(Resource.String.Purchased);
                }
                else if (value == "T")
                {
                    value = context.GetString(Resource.String.Transferred);
                }
                return value;
            }
        }

        public string GetProductCode(int index = 0)
        {
            return base.GetPropertyValue("ProductCode", index);
        }

        public bool GetLotTracked(int index = 0)
        {
            return base.GetPropertyValue("LotTracked", index) == "1" ? true : false;
        }

        public bool GetSerialTracked(int index = 0)
        {
            return base.GetPropertyValue("SerialTracked", index) == "1" ? true : false;
        }

        public Bitmap GetPicture(int index = 0)
        {
            return base.GetPropertyBitmap("Picture", index);
        }

        public string GetQtyOnHand(int index = 0,string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerQtyOnHand", index));
        }

        public string GetQtyAvailable(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerQtyAvailable", index));
        }
    }
}