using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Business.Inventory;
using Android.Graphics;
using SyteLine.Classes.Activities.Inventory;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Adapters.Inventory
{
    public class ItemLocDetailsAdapter : CSIBaseAdapter
    {
        public ItemLocDetailsAdapter(Activity context, List<Common.AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = objectList[position];

            View view = convertView;
            TextView Text;
            TextView Label;
            Switch Switch;

            if (view == null) // no view to re-use, create new
            {
                switch (item.GetKeyName())
                {
                    case "LotTracked":
                    case "SerialTracked":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelSwitchViewer, null);
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Switch.Checked = item.GetBoolean(item.GetKeyName());
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    case "-":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceLarge);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    case "--":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceMedium);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    default:
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelTextViewer, null);
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText(item.GetString(item.GetKeyName()), null);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                }
            }

            return view;
        }
    }
}