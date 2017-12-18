using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Adapters.Common;
using Android.Graphics;

namespace SyteLine.Classes.Adapters.Purchase
{
    public class PurchaseOrderDetailsAdapter : CSIBaseAdapter
    {
        public PurchaseOrderDetailsAdapter(Activity context, List<AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var obj = objectList[position];

            View view = convertView;
            TextView Text;
            TextView Label;
            Switch Switch;
            try
            {
                switch (obj.GetKeyName())
                {
                    case "LotTracked":
                    case "SerialTracked":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelSwitchViewer, null);
                        //}
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Switch.Checked = obj.GetBoolean(obj.GetKeyName());
                        Label.SetText(obj.GetLabel(obj.GetKeyName()), null);
                        break;
                    case "Overview":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelMultiLinesTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText(obj.GetString(obj.GetKeyName()), null);
                        Label.SetText(obj.GetLabel(obj.GetKeyName()), null);
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
                        Label.SetText(obj.GetLabel(obj.GetKeyName()), null);
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
                        Label.SetText(obj.GetLabel(obj.GetKeyName()), null);
                        break;
                    default:
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText(obj.GetString(obj.GetKeyName()), null);
                        Label.SetText(obj.GetLabel(obj.GetKeyName()), null);
                        break;
                }
                return view;
            }catch (Exception Ex)
            {
                //throw Ex;
                Toast.MakeText(context, "PurchaseOrderDetailsAdapter -> GetView() -> " + Ex.Message, ToastLength.Short).Show();
            }
            finally
            {
            }
            return null;
        }
    }
}