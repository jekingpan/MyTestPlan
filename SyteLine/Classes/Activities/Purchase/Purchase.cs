using Android.App;
using Android.OS;
using Android.Widget;
using static Android.Widget.AdapterView;
using System;
using Android.Content;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Activities.Purchase
{
    [Activity(Label = "@string/Purchase")]
    public class Purchase : CSIBaseActivity
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
                    Name = GetString(Resource.String.PurchaseOrders),
                    ActivityType = typeof(PurchaseOrders)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.shipping,
                    Name = GetString(Resource.String.PurchaseReceive),
                    //ActivityType = typeof(QuantityMove)
                });
                GridView.Adapter = GridAdapter;

                GridView.ItemClick += delegate (object sender, ItemClickEventArgs args)
                {
                    //Toast.MakeText(GridView.Context, GridAdapter.ActionItems[args.Position].Name, ToastLength.Short).Show();
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
