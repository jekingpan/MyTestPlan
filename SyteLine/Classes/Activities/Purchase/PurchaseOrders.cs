using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Activities.Common;
using Android.Views;
using SyteLine.Classes.Adapters.Inventory;
using SyteLine.Classes.Business.Purchase;
using static Android.Widget.AdapterView;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/PurchaseOrders")]
    public class PurchaseOrders : BaseSearchActivity
    {
        protected int DateQueryMenu = 0;
        protected string DateQueryString = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            BaseObject = new IDOPurchaseOrders(Intent.GetStringExtra("SessionToken"), this);
            base.OnCreate(savedInstanceState);
        }

        protected override void ListViewClicked(object sender, ItemClickEventArgs args)
        {
            base.ListViewClicked(sender, args);
            PurchaseOrdersAdapter po = (PurchaseOrdersAdapter)ListView.Adapter;
            Intent intent = new Intent(this, typeof(PurchaseOrderDetails));
            intent.PutExtra("SessionToken", this.Intent.GetStringExtra("SessionToken"));
            intent.PutExtra("PoNum", po.POs[args.Position].PoNum);
            StartActivity(intent);
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);
            List<BasePurchaseOrder> Rows;
            PurchaseOrdersAdapter Adapter = (PurchaseOrdersAdapter)ListView.Adapter;
            IDOPurchaseOrders Orders = (IDOPurchaseOrders)BaseObject;
            if (!Append)
            {
                Rows = new List<BasePurchaseOrder>();
            }
            else
            {
                Rows = Adapter.POs;
            }
            for (int i = 0; i < Orders.GetRowCount(); i++)
            {
                LastKey = Orders.GetPoNum(i);
                Rows.Add(new BasePurchaseOrder()
                {
                    PoNum = Orders.GetPoNum(i),
                    VendNum = Orders.GetVendNum(i),
                    VendorName = Orders.GetVendorName(i),
                    Stat = Orders.GetStat(i),
                    OrderDate = Orders.GetOrderDate(i),
                    Whse = Orders.GetWhse(i),
                    Type = Orders.GetType(i)
                });
            }
            if (!Append)
            {
                ListView.Adapter = new PurchaseOrdersAdapter(this, Rows);
            }
        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            IDOPurchaseOrders Orders = (IDOPurchaseOrders)BaseObject;
            if (QueryString == "")
            {
                QueryString = "%";
                Orders.BuilderFilterByPoNum(QueryString);
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
            Orders.parm.PropertyList = "PoNum,OrderDate,Stat,Type,VendNum,VendorName,Whse";
            Orders.BuilderAdditionalFilter("Stat IN (N'P', N'O')");
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
            if (item.ItemId == Resource.String.BeforeLast3Months)
            {
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = "datediff(month ,getdate(), OrderDate) < -3";
                InitialList();
            }
            else if (item.ItemId == Resource.String.InLast3Month)
            {
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = "datediff(day,getdate(), OrderDate) < 0 AND datediff(month ,getdate(), OrderDate) >= -3";
                InitialList();
            }
            else if (item.ItemId == Resource.String.InThisMonth)
            {
                item.SetChecked(!item.IsChecked);
                DateQueryMenu = item.IsChecked ? item.ItemId : 0;
                DateQueryString = "datediff(month ,getdate(), OrderDate) = 0";
                InitialList();
            }
            else if (item.ItemId == Resource.String.All)
            {
                DateQueryString = "";
                DateQueryMenu = 0;
                InitialList();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}