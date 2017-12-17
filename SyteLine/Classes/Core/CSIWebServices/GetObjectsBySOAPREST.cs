using System;
using System.Net;
using System.IO;
using Android.Util;
using Java.IO;

namespace SyteLine.Classes.Core.CSIWebServices
{
    class GetObjectsBySOAPREST
    {
        public static string GetConfigurationNames = "GetConfigurationNames";
        public static string CreateSessionToken = "CreateSessionToken";
        public static string LoadDataSet = "LoadDataSet";
        public static string SaveDataSet = "SaveDataSet";
        public static string CallMethod = "CallMethod";
        public static string LoadJson = "LoadJson";
        public static string SaveJson = "SaveJson";
        
        internal static void CallSOAP(ref SOAPParameters parm)
        {
            string BaseURL = "/IDORequestService/IDOWebService.asmx";
            try
            {
                Core.CSIWebServices.IDOWebService ido = new Core.CSIWebServices.IDOWebService(parm.Url + BaseURL);
                //webservice调用完成后触发
                ido.Timeout = 20000;
                if (parm.Command == GetConfigurationNames)
                {
                    parm.OutPutStrings = ido.GetConfigurationNames();
                }
                if (parm.Command == CreateSessionToken)
                {
                    parm.OutPutString = ido.CreateSessionToken(parm.User, parm.Password, parm.Configuration);
                }
                if (parm.Command == LoadDataSet)
                {
                    parm.OutPutDataSet = ido.LoadDataSet(parm.Token, parm.IDOName, parm.PropertyList, parm.Filter, parm.OrderBy, parm.PostQueryMethod, parm.RecordCap);
                }
                if (parm.Command == CallMethod)
                {
                    parm.OutPutObject = ido.CallMethod(parm.Token, parm.IDOName, parm.MethodName, ref parm.MethodParameters);
                }
                if (parm.Command == LoadJson)
                {
                    parm.OutPutJsonString = ido.LoadJson(parm.Token, parm.IDOName, parm.PropertyList, parm.Filter, parm.OrderBy, parm.PostQueryMethod, parm.RecordCap);
                }
                if (parm.Command == SaveDataSet)
                {
                    parm.OutPutDataSet = ido.SaveDataSet(parm.Token, parm.UpdateDataSet, parm.RefreshAfterSave, parm.CustomInsert, parm.CustomUpdate, parm.CustomDelete);
                }
                if (parm.Command == SaveJson)
                {
                    parm.OutPutJsonString = ido.SaveJson(parm.Token, parm.UpdateJsonObject, parm.CustomInsert, parm.CustomUpdate, parm.CustomDelete);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        internal static void CallREST(ref SOAPParameters parm)
        {
            if (!(parm.Filter is null))
            {
                parm.Filter = parm.Filter.Replace("%", "%25");//replace % for URL path
            }
            string BaseURL = "/IDORequestService/MGRestService.svc";
            try
            {
                Core.CSIWebServices.IDORESTService ido = new Core.CSIWebServices.IDORESTService(parm.Url + BaseURL);
                //webservice调用完成后触发
                ido.Timeout = 20000;
                if (parm.Command == GetConfigurationNames)
                {
                    parm.OutPutStrings = ido.GetConfigurationNames();
                }
                if (parm.Command == CreateSessionToken)
                {
                    parm.OutPutString = ido.CreateSessionToken(parm.User, parm.Password, parm.Configuration);
                }
                if (parm.Command == LoadDataSet)
                {
                    parm.OutPutJsonString = ido.LoadJson(parm.Token, parm.IDOName, parm.PropertyList, parm.Filter, parm.OrderBy, parm.PostQueryMethod, parm.RecordCap);
                }
                if (parm.Command == CallMethod)
                {
                    parm.OutPutJsonString = ido.CallMethod(parm.Token, parm.IDOName, parm.MethodName, ref parm.MethodParameters);
                }
                if (parm.Command == LoadJson)
                {
                    parm.OutPutJsonString = ido.LoadJson(parm.Token, parm.IDOName, parm.PropertyList, parm.Filter, parm.OrderBy, parm.PostQueryMethod, parm.RecordCap);
                }
                if (parm.Command == SaveDataSet)
                {
                    //parm.OutPutDataSet = ido.SaveDataSet(parm.Token, parm.UpdateDataSet, parm.RefreshAfterSave, parm.CustomInsert, parm.CustomUpdate, parm.CustomDelete);
                }
                if (parm.Command == SaveJson)
                {
                    //parm.OutPutJsonString = ido.SaveJson(parm.Token, parm.UpdateJsonObject, parm.CustomInsert, parm.CustomUpdate, parm.CustomDelete);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}