using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Business.Inventory;
using SyteLine.Classes.Business.Purchase;
using SyteLine.Classes.Adapters.Common;
using SyteLine.Classes.Adapters.Purchase;
using static SyteLine.Classes.Adapters.Common.AdapterListItem;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/PurchaseOrderDetails")]
    public class PurchaseOrderDetails : CSIBaseDetailActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.BaseObject = new IDOPurchaseOrders(base.Intent.GetStringExtra("SessionToken"), this);
            base.AddSecondObject(new IDOPurchaseOrderLines(base.Intent.GetStringExtra("SessionToken"), this));
            base.OnCreate(savedInstanceState);
        }
        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            try
            {
                IDOPurchaseOrders Orders = (IDOPurchaseOrders)BaseObject;
                Orders.parm.PropertyList = "";
                SetAdapterLists(0, "PoNum", "PoNum", ValueTypes.String, GetString(Resource.String.General), Resource.Layout.CommonSplitterViewer);
                SetAdapterLists(0, "Type", "Type", ValueTypes.String, GetString(Resource.String.Type));
                SetAdapterLists(0, "Stat", "Stat", ValueTypes.String, GetString(Resource.String.Status));
                SetAdapterLists(0, "Whse", "Whse", ValueTypes.String, GetString(Resource.String.Warehouse));
                SetAdapterLists(0, "OrderDate", "OrderDate", ValueTypes.Date, GetString(Resource.String.OrderDate));
                SetAdapterLists(0, "Buyer", "Buyer", ValueTypes.String, GetString(Resource.String.Buyer));
            
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
                    poLines.parm.PropertyList = "PoNum";
                    SetAdapterLists(1, "PoLine", "PoLine", ValueTypes.String, GetString(Resource.String.OrderLine) + " - {0}", Resource.Layout.CommonSplitterViewer);
                    SetAdapterLists(1, "Item", "Item", ValueTypes.String, GetString(Resource.String.Item));
                    SetAdapterLists(1, "Description", "Description", ValueTypes.String, GetString(Resource.String.Description));
                    SetAdapterLists(1, "QtyOrderedConv", "QtyOrderedConv", ValueTypes.Decimal, GetString(Resource.String.Quantity));
                    SetAdapterLists(1, "UM", "UM", ValueTypes.String, GetString(Resource.String.UM));
                    SetAdapterLists(1, "Stat", "Stat", ValueTypes.String, GetString(Resource.String.Status));
                    SetAdapterLists(1, "DueDate", "DueDate", ValueTypes.Date, GetString(Resource.String.DueDate));
                    SetAdapterLists(1, "Whse", "Whse", ValueTypes.String, GetString(Resource.String.General));

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

        protected override void RegisterAdapter(bool Append)
        {
            try
            {
                base.RegisterAdapter(Append);

                IDOPurchaseOrders POs = (IDOPurchaseOrders)BaseObject;

                SetKey(POs.GetPoNum());
                SetSubKey(POs.GetVendNum());
                SetSubKeyDescription(POs.GetVendorName());
                
                ListView.Adapter = new PurchaseOrderDetailsAdapter(this, AdapterLists);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override string UpdatePropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            string value = "";
            switch (objIndex)
            {
                case 0:
                    IDOPurchaseOrders POs = (IDOPurchaseOrders)BaseObject;
                    value = POs.GetPropertyDisplayedValue(name, row);
                    break;
                case 1:
                    IDOPurchaseOrderLines poLines = (IDOPurchaseOrderLines)GetSecondObject(objIndex);
                    value = poLines.GetPropertyDisplayedValue(name, row);
                    break;
                default:
                    break;
            }
            return value;
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
