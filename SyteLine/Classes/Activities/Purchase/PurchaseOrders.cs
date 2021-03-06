﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Activities.Common;
using Android.Views;
using SyteLine.Classes.Business.Purchase;
using static Android.Widget.AdapterView;
using SyteLine.Classes.Adapters.Purchase;
using SyteLine.Classes.Adapters.Common;
using System;
using SyteLine.Classes.Core.Common;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/PurchaseOrders")]
    public class PurchaseOrders : CSIBaseSearchActivity
    {
        protected int DateQueryMenu = 0;
        protected string DateQueryString = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            PrimaryBusinessObject = new IDOPurchaseOrders(Intent.GetStringExtra("SessionToken"), this);
            base.OnCreate(savedInstanceState);
        }

        protected override void ListViewClicked(object sender, ItemClickEventArgs args)
        {
            base.ListViewClicked(sender, args);
            PurchaseOrdersAdapter po = (PurchaseOrdersAdapter)ListView.Adapter;
            if (po.objectList[args.Position].GetString("Type") == "R")
            {
                Intent intent = new Intent(this, typeof(PurchaseOrderDetails));
                SetDefaultIntent(intent);
                intent.PutExtra("PoNum", po.objectList[args.Position].GetString("PoNum"));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(PurchaseOrderBlanketDetails));
                SetDefaultIntent(intent);
                intent.PutExtra("PoNum", po.objectList[args.Position].GetString("PoNum"));
                StartActivity(intent);
            }
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);
            
            if (!Append)
            {
                ListView.Adapter = new PurchaseOrdersAdapter(this, AdapterLists);
            }
        }

        protected override void BeforeReadIDOs()
        {
            base.BeforeReadIDOs();
            IDOPurchaseOrders Orders = (IDOPurchaseOrders)PrimaryBusinessObject;
            Orders.parm.PropertyList = "";
            AdapterList adptList = new AdapterList()
            {
                KeyName = "PoNum"
            };
            adptList.Add("PoNum");
            adptList.Add("OrderDate", AdapterListItem.ValueTypes.Date);
            adptList.Add("Stat");
            adptList.Add("Type");
            adptList.Add("VendNum");
            adptList.Add("VendorName");
            adptList.Add("Whse");
            SetAdapterLists(0, adptList);
            
            if (QueryString == "")
            {
                //QueryString = "%";
                Orders.BuilderFilterByPoNum("%");
            }
            else
            {
                Orders.BuilderFilterByPoNumOrVendNumOrVendorName(QueryString);
            }
            if (LastKey != "")
            {
                Orders.BuilderAdditionalFilter(string.Format("PoNum > N'{0}'", LastKey));
            }
            if (DateQueryString != "")
            {
                Orders.BuilderAdditionalFilter(DateQueryString);
            }
            Orders.BuilderAdditionalFilter(string.Format("Stat IN (N'P', N'O') AND Whse = N'{0}'", DefaultWhse()));
        }

        protected override string GetPropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            string value = "";
            switch (objIndex)
            {
                case 0:
                    IDOPurchaseOrders POs = (IDOPurchaseOrders)PrimaryBusinessObject;
                    value = POs.GetPropertyDisplayedValue(name, row);
                    break;
                default:
                    break;
            }
            return value;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(1, Resource.String.All, 0, Resource.String.All);
            menu.Add(1, Resource.String.InThisMonth, 0, Resource.String.InThisMonth).SetCheckable(true);
            menu.Add(1, Resource.String.InLast3Month, 0, Resource.String.InLast3Month).SetCheckable(true);
            menu.Add(1, Resource.String.BeforeLast3Months, 0, Resource.String.BeforeLast3Months).SetCheckable(true);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.FindItem(Resource.String.InLast3Month).SetChecked(DateQueryMenu == Resource.String.InLast3Month);
            menu.FindItem(Resource.String.BeforeLast3Months).SetChecked(DateQueryMenu == Resource.String.BeforeLast3Months);
            menu.FindItem(Resource.String.InThisMonth).SetChecked(DateQueryMenu == Resource.String.InThisMonth);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            string Date = "", Date2 = "";
            if (item.ItemId == Resource.String.BeforeLast3Months)
            {
                Date = DateTime.Now.Date.AddMonths(-3).ToShortDateString();
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = string.Format("OrderDate < N'{0}'", Date);//"{datediff}(month ,getdate(), OrderDate) < -3";
                InitializeActivity();
            }
            else if (item.ItemId == Resource.String.InLast3Month)
            {
                Date = DateTime.Now.Date.AddMonths(-3).ToShortDateString();
                Date2 = DateTime.Now.Date.AddDays(1).ToShortDateString();
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = string.Format("OrderDate Between N'{0}' and N'{1}'", Date, Date2);//"{datediff}(day,getdate(), OrderDate) < 0 AND {datediff}(month ,getdate(), OrderDate) >= -3";
                InitializeActivity();
            }
            else if (item.ItemId == Resource.String.InThisMonth)
            {
                Date = DateTime.Now.Date.AddDays(1 - DateTime.Now.Date.Day).ToShortDateString();
                Date2 = DateTime.Now.Date.AddDays(1).ToShortDateString();
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = string.Format("OrderDate Between N'{0}' and N'{1}'", Date, Date2);//"{datediff}(month ,getdate(), OrderDate) = 0";
                InitializeActivity();
            }
            else if (item.ItemId == Resource.String.All)
            {
                DateQueryString = "";
                DateQueryMenu = 0;
                InitializeActivity();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}