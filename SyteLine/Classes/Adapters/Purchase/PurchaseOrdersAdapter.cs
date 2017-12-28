using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Business.Inventory;
using Android.Graphics;
using SyteLine.Classes.Activities.Inventory;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Adapters.Purchase
{
    public class PurchaseOrdersAdapter : CSIBaseAdapter
    {
        public PurchaseOrdersAdapter(Activity context, List<AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var order = objectList[position];

            View view = convertView;

            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItemPurchaseOrderViewer, null);

            TextView PONumEdit = view.FindViewById<TextView>(Resource.Id.PONumrEdit);
            TextView VendNumEdit = view.FindViewById<TextView>(Resource.Id.VendNumEdit);
            TextView VendorNameEdit = view.FindViewById<TextView>(Resource.Id.VendorNameEdit);
            TextView DateEdit = view.FindViewById<TextView>(Resource.Id.DateEdit);
            TextView StatEdit = view.FindViewById<TextView>(Resource.Id.StatEdit);
            TextView TypeEdit = view.FindViewById<TextView>(Resource.Id.TypeEdit);
            TextView WhseEdit = view.FindViewById<TextView>(Resource.Id.WhseEdit);

            PONumEdit.SetText(order.GetString("PoNum"),null);
            VendNumEdit.SetText(order.GetString("VendNum"), null);
            VendorNameEdit.SetText(order.GetString("VendorName"), null);
            DateEdit.SetText(order.GetString("OrderDate"), null);
            StatEdit.SetText(order.GetString("Stat"), null);
            TypeEdit.SetText(order.GetString("Type"), null);
            WhseEdit.SetText(order.GetString("Whse"), null);

            return view;
        }
    }
}