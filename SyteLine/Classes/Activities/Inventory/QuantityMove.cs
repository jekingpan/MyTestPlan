using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Common;
using SyteLine.Classes.Adapters.Inventory;
using SyteLine.Classes.Business.Inventory;
using static SyteLine.Classes.Adapters.Common.AdapterListItem;

namespace SyteLine.Classes.Activities.Inventory
{

    [Activity(Label = "@string/QuantityMove")]
    public class QuantityMove : CSIBaseProcessActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            BaseObject = new IDODcmoves(Intent.GetStringExtra("SessionToken"), this);
            StartRefresh = false;
            defaultLayoutID = Resource.Layout.FunctionQuantityMoveViewer;
            base.OnCreate(savedInstanceState);
        }

        protected override void RegisterAdapter(bool Append)
        {
            base.RegisterAdapter(Append);

            if (!Append)
            {
                ListView.Adapter = new QuantityMoveAdapter(this, AdapterLists);
            }
        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            IDODcmoves Items = (IDODcmoves)BaseObject;
            Items.parm.PropertyList = "";

            SetAdapterLists(0, "Whse", "Whse", ValueTypes.String, GetString(Resource.String.Warehouse), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "Item", "Item", ValueTypes.String, GetString(Resource.String.Item), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "ItemDescription", "ItemDescription", ValueTypes.String, GetString(Resource.String.Description), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "QtyMoved", "QtyMoved", ValueTypes.Decimal, GetString(Resource.String.Quantity), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "UM", "UM", ValueTypes.String, GetString(Resource.String.UnitofMeasure), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "Loc1", "Loc1", ValueTypes.String, GetString(Resource.String.Location), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "Lot1", "Lot1", ValueTypes.String, GetString(Resource.String.Lot), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "Loc2", "Loc2", ValueTypes.String, GetString(Resource.String.Location), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "Lot2", "Lot2", ValueTypes.String, GetString(Resource.String.Lot), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "TransDate", "TransDate", ValueTypes.Date, GetString(Resource.String.Date), Resource.Layout.CommonFloatingLabelEditViewer);
            SetAdapterLists(0, "DocumentNum", "DocumentNum", ValueTypes.String, GetString(Resource.String.DocumentNum), Resource.Layout.CommonFloatingLabelEditViewer);
        }

        protected override void UpdateAdapterLists(int index = 0)
        {
            base.UpdateAdapterLists(index);
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