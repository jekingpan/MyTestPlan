using Android.App;
using Android.OS;
using Android.Widget;
using static Android.Widget.AdapterView;
using Android.Views;
using Android.Graphics;
using SyteLine.Classes.Adapters.Common;
using SyteLine.Classes.Core.Common;

namespace SyteLine.Classes.Activities.Common
{
    [Activity(Label = "@string/app_name")]
    public class CSIBaseProcessActivity : CSIBaseActivity
    {
        protected ListView ListView;
        protected string QueryString = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (defaultLayoutID != 0){

                defaultLayoutID = Resource.Layout.CommonListViewer;
            }

            base.OnCreate(savedInstanceState);
        }

        protected virtual void ListViewClicked(object sender, ItemClickEventArgs args)
        {

        }

        protected override void RegisterAdapter(bool Append = false)
        {
            base.RegisterAdapter(Append);
        }

        protected override void InitialList()
        {
            ListView = FindViewById<ListView>(Resource.Id.ListView);

            ListView.ItemClick += ListViewClicked;
            base.InitialList();
        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
        }

        protected override string UpdatePropertyDisplayedValue(BaseBusinessObject obj, int objIndex, string name, int row)
        {
            return base.UpdatePropertyDisplayedValue(obj, objIndex, name, row);
        }

        protected override void UpdateAdapterLists(int index = 0)
        {
            base.UpdateAdapterLists(index);
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
                        Value = obj.Value,
                        ValueType = obj.ValueType,
                        DisplayedValue = obj.DisplayedValue,
                        Key = obj.Key,
                        ActivityType = obj.ActivityType
                    };
                    colonList.Add(colonItem);
                }
                AdapterLists.Add(colonList);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
    }
}