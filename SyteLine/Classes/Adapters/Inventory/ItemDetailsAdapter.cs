using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Adapters.Inventory
{
    public class ItemDetailsAdapter : CSIBaseDetailsAdapter
    {
        public ItemDetailsAdapter(Activity context, List<AdapterList> adpList)
            : base(context, adpList)
        {
            ;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return base.GetView(position, convertView, parent);
        }
    }
}