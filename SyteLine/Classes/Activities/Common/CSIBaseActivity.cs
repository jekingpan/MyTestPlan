using Android.App;
using Android.OS;
using Android.Widget;
using SyteLine.Classes.Core.Common;
using System;
using Android.Views;
using System.Threading.Tasks;
using System.Collections.Generic;
using SyteLine.Classes.Adapters.Common;
using static SyteLine.Classes.Adapters.Common.AdapterListItem;
using Android.Content;
using ZXing.Mobile;
using ZXing;

namespace SyteLine.Classes.Activities.Common
{
    [Activity(Label = "@string/app_name")]
    public class CSIBaseActivity : Activity
    {
        protected int defaultLayoutID = 0;
        protected TextView LoadingTextView;
        protected BaseBusinessObject PrimaryBusinessObject;
        protected bool HasMoreRow = false;
        protected List<BaseBusinessObject> BusinessObjects = new List<BaseBusinessObject>();
        protected List<AdapterList> AdapterLists = new List<AdapterList>();
        protected List<AdapterList> AdapterListTemplate = new List<AdapterList>();
        protected bool StartRefresh = true;

        protected MobileBarcodeScanner Scanner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            MobileBarcodeScanner.Initialize(Application);

            SetTheme(Android.Resource.Style.ThemeHoloLight);

            base.OnCreate(savedInstanceState);
            // Set our view from the Inentory layout resource
            if (defaultLayoutID != 0)
            {
                SetContentView(defaultLayoutID);
                LoadingTextView = FindViewById<TextView>(Resource.Id.LoadingTextView);
            }
            
            try
            {
                if (!(PrimaryBusinessObject == null))
                {
                    if (BusinessObjects.Count == 0)
                    {
                        BusinessObjects.Add(PrimaryBusinessObject);
                    }
                    if (!(AdapterLists == null))
                    {
                        InitializeActivity();
                    }
                }
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "OnCreate() -> " + Ex.Message, ToastLength.Short).Show();
            }

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }

        public virtual void SetAdapterLists(int BaseObjectIndex, AdapterList adapterList)
        {
            string propertyList = BusinessObjects[BaseObjectIndex].parm.PropertyList;
            foreach (AdapterListItem obj in adapterList.ObjectList.Values)
            {
                if (!string.IsNullOrEmpty(obj.Name))
                {
                    if (string.IsNullOrEmpty(propertyList))
                    {
                        propertyList = "" + obj.Name;
                    }
                    else
                    {
                        propertyList += "," + obj.Name;
                    }
                }
            }
            BusinessObjects[BaseObjectIndex].parm.PropertyList = propertyList;
            adapterList.ObjIndex = BaseObjectIndex;
            AdapterListTemplate.Add(adapterList);
        }

        public virtual void SetAdapterLists(int BaseObjectIndex, string key, string property, ValueTypes type, string lable, int defaultLaoutID = Resource.Layout.CommonLabelTextViewer, Type defaultActivity = null)
        {
            string propertyList = BusinessObjects[BaseObjectIndex].parm.PropertyList;
            if (string.IsNullOrEmpty(propertyList))
            {
                propertyList = "" + property;
            }
            else
            {
                propertyList += "," + property;
            }
            if (!string.IsNullOrEmpty(property))
            {
                BusinessObjects[BaseObjectIndex].parm.PropertyList = propertyList;
            }
            AdapterListTemplate.Add(new AdapterList(key, property, "", lable, type, defaultLaoutID, defaultActivity)
            {
                ObjIndex = BaseObjectIndex
            });
        }

        public virtual List<AdapterList> GetAdapterLists()
        {
            return AdapterLists;
        }

        protected virtual void AddBusinessObjects(BaseBusinessObject o)
        {
            if(BusinessObjects.Count == 0)
            {
                BusinessObjects.Add(PrimaryBusinessObject);
            }
            BusinessObjects.Add(o);
        }

        protected virtual BaseBusinessObject GetSecondObject(int index)
        {
            if (index < BusinessObjects.Count)
            {

                return BusinessObjects[index];
            }else
            {
                return null;
            }
        }

        protected virtual void RegisterAdapter(bool Append = false)
        {
            //if (!Append)
            //{
                AdapterListTemplate.Clear();
            //}
            if (!(LoadingTextView is null))
            {
                LoadingTextView.Visibility = ViewStates.Gone;
            }
        }
        
        protected virtual async void InitializeActivity()
        {
            AdapterListTemplate.Clear();
            AdapterLists.Clear();
            if (!(LoadingTextView is null))
            {
                LoadingTextView.Visibility = ViewStates.Visible;
            }

            try
            {
                for (int i = 0; i < BusinessObjects.Count; i++)
                {
                    //await Task.Run(async () =>
                    //{
                    //    await ReadIDOs(i);
                    //    UpdateAdapterLists(i);
                    //}
                    //);
                    await ReadIDOs(i);
                    PostReadIDOs(i);
                }

                StartRefresh = true;

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

        protected virtual void BeforeReadIDOs()
        {
        }

        protected virtual void BeforeReadIDOs(int index)
        {
            if (index == 0)
            {
                BeforeReadIDOs();
            }
        }

        protected virtual async Task ReadIDOs(int index = 0)
        {
            try
            {
                BeforeReadIDOs(index);

                if (StartRefresh)
                {
                    await Task.Run(() => BusinessObjects[index].Read());
                }

                if (BusinessObjects[index].GetRowCount() < int.Parse(new Configure().RecordCap))
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

        protected virtual string GetPropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            //called by UpdateAdapterLists;
            return "";
        }

        protected virtual void PostReadIDOs(int index = 0)
        {
            ;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main_Menus, menu);
            IMenuItem item = menu.FindItem(Resource.Id.Menu_CurrentConfiguration);
            item.SetTitle(GetString(Resource.String.About));           
            //item.SetEnabled(false);
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
            else if (item.ItemId == Resource.Id.Menu_CurrentConfiguration)
            {
                AlertDialog alertDialog = null;
                AlertDialog.Builder builder = null;
                alertDialog = null;
                builder = new AlertDialog.Builder(this);
                alertDialog = builder
                .SetTitle(GetString(Resource.String.About))
                .SetMessage((string.Format(GetString(Resource.String.AboutSystem), UserName() + " " + UserDesc(),
                              EmpNum() + " " + EmpName(), new Configure().Configuration, "0,1(a)", "Lisenced", "N/A")))
                .Create();
                alertDialog.Show();
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        protected void SetDefaultIntent(Intent intent)
        {
            intent.PutExtra("SessionToken", Intent.GetStringExtra("SessionToken"));
            intent.PutExtra("UserName", Intent.GetStringExtra("UserName"));
            intent.PutExtra("UserDesc", Intent.GetStringExtra("UserDesc"));
            intent.PutExtra("DefaultWhse", Intent.GetStringExtra("DefaultWhse"));
            intent.PutExtra("EmpNum", Intent.GetStringExtra("EmpNum"));
            intent.PutExtra("EmpName", Intent.GetStringExtra("EmpName"));
        }

        public string SessionToken()
        {
            return this.Intent.GetStringExtra("SessionToken");
        }

        public string UserName()
        {
            return this.Intent.GetStringExtra("UserName");
        }

        public string UserDesc()
        {
            return this.Intent.GetStringExtra("UserDesc");
        }

        public string DefaultLoc()
        {
            return this.Intent.GetStringExtra("DefaultLoc");
        }

        public string DefaultWhse()
        {
            return this.Intent.GetStringExtra("DefaultWhse");
        }

        public string UserLocalWhse()
        {
            return this.Intent.GetStringExtra("UserLocalWhse");
        }

        public string EmpNum()
        {
            return this.Intent.GetStringExtra("EmpNum");
        }

        public string EmpName()
        {
            return this.Intent.GetStringExtra("EmpName");
        }

        public string CurrentDateTime()
        {
            return string.Format("{0} {1}", DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.ToShortTimeString());
        }

        public async Task<ZXing.Result> ScanCode()
        {
            ZXing.Result result = null;
            MobileBarcodeScanningOptions opts = new MobileBarcodeScanningOptions
            {
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                    {
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.CODE_93
                    }
            };
            try
            {
                Scanner = new MobileBarcodeScanner();
                result = await Scanner.Scan(opts);
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, Ex.Message, ToastLength.Short).Show();
            }
            return result;
        }
    }
}
