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
using static SyteLine.Classes.Adapters.Common.AdapterListItem;

namespace SyteLine.Classes.Adapters.Common
{
    public class AdapterList
    {
        public string KeyName;
        public int ObjIndex = 0;
        public int ObjRow = 0;
        //private Dictionary<string, AdapterListItem> objectList = new Dictionary<string, AdapterListItem>();

        public Dictionary<string, AdapterListItem> ObjectList { get; private set; }

        public AdapterList()
        {
            ObjectList = new Dictionary<string, AdapterListItem>();
        }

        public AdapterList(string name, ValueTypes vType = ValueTypes.String)
        {
            ObjectList = new Dictionary<string, AdapterListItem>();
            Add(name, name, vType);
        }

        public AdapterList(string name, object value, ValueTypes vType = ValueTypes.String)
        {
            ObjectList = new Dictionary<string, AdapterListItem>();
            Add(name, value, vType);
        }

        public AdapterList(string name, object value, string label, ValueTypes vType = ValueTypes.String, int layoutID = Resource.Layout.CommonLabelTextViewer, Type activity = null)
        {
            ObjectList = new Dictionary<string, AdapterListItem>();
            Add(name, value, label, vType, layoutID, activity);
        }

        public AdapterList(string key, string name, object value, string label, ValueTypes vType = ValueTypes.String, int layoutID = Resource.Layout.CommonLabelTextViewer, Type activity = null)
        {
            ObjectList = new Dictionary<string, AdapterListItem>();
            Add(key, name, value, label, vType, layoutID, activity);
        }

        public void Add(string name, ValueTypes vType = ValueTypes.String, Type activity = null)
        {
            Add(name, "", name, vType, activity);
        }

        public void Add(string name, object value, ValueTypes vType = ValueTypes.String, Type activity = null)
        {
            Add(name, value, name, vType, activity);
        }

        public void Add(string name, object value, string lable, ValueTypes vType, Type activity)
        {
            Add(name, name, value, lable, vType, activity);
        }

        public void Add(string key, string name, object value, string lable, ValueTypes vType = ValueTypes.String, Type activity = null)
        {
            Add(key, name, value, lable, vType, Resource.Layout.CommonLabelTextViewer, activity);
        }

        public void Add(string name, object value, string lable, ValueTypes vType = ValueTypes.String, int layoutID = Resource.Layout.CommonLabelTextViewer, Type activity = null)
        {
            Add(name, name, value, lable, vType, layoutID, activity);
        }

        public void Add(string key, string name, object value, string label, ValueTypes vType = ValueTypes.String, int layoutID = Resource.Layout.CommonLabelTextViewer, Type activity = null)
        {
            ObjectList.Add(name,new AdapterListItem()
            {
                Key = key,
                Name = name,
                Value = value,
                ValueType = vType,
                Label = label,
                LayoutID = layoutID,
                ActivityType = activity,
            });
        }

        public void Add(AdapterListItem listItem)
        {
            ObjectList.Add(listItem.Key, listItem);
        }

        public string GetLabel(string name)
        {
            try
            {
                return ObjectList.GetValueOrDefault(name).Label.Replace("{0}", GetString(name));
            }
            catch
            {
                return "";
            }
        }

        public string GetDisplayedValue(string name)
        {
            try
            {
                return string.IsNullOrEmpty(ObjectList.GetValueOrDefault(name).DisplayedValue) ? (string)GetValue(name) : ObjectList.GetValueOrDefault(name).DisplayedValue;
            }
            catch
            {
                return null;
            }
        }

        public object GetValue(string name)
        {
            try
            {
                return ObjectList.GetValueOrDefault(name).Value;
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
                return (bool)GetValue(name);
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
                return (Decimal)GetValue(name);
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
                return (int)GetValue(name);
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
                return (Bitmap)GetValue(name);
            }
            catch
            {
                return null;
            }
        }

        public string GetString(string name)
        {
            string value = "";
            try
            {
                //value = string.IsNullOrEmpty(GetDisplayedValue(name)) ? (string)GetValue(name) : GetDisplayedValue(name);
                value = (string)GetValue(name);
                return value;
            }
            catch
            {
                return "";
            }
        }

        public void SetString(string name, string value)
        {
            ObjectList[name].Value = value;
        }

        public string GetName(string key)
        {
            try
            {
                return (string)ObjectList.GetValueOrDefault(key).Name;
            }
            catch
            {
                return "";
            }
        }

        public int GetLayoutID(string name)
        {
            try
            {
                return ObjectList.GetValueOrDefault(name).LayoutID;
            }
            catch
            {
                return 0;
            }
        }

        public Type GetActivityType(string name)
        {
            try
            {
                return ObjectList.GetValueOrDefault(name).ActivityType;
            }
            catch
            {
                return null;
            }
        }

        public string GetKey(int index)
        {
            try
            {
                return (string)ObjectList.Keys.ToArray()[index];
            }
            catch
            {
                return "";
            }
        }

        public string GetFirstKey()
        {
            try
            {
                return ObjectList.Keys.First();
            }
            catch
            {
                return "";
            }
        }

        public ValueTypes GetValueType(string name)
        {
            return ObjectList.GetValueOrDefault(name).ValueType;
        }
    }

    public class AdapterListItem
    {
        public string Key { set; get; }
        public string Name { set; get; }
        public string Label { set; get; }
        public object Value { set; get; }
        public string DisplayedValue { set; get; }
        public int LayoutID { set; get; }
        public bool Modified { set; get; }
        public Type ActivityType { set; get; }
        public ValueTypes ValueType { set; get; }

        public enum ValueTypes { String, Int, Decimal, Date, DateTime, Boolean, Bitmap};
    }
}