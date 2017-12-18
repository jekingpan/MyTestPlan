using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;
using Android.Views;
using SyteLine.Classes.Adapters.Inventory;
using SyteLine.Classes.Business.Inventory;
using static Android.Widget.AdapterView;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Activities.Inventory
{
    [Activity(Label = "@string/Items")]
    public class Items : BaseSearchActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            BaseObject = new IDOItems(Intent.GetStringExtra("SessionToken"), this);
            base.OnCreate(savedInstanceState);
        }

        protected override void ListViewClicked(object sender, ItemClickEventArgs args)
        {
            base.ListViewClicked(sender, args);
            ItemsAdapter itm = (ItemsAdapter)ListView.Adapter;
            Intent intent = new Intent(this, typeof(ItemDetails));
            intent.PutExtra("SessionToken", this.Intent.GetStringExtra("SessionToken"));
            intent.PutExtra("Item", itm.objectList[args.Position].GetString("Item"));
            StartActivity(intent);
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);
            List<AdapterList> Rows;
            ItemsAdapter Adapter = (ItemsAdapter)ListView.Adapter;
            IDOItems Items = (IDOItems)BaseObject;
            if (!Append)
            {
                Rows = new List<AdapterList>();
            }
            else
            {
                Rows = Adapter.objectList;
            }
            for (int i = 0; i < BaseObject.GetRowCount(); i++)
            {
                LastKey = Items.GetItem(i);
                AdapterList adptList = new AdapterList();
                adptList.Add("Item", Items.GetItem(i));
                adptList.Add("Description", Items.GetDescription(i));
                adptList.Add("DerQtyOnHand", Items.GetQtyOnHand(i));
                adptList.Add("UM", Items.GetUM(i));
                adptList.Add("MatlType", Items.GetMatlType(i));
                adptList.Add("PMTCode", Items.GetMatlType(i));
                adptList.Add("ProductCode", Items.GetProductCode(i));
                adptList.Add("Picture", Items.GetPicture(i));
                adptList.Add("LotTracked", Items.GetLotTracked(i));
                adptList.Add("SerialTracked", Items.GetSerialTracked(i));
                Rows.Add(adptList);
            }
            if (!Append)
            {
                ListView.Adapter = new ItemsAdapter(this, Rows);
            }

        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            IDOItems Items = (IDOItems)BaseObject;
            if (QueryString == "")
            {
                QueryString = "%";
                Items.BuilderFilterByItem(QueryString);
            }
            else
            {
                Items.BuilderFilterByItemOrDesc(QueryString);
            }
            if (LastKey != "")
            {
                Items.BuilderAdditionalFilter(string.Format("Item > N'{0}'", LastKey));
            }
            Items.parm.PropertyList = "Item,Description,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,LotTracked,SerialTracked";//,Picture
            if (new Configure().LoadPicture)
            {
                Items.parm.PropertyList += ",Picture";
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
    }
}