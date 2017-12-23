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

namespace SyteLine.Classes.Core.Common
{
    public class BaseIDOResult
    {
        public string IDOName = "";
        public List<string> ObjectNames = new List<string>();
        public List<BaseIDOObject> Objects = new List<BaseIDOObject>();
        protected string ResultType = "SOAP";//SOAP or REST

        public BaseIDOResult(string ResultType)
        {
            this.ResultType = ResultType;
        }

        public void DeleteRow(int Row)
        {
            Objects[Row].Deleted = true;
        }

        public int InsertRow()
        {
            BaseIDOObject obj = new BaseIDOObject()
            {
                Inserted = true
            };
            Objects.Add(obj);
            return Objects.Count - 1;
        }

        public string GetPropertyValue(string Name, int Row)
        {
            string value = "";
            if (ResultType == "SOAP")
            {
                int colIndex = ObjectNames.IndexOf(Name);
                if (colIndex >= 0)
                {
                    if (Row >= 0 && Row < Objects.Count())
                    {
                        value = Objects[Row].ObjectItems[colIndex].ItemValue;
                    }
                }
            }
            else if (ResultType == "REST")
            {
                value = Objects[Row].GetItemValue(Name);
            }
            return value;
        }

        public void SetPropertyValue(string Name, int Row, string Value)
        {
            if (ResultType == "SOAP")
            {
                int colIndex = ObjectNames.IndexOf(Name);
                if (colIndex >= 0)
                {
                    if (Row >= 0 && Row < Objects.Count())
                    {
                        Objects[Row].ObjectItems[colIndex].ItemValue = Value;
                        Objects[Row].Updated = true;
                        Objects[Row].ObjectItems[colIndex].Updated = true;
                    }
                }
            }
            else if (ResultType == "REST")
            {
                Objects[Row].SetItemValue(Name, Value);
                Objects[Row].Updated = true;
            }
        }
    }

    public class BaseIDOObject
    {
        public int EditStatus;
        public string ID;
        public bool Updated { get; set; }
        public bool Inserted { get; set; }
        public bool Deleted { get; set; }
        public List<BaseIDOObjectItem> ObjectItems = new List<BaseIDOObjectItem>();

        public string GetItemValue(string Name)
        {
            foreach (BaseIDOObjectItem it in ObjectItems)
            {
                if (it.ItemName == Name)
                {
                    return it.ItemValue;
                }
            }
            return "";
        }

        public void SetItemValue(string Name, string Value)
        {
            foreach (BaseIDOObjectItem it in ObjectItems)
            {
                if (it.ItemName == Name)
                {
                    it.ItemValue = Value;
                    it.Updated = true;
                }
            }

        }
    }

    public class BaseIDOObjectItem
    {
        public string ItemName { get; set; }
        public string ItemValue { get; set; }
        public bool Updated { get; set; }
    }
}