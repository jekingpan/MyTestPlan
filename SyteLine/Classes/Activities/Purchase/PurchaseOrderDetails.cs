﻿using System;
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
            base.PrimaryBusinessObject = new IDOPurchaseOrders(base.Intent.GetStringExtra("SessionToken"), this);
            base.AddBusinessObjects(new IDOPurchaseOrderLines(base.Intent.GetStringExtra("SessionToken"), this));
            base.OnCreate(savedInstanceState);
        }
        protected override void BeforeReadIDOs()
        {
            base.BeforeReadIDOs();
            try
            {
                IDOPurchaseOrders Orders = (IDOPurchaseOrders)PrimaryBusinessObject;
                Orders.parm.PropertyList = "VendNum,VendorName";
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
        protected override void BeforeReadIDOs(int index)
        {
            base.BeforeReadIDOs(index);
            try
            {
                if (index == 1)
                {
                    IDOPurchaseOrderLines poLines = (IDOPurchaseOrderLines)GetSecondObject(index);
                    poLines.parm.PropertyList = "PoNum";
                    SetAdapterLists(1, "PoLine", "PoLine", ValueTypes.String, GetString(Resource.String.OrderLine) + GetString(Resource.String.Line0), Resource.Layout.CommonSubSplitterViewer);
                    SetAdapterLists(1, "Item", "Item", ValueTypes.String, GetString(Resource.String.Item), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "Description", "Description", ValueTypes.String, GetString(Resource.String.Description), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "QtyOrderedConv", "QtyOrderedConv", ValueTypes.Decimal, GetString(Resource.String.Quantity), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "DerQtyReceivedConv", "DerQtyReceivedConv", ValueTypes.Decimal, GetString(Resource.String.QuantityReceived), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "UM", "UM", ValueTypes.String, GetString(Resource.String.UM), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "Stat", "Stat", ValueTypes.String, GetString(Resource.String.Status), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "DueDate", "DueDate", ValueTypes.Date, GetString(Resource.String.DueDate), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "PromiseDate", "PromiseDate", ValueTypes.Date, GetString(Resource.String.PromiseDate), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(1, "Whse", "Whse", ValueTypes.String, GetString(Resource.String.General), Resource.Layout.CommonSubLabelTextViewer);
                    
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

                IDOPurchaseOrders POs = (IDOPurchaseOrders)PrimaryBusinessObject;

                SetKey(POs.GetPropertyValue("PoNum"));
                SetSubKey(POs.GetPropertyValue("VendNum"));
                SetSubKeyDescription(POs.GetPropertyValue("VendorName"));

                ListView.Adapter = new PurchaseOrderDetailsAdapter(this, AdapterLists);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override string GetPropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            string value = "";
            switch (objIndex)
            {
                case 0:
                    IDOPurchaseOrders POs = (IDOPurchaseOrders)PrimaryBusinessObject;
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
