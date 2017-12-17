using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Widget.AdapterView;

namespace SyteLine.Classes.Adapters.Common
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ListViewerAdapterItem
    {
        public int ImageId { get; set; }
        public int StringId { get; set; }
        public Type ActivityType { get; set; }
    }

    public class ListViewerAdapter : BaseAdapter<ListViewerAdapterItem>
    {
        private List<ListViewerAdapterItem> items;
        private Activity context;

        public ListViewerAdapter(Activity context, List<ListViewerAdapterItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ListViewerAdapterItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ListViewerAdapterItem item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ImageTextViewerLarge, null);
            
            ImageView ImageView = view.FindViewById<ImageView>(Resource.Id.ImageView);
            TextView ActionText = view.FindViewById<TextView>(Resource.Id.ActionText);

            if (item.ImageId > 0)
            {
                ImageView.SetImageResource(item.ImageId);
            }
            if (item.StringId > 0)
            {
                ActionText.SetText(item.StringId);
            }

            return view;
        }

    }
}