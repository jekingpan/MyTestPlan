using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Inventory;
using SyteLine.Classes.Business.Inventory;
using SyteLine.Classes.Business.Purchase;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/PurchaseOrderDetails")]
    public class PurchaseOrderDetails : BaseDetailActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.BaseObject = new IDOItems(base.Intent.GetStringExtra("SessionToken"), this);
            base.AddSecondObject(new IDOPurchaseOrderLines(base.Intent.GetStringExtra("SessionToken"), this));
            base.OnCreate(savedInstanceState);
        }

        protected override void RegisterAdapter(bool Append)
        {
            List<BasePurchaseOrder> Rows;
            try
            {
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
                Orders.parm.PropertyList = "Item,Description,Overview,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,LotTracked,SerialTracked";//,Picture
                if (new Configure().LoadPicture)
                {
                    BaseObject.parm.PropertyList += ",Picture";
                }
                //Orders.BuilderFilterByItem(Intent.GetStringExtra("Item"));
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
                    //poLines.BuilderFilterByItem(Intent.GetStringExtra("Item"));
                    poLines.BuilderAdditionalFilter("QtyOnHand <> 0");
                    poLines.parm.RecordCap = -1;
                    poLines.SetOrderBy("ItmwhseQtyOnHand DESC,QtyOnHand DESC,Rank");
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
