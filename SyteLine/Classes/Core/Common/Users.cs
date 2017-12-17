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
using SyteLine.Classes.Core.CSIWebServices;
using Android.Util;
using Java.IO;
using System.Collections;

namespace SyteLine.Classes.Core.Common
{
    class Users
    {
        public string[] UserId { get; set; }
        public string[] UserName { get; set; }
        public string[] UserDescription { get; set; }
        private Configure configure;
        private string Token;
        private SOAPParameters parm = new SOAPParameters();
        private List<string> Property;
        private List<List<string>> Rows;

        public Users(string TokenSession)
        {
            Token = TokenSession;
            configure = new Configure();
        }

        public void ReadCurrent()
        {
            try
            {
                if (configure.UseRESTForRequest)
                {
                    CallByREST();
                }
                else
                {
                    CallBySOAP("UserNames", "Username,UserDesc", string.Format("Username = N'{0}'", configure.User));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Read()
        {
            try
            {
                if (configure.UseRESTForRequest)
                {
                    CallByREST();
                }
                else
                {
                    CallBySOAP("UserNames", "Username,UserDesc", "");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        
        private void PasreJson()
        {
            Property = new List<string>();
            Rows = new List<List<string>>();
            ReadJsonHead(new JsonReader(new StringReader(parm.OutPutJsonString)));
        }

        private void ReadJsonHead(JsonReader jReader)
        {
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("Items"))
                {
                    ReadItemsArray(jReader);
                }
                else if (name.Equals("PropertyList"))
                {
                    jReader.BeginArray();
                    while (jReader.HasNext)
                    {
                        Property.Add(jReader.NextString());
                    }
                    jReader.EndArray();
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            jReader.EndObject();
            jReader.Close();
        }

        private void ReadItemsArray(JsonReader jReader)
        {
            jReader.BeginArray();
            while (jReader.HasNext)
            {
                ReadItem(jReader);
            }
            jReader.EndArray();
        }

        private void ReadItem(JsonReader jReader)
        {
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("Properties"))
                {
                    ReadPropertiesArray(jReader);
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            jReader.EndObject();
        }

        private void ReadPropertiesArray(JsonReader jReader)
        {
            jReader.BeginArray();
            List<string> col = new List<string>();
            while (jReader.HasNext)
            {
                jReader.BeginObject();
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Property"))
                    {
                        col.Add(jReader.NextString());
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
            }
            jReader.EndArray();
            Rows.Add(col);
        }        

        private void CallBySOAP(string IDO, string Property, string Filter)
        {
            try
            {
                parm.Url = configure.updateUrl() + "/IDORequestService/IDOWebService.asmx";
                parm.Command = "LoadJson";
                parm.Token = Token;
                parm.Filter = Filter;
                parm.IDOName = IDO;
                parm.PropertyList = Property;
                GetObjectsBySOAPREST.CallSOAP(ref parm);
                PasreJson();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CallByREST()
        {
        }
    }
}