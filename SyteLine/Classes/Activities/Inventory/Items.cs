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
            intent.PutExtra("Item", itm.Items[args.Position].Item);
            StartActivity(intent);
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);
            List<BaseItem> Rows;
            ItemsAdapter Adapter = (ItemsAdapter)ListView.Adapter;
            IDOItems Items = (IDOItems)BaseObject;
            if (!Append)
            {
                Rows = new List<BaseItem>();
            }
            else
            {
                Rows = Adapter.Items;
            }
            for (int i = 0; i < BaseObject.GetRowCount(); i++)
            {
                LastKey = Items.GetItem(i);
                Rows.Add(new BaseItem()
                {
                    Item = Items.GetItem(i),
                    Description = Items.GetDescription(i),
                    DerQtyOnHand = Items.GetQtyOnHand(i),
                    UM = Items.GetUM(i),
                    MatlType = Items.GetMatlType(i),
                    PMTCode = Items.GetMatlType(i),
                    ProductCode = Items.GetProductCode(i),
                    Picture = Items.GetPicture(i),
                    LotTracked = Items.GetLotTracked(i),
                    SerialTracked = Items.GetSerialTracked(i)
                });
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