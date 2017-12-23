using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;
using Android.Graphics;
using System;
using Android.Content;

namespace SyteLine.Classes.Business.Inventory
{
    public class IDODcmoves : BaseBusinessObject
    {
        public IDODcmoves(SOAPParameters parm, Context con = null) : base(parm, con)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDODcmoves(string Token, Context con = null) : base(Token, con)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLDcmoves";
            parm.PropertyList = "TransNum,TransType,Stat,Termid,TransDate,EmpNum,EmpName,Item,ItemDescription"
                + ",Whse,Loc1,Lot1,Loc2,Lot2,QtyMoved,UM,DocumentNum";
        }

        public void BuilderFilterByEmpNum(string EmpNum)
        {
            parm.Filter = string.Format("EmpNum Like N'{0}'", EmpNum);
        }

        public void BuilderFilterByTransNum(string TransNum)
        {
            parm.Filter = string.Format("TransNum Like N'{0}'", TransNum);
        }

        public void BuilderFilterByItemOrDesc(string Item)
        {
            parm.Filter = string.Format("Item Like N'{0}' OR ItemDescription Like N'{0}'", Item);
        }

        private string GetStat(int index = 0, int rtnCode = 0)
        {
            string value = base.GetPropertyValue("Stat", index);
            if (rtnCode == 1 || context is null)
            {
                return value;
            }
            else{
                if (value == "E")
                {
                    value = context.GetString(Resource.String.Error);
                }
                else if (value == "P")
                {
                    value = context.GetString(Resource.String.Pending);
                }
                else if (value == "U")
                {
                    value = context.GetString(Resource.String.Unposted);
                }
                return value;
            }
            
        }

        private string GetTransDate(int index = 0)
        {
            try
            {
                return DateTime.ParseExact(base.GetPropertyValue("TransDate", index), "yyyyMMdd HH:mm:ss.fff", System.Globalization.CultureInfo.CurrentCulture).ToShortDateString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetQtyMoved(int index = 0,string Format = "{0:###,###,###,###,##0.00######}")
        {
            return string.Format(Format, GetPropertyDecimalValue("QtyMoved", index));
        }

        public override string GetPropertyDisplayedValue(string Name, int Row)
        {
            string value = "";
            switch (Name)
            {
                case "Stat":
                    value = GetStat(Row);
                    break;
                case "QtyMoved":
                    value = GetQtyMoved(Row);
                    break;
                case "TransDate":
                    value = GetTransDate(Row);
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}