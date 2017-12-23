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

        private string GetMatlType(int index = 0, int rtnCode = 0)
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

        private string GetPMTCode(int index = 0, int rtnCode = 0)
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

        private string GetQtyOnHand(int index = 0,string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerQtyOnHand", index));
        }

        private string GetQtyAvailable(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerQtyAvailable", index));
        }

        public override string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                case "MatlType":
                    value = GetMatlType(Row);
                    break;
                case "PMTCode":
                    value = GetPMTCode(Row);
                    break;
                case "DerQtyOnHand":
                    value = GetQtyOnHand(Row);
                    break;
                case "DerQtyAvailable":
                    value = GetQtyAvailable(Row);
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}