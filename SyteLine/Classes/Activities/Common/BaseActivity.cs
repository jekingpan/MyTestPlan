using Android.App;
using Android.OS;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using System;
using Android.Views;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SyteLine.Classes.Activities.Common
{
    [Activity(Label = "@string/app_name")]
    public class BaseActivity : Activity
    {
        protected int defaultLayoutID = 0;
        protected TextView LoadingTextView;
        protected BaseBusinessObject BaseObject;
        protected bool HasMoreRow = false;
        protected List<BaseBusinessObject> SencondObjects = new List<BaseBusinessObject>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the Inentory layout resource
            if (defaultLayoutID != 0)
            {
                SetContentView(defaultLayoutID);
                LoadingTextView = FindViewById<TextView>(Resource.Id.LoadingTextView);
            }
            
            try
            {
                if (!(BaseObject is null))
                {
                    if (SencondObjects.Count == 0)
                    {
                        SencondObjects.Add(BaseObject);
                    }
                    InitialList();
                }
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "OnCreate() -> " + Ex.Message, ToastLength.Short).Show();
            }

        }

        protected virtual void AddSecondObject(BaseBusinessObject o)
        {
            if(SencondObjects.Count == 0)
            {
                SencondObjects.Add(BaseObject);
            }
            SencondObjects.Add(o);
        }

        protected virtual BaseBusinessObject GetSecondObject(int index)
        {
            if (index < SencondObjects.Count)
            {

                return SencondObjects[index];
            }else
            {
                return null;
            }
        }

        protected virtual void RegisterAdapter(bool Append = false)
        {
            if (!(LoadingTextView is null))
            {
                LoadingTextView.Visibility = ViewStates.Gone;
            }
            
        }
        
        protected virtual async void InitialList()
        {
            if (!(LoadingTextView is null))
            {
                LoadingTextView.Visibility = ViewStates.Visible;
            }

            try
            {
                for (int i = 0; i< SencondObjects.Count; i++)
                {
                    await ReadIDOs(i);
                }
                RegisterAdapter(false);
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "InitialList() -> " + Ex.Message, ToastLength.Short).Show();
                if (!(LoadingTextView is null))
                {
                    LoadingTextView.Visibility = ViewStates.Visible;
                }
            }

            if (!(LoadingTextView is null))
            {
                LoadingTextView.Visibility = ViewStates.Gone;
            }
        }

        protected virtual void PrepareIDOs()
        {
        }

        protected virtual void PrepareIDOs(int index = 0)
        {
            if (index == 0)
            {
                PrepareIDOs();
            }
        }

        protected virtual async Task ReadIDOs(int index = 0)
        {
            try
            {
                PrepareIDOs(index);
                await Task.Run(() => SencondObjects[index].Read());

                if (SencondObjects[index].GetRowCount() < int.Parse(new Configure().RecordCap))
                {
                    HasMoreRow = false;
                }
                else
                {
                    HasMoreRow = true;
                }
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "ReadIDOs(int index) -> " + Ex.Message, ToastLength.Short).Show();
                throw Ex;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main_Menus, menu);
            IMenuItem item = menu.FindItem(Resource.Id.Menu_CurrentConfiguration);
            item.SetTitle(new Configure().Configuration);
            item.SetEnabled(false);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            //    ToastLength.Short).Show();
            if (item.ItemId == Resource.Id.Menu_Settings)
            {
                StartActivity(typeof(Admin.Settings));
            }
            else if (item.ItemId == Resource.Id.Menu_SignOut)
            {
                StartActivity(typeof(Admin.Login));
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }
    }
}
