using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.App;
using System;
using static Android.Widget.AdapterView;

namespace SyteLine.Classes.Adapters.Common
{
    public class CSIBaseGridViewerAdapter : BaseAdapter
    {
        private Activity context;
        private GridView parentView;
        // references to our images
        public List<GridViewActionItem> ActionItems = new List<GridViewActionItem>();

        public CSIBaseGridViewerAdapter(Activity c,GridView gridView)
            : base()
        {
            parentView = gridView;
            this.context = c;
        }

        public override int Count
        {
            get { return ActionItems.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                var item = ActionItems[position];

                View view = convertView;
                if (view == null) // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.CommonImageTextViewerGrid, null);

                ImageView imageView = view.FindViewById<ImageView>(Resource.Id.ImageView);
                TextView textView = view.FindViewById<TextView>(Resource.Id.ActionText);

                //imageView.LayoutParameters = new AbsListView.LayoutParams(300, 300);
                imageView.SetScaleType(ImageView.ScaleType.FitCenter);
                imageView.SetPadding(28, 28, 28, 28);
                imageView.SetImageResource(item.ThumbId);
                textView.Text = item.Name;
                return view;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }

    public class GridViewActionItem
    {
        public int ThumbId { get; set; }
        public string Name { get; set; }
        public Type ActivityType { get; set; }
    }
}