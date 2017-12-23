using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Business.Admin;
using static Android.Widget.AdapterView;
using Android.Content;
using SyteLine.Classes.Adapters.Common;
using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Activities.Common;

namespace SyteLine.Classes.Activities.Admin
{
    [Activity(Label = "@string/Welcome")]
    public class Main : CSIBaseActivity
    {
        private DateTime? LastBackKeyDownTime;
        private ListView CommandListView;
        private List<ListViewerAdapterItem> CommandList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                this.Title = string.Format(GetString(Resource.String.Welcome1), GetUserDesc() + "("+ Intent.GetStringExtra("User") + ")");

                // Set our view from the Inentory layout resource
                SetContentView(Resource.Layout.CommonListViewer);

                CommandListView = FindViewById<ListView>(Resource.Id.ListView);

                InitialCommandList();

                CommandListView.ItemClick += delegate (object sender, ItemClickEventArgs args)
                {
                    Intent intent = new Intent(this, CommandList[args.Position].ActivityType);
                    intent.PutExtra("SessionToken", this.Intent.GetStringExtra("SessionToken"));
                    StartActivity(intent);
                };
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "Main -> OnCreate() -> " + Ex.Message, ToastLength.Short).Show();
            }
        }

        private string GetUserDesc()
        {
            IDOUsers users = new IDOUsers(Intent.GetStringExtra("SessionToken"));
            users.BuilderFileByUserName(Intent.GetStringExtra("User"));
            users.Read();
            return users.GetPropertyValue("UserDesc");
        }

        public void InitialCommandList()
        {
            CommandList = new List<ListViewerAdapterItem>
            {
                new ListViewerAdapterItem()
                {
                    ImageId = Resource.Drawable.manufacturers,
                    StringId = Resource.String.Inventory,
                    ActivityType = typeof(Inventory.Inventory)
                },
                new ListViewerAdapterItem()
                {
                    ImageId = Resource.Drawable.shipping,
                    StringId = Resource.String.Purchase,
                    ActivityType = typeof(Purchase.Purchase)
                },
                new ListViewerAdapterItem()
                {
                    ImageId = Resource.Drawable.cash,
                    StringId = Resource.String.Sales,
                    ActivityType = typeof(Sales.Sales)
                },
                new ListViewerAdapterItem()
                {
                    ImageId = Resource.Drawable.shopfloor,
                    StringId = Resource.String.Shopfloor,
                    ActivityType = typeof(Shopfloor.Shopfloor)
                },
                new ListViewerAdapterItem()
                {
                    ImageId = Resource.Drawable.report,
                    StringId = Resource.String.Administration,
                    ActivityType = typeof(Settings)
                }
            };

            CommandListView.Adapter = new CSIBaseListViewerAdapter(this, CommandList);

        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!LastBackKeyDownTime.HasValue || DateTime.Now - LastBackKeyDownTime.Value > new TimeSpan(0, 0, 2))
                {
                    Toast.MakeText(this, Resource.String.ClickBackToExit, ToastLength.Short).Show();
                    LastBackKeyDownTime = DateTime.Now;
                }
                else
                {
                    Finish();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
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