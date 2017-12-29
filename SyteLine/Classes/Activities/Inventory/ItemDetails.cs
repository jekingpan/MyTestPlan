using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Inventory;
using SyteLine.Classes.Business.Inventory;
using SyteLine.Classes.Adapters.Common;
using static SyteLine.Classes.Adapters.Common.AdapterListItem;

namespace SyteLine.Classes.Activities.Inventory
{
    [Activity(Label = "@string/ItemDetails")]
    public class ItemDetails : CSIBaseDetailActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.PrimaryBusinessObject = new IDOItems(SessionToken(), this);
            base.AddBusinessObjects(new IDOItemLocs(SessionToken(), this));
            base.OnCreate(savedInstanceState);
        }

        protected override void BeforeReadIDOs()
        {
            base.BeforeReadIDOs();
            try
            {
                IDOItems Items = (IDOItems)PrimaryBusinessObject;
                Items.parm.PropertyList = "Item,Description";//,Overview,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,LotTracked,SerialTracked";
                SetAdapterLists(0, "-", "", ValueTypes.String, GetString(Resource.String.General), Resource.Layout.CommonSplitterViewer);
                SetAdapterLists(0, "DerQtyOnHand", "DerQtyOnHand", ValueTypes.Decimal, GetString(Resource.String.OnHandQuantity));
                SetAdapterLists(0, "UM", "UM", ValueTypes.String, GetString(Resource.String.UnitofMeasure));
                SetAdapterLists(0, "MatlType", "MatlType", ValueTypes.String, GetString(Resource.String.MaterialType));
                SetAdapterLists(0, "PMTCode", "PMTCode", ValueTypes.String, GetString(Resource.String.MaterialSource));
                SetAdapterLists(0, "ProductCode", "ProductCode", ValueTypes.String, GetString(Resource.String.ProductCode));
                SetAdapterLists(0, "LotTracked", "LotTracked", ValueTypes.Boolean, GetString(Resource.String.LotTracked), Resource.Layout.CommonLabelSwitchViewer);
                SetAdapterLists(0, "SerialTracked", "SerialTracked", ValueTypes.Boolean, GetString(Resource.String.SNTracked), Resource.Layout.CommonLabelSwitchViewer);
                SetAdapterLists(0, "Overview", "Overview", ValueTypes.String, GetString(Resource.String.Overview), Resource.Layout.CommonLabelMultiLinesTextViewer);
                Items.SetOrderBy("Item");

                if (new Configure().LoadPicture)
                {
                    SetAdapterLists(0, "Picture", "Picture", ValueTypes.Bitmap, "");
                }
                Items.BuilderFilterByItem(Intent.GetStringExtra("Item"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override void BeforeReadIDOs(int index)
        {
            base.BeforeReadIDOs(index);
            try
            {
                if (index == 1)
                {
                    IDOItemLocs itemLoc = (IDOItemLocs)GetSecondObject(index);
                    itemLoc.HideDuplcatedCol("Whse");
                    itemLoc.HideDuplcatedCol("WhsName");
                    itemLoc.HideDuplcatedCol("ItmwhseQtyOnHand");
                    itemLoc.parm.PropertyList = "";
                    SetAdapterLists(index, "Whse", "Whse", ValueTypes.String, GetString(Resource.String.Warehouse) + " - {0}", Resource.Layout.CommonSplitterViewer);
                    SetAdapterLists(index, "WhsName", "WhsName", ValueTypes.String, GetString(Resource.String.WarehouseName));
                    SetAdapterLists(index, "ItmwhseQtyOnHand", "ItmwhseQtyOnHand", ValueTypes.Decimal, GetString(Resource.String.ItmwhseQtyOnHand));
                    SetAdapterLists(index, "Loc", "Loc", ValueTypes.String, GetString(Resource.String.Location) + " - {0}", Resource.Layout.CommonSubSplitterViewer);
                    SetAdapterLists(index, "LocDescription", "LocDescription", ValueTypes.String, GetString(Resource.String.LocDescription), Resource.Layout.CommonSubLabelTextViewer); 
                    SetAdapterLists(index, "LocType", "LocType", ValueTypes.String, GetString(Resource.String.LocType), Resource.Layout.CommonSubLabelTextViewer); 
                    SetAdapterLists(index, "Rank", "Rank", ValueTypes.String, GetString(Resource.String.Rank), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(index, "ItmIssueBy", "ItmIssueBy", ValueTypes.String, GetString(Resource.String.ItmIssueBy), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(index, "QtyOnHand", "QtyOnHand", ValueTypes.Decimal, GetString(Resource.String.QtyOnHand), Resource.Layout.CommonSubLabelTextViewer);
                    SetAdapterLists(index, "QtyRsvd", "QtyRsvd", ValueTypes.Decimal, GetString(Resource.String.QtyRsvd), Resource.Layout.CommonSubLabelTextViewer);

                    itemLoc.BuilderFilterByItem(Intent.GetStringExtra("Item"));
                    itemLoc.BuilderAdditionalFilter("QtyOnHand <> 0");
                    itemLoc.parm.RecordCap = -1;
                    itemLoc.SetOrderBy("ItmwhseQtyOnHand DESC,QtyOnHand DESC,Rank");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override void RegisterAdapter(bool Append)
        {
            try
            {
                base.RegisterAdapter(Append);

                IDOItems Items = (IDOItems)PrimaryBusinessObject;

                SetKey(Items.GetPropertyValue("Item"));
                SetKeyDescription(Items.GetPropertyValue("Description"));
                if (new Configure().LoadPicture)
                {
                    SetImageView(Items.GetPropertyBitmap("Picture"));
                }
                
                ListView.Adapter = new ItemDetailsAdapter(this, AdapterLists);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
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
                case 1:
                    IDOItemLocs itemLoc = (IDOItemLocs)GetSecondObject(objIndex);
                    value = itemLoc.GetPropertyDisplayedValue(name, row);
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
