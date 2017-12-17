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
    public class BasePurchaseOrder
    {
        public string PoNum { get; set; }
        public string OrderDate { get; set; }
        public string Stat { get; set; }
        public string Type { get; set; }
        public string VendNum { get; set; }
        public string VendorName { get; set; }
        public string TermsCode { get; set; }
        public string TermsCodeDesc { get; set; }
        public string ShipCode { get; set; }
        public string ShipCodeDesc { get; set; }
        public string PoCost { get; set; }
        public string Whse { get; set; }
        public string BuilderPoOrigSite { get; set; }
        public string BuilderPoNum { get; set; }
        public string DerPchChgNum { get; set; }
        public string DerPchStat { get; set; }
        public string VendLcrNum { get; set; }
        public string ShipAddr { get; set; }
        public string FormatedShipToAddress { get; set; }
        public string Buyer { get; set; }
        public string ReqNum { get; set; }

        //public BasePurchaseOrderLine ItemLoc = new BasePurchaseOrderLine();


        public object GetValue(string PropertyName)
        {
            switch (PropertyName)
            {
                case "PoNum":
                    return PoNum;
                case "OrderDate":
                    return OrderDate;
                case "Stat":
                    return Stat;
                case "Type":
                    return Type;
                case "VendNum":
                    return VendNum;
                case "VendorName":
                    return VendorName;
                case "TermsCode":
                    return TermsCode;
                case "TermsCodeDesc":
                    return TermsCodeDesc;
                case "ShipCode":
                    return ShipCode;
                case "ShipCodeDesc":
                    return ShipCodeDesc;
                case "PoCost":
                    return PoCost;
                case "Whse":
                    return Whse;
                case "BuilderPoOrigSite":
                    return BuilderPoOrigSite;
                case "BuilderPoNum":
                    return BuilderPoNum;
                case "DerPchChgNum":
                    return DerPchChgNum;
                case "DerPchStat":
                    return DerPchStat;
                case "VendLcrNum":
                    return VendLcrNum;
                case "ShipAddr":
                    return ShipAddr;
                case "FormatedShipToAddress":
                    return FormatedShipToAddress;
                case "Buyer":
                    return Buyer;
                case "ReqNum":
                    return ReqNum;
                default:
                    return null;
            }
        }
    }

    public class PurchaseOrdersAdapter : BaseAdapter<BasePurchaseOrder>
    {
        public List<BasePurchaseOrder> POs { get; }
        Activity context;

        public PurchaseOrdersAdapter(Activity context, List<BasePurchaseOrder> itms)
            : base()
        {
            this.context = context;
            this.POs = itms;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override BasePurchaseOrder this[int position]
        {
            get { return POs[position]; }
        }

        public override int Count
        {
            get { return POs.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var order = POs[position];

            View view = convertView;

            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.PurchaseOrderListViewer, null);

            TextView PONumEdit = view.FindViewById<TextView>(Resource.Id.PONumrEdit);
            TextView VendNumEdit = view.FindViewById<TextView>(Resource.Id.VendNumEdit);
            TextView VendorNameEdit = view.FindViewById<TextView>(Resource.Id.VendorNameEdit);
            TextView DateEdit = view.FindViewById<TextView>(Resource.Id.DateEdit);
            TextView StatEdit = view.FindViewById<TextView>(Resource.Id.StatEdit);
            TextView TypeEdit = view.FindViewById<TextView>(Resource.Id.TypeEdit);
            TextView WhseEdit = view.FindViewById<TextView>(Resource.Id.WhseEdit);

            PONumEdit.SetText(order.PoNum,null);
            VendNumEdit.SetText(order.VendNum, null);
            VendorNameEdit.SetText(order.VendorName, null);
            DateEdit.SetText(order.OrderDate, null);
            StatEdit.SetText(order.Stat, null);
            TypeEdit.SetText(order.Type, null);
            WhseEdit.SetText(order.Whse, null);

            return view;
        }
    }
}