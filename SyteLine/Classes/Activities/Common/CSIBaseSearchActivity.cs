using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using static Android.Widget.AdapterView;
using Android.Views;
using SyteLine.Classes.Adapters.Common;

namespace SyteLine.Classes.Activities.Common
{
    [Activity(Label = "@string/app_name")]
    public class CSIBaseSearchActivity : CSIBaseActivity
    {
        protected ListView ListView;
        protected string QueryString = "";
        protected string LastKey = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            defaultLayoutID = Resource.Layout.CommonSearchViewer;

            base.OnCreate(savedInstanceState);

            ListView.ItemClick += ListViewClicked;
        }

        protected virtual void ListViewClicked(object sender, ItemClickEventArgs args)
        {

        }

        protected override void RegisterAdapter(bool Append = false)
        {
            base.RegisterAdapter(Append);
        }

        protected override void InitializeActivity()
        {
            LastKey = "";

            ListView = FindViewById<ListView>(Resource.Id.ListView);

            base.InitializeActivity();

            if (AdapterLists.Count != 0)
            {
                LastKey = AdapterLists[AdapterLists.Count - 1].GetString(AdapterLists[AdapterLists.Count - 1].KeyName);
            }
        }

        protected async void GetMoreList()
        {
            LoadingTextView.Visibility = ViewStates.Visible;

            try
            {
                //await Task.Run(() => ReadIDOs());
                await ReadIDOs();
                PostReadIDOs();

                RegisterAdapter(true);
                LoadingTextView.Visibility = ViewStates.Gone;
                LastKey = AdapterLists[AdapterLists.Count - 1].GetString(AdapterLists[AdapterLists.Count - 1].KeyName);
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "GetMoreList() -> " + Ex.Message, ToastLength.Short).Show();
                LoadingTextView.Visibility = ViewStates.Visible;
            }
        }

        protected override void BeforeReadIDOs()
        {
            base.BeforeReadIDOs();
        }

        protected override void PostReadIDOs(int index = 0)
        {
            base.PostReadIDOs(index);

            //foreach (BaseBusinessObject o in  
            for (int i = 0; i < BusinessObjects[index].GetRowCount(); i++) //go through IDO rows
            {
                foreach (AdapterList ListTemp in AdapterListTemplate)
                {
                    if (ListTemp.ObjIndex != index)
                    {
                        continue;
                    }
                    AdapterList colonList = new AdapterList
                    {
                        KeyName = ListTemp.KeyName
                    };
                    foreach (string key in ListTemp.ObjectList.Keys) //go through adapter items
                    {
                        AdapterListItem obj = ListTemp.ObjectList[key];
                        AdapterListItem colonItem = new AdapterListItem
                        {
                            Name = obj.Name,
                            Label = obj.Label,
                            LayoutID = obj.LayoutID,
                            ValueType = obj.ValueType,
                            DisplayedValue = obj.DisplayedValue,
                            Key = obj.Key,
                            ActivityType = obj.ActivityType
                        };
                        if (!string.IsNullOrEmpty(obj.Name))
                        {
                            switch (obj.ValueType)
                            {
                                case AdapterListItem.ValueTypes.String:
                                    colonItem.Value = BusinessObjects[index].GetPropertyValue(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Int:
                                    colonItem.Value = BusinessObjects[index].GetPropertyInt(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Decimal:
                                    colonItem.Value = string.Format("{0:###,###,###,###,##0.00######}", BusinessObjects[index].GetPropertyDecimalValue(obj.Name, i));
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Date:
                                    colonItem.Value = BusinessObjects[index].GetPropertyValue(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.DateTime:
                                    colonItem.Value = BusinessObjects[index].GetPropertyValue(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Bitmap:
                                    colonItem.Value = BusinessObjects[index].GetPropertyBitmap(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Boolean:
                                    colonItem.Value = BusinessObjects[index].GetPropertyBoolean(obj.Name, i);
                                    colonItem.DisplayedValue = GetPropertyDisplayedValue(BusinessObjects[index], index, obj.Name, i);
                                    break;
                                default:
                                    colonItem.Value = null;
                                    break;
                            }
                            if (colonList.KeyName == obj.Name)
                            {
                                LastKey = (string)colonItem.Value;
                            }
                        }
                        colonList.ObjectList.Add(key, colonItem);
                    }
                    AdapterLists.Add(colonList);
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SearchView_Menus, menu);
            SearchView Menu_Search = (SearchView)menu.FindItem(Resource.Id.Menu_Search).ActionView;

            Menu_Search.QueryTextSubmit += (s,args) =>
            {
                args.Handled = false;
                QueryString = string.Format("%{0}%", args.Query);
                InitializeActivity();
                args.Handled = true;
            };

            Menu_Search.QueryTextChange += (s, args) =>
            {
                QueryString = string.Format("%{0}%", args.NewText);
            };

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.FindItem(Resource.Id.Menu_GetMoreRows).SetEnabled(HasMoreRow);

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.Menu_Refresh)
            {
                InitializeActivity();
            }
            else if (item.ItemId == Resource.Id.Menu_GetMoreRows)
            {
                GetMoreList();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}