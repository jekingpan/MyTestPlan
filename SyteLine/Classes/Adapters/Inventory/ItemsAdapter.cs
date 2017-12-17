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

namespace SyteLine.Classes.Adapters.Inventory
{
    public class ItemsAdapter : BaseAdapter<BaseItem>
    {
        public List<BaseItem> Items { get; }
        Activity context;

        public ItemsAdapter(Activity context, List<BaseItem> itms)
            : base()
        {
            this.context = context;
            this.Items = itms;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override BaseItem this[int position]
        {
            get { return Items[position]; }
        }

        public override int Count
        {
            get { return Items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            View view = convertView;

            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ItemListViewer, null);

            TextView ItemEdit = view.FindViewById<TextView>(Resource.Id.ItemEdit);
            TextView DescriptionEdit = view.FindViewById<TextView>(Resource.Id.DescriptionEdit);
            TextView OnHandQuantityEdit = view.FindViewById<TextView>(Resource.Id.OnHandQuantityEdit);
            TextView UnitofMeasureEdit = view.FindViewById<TextView>(Resource.Id.UnitofMeasureEdit);
            ImageView ItemImage = view.FindViewById<ImageView>(Resource.Id.ItemImage);
            ImageView LotTrackedImageView = view.FindViewById<ImageView>(Resource.Id.LotTrackedImageView);
            ImageView SNTrackedImageView = view.FindViewById<ImageView>(Resource.Id.SNTrackedImageView);

            ItemEdit.SetText(item.Item, null);
            DescriptionEdit.SetText(item.Description, null);
            OnHandQuantityEdit.SetText(item.DerQtyOnHand, null);
            UnitofMeasureEdit.SetText(item.UM, null);
            LotTrackedImageView.SetImageResource(item.LotTracked?
                Android.Resource.Drawable.CheckboxOnBackground: Android.Resource.Drawable.CheckboxOffBackground);
            SNTrackedImageView.SetImageResource(item.SerialTracked ?
                Android.Resource.Drawable.CheckboxOnBackground : Android.Resource.Drawable.CheckboxOffBackground);
            if (new Configure().LoadPicture)
            {
                ItemImage.Visibility = ViewStates.Visible;
                ItemImage.SetImageBitmap(item.Picture);
            }
            else
            {
                ItemImage.Visibility = ViewStates.Gone;
            }

            return view;
        }
    }
}