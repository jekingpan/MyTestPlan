using Android.App;
using Android.OS;
using Android.Widget;
using static Android.Widget.AdapterView;
using System;
using Android.Content;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Activities.Inventory
{
    [Activity(Label = "@string/Inventory")]
    public class Inventory : BaseActivity
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
                GridViewAdapter GridAdapter = new GridViewAdapter(this, GridView);
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.stock,
                    Name = GetString(Resource.String.Items),
                    ActivityType = typeof(Items)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.move,
                    Name = GetString(Resource.String.QuantityMove),
                    //ActivityType = typeof(QuantityMove)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.moveout,
                    Name = GetString(Resource.String.MiscIssue),
                    //ActivityType = typeof(MiscellaneousIssue)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.movein,
                    Name = GetString(Resource.String.MiscReceive),
                    //ActivityType = typeof(MiscellaneousReceive)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.report,
                    Name = GetString(Resource.String.TransferOrders),
                    //ActivityType = typeof(MiscellaneousReceive)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.transferout,
                    Name = GetString(Resource.String.TransferShip),
                    //ActivityType = typeof(MiscellaneousReceive)
                });
                GridAdapter.ActionItems.Add(new GridViewActionItem()
                {
                    ThumbId = Resource.Drawable.transferin,
                    Name = GetString(Resource.String.TransferReceive),
                    //ActivityType = typeof(MiscellaneousIssue)
                });
                GridView.Adapter = GridAdapter;

                GridView.ItemClick += delegate (object sender, ItemClickEventArgs args)
                {
                    //Toast.MakeText(GridView.Context, GridAdapter.ActionItems[args.Position].Name, ToastLength.Short).Show();
                    if (!(GridAdapter.ActionItems[args.Position].ActivityType is null))
                    {
                        Intent intent = new Intent(this, GridAdapter.ActionItems[args.Position].ActivityType);
                        intent.PutExtra("SessionToken", this.Intent.GetStringExtra("SessionToken"));
                        this.StartActivity(intent);
                    }
                };
            }
            catch(Exception Ex)
            {
                throw Ex;
            }           
            
        }
    }
}
