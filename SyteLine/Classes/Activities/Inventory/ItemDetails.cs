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
            List<BaseItem> Rows;
            try
            {
                base.RegisterAdapter(Append);

                IDOItems Items = (IDOItems)BaseObject;

                SetKey(Items.GetItem());
                SetKeyDescription(Items.GetDescription()); if (new Configure().LoadPicture)
                {
                    SetImageView(Items.GetPicture());
                }

                Rows = new List<BaseItem>();
                for (int i = 0; i < Items.GetRowCount(); i++)
                {
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "-",
                        LableString = GetString(Resource.String.General),
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "DerQtyOnHand",
                        LableString = GetString(Resource.String.OnHandQuantity),
                        DerQtyOnHand = Items.GetQtyOnHand(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "UM",
                        LableString = GetString(Resource.String.UnitofMeasure),
                        UM = Items.GetUM(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "MatlType",
                        LableString = GetString(Resource.String.MaterialType),
                        MatlType = Items.GetMatlType(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "PMTCode",
                        LableString = GetString(Resource.String.MaterialSource),
                        PMTCode = Items.GetPMTCode(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "ProductCode",
                        LableString = GetString(Resource.String.ProductCode),
                        ProductCode = Items.GetProductCode(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "LotTracked",
                        LableString = GetString(Resource.String.LotTracked),
                        LotTracked = Items.GetLotTracked(i)
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "SerialTracked",
                        LableString = GetString(Resource.String.SNTracked),
                        SerialTracked = Items.GetSerialTracked(i)
                    });
                    if ((Items.GetOverview(i) != "") || !(Items.GetOverview(i) is null))
                    {
                        Rows.Add(new BaseItem()
                        {
                            PropertyName = "-",
                            LableString = GetString(Resource.String.Overview),
                        });
                        Rows.Add(new BaseItem()
                        {
                            PropertyName = "Overview",
                            LableString = "",
                            Overview = Items.GetOverview(i)
                        });
                    }
                }

                string whse = "";
                IDOItemLocs itemLoc = (IDOItemLocs)SencondObjects[1];
                List<BaseItemLoc> itemLocRows = new List<BaseItemLoc>();
                for (int i = 0; i < itemLoc.GetRowCount(); i++)
                {
                    if (whse != itemLoc.GetWhse(i))
                    {
                        if (itemLoc.GetWhsName(i) != "")
                        {
                            Rows.Add(new BaseItem()
                            {
                                PropertyName = "-",
                                LableString = string.Format("{0}\r\n{1}", itemLoc.GetWhse(i), itemLoc.GetWhsName(i))
                            });
                        }
                        else
                        {
                            Rows.Add(new BaseItem()
                            {
                                PropertyName = "-",
                                LableString = itemLoc.GetWhse(i)
                            });
                        }
                        Rows.Add(new BaseItem()
                        {
                            PropertyName = "ItmwhseQtyOnHand",
                            LableString = GetString(Resource.String.ItmwhseQtyOnHand),
                            ItemLoc = new BaseItemLoc()
                            {
                                ItmwhseQtyOnHand = itemLoc.GetItmwhseQtyOnHand(i)
                            }
                        });
                        whse = itemLoc.GetWhse(i);
                    }
                    if (itemLoc.GetLocDescription(i) != "")
                    {
                        Rows.Add(new BaseItem()
                        {
                            PropertyName = "--",
                            LableString = string.Format("{0}\r\n{1}", itemLoc.GetLoc(i), itemLoc.GetLocDescription(i))
                        });
                    }
                    else
                    {
                        Rows.Add(new BaseItem()
                        {
                            PropertyName = "--",
                            LableString = itemLoc.GetLoc(i)
                        });
                    }
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "LocType",
                        LableString = GetString(Resource.String.LocType),
                        ItemLoc = new BaseItemLoc()
                        {
                            LocType = itemLoc.GetLocType(i)
                        }
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "Rank",
                        LableString = GetString(Resource.String.Rank),
                        ItemLoc = new BaseItemLoc()
                        {
                            Rank = itemLoc.GetRank(i)
                        }
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "ItmIssueBy",
                        LableString = GetString(Resource.String.ItmIssueBy),
                        ItemLoc = new BaseItemLoc()
                        {
                            ItmIssueBy = itemLoc.GetItmIssueBy(i)
                        }
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "QtyOnHand",
                        LableString = GetString(Resource.String.QtyOnHand),
                        ItemLoc = new BaseItemLoc()
                        {
                            QtyOnHand = itemLoc.GetQtyOnHand(i)
                        }
                    });
                    Rows.Add(new BaseItem()
                    {
                        PropertyName = "QtyRsvd",
                        LableString = GetString(Resource.String.QtyRsvd),
                        ItemLoc = new BaseItemLoc()
                        {
                            QtyRsvd = itemLoc.GetQtyRsvd(i)
                        }
                    });
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
