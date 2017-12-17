using System;
using System.IO;
using Android.Util;
using Java.IO;
using System.Net;
using System.Json;
using SyteLine.Classes.Core.CSIWebServices;
using System.Collections.Generic;

namespace SyteLine.Classes.Core.Common
{
    public class Configure
    {
        //this is the class for get default configuration for the application.
        public string CSIWebServer { get; set; }
        public string Configuration { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool EnableHTTPS { get; set; }
        public bool UseRESTForRequest { get; set; }
        public bool SaveUser { get; set; }
        public bool LoadPicture { get; set; }
        public string RecordCap { get; set; }
        public bool SavePassword { get; set; }

        private string url;

        private static string filePath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    "configuration.dat");

        public Configure()
        {
            CSIWebServer = "";
            Configuration = "";
            User = "";
            Password = "";
            EnableHTTPS = false;
            UseRESTForRequest = false;
            SaveUser = false;
            SavePassword = false;
            LoadPicture = false;
            RecordCap = "20";
            ReadConfigure();
        }

        public string UpdateUrl()
        {
            url = "";
            if (EnableHTTPS)
            {
                url += "https://" + CSIWebServer;
            }
            else
            {
                url += "http://" + CSIWebServer;
            }
            return url;
        }

        private void ReadConfigure()
        {
            if (!System.IO.File.Exists(filePath))
            {
                SaveConfigure();
            }
            ReadJsonStream(System.IO.File.Open(filePath, FileMode.Open));
        }

        public void SaveConfigure()
        {
            System.IO.File.Delete(filePath);
            WriteJsonStream(System.IO.File.Open(filePath,FileMode.CreateNew));
        }

        private void WriteJsonStream(FileStream output)
        {
            JsonWriter jWriter = new JsonWriter(new OutputStreamWriter(output));
            jWriter.BeginObject();
            jWriter.Name("CSIWebServerName").Value(CSIWebServer);
            jWriter.Name("Configuration").Value(Configuration);
            jWriter.Name("EnableHTTPS").Value(EnableHTTPS); 
            jWriter.Name("UseRESTForRequest").Value(UseRESTForRequest);
            jWriter.Name("SaveUser").Value(SaveUser);
            jWriter.Name("SavePassword").Value(SavePassword);
            jWriter.Name("User").Value(SaveUser ? User : "");
            jWriter.Name("Password").Value(SaveUser && SavePassword ? Password : "");
            jWriter.Name("LoadPicture").Value(LoadPicture);
            jWriter.Name("RecordCap").Value(RecordCap);
            jWriter.EndObject();
            jWriter.Close();
            output.Close();
        }

        private void ReadJsonStream(FileStream input)
        {
            JsonReader jReader = new JsonReader(new InputStreamReader(input));
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("CSIWebServerName"))
                {
                    CSIWebServer = jReader.NextString();
                }
                else if (name.Equals("Configuration"))
                {
                    Configuration = jReader.NextString();
                }
                else if (name.Equals("User"))
                {
                    User = jReader.NextString();
                }
                else if (name.Equals("Password"))
                {
                    Password = jReader.NextString();
                }
                else if (name.Equals("EnableHTTPS"))
                {
                    EnableHTTPS = jReader.NextBoolean();
                }
                else if (name.Equals("UseRESTForRequest"))
                {
                    UseRESTForRequest = jReader.NextBoolean();
                }
                else if (name.Equals("SaveUser"))
                {
                    SaveUser = jReader.NextBoolean();
                }
                else if (name.Equals("SavePassword"))
                {
                    SavePassword = jReader.NextBoolean();
                }
                else if (name.Equals("LoadPicture"))
                {
                    LoadPicture = jReader.NextBoolean();
                }
                else if (name.Equals("RecordCap"))
                {
                    RecordCap = jReader.NextString();
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            
            jReader.EndObject();
            jReader.Close();
            input.Close();
        }
        
        public string[] GetConfigurationList()
        {
            UpdateUrl();
            try
            {
                if (UseRESTForRequest)
                {
                    return GetConfigListByREST();
                }
                else
                {
                    return GetConfigListBySOAP();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string[] GetConfigListBySOAP()
        {
            SOAPParameters parm = new SOAPParameters();
            try
            {
                parm.Url = url;
                parm.Command = GetObjectsBySOAPREST.GetConfigurationNames;
                GetObjectsBySOAPREST.CallSOAP(ref parm);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return parm.OutPutStrings;
        }

        private string[] GetConfigListByREST()
        {
            SOAPParameters parm = new SOAPParameters();
            try
            {
                parm.Url = url;
                parm.Command = GetObjectsBySOAPREST.GetConfigurationNames;
                GetObjectsBySOAPREST.CallREST(ref parm);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return parm.OutPutStrings;
        }

        public string GetToken()
        {
            UpdateUrl();
            try
            {
                if (UseRESTForRequest)
                {
                    return GetTokenByREST();
                }
                else
                {
                    return GetTokenBySOAP();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private string GetTokenBySOAP()
        {
            SOAPParameters parm = new SOAPParameters();
            try
            {
                parm.Url = url;
                parm.Command = GetObjectsBySOAPREST.CreateSessionToken;
                parm.User = User;
                parm.Password = Password;
                parm.Configuration = Configuration;
                GetObjectsBySOAPREST.CallSOAP(ref parm);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return parm.OutPutString;
        }

        private string GetTokenByREST()
        {
            SOAPParameters parm = new SOAPParameters();
            try
            {
                parm.Url = url;
                parm.Command = GetObjectsBySOAPREST.CreateSessionToken;
                parm.User = User;
                parm.Password = Password;
                parm.Configuration = Configuration;
                GetObjectsBySOAPREST.CallREST(ref parm);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return parm.OutPutString;
        }
    }


}