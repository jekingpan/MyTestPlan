using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SyteLine.Classes.Activities.Common;

namespace SyteLine.Classes.Activities.Inventory
{

    [Activity(Label = "@string/MiscIssueReceipt")]
    public class MiscIssueReceipt : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the Inentory layout resource
            SetContentView(Resource.Layout.CommonSearchViewer);
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.SearchView_Menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //if (item.ItemId == Resource.Id.Menu_Refresh)
            //{
            //}
            //else if (item.ItemId == Resource.Id.Menu_GetMoreRows)
            //{
            //}
            return base.OnOptionsItemSelected(item);
        }
    }
}