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

namespace SyteLine.Classes.Activities.Inventory
{
    [Activity(Label = "@string/ItemDetails")]
    public class ItemDetails : BaseDetailActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.BaseObject = new IDOItems(base.Intent.GetStringExtra("SessionToken"), this);
            base.AddSecondObject( new IDOItemLocs(base.Intent.GetStringExtra("SessionToken"), this));
            base.OnCreate(savedInstanceState);
        }

        protected override void RegisterAdapter(bool Append)
        {
            List<AdapterList> Rows;
            try
            {
                base.RegisterAdapter(Append);

                IDOItems Items = (IDOItems)BaseObject;

                SetKey(Items.GetItem());
                SetKeyDescription(Items.GetDescription());
                if (new Configure().LoadPicture)
                {
                    SetImageView(Items.GetPicture());
                }

                Rows = new List<AdapterList>();
                for (int i = 0; i < Items.GetRowCount(); i++)
                {
                    Rows.Add(new AdapterList("-", "", GetString(Resource.String.General)));
                    Rows.Add(new AdapterList("DerQtyOnHand", Items.GetQtyOnHand(i), GetString(Resource.String.OnHandQuantity)));
                    Rows.Add(new AdapterList("UM", Items.GetUM(i), GetString(Resource.String.UnitofMeasure)));
                    Rows.Add(new AdapterList("MatlType", Items.GetMatlType(i), GetString(Resource.String.MaterialType)));
                    Rows.Add(new AdapterList("PMTCode", Items.GetPMTCode(i), GetString(Resource.String.MaterialType)));
                    Rows.Add(new AdapterList("ProductCode", Items.GetProductCode(i), GetString(Resource.String.ProductCode)));
                    Rows.Add(new AdapterList("LotTracked", Items.GetLotTracked(i), GetString(Resource.String.LotTracked)));
                    Rows.Add(new AdapterList("SerialTracked", Items.GetSerialTracked(i), GetString(Resource.String.SNTracked)));

                    if (!string.IsNullOrEmpty(Items.GetOverview(i)))
                    {
                        Rows.Add(new AdapterList("--", "", GetString(Resource.String.Overview)));
                        Rows.Add(new AdapterList("Overview", Items.GetOverview(i), ""));
                    }
                }

                string whse = "";
                IDOItemLocs itemLoc = (IDOItemLocs)SencondObjects[1];
                for (int i = 0; i < itemLoc.GetRowCount(); i++)
                {
                    if (whse != itemLoc.GetWhse(i))
                    {
                        if (!string.IsNullOrEmpty(itemLoc.GetWhsName(i)))
                        {
                            Rows.Add(new AdapterList("-", "", string.Format("{0}\r\n{1}", itemLoc.GetWhse(i), itemLoc.GetWhsName(i))));
                        }
                        else
                        {
                            Rows.Add(new AdapterList("-", "", itemLoc.GetWhse(i)));
                        }
                        Rows.Add(new AdapterList("ItmwhseQtyOnHand", itemLoc.GetItmwhseQtyOnHand(i), GetString(Resource.String.ItmwhseQtyOnHand)));
                        whse = itemLoc.GetWhse(i);
                    }
                    if (!string.IsNullOrEmpty(itemLoc.GetLocDescription(i)))
                    {
                        Rows.Add(new AdapterList("--", "", string.Format("{0}\r\n{1}", itemLoc.GetLoc(i), itemLoc.GetLocDescription(i))));
                    }
                    else
                    {
                        Rows.Add(new AdapterList("--", "", itemLoc.GetLoc(i)));
                    }
                    Rows.Add(new AdapterList("LocType", itemLoc.GetLocType(i), GetString(Resource.String.LocType)));
                    Rows.Add(new AdapterList("Rank", itemLoc.GetRank(i), GetString(Resource.String.Rank)));
                    Rows.Add(new AdapterList("ItmIssueBy", itemLoc.GetItmIssueBy(i), GetString(Resource.String.ItmIssueBy)));
                    Rows.Add(new AdapterList("QtyOnHand", itemLoc.GetQtyOnHand(i), GetString(Resource.String.QtyOnHand)));
                    Rows.Add(new AdapterList("QtyRsvd", itemLoc.GetQtyRsvd(i), GetString(Resource.String.QtyRsvd)));
                }
                ListView.Adapter = new ItemDetailsAdapter(this, Rows);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
            try
            {
                IDOItems Items = (IDOItems)BaseObject;
                Items.parm.PropertyList = "Item,Description,Overview,DerQtyOnHand,UM,MatlType,PMTCode,ProductCode,LotTracked,SerialTracked";//,Picture
                if (new Configure().LoadPicture)
                {
                    BaseObject.parm.PropertyList += ",Picture";
                }
                Items.BuilderFilterByItem(Intent.GetStringExtra("Item"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected override void PrepareIDOs(int index)
        {
            base.PrepareIDOs(index);
            try
            {
                if (index == 1)
                {
                    IDOItemLocs itemLoc = (IDOItemLocs)GetSecondObject(index);
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
