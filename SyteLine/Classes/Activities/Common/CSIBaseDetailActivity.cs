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
    public class CSIBaseDetailActivity : CSIBaseActivity
    {
        protected ListView ListView;
        protected string QueryString = "";
        protected bool EnableGetMoreRow = true;

        protected TextView KeyEdit;
        protected TextView KeyDescriptionEdit;
        protected TextView SubKeyEdit;
        protected TextView SubKeyDescriptionEdit;
        protected ImageView ImageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            defaultLayoutID = Resource.Layout.CommonHeadDetailsViewer;

            base.OnCreate(savedInstanceState);
        }

        public void SetKey(string value)
        {
            KeyEdit.SetText(value, null);
            KeyEdit.Visibility = ViewStates.Visible;
        }


        public void SetKeyDescription(string value)
        {
            KeyDescriptionEdit.SetText(value, null);
            KeyDescriptionEdit.Visibility = ViewStates.Visible;
        }


        public void SetSubKey(string value)
        {
            SubKeyEdit.SetText(value, null);
            SubKeyEdit.Visibility = ViewStates.Visible;
        }

        public void SetSubKeyDescription(string value)
        {
            SubKeyDescriptionEdit.SetText(value, null);
            SubKeyDescriptionEdit.Visibility = ViewStates.Visible;
        }

        public void SetImageView(Bitmap pic)
        {
            ImageView.SetImageBitmap(pic);
            ImageView.Visibility = ViewStates.Visible;
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

            KeyEdit = FindViewById<TextView>(Resource.Id.KeyEdit);
            KeyDescriptionEdit = FindViewById<TextView>(Resource.Id.KeyDescriptionEdit);
            SubKeyEdit = FindViewById<TextView>(Resource.Id.SubKeyEdit);
            SubKeyDescriptionEdit = FindViewById<TextView>(Resource.Id.SubKeyDescriptionEdit);
            ImageView = FindViewById<ImageView>(Resource.Id.ImageView);
            KeyEdit.Visibility = ViewStates.Gone;
            KeyDescriptionEdit.Visibility = ViewStates.Gone;
            SubKeyEdit.Visibility = ViewStates.Gone;
            SubKeyDescriptionEdit.Visibility = ViewStates.Gone;
            ImageView.Visibility = ViewStates.Gone;

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
            //foreach (BaseBusinessObject o in  
            for (int i = 0; i < SencondObjects[index].GetRowCount(); i++) //go through IDO rows
            {
                for (int j = 0; j < AdapterLists.Count; j++) //go through defind adapter/properties
                {
                    foreach (AdapterListItem obj in AdapterLists[j].ObjectList.Values) //go through adapter items
                    {
                        if (!string.IsNullOrEmpty(obj.Name) && (!obj.Modified))
                        {
                            switch (obj.ValueType)
                            {
                                case AdapterListItem.ValueTypes.String:
                                    obj.Value = SencondObjects[index].GetPropertyValue(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Int:
                                    obj.Value = SencondObjects[index].GetPropertyInt(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Decimal:
                                    obj.Value = string.Format("{0:###,###,###,###,##0.00######}", SencondObjects[index].GetPropertyDecimalValue(obj.Name, i));
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Date:
                                    obj.Value = SencondObjects[index].GetPropertyValue(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.DateTime:
                                    obj.Value = SencondObjects[index].GetPropertyValue(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Bitmap:
                                    obj.Value = SencondObjects[index].GetPropertyBitmap(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                case AdapterListItem.ValueTypes.Boolean:
                                    obj.Value = SencondObjects[index].GetPropertyBoolean(obj.Name, i);
                                    obj.DisplayedValue = UpdatePropertyDisplayedValue(SencondObjects[index], index, obj.Name, i);
                                    break;
                                default:
                                    obj.Value = null;
                                    break;
                            }

                            obj.Modified = true;
                        }
                    }
                }
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