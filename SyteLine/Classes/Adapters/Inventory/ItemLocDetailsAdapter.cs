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
    public class ItemLocDetailsAdapter : CSIBaseDetailsAdapter
    {
        public ItemLocDetailsAdapter(Activity context, List<Common.AdapterList> adpList)
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