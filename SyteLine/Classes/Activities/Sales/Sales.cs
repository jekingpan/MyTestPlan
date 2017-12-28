using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using static Android.Widget.AdapterView;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Activities.Sales
{
    [Activity(Label = "@string/Sales")]
    public class Sales : CSIBaseActivity
    {
        private GridView GridView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the Inentory layout resource
            SetContentView(Resource.Layout.CommonGridViewer);

            GridView = FindViewById<GridView>(Resource.Id.GridView);

            try
            {
                CSIBaseGridViewerAdapter GridAdapter = new CSIBaseGridViewerAdapter(this, GridView);
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.report,
                    Name = GetString(Resource.String.SalesOrders),
                    //ActivityType = typeof(Item)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.shipping,
                    Name = GetString(Resource.String.SalesShip),
                    //ActivityType = typeof(QuantityMove)
                });
                GridView.Adapter = GridAdapter;

                GridView.ItemClick += delegate (object sender, ItemClickEventArgs args) {
                    Toast.MakeText(GridView.Context, GridAdapter.ActionItems[args.Position].Name, ToastLength.Short).Show();
                    if (!(GridAdapter.ActionItems[args.Position].ActivityType is null))
                    {
                        Intent intent = new Intent(this, GridAdapter.ActionItems[args.Position].ActivityType);
                        SetDefaultIntent(intent);
                        this.StartActivity(intent);
                    }
                };
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
    }
}