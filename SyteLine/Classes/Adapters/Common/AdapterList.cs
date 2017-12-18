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
using Android.Graphics;

namespace SyteLine.Classes.Adapters.Common
{
    public class AdapterList
    {
        private Dictionary<string, object> ObjectList = new Dictionary<string, object>();
        private Dictionary<string, string> LableList = new Dictionary<string, string>();

        public AdapterList(string Name)
        {
            Add("AdapterListID", Name); //Used for specific object - Label Value Pait List this is added for default key to identity the AdapterList.
        }

        public AdapterList(string Name, object Value, string Label)
        {
            Add("AdapterListID", Name); //Used for specific object - Label Value Pait List this is added for default key to identity the AdapterList.
            Add(Name, Value, Label); //Used for specific object - Label Value Pait List this is added for default key to identity the AdapterList.
        }

        public AdapterList()
        {
            Add("AdapterListID", ""); //User for Name Value Pair list.
        }

        public void Add(string name, object value)
        {
            Add(name, value, "");
        }

        public void Add(string name, object value, string label)
        {
            ObjectList.Add(name, value);
            LableList.Add(name, label);
        }

        public string GetLabel(string name)
        {
            try
            {
                return LableList.GetValueOrDefault(name);
            }
            catch
            {
                return "";
            }
        }

        public object GetObject(string name)
        {
            try
            {
                return ObjectList.GetValueOrDefault(name);
            }
            catch
            {
                return null;
            }
        }

        public bool GetBoolean(string name)
        {
            try
            {
                return (bool)GetObject(name);
            }
            catch
            {
                return false;
            }
        }

        public Decimal GetDecimal(string name)
        {
            try
            {
                return (Decimal)GetObject(name);
            }
            catch
            {
                return 0;
            }
        }

        public int GetInt(string name)
        {
            try
            {
                return (int)GetObject(name);
            }
            catch
            {
                return 0;
            }
        }

        public Bitmap GetBitmap(string name)
        {
            try
            {
                return (Bitmap)GetObject(name);
            }
            catch
            {
                return null;
            }
        }

        public string GetString(string name)
        {
            try
            {
                return (string)GetObject(name);
            }
            catch
            {
                return "";
            }
        }

        public string GetKeyName()
        {
            try
            {
                return (string)ObjectList.GetValueOrDefault("AdapterListID");
            }
            catch
            {
                return "";
            }
        }
    }
}