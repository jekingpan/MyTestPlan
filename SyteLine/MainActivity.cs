using System;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using SyteLine.Classes.Activities.Admin;
using SyteLine.Classes.Activities.Inventory;

namespace SyteLine
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            StartActivity(typeof(Login));
            Finish();
        }
    }
    
}