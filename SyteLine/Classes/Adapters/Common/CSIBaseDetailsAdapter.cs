using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace SyteLine.Classes.Adapters.Common
{
    public class CSIBaseDetailsAdapter : CSIBaseAdapter
    {
        public CSIBaseDetailsAdapter(Activity context, List<AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            AdapterList item = objectList[position];

            View view = convertView;
            TextView Text;
            TextView Label;
            Switch Switch;

            string Key = item.GetFirstKey();
            int LayoutID = item.GetLayoutID(Key);

            try
            {
                //if (view == null) // no view to re-use, create new
                //{
                //    view = context.LayoutInflater.Inflate(item.GetLayoutID(Key), null);
                //}
                view = context.LayoutInflater.Inflate(item.GetLayoutID(Key), null);
                Label = view.FindViewById<TextView>(Resource.Id.Label);
                Label.SetText(item.GetLabel(Key), null);
                switch (LayoutID)
                {
                    case Resource.Layout.CommonLabelSwitchViewer:
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Switch.Checked = item.GetBoolean(Key);
                        break;
                    case Resource.Layout.CommonSplitterViewer:
                    case Resource.Layout.CommonSplitterSmallViewer:
                        break;
                    case Resource.Layout.CommonLabelTextViewer:
                    case Resource.Layout.CommonLabelMultiLinesTextViewer:
                    default:
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Text.SetText(item.GetString(Key), null);
                        break;
                }
                return view;
            }catch (Exception Ex)
            {
                Toast.MakeText(context, "CSIBaseDetailsAdapter -> GetView() -> " + Ex.Message, ToastLength.Short).Show();
                return null;
            }
        }
    }
}