using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Business.Inventory;
using System;
using SyteLine.Classes.Business.Purchase;

namespace SyteLine.Classes.Activities.Inventory
{
    [Activity(Label = "@string/PurchaseReceive")]
    public class PurchaseOrderReceive : CSIBaseActivity
    {
        private TextView WarehouseEdit;
        private TextView TransDateDateEdit;
        private EditText OrderNumEdit;
        private EditText LineEdit;
        private EditText ReleaseEdit;
        private EditText LocationEdit;
        private EditText UMEdit;
        private EditText QuantityEdit;
        private EditText LotEdit;
        private EditText ReasonEdit;
        private TextView ItemEdit;
        private TextView ItemDescriptionEdit;
        private TextView ReasonDescriptionEdit;
        private Switch LotSwitch;
        private Switch SNSwitch;
        private Button ScanOrderButton;
        private Button ScanUMButton;
        private Button ScanLocationButton;
        private Button ScanQuantityButton;
        private Button ScanLotButton;
        private LinearLayout POLinearLayout;
        private LinearLayout ItemLinearLayout;
        private LinearLayout InventoryLinearLayout;

        IDOItems Item;
        IDOPurchaseOrders PO;
        IDOPurchaseOrderLines POItem;
        IDOPurchaseOrderBlanketLines POBlnLine;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            PrimaryBusinessObject = new IDODcmoves(SessionToken(), this);
            PO = new IDOPurchaseOrders(SessionToken(), this);
            Item = new IDOItems(SessionToken(), this);
            AddBusinessObjects(PO);
            AddBusinessObjects(Item);

            StartRefresh = false;
            defaultLayoutID = Resource.Layout.FunctionPurchaseOrderReceiveViewer;

            base.OnCreate(savedInstanceState);
        }

        protected override void InitializeActivity()
        {
            InitialComponents();
            InitialIDOs();
        }

        private void InitialComponents()
        {
            WarehouseEdit = FindViewById<TextView>(Resource.Id.WarehouseEdit);
            TransDateDateEdit = FindViewById<TextView>(Resource.Id.TransDateEdit);
            OrderNumEdit = FindViewById<EditText>(Resource.Id.OrderNumEdit);
            LineEdit = FindViewById<EditText>(Resource.Id.LineEdit);
            ReleaseEdit = FindViewById<EditText>(Resource.Id.ReleaseEdit);
            LocationEdit = FindViewById<EditText>(Resource.Id.LocationEdit);
            UMEdit = FindViewById<EditText>(Resource.Id.UMEdit);
            QuantityEdit = FindViewById<EditText>(Resource.Id.QuantityEdit);
            LotEdit = FindViewById<EditText>(Resource.Id.LotEdit);
            ReasonEdit = FindViewById<EditText>(Resource.Id.ReasonEdit);
            ItemEdit = FindViewById<TextView>(Resource.Id.ItemEdit);
            ItemDescriptionEdit = FindViewById<TextView>(Resource.Id.ItemDescriptionEdit);
            ReasonDescriptionEdit = FindViewById<TextView>(Resource.Id.ReasonDescriptionEdit);
            LotSwitch = FindViewById<Switch>(Resource.Id.LotSwitch);
            SNSwitch = FindViewById<Switch>(Resource.Id.SNSwitch);

            ScanOrderButton = FindViewById<Button>(Resource.Id.ScanOrderButton);
            ScanUMButton = FindViewById<Button>(Resource.Id.ScanUMButton);
            ScanLocationButton = FindViewById<Button>(Resource.Id.ScanLocationButton);
            ScanQuantityButton = FindViewById<Button>(Resource.Id.ScanQuantityButton);
            ScanLotButton = FindViewById<Button>(Resource.Id.ScanLotButton);

            POLinearLayout = FindViewById<LinearLayout>(Resource.Id.POLinearLayout);
            ItemLinearLayout = FindViewById<LinearLayout>(Resource.Id.ItemLinearLayout);
            InventoryLinearLayout = FindViewById<LinearLayout>(Resource.Id.InventoryLinearLayout);

            ItemLinearLayout.Visibility = ViewStates.Gone;
            InventoryLinearLayout.Visibility = ViewStates.Gone;

            WarehouseEdit.SetText(DefaultWhse(), null);
            TransDateDateEdit.SetText(CurrentDateTime(), null);

            ScanOrderButton.Click += ScanOrderButtonClicked;
            ScanUMButton.Click += ScanUMButtonClicked;
            ScanLocationButton.Click += ScanLocationButtonClicked;
            ScanQuantityButton.Click += ScanQuantityButtonClicked;
            ScanLotButton.Click += ScanLotButtonClicked;

        }

        private void InitialIDOs()
        {
            PO.parm.PropertyList = "PoNum,PoLine,";
            Item.parm.PropertyList = "";
        }

        private void OrderNumberChanged()
        {

        }

        private async void ScanOrderButtonClicked(object sender, EventArgs args)
        {
            ZXing.Result result = await ScanCode();
        }

        private async void ScanUMButtonClicked(object sender, EventArgs args)
        {
            ZXing.Result result = await ScanCode();
            OrderNumEdit.SetText(result.Text, null);
        }

        private async void ScanLocationButtonClicked(object sender, EventArgs args)
        {
            ZXing.Result result = await ScanCode();
        }

        private async void ScanQuantityButtonClicked(object sender, EventArgs args)
        {
            ZXing.Result result = await ScanCode();
        }

        private async void ScanLotButtonClicked(object sender, EventArgs args)
        {
            ZXing.Result result = await ScanCode();
        }

        protected override void PostReadIDOs(int index = 0)
        {
            base.PostReadIDOs(index);
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