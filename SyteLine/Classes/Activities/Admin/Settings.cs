using System;
using Android.App;
using Android.OS;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using Android.Views;
using SyteLine.Classes.Activities.Common;

namespace SyteLine.Classes.Activities.Admin
{
    [Activity(Label = "@string/Settings")]
    public class Settings : CSIBaseActivity
    {
        private Configure configure = new Configure();

        EditText CSIWebServer;
        Switch EnableHTTPS;
        Switch UseRESTForRequest;
        Switch LoadPicture;
        EditText User;
        EditText Password;
        Spinner Configuration;
        Button SaveButton;
        Button TestButton;
        EditText RecordCap;
        Switch SaveUser;
        Switch SavePassword;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //    // Set our view from the Inentory layout resource
            //    SetContentView(Resource.Layout.SystemSettings);
            defaultLayoutID = Resource.Layout.SystemSettings;

            base.OnCreate(savedInstanceState);

            CSIWebServer = FindViewById<EditText>(Resource.Id.CSIWebServerEdit);
            EnableHTTPS = FindViewById<Switch>(Resource.Id.EnableHTTPSEdit);
            UseRESTForRequest = FindViewById<Switch>(Resource.Id.UseRESTForRequestEdit);
            User = FindViewById<EditText>(Resource.Id.UserEdit);
            Password = FindViewById<EditText>(Resource.Id.PasswordEdit);
            Configuration = FindViewById<Spinner>(Resource.Id.ConfigurationEdit);
            LoadPicture = FindViewById<Switch>(Resource.Id.LoadPictureEdit);
            RecordCap = FindViewById<EditText>(Resource.Id.RecordCapEdit);
            SaveButton = FindViewById<Button>(Resource.Id.SaveButton);
            TestButton = FindViewById<Button>(Resource.Id.TestButton);
            SaveUser = FindViewById<Switch>(Resource.Id.SaveUserSwitch);
            SavePassword = FindViewById<Switch>(Resource.Id.SavePasswordSwitch);

            CSIWebServer.SetText(configure.CSIWebServer, null);
            User.SetText(configure.User, null);
            Password.SetText(configure.Password, null);
            EnableHTTPS.Checked = configure.EnableHTTPS;
            UseRESTForRequest.Checked = configure.UseRESTForRequest;
            LoadPicture.Checked = configure.LoadPicture;
            RecordCap.SetText(configure.RecordCap,null);
            SaveUser.Checked = configure.SaveUser;
            SavePassword.Checked = configure.SavePassword;

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.Add(configure.Configuration);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Configuration.Adapter = adapter;
            Configuration.SetSelection(0);

            SaveButton.Click += SaveButtonClicked;
            TestButton.Click += TestButtonClicked;
            LoadPicture.CheckedChange += delegate
            {
                if (LoadPicture.Checked)
                {
                    Toast.MakeText(this, Resource.String.LoadPictureMessage, ToastLength.Short).Show();
                }
            };
            SaveUser.CheckedChange += delegate
            {
                EnableDisableComponents();
            };
            ;
            SavePassword.CheckedChange += delegate
            {
                EnableDisableComponents();
            };
            EnableDisableComponents();
        }

        private void EnableDisableComponents()
        {
            User.Enabled = SaveUser.Checked;
            SavePassword.Enabled = SaveUser.Checked;
            if (!SaveUser.Checked)
            {
                User.SetText("", null);
                SavePassword.Checked = false;
            }
            Password.Enabled = SaveUser.Checked && SavePassword.Checked;
            if (!SavePassword.Checked)
            {
                Password.SetText("", null);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Settings_Menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            //    ToastLength.Short).Show();
            if (item.ItemId == Resource.Id.Menu_Save)
            {
                SaveSettings();
            }
            else if (item.ItemId == Resource.Id.Menu_Test)
            {
                TestSettings();
            }
            return base.OnOptionsItemSelected(item);
        }

        private void TestButtonClicked(object sender, EventArgs e)
        {
            TestSettings();
        }
        private void TestSettings()
        {
            configure.CSIWebServer = CSIWebServer.Text;
            configure.User = User.Text;
            configure.Password = Password.Text;
            configure.EnableHTTPS = EnableHTTPS.Checked;
            configure.UseRESTForRequest = UseRESTForRequest.Checked;

            try
            {
                string[] List = configure.GetConfigurationList();
                int DefualtIndex = 0;
                ArrayAdapter Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);
                for (int i = 0; i < List.Length; i++)
                {
                    Adapter.Add(List[i]);
                    if (configure.Configuration == List[i])
                    {
                        DefualtIndex = i;
                    }
                }
                Adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                Configuration.Adapter = Adapter;
                Configuration.SetSelection(DefualtIndex);
                Toast.MakeText(this, Resource.String.PassTest, ToastLength.Short).Show();
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "TestSettings() -> " + Ex.Message, ToastLength.Short).Show();
            }
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            configure.CSIWebServer = CSIWebServer.Text;
            configure.SaveUser = SaveUser.Checked;
            configure.SavePassword = SavePassword.Checked;
            configure.User = User.Text;
            configure.Password = Password.Text;
            configure.EnableHTTPS = EnableHTTPS.Checked;
            configure.UseRESTForRequest = UseRESTForRequest.Checked;
            configure.LoadPicture = LoadPicture.Checked;
            configure.RecordCap = RecordCap.Text;
            configure.Configuration = Configuration.SelectedItem.ToString();
            configure.SaveConfigure();
            Toast.MakeText(this, Resource.String.Saved, ToastLength.Short).Show();
        }

    }
}