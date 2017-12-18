using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Business.Inventory;
using Android.Graphics;
using SyteLine.Classes.Activities.Inventory;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Adapters.Common
{
    public class CSIBaseAdapter : BaseAdapter<AdapterList>
    {
        public List<AdapterList> objectList { get; }
        protected Activity context;

        public CSIBaseAdapter(Activity context, List<AdapterList> adpList)
            : base()
        {
            this.context = context;
            this.objectList = adpList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override AdapterList this[int position]
        {
            get { return objectList[position]; }
        }

        public override int Count
        {
            get { return objectList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return convertView;
        }
    }
}