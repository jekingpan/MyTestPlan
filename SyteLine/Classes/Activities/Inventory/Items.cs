using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
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
    public class Items : CSIBaseSearchActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {            
            if (PrimaryBusinessObject == null)
            {
                PrimaryBusinessObject = new IDOItems(SessionToken(), this);
            }
            base.OnCreate(savedInstanceState);
        }

        protected override void ListViewClicked(object sender, ItemClickEventArgs args)
        {
            base.ListViewClicked(sender, args);
            ItemsAdapter itm = (ItemsAdapter)ListView.Adapter;
            Intent intent = new Intent(this, typeof(ItemDetails));
            SetDefaultIntent(intent);
            intent.PutExtra("Item", itm.objectList[args.Position].GetString("Item"));
            StartActivity(intent);
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);

            if (!Append)
            {
                ListView.Adapter = new ItemsAdapter(this, AdapterLists);
            }
        }

        protected override void BeforeReadIDOs()
        {
            base.BeforeReadIDOs();
            IDOItems Items = (IDOItems)PrimaryBusinessObject;
            Items.parm.PropertyList = "";

            AdapterList adptList = new AdapterList
            {
                KeyName = "Item"
            };
            adptList.Add("Item");
            adptList.Add("Description");
            adptList.Add("DerQtyOnHand", AdapterListItem.ValueTypes.Decimal);
            adptList.Add("UM");
            adptList.Add("MatlType");
            adptList.Add("PMTCode");
            adptList.Add("ProductCode");
            adptList.Add("LotTracked", AdapterListItem.ValueTypes.Boolean);
            adptList.Add("SerialTracked", AdapterListItem.ValueTypes.Boolean);
            if (new Configure().LoadPicture)
            {
                adptList.Add("Picture", AdapterListItem.ValueTypes.Bitmap);
            }
            if (QueryString == "")
            {
                //QueryString = "%";
                Items.BuilderFilterByItem("%");
            }
            else
            {
                Items.BuilderFilterByItemOrDesc(QueryString);
            }
            if (LastKey != "")
            {
                Items.BuilderAdditionalFilter(string.Format("Item > N'{0}'", LastKey));
            }
            SetAdapterLists(0, adptList);

        }

        protected override string GetPropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            string value = "";
            switch (objIndex)
            {
                case 0:
                    IDOItems Items = (IDOItems)PrimaryBusinessObject;
                    value = Items.GetPropertyDisplayedValue(name, row);
                    break;
                default:
                    break;
            }
            return value;
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