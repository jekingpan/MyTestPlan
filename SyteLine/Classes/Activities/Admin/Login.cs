using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using System;
using SyteLine.Classes.Core.Common;
using System.Threading.Tasks;
using Android.Content;
using SyteLine.Classes.Activities.Common;
using SyteLine.Classes.Business.Admin;

namespace SyteLine.Classes.Activities.Admin
{
    [Activity(Label = "@string/app_name" , MainLauncher = true)]
    public class Login : CSIBaseActivity
    {
        private Configure configure;
        private bool ConfigurationListSynced = false;
        private DateTime? LastBackKeyDownTime;


        EditText User;
        EditText Password;
        Spinner Configuration;
        Switch SaveUser;
        Switch SavePassword;

        Button LoginButton;
        Button SettingsButton;
        TextView ErrorInformationLabel;

        protected override async void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            await SyncConfigurationListAsync();
        }

        protected override async void OnResume()
        {
            base.OnResume();
            configure = new Configure();
            await SyncConfigurationListAsync();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            defaultLayoutID = Resource.Layout.SystemLogin;

            base.OnCreate(savedInstanceState);
            configure = new Configure();

            User = FindViewById<EditText>(Resource.Id.UserEdit);
            Password = FindViewById<EditText>(Resource.Id.PasswordEdit);
            Configuration = FindViewById<Spinner>(Resource.Id.ConfigurationEdit);
            SaveUser = FindViewById<Switch>(Resource.Id.SaveUserSwitch);
            SavePassword = FindViewById<Switch>(Resource.Id.SavePasswordSwitch);

            LoginButton = FindViewById<Button>(Resource.Id.LoginButton);
            SettingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            ErrorInformationLabel = FindViewById<TextView>(Resource.Id.ErrorInformationLabel);

            SaveUser.Checked = configure.SaveUser;
            SavePassword.Checked = configure.SavePassword;
            User.Text = configure.User;
            Password.Text = configure.Password;

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.Add(configure.Configuration);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            adapter.Clear();
            Configuration.Adapter = adapter;
            Configuration.SetSelection(0);

            LoginButton.Click += LoginButtonClicked;
            LoginButton.RequestFocus();

            SettingsButton.Click += (sender, e) =>
            {
                StartActivity(typeof(Settings));
            };
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            configure.SaveUser = SaveUser.Checked;
            configure.SavePassword = SavePassword.Checked;
            configure.User = User.Text;
            configure.Password = Password.Text;
            configure.Configuration = Configuration.SelectedItem.ToString();
            if (true)
            {
                try
                {
                    string SessionToken = configure.GetToken();
                    this.Intent.PutExtra("SessionToken", SessionToken);

                    if (!SessionToken.Equals(string.Empty))
                    {
                        CallIDOUsers(SessionToken);
                        CallIDOEmployees(SessionToken);
                        CallIDOInvParms(SessionToken);

                        Intent intent = new Intent(this, typeof(Main));
                        SetDefaultIntent(intent);
                        StartActivity(intent);
                        configure.SaveConfigure();
                        Finish();
                    }
                }
                catch (Exception Ex)
                {
                    ConfigurationListSynced = false;
                    Toast.MakeText(this, "LoginButtonClicked() -> " + Ex.Message, ToastLength.Short).Show();
                }
            }
        }

        private void CallIDOUsers(string SessionToken)
        {
            try
            {
                IDOUsers users = new IDOUsers(SessionToken);
                users.BuilderFileByUserName(configure.User);
                users.Read();
                this.Intent.PutExtra("UserName", users.GetPropertyValue("Username"));
                this.Intent.PutExtra("UserDesc", users.GetPropertyValue("UserDesc"));
                this.Intent.PutExtra("UserLocalWhse", users.GetPropertyValue("UserLocalWhse"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CallIDOEmployees(string SessionToken)
        {
            try
            {
                IDOEmployees users = new IDOEmployees(SessionToken);
                users.BuilderFileByUserName(configure.User);
                users.Read();
                this.Intent.PutExtra("EmpNum", users.GetPropertyValue("EmpNum"));
                this.Intent.PutExtra("Nickname", users.GetPropertyValue("Nickname"));
                this.Intent.PutExtra("Name", users.GetPropertyValue("Name"));
            }
            catch
            {
                ;
            }
        }

        private void CallIDOInvParms(string SessionToken)
        {
            try
            {
                IDOInvParms invParms = new IDOInvParms(SessionToken);
                invParms.Read();
                this.Intent.PutExtra("DefaultLoc", invParms.GetPropertyValue("DefaultLoc"));
                this.Intent.PutExtra("DefaultWhse", invParms.GetPropertyValue("DefWhse"));
            }
            catch
            {
                ;
            }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Login_Menus, menu);
            return true;
            //return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            //    ToastLength.Short).Show();
            if (item.ItemId == Resource.Id.Menu_Settings)
            {
                StartActivity(typeof(Settings));
            }
            else if (item.ItemId == Resource.Id.Menu_Refresh)
            {                
                SyncConfigurationList();
            }
            else if (item.ItemId == Resource.Id.Menu_OffLine)
            {
                Toast.MakeText(this, Resource.String.OffLine, ToastLength.Short).Show();
            }
            return true;
            //return base.OnOptionsItemSelected(item);
        }

        private void SyncConfigurationList()
        {
            if (configure.CSIWebServer.Equals(string.Empty))
            {
                LoginButton.Enabled = false;
                ErrorInformationLabel.Visibility = ViewStates.Visible;
                ConfigurationListSynced = false;
                return;
            }
            try
            {
                if (!ConfigurationListSynced)
                {
                    ErrorInformationLabel.Visibility = ViewStates.Invisible;
                    //string[] List = ValidateConfig();
                    string[] List = ValidateConfig();
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
                    ConfigurationListSynced = true;
                }
                LoginButton.Enabled = true;
            }
            catch (Exception Ex)
            {
                LoginButton.Enabled = false;
                ErrorInformationLabel.Visibility = ViewStates.Visible;
                ConfigurationListSynced = false;
                Toast.MakeText(this, "SyncConfigurationList() -> " + Ex.Message, ToastLength.Short).Show();
            }
        }

        private async Task SyncConfigurationListAsync()
        {
            if (configure.CSIWebServer.Equals(string.Empty))
            {
                LoginButton.Enabled = false;
                ErrorInformationLabel.Visibility = ViewStates.Visible;
                ConfigurationListSynced = false;
                return;
            }
            try
            {
                if (!ConfigurationListSynced)
                {
                    ErrorInformationLabel.Visibility = ViewStates.Invisible;
                    //string[] List = ValidateConfig();
                    string[] List = await ValidateConfigasync();
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
                    ConfigurationListSynced = true;
                }
                LoginButton.Enabled = true;
            }
            catch (Exception Ex)
            {
                LoginButton.Enabled = false;
                ErrorInformationLabel.Visibility = ViewStates.Visible;
                Toast.MakeText(this, "SyncConfigurationListAsync() -> " + Ex.Message, ToastLength.Short).Show();
            }
        }

        private async Task<string[]> ValidateConfigasync ()
        {
            try
            {
                return await Task.Run(()=> configure.GetConfigurationList());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string[] ValidateConfig()
        {
            try
            {                
                return configure.GetConfigurationList();
            }
            catch (Exception Ex)
            {                
                throw Ex;
            }
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

    }
}