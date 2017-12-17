using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace SyteLine.Classes.Core.Common
{
    public class TopMenu
    {
        public int ImageId { get; set; }
        public int StringId { get; set; }
        public Type TargetActivityType { get; set; }
        public bool FinishActivity { get; set; }
        public bool CloseSession { get; set; }
    }

    public class MenuAdapter : BaseAdapter<TopMenu>
    {
        List<TopMenu> items;
        Activity context;

        public MenuAdapter(Activity context, List<TopMenu> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TopMenu this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ImageTextViewer1, null);
            if (item.ImageId > 0)
            {
                view.FindViewById<ImageView>(Resource.Id.ImageView).SetImageResource(item.ImageId);
            }
            if (item.StringId > 0)
            {
                view.FindViewById<TextView>(Resource.Id.ActionButton).SetText(item.StringId);
            }
            //view.FindViewById<TextView>(Resource.Id.ActionButton).Click += delegate {
            view.Click += delegate {
                Intent intent = new Intent(context, items[position].TargetActivityType);
                intent.PutExtra("SessionToken", context.Intent.GetStringExtra("SessionToken"));
                context.StartActivity(intent);
                if (items[position].FinishActivity)
                {
                    context.Finish();
                }
            };
            return view;
        }
    }
}