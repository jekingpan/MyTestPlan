﻿using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Adapters.Inventory
{
    public class ItemDetailsAdapter : CSIBaseAdapter
    {
        public ItemDetailsAdapter(Activity context, List<AdapterList> adpList)
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
            try
            {
                switch (item.GetKeyName())
                {
                    case "LotTracked":
                    case "SerialTracked":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelSwitchViewer, null);
                        //}
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Switch.Checked = item.GetBoolean(item.GetKeyName());
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    case "Overview":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelMultiLinesTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText((string)item.GetString(item.GetKeyName()), null);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    case "-":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        //}
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceLarge);
                        Label.SetTextSize(Android.Util.ComplexUnitType.Pt, 12);
                        Label.SetBackgroundColor(Color.DarkGray);
                        Label.SetTextColor(Color.FloralWhite);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    case "--":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        //}
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceMedium);
                        Label.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                        Label.SetBackgroundColor(Color.Gray);
                        Label.SetTextColor(Color.FloralWhite);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                    default:
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText((string)item.GetString(item.GetKeyName()), null);
                        Label.SetText(item.GetLabel(item.GetKeyName()), null);
                        break;
                }
                return view;
            }catch (Exception Ex)
            {
                //throw Ex;
                Toast.MakeText(context, "ItemDetailsAdapterItem -> GetView() -> " + Ex.Message, ToastLength.Short).Show();
            }
            finally
            {
            }
            return null;
        }
    }
}