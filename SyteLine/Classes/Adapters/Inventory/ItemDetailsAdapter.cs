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

namespace SyteLine.Classes.Adapters.Inventory
{
    public class BaseItem
    {
        //Item,Description,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,Picture
        public string Item { get; set; }
        public string Description { get; set; }
        public string Overview {get; set;}
        public string DerQtyOnHand { get; set; }
        public string UM { get; set; }
        public string MatlType { get; set; }
        public string PMTCode { get; set; }
        public string ProductCode { get; set; }
        public Bitmap Picture { get; set; }
        public bool LotTracked { get; set; }
        public bool SerialTracked { get; set; }
        public string PropertyName;
        public string LableString;
        public BaseItemLoc ItemLoc = new BaseItemLoc();

        public object GetValue(string PropertyName)
        {
            switch (PropertyName)
            {
                case "Item":
                    return Item;
                case "Description":
                    return Description;
                case "Overview":
                    return Overview;
                case "DerQtyOnHand":
                    return DerQtyOnHand;
                case "UM":
                    return UM;
                case "MatlType":
                    return MatlType;
                case "PMTCode":
                    return PMTCode;
                case "ProductCode":
                    return ProductCode;
                case "Picture":
                    return Picture;
                case "LotTracked":
                    return LotTracked;
                case "SerialTracked":
                    return SerialTracked;
                case "ItmwhseQtyOnHand":
                    return ItemLoc.ItmwhseQtyOnHand;
                case "LocType":
                    return ItemLoc.LocType;
                case "Rank":
                    return ItemLoc.Rank;
                case "ItmIssueBy":
                    return ItemLoc.ItmIssueBy;
                case "QtyOnHand":
                    return ItemLoc.QtyOnHand;
                case "QtyRsvd":
                    return ItemLoc.QtyRsvd;
                default:
                    return null;
            }
        }
    }

    public class ItemDetailsAdapter : BaseAdapter<BaseItem>
    {
        public List<BaseItem> Items { get; }
        Activity context;

        public ItemDetailsAdapter(Activity context, List<BaseItem> itms)
            : base()
        {
            this.context = context;
            this.Items = itms;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override BaseItem this[int position]
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
            try
            {
                switch (item.PropertyName)
                {
                    case "LotTracked":
                    case "SerialTracked":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelSwitchViewer, null);
                        //}
                        Switch = view.FindViewById<Switch>(Resource.Id.Switch);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Switch.Checked = (bool)item.GetValue(item.PropertyName);
                        Label.SetText(item.LableString, null);
                        break;
                    case "Overview":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelMultiLinesTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText((string)item.GetValue(item.PropertyName), null);
                        Label.SetText(item.LableString, null);
                        break;
                    case "-":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        //}
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceLarge);
                        Label.SetTextSize(Android.Util.ComplexUnitType.Pt, 12);
;                        //Label.SetTextColor(Color.DarkBlue);
                        Label.SetText(item.LableString, null);
                        break;
                    case "--":
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonSplitterViewer, null);
                        //}
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Label.SetTextAppearance(Android.Resource.Attribute.TextAppearanceMedium);
                        Label.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                        //Label.SetTextColor(Color.DarkCyan);
                        Label.SetText(item.LableString, null);
                        break;
                    default:
                        //if (view == null) // no view to re-use, create new
                        //{
                            view = context.LayoutInflater.Inflate(Resource.Layout.CommonLabelTextViewer, null);
                        //}
                        Text = view.FindViewById<TextView>(Resource.Id.Text);
                        Label = view.FindViewById<TextView>(Resource.Id.Label);
                        Text.SetText((string)item.GetValue(item.PropertyName), null);
                        Label.SetText(item.LableString, null);
                        break;
                }
                return view;
            }catch (Exception Ex)
            {
                //throw Ex;
                Toast.MakeText(context, "ItemDetailsAdapterItem -> GetView() -> " + Ex.Message, ToastLength.Short).Show();
            }
            finally
            {
            }
            return null;
        }
    }
}