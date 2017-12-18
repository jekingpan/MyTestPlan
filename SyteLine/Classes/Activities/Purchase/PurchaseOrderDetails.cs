using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Business.Inventory;
using SyteLine.Classes.Business.Purchase;
using SyteLine.Classes.Adapters.Common;
using SyteLine.Classes.Adapters.Purchase;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/PurchaseOrderDetails")]
    public class PurchaseOrderDetails : BaseDetailActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.BaseObject = new IDOPurchaseOrders(base.Intent.GetStringExtra("SessionToken"), this);
            base.AddSecondObject(new IDOPurchaseOrderLines(base.Intent.GetStringExtra("SessionToken"), this));
            base.OnCreate(savedInstanceState);
        }

        protected override void RegisterAdapter(bool Append)
        {
            List<AdapterList> Rows;
            try
            {
                base.RegisterAdapter(Append);

                IDOPurchaseOrders POs = (IDOPurchaseOrders)BaseObject;

                SetKey(POs.GetPoNum());
                SetSubKey(POs.GetVendNum());
                SetSubKeyDescription(POs.GetVendorName());

                Rows = new List<AdapterList>();
                for (int i = 0; i < POs.GetRowCount(); i++)
                {
                    Rows.Add(new AdapterList("-", "", GetString(Resource.String.General)));
                    Rows.Add(new AdapterList("Type", POs.GetType(i), GetString(Resource.String.Type)));
                    Rows.Add(new AdapterList("Stat", POs.GetStat(i), GetString(Resource.String.Status)));
                    Rows.Add(new AdapterList("Whse", POs.GetWhse(i), GetString(Resource.String.Warehouse)));
                    Rows.Add(new AdapterList("OrderDate", POs.GetOrderDate(i), GetString(Resource.String.OrderDate)));
                    Rows.Add(new AdapterList("Buyer", POs.GetBuyer(i), GetString(Resource.String.Buyer)));
                }

                IDOPurchaseOrderLines POLines = (IDOPurchaseOrderLines)SencondObjects[1];
                for (int i = 0; i < POLines.GetRowCount(); i++)
                {
                    Rows.Add(new AdapterList("--", POLines.GetPoLine(i), string.Format("{0} - {1}", GetString(Resource.String.OrderLine), POLines.GetPoLine(i))));
                    Rows.Add(new AdapterList("Item", POLines.GetItem(i), GetString(Resource.String.Item)));
                    Rows.Add(new AdapterList("Description", POLines.GetDescription(i), GetString(Resource.String.Description)));
                    if (!string.IsNullOrEmpty(POLines.GetDerItemOverview(i)))
                    {
                        Rows.Add(new AdapterList("DerItemOverview", POLines.GetDerItemOverview(i), GetString(Resource.String.Overview)));
                    }
                    Rows.Add(new AdapterList("QtyOrderedConv", POLines.GetQtyOrderedConv(i), GetString(Resource.String.Quantity)));
                    Rows.Add(new AdapterList("UM", POLines.GetUM(i), GetString(Resource.String.UM)));
                    Rows.Add(new AdapterList("Stat", POLines.GetStat(i), GetString(Resource.String.Status)));
                    Rows.Add(new AdapterList("DueDate", POLines.GetDueDate(i), GetString(Resource.String.DueDate)));
                    Rows.Add(new AdapterList("Whse", POLines.GetWhse(i), GetString(Resource.String.Warehouse)));
                }
                ListView.Adapter = new PurchaseOrderDetailsAdapter(this, Rows);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            try
            {
                IDOPurchaseOrders Orders = (IDOPurchaseOrders)BaseObject;
                Orders.parm.PropertyList = "PoNum,OrderDate,Stat,Type,VendNum,VendorName,Whse,Buyer";
                Orders.BuilderFilterByPoNum(Intent.GetStringExtra("PoNum"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        protected override void PrepareIDOs(int index)
        {
            base.PrepareIDOs(index);
            try
            {
                if (index == 1)
                {
                    IDOPurchaseOrderLines poLines = (IDOPurchaseOrderLines)GetSecondObject(index);
                    poLines.parm.PropertyList = "PoNum,PoLine,VenadrName,Item,Description,DerItemOverview,QtyOrderedConv,UM"
                    + ",Stat,DueDate,PromiseDate,Whse";
                    poLines.BuilderFilterByPoNum(Intent.GetStringExtra("PoNum"));
                    poLines.parm.RecordCap = -1;
                    poLines.SetOrderBy("PoLine");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
    }
}
