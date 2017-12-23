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
    public class CSIJson
    {
        protected BaseIDOResult iResult;

        public CSIJson(BaseIDOResult result = null, string ResultType = "SOAP")
        {
            if (result is null)
            {
                iResult = new BaseIDOResult(ResultType);
            }
            else
            {
                iResult = result;
            }
        }

        public virtual BaseIDOResult PasreJson(string JsonString)
        {
            return iResult;
        }

        public virtual string BuildJson(int indicator = 0)
        {
            //0: all
            //1: insert
            //2: update
            //4: delete
            string output = "";
            return output;
        }

        //public virtual string BuildJsonForNew()
        //{
        //    string output = "";
        //    return output;
        //}

        //public virtual string BuildJsonForUpdate()
        //{
        //    string output = "";
        //    return output;
        //}

        //public virtual string BuildJsonForDelete()
        //{
        //    string output = "";
        //    return output;
        //}
    }
}