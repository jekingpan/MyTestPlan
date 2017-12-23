using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;
using Android.Graphics;
using System;
using Android.Content;

namespace SyteLine.Classes.Business.Inventory
{
    public class IDOItemLocs : BaseBusinessObject
    {
        public IDOItemLocs(SOAPParameters parm, Context con = null) : base(parm, con)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOItemLocs(string Token, Context con = null) : base(Token, con)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLItemLocs";
            parm.PropertyList = "Item,Whse,WhsName,ItmDescription,ItmwhseQtyOnHand,Loc,LocDescription,LocType,Rank"
                + ",QtyOnHand,QtyRsvd,DerIWhseTotalRsvdCO,DerIWhseTotalNonNetStock,ItmIssueBy";
            parm.OrderBy = "Item,Whse,Loc";
        }

        public void BuilderFilterByWhse(string Whse)
        {
            parm.Filter = string.Format("Whse Like N'{0}'", Whse);
        }

        public void BuilderFilterByLoc(string Loc)
        {
            parm.Filter = string.Format("Loc Like N'{0}'", Loc);
        }
        
        public void BuilderFilterByWhseAndLoc(string Whse, string Loc)
        {
            parm.Filter = string.Format("Whse Like N'{0}' AND ItmDescription Like N'{1}'", Loc);
        }
        
        public void BuilderFilterByItem(string Item)
        {
            parm.Filter = string.Format("Item Like N'{0}'", Item);
        }

        public void BuilderFilterByItemOrDesc(string Item)
        {
            parm.Filter = string.Format("Item Like N'{0}' OR ItmDescription Like N'{0}'", Item);
        }

        private string GetLocType(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("LocType", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else{
                if (value == "S")
                {
                    value = context.GetString(Resource.String.Stock);
                }
                else if (value == "T")
                {
                    value = context.GetString(Resource.String.Transit);
                }
                return value;
            }
        }

        private string GetItmIssueBy(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("ItmIssueBy", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else
            {
                if (value == "LOT")
                {
                    value = context.GetString(Resource.String.Lot);
                }
                else if (value == "LOC")
                {
                    value = context.GetString(Resource.String.Location);
                }
                else if (value == "EXP")
                {
                    value = context.GetString(Resource.String.ExpirationDate);
                }
                return value;
            }
        }

        private string GetQtyOnHand(int index = 0,string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyOnHand", index));
        }

        private string GetItmwhseQtyOnHand(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("ItmwhseQtyOnHand", index));
        }

        private string GetQtyRsvd(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyRsvd", index));
        }

        private string GetWhseTotalRsvdCO(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerIWhseTotalRsvdCO", index));
        }

        private string GetWhseTotalNonNetStock(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerIWhseTotalNonNetStock", index));
        }

        public override string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                case "ItmIssueBy":
                    value = GetItmIssueBy(Row);
                    break;
                case "LocType":
                    value = GetLocType(Row);
                    break;
                case "QtyOnHand":
                    value = GetQtyOnHand(Row);
                    break;
                case "ItmwhseQtyOnHand":
                    value = GetItmwhseQtyOnHand(Row);
                    break;
                case "QtyRsvd":
                    value = GetQtyRsvd(Row);
                    break;
                case "DerIWhseTotalRsvdCO":
                    value = GetWhseTotalRsvdCO(Row);
                    break;
                case "DerIWhseTotalNonNetStock":
                    value = GetWhseTotalNonNetStock(Row);
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}