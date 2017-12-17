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
using Android.Util;
using System.Net;
using Java.IO;
using System.IO;
using System.Json;

namespace SyteLine.Classes.Core.CSIWebServices
{
    class IDORESTService
    {
        public int Timeout;
        public string ReqMethod = "Get";
        private string BasePath;
        private WebHeaderCollection ReqHeaders;

        public IDORESTService(string url)
        {
            BasePath = url;
        }

        public string[] GetConfigurationNames()
        {
            //json/configurations
            string path = BasePath + "/json/configurations";
            List<String> configureList = new List<String>();
            try
            {
                JsonReader reader = new JsonReader(new Java.IO.StringReader(ExecuteHTTPRequest(path).ToString()));
                reader.BeginArray();
                while (reader.HasNext)
                {
                    configureList.Add(reader.NextString());
                }
                reader.EndArray();
                reader.Close();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            return configureList.ToArray();
        }

        public string CreateSessionToken(string User, string Password, string Configuration)
        {
            //json/token/{config}
            string path = BasePath + string.Format("/json/token/{0}",Configuration);
            string output = "";
            ReqMethod = "Get";
            ReqHeaders = new WebHeaderCollection
            {
                { "UserId", User },
                { "Password", Password }
            };
            JsonReader reader = new JsonReader(new Java.IO.StringReader(ExecuteHTTPRequest(path).ToString()));
            reader.BeginObject();
            while (reader.HasNext)
            {
                string name = reader.NextName();
                if (name.Equals("Message"))
                {
                    string msg = reader.NextString();
                    if (!msg.ToLower().Equals("success"))
                    {
                        throw new Exception(msg);
                    }
                }
                else if (name.Equals("Token"))
                {
                    output = reader.NextString();
                }
                else
                {
                    reader.SkipValue();
                }
            }
            reader.EndObject();
            reader.Close();
            return output;
        }

        public string CallMethod(string Token, string IDOName, string MethodName, ref string MethodParameters)
        {
            //json/method/{ido}/{method}/?parms={parms
            return "";
        }


        public string LoadJson(string Token, string IDOName, string PropertyList, string Filter, string OrderBy, string PostQueryMethod, int RecordCap)
        {
            //json/{ido}/{props}/adv/?filter={filter}&orderby={orderby}
            //&rowcap={recordcap}&distinct={distinct}&customloadmethod={clm}
            //&customloadmethodparms={clmparms}&loadtype={loadtype}
            //&bookmark={bookmark}&postquerycommand={pqc}&readonly={ro}
            //string path = BasePath + string.Format("/json/{0}/{1}/" +
            //    "adv/?filter={2}&orderby={3}&rowcap={4}" +
            //    "&distinct={5}&customloadmethod={6}" +
            //    "&customloadmethodparms={7}&loadtype={8}bookmark={9}" +
            //    "&postquerycommand={10}&readonly={11}", IDOName, PropertyList, Filter, OrderBy, RecordCap, "","","","","","","");
            string path = BasePath + string.Format("/json/{0}/{1}/" +
                "adv/?filter={2}&orderby={3}&rowcap={4}", IDOName, PropertyList, Filter, OrderBy, RecordCap);
            ReqMethod = "Get";
            ReqHeaders = new WebHeaderCollection
            {
                { "Authorization", Token }
            };
            return ExecuteHTTPRequest(path).ToString();
        }

        public void AddItem()
        {
            //json/additem
        }

        public void AddItems()
        {
            //json/additems
        }

        public void UpdateItem()
        {
            //json/updateitem
        }

        public void UpdateItems()
        {
            //json/updateitems
        }

        public void DeleteItem()
        {
            //json/deleteitem
        }

        public void DeleteItems()
        {
            //json/deleteitems
        }
        
        private JsonValue ExecuteHTTPRequest(string path)
        {
            JsonValue output;
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(path));
            //request.ContentType = "application/json";
            request.Timeout = Timeout;
            request.Method = ReqMethod;
            if (!(ReqHeaders is null)){
                request.Headers = ReqHeaders;
            }
            try
            {
                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        //StreamReader reader = new StreamReader(stream);
                        //string abc = reader.ReadToEnd();
                        //output = JsonValue.Parse(abc);
                        output = JsonValue.Load(stream);
                        //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                        // Return the JSON document:
                        //output = new JsonReader(new InputStreamReader(stream));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return output;
        }
    }
}