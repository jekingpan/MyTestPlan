using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using static Android.Widget.AdapterView;
using Android.Views;

namespace SyteLine.Classes.Activities.Common
{
    [Activity(Label = "@string/app_name")]
    public class BaseSearchActivity : BaseActivity
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

        protected override void InitialList()
        {
            LastKey = "";

            ListView = FindViewById<ListView>(Resource.Id.ListView);

            base.InitialList();
        }

        protected async void GetMoreList()
        {
            LoadingTextView.Visibility = ViewStates.Visible;

            try
            {
                await Task.Run(() => ReadIDOs());
                RegisterAdapter(true);
                LoadingTextView.Visibility = ViewStates.Gone;
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "GetMoreList() -> " + Ex.Message, ToastLength.Short).Show();
                LoadingTextView.Visibility = ViewStates.Visible;
            }
        }

        protected override void PrepareIDOs()
        {
            base.PrepareIDOs();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SearchView_Menus, menu);
            SearchView Menu_Search = (SearchView)menu.FindItem(Resource.Id.Menu_Search).ActionView;

            Menu_Search.QueryTextSubmit += (s,args) =>
            {
                QueryString = string.Format("%{0}%", args.Query);
                InitialList();
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
                InitialList();
            }
            else if (item.ItemId == Resource.Id.Menu_GetMoreRows)
            {
                GetMoreList();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}