using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Adapters.Common;
using Android.Graphics;

namespace SyteLine.Classes.Adapters.Purchase
{
    public class PurchaseOrderDetailsAdapter : CSIBaseDetailsAdapter
    {
        public PurchaseOrderDetailsAdapter(Activity context, List<AdapterList> adpList)
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