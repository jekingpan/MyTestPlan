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

namespace SyteLine.Classes.Adapters.Inventory
{
    public class ItemsAdapter : CSIBaseAdapter
    {
        public ItemsAdapter(Activity context, List<Common.AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = objectList[position];

            View view = convertView;

            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItemItemViewer, null);

            TextView ItemEdit = view.FindViewById<TextView>(Resource.Id.ItemEdit);
            TextView DescriptionEdit = view.FindViewById<TextView>(Resource.Id.DescriptionEdit);
            TextView OnHandQuantityEdit = view.FindViewById<TextView>(Resource.Id.OnHandQuantityEdit);
            TextView UnitofMeasureEdit = view.FindViewById<TextView>(Resource.Id.UnitofMeasureEdit);
            ImageView ItemImage = view.FindViewById<ImageView>(Resource.Id.ItemImage);
            ImageView LotTrackedImageView = view.FindViewById<ImageView>(Resource.Id.LotTrackedImageView);
            ImageView SNTrackedImageView = view.FindViewById<ImageView>(Resource.Id.SNTrackedImageView);
            TextView MaterialTypeEdit = view.FindViewById<TextView>(Resource.Id.MaterialTypeEdit);
            TextView MaterialSourceEdit = view.FindViewById<TextView>(Resource.Id.MaterialSourceEdit);
            TextView ProductCodeEdit = view.FindViewById<TextView>(Resource.Id.ProductCodeEdit);

            ItemEdit.SetText(item.GetString("Item"), null);
            DescriptionEdit.SetText(item.GetString("Description"), null);
            OnHandQuantityEdit.SetText(item.GetString("DerQtyOnHand"), null);
            UnitofMeasureEdit.SetText(item.GetString("UM"), null);
            MaterialTypeEdit.SetText(item.GetString("MatlType"), null);
            MaterialSourceEdit.SetText(item.GetString("PMTCode"), null);
            ProductCodeEdit.SetText(item.GetString("ProductCode"), null);
            LotTrackedImageView.SetImageResource(item.GetBoolean("LotTracked") ?
                Android.Resource.Drawable.CheckboxOnBackground: Android.Resource.Drawable.CheckboxOffBackground);
            SNTrackedImageView.SetImageResource(item.GetBoolean("SerialTracked")  ?
                Android.Resource.Drawable.CheckboxOnBackground : Android.Resource.Drawable.CheckboxOffBackground);
            if (new Configure().LoadPicture)
            {
                ItemImage.Visibility = ViewStates.Visible;
                ItemImage.SetImageBitmap(item.GetBitmap("Picture"));
            }
            else
            {
                ItemImage.Visibility = ViewStates.Gone;
            }

            return view;
        }
    }
}