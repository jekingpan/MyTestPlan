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

        public string GetItem( int index = 0)
        {
            return base.GetPropertyValue("Item", index);
        }

        public string GetItmDescription(int index = 0)
        {
            return base.GetPropertyValue("ItmDescription", index);
        }

        public string GetWhse(int index = 0)
        {
            return base.GetPropertyValue("Whse", index);
        }

        public string GetWhsName(int index = 0)
        {
            return base.GetPropertyValue("WhsName", index);
        }

        public string GetLocDescription(int index = 0)
        {
            return base.GetPropertyValue("LocDescription", index);
        }

        public string GetLoc(int index = 0)
        {
            return base.GetPropertyValue("Loc", index);
        }

        public string GetRank(int index = 0)
        {
            return base.GetPropertyValue("Rank", index);        }


        public string GetLocType(int index = 0, int rtnCode = 0)
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

        public string GetItmIssueBy(int index = 0, int rtnCode = 0)
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
        
        public string GetQtyOnHand(int index = 0,string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyOnHand", index));
        }

        public string GetItmwhseQtyOnHand(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("ItmwhseQtyOnHand", index));
        }

        public string GetQtyRsvd(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyRsvd", index));
        }

        public string GetWhseTotalRsvdCO(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerIWhseTotalRsvdCO", index));
        }

        public string GetWhseTotalNonNetStock(int index = 0, string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("DerIWhseTotalNonNetStock", index));
        }
    }
}