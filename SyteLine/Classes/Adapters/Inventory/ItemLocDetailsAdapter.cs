using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Business.Inventory;
using Android.Graphics;
using SyteLine.Classes.Activities.Inventory;

namespace SyteLine.Classes.Adapters.Inventory
{
    public class BaseItemLoc
    {
        //Item,Description,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,Picture
        public string Item { get; set; }
        public string ItmDescription { get; set; }
        public string Whse { get; set;}
        public string WhsName { get; set; }
        public string ItmwhseQtyOnHand { get; set; }
        public string Loc { get; set; }
        public string LocDescription { get; set; }
        public string LocType { get; set; }
        public bool LotTracked { get; set; }
        public bool SerialTracked { get; set; }
        public string QtyOnHand { get; set; }
        public string QtyRsvd { get; set; }
        public string DerIWhseTotalRsvdCO { get; set; }
        public string DerIWhseTotalNonNetStock { get; set; }
        public string ItmIssueBy { get; set; }
        public string Rank { get; set; }
        public string PropertyName;
        public string LableString;

        public object GetValue(string PropertyName)
        {
            switch (PropertyName)
            {
                case "Item":
                    return Item;
                case "ItmDescription":
                    return ItmDescription;
                case "Whse":
                    return Whse;
                case "WhsName":
                    return WhsName;
                case "ItmwhseQtyOnHand":
                    return ItmwhseQtyOnHand;
                case "Loc":
                    return Loc;
                case "LocDescription":
                    return LocDescription;
                case "Rank":
                    return Rank;
                case "LocType":
                    return LocType;
                case "QtyOnHand":
                    return QtyOnHand;
                case "QtyRsvd":
                    return QtyRsvd;
                case "DerIWhseTotalRsvdCO":
                    return DerIWhseTotalRsvdCO;
                case "DerIWhseTotalNonNetStock":
                    return DerIWhseTotalNonNetStock;
                case "ItmIssueBy":
                    return ItmIssueBy;
                case "LotTracked":
                    return LotTracked;
                case "SerialTracked":
                    return SerialTracked; 
                default:
                    return null;
            }
        }
    }

    public class ItemLocDetailsAdapter : BaseAdapter<BaseItemLoc>
    {
        public List<BaseItemLoc> Items { get; }
        Activity context;

        public ItemLocDetailsAdapter(Activity context, List<BaseItemLoc> itms)
            : base()
        {
            this.context = context;
            this.Items = itms;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override BaseItemLoc this[int position]
        {
            get { return Items[position]; }
        }

        public override int Count
        {
            get { return Items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            View view = convertView;
            TextView Text;
            TextView Label;
            Switch Switch;

            if (view == null) // no view to re-use, create new
            {
                switch (item.PropertyName)
                {
                    case "LotTracked":
                    case "SerialTracked":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelSwitchViewer, null);
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Switch.Checked = (bool)item.GetValue(item.PropertyName);
                        Label.SetText(item.LableString, null);
                        break;
                    case "-":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceLarge);
                        Label.SetText(item.LableString, null);
                        break;
                    case "--":
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceMedium);
                        Label.SetText(item.LableString, null);
                        break;
                    default:
                        view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelTextViewer, null);
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText((string)item.GetValue(item.PropertyName), null);
                        Label.SetText(item.LableString, null);
                        break;
                }
            }

            return view;
        }
    }
}