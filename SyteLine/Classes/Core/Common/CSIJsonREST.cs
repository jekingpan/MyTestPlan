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
using Java.IO;

namespace SyteLine.Classes.Core.Common
{
    class CSIJsonREST : CSIJson
    {
        public CSIJsonREST(BaseIDOResult result = null) : base(result, "REST")
        {
            ;
        }

        public override BaseIDOResult PasreJson(string JsonString)
        {
            try
            {
                ReadRESTJsonHead(new JsonReader(new Java.IO.StringReader(JsonString)));
            }
            catch (Exception Ex)
            {
                iResult = null;
                //return iResult;
                throw Ex;
            }
            return iResult;
        }

        private void ReadRESTJsonHead(JsonReader jReader)
        {
            jReader.BeginObject();
            try
            {
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Items"))
                    {
                        ReadRESTItemsArray(jReader);
                    }
                    else if (name.Equals("Message"))
                    {
                        string msg = jReader.NextString();
                        if (!msg.Equals("Success"))
                        {
                            throw new Exception(msg);
                        }
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            jReader.Close();
        }

        private void ReadRESTItemsArray(JsonReader jReader)
        {
            try
            {
                jReader.BeginArray();
                while (jReader.HasNext)
                {
                    ReadRESTPropertiesArray(jReader);
                }
                jReader.EndArray();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            //jReader.Close();
        }

        private void ReadRESTPropertiesArray(JsonReader jReader)
        {
            try
            {
                BaseIDOObject col = new BaseIDOObject();
                iResult.Objects.Add(col);
                jReader.BeginArray();
                while (jReader.HasNext)
                {
                    ReadRESTProperties(jReader);
                }
                jReader.EndArray();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void ReadRESTProperties(JsonReader jReader)
        {
            try
            {
                BaseIDOObjectItem item = new BaseIDOObjectItem();
                jReader.BeginObject();
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Name"))
                    {
                        item.ItemName = jReader.NextString();
                    }
                    else if (name.Equals("Value"))
                    {
                        JsonToken peek = jReader.Peek();
                        if (peek == JsonToken.Null)
                        {
                            jReader.SkipValue();
                        }
                        else
                        {
                            item.ItemValue = jReader.NextString();
                        }
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
                iResult.Objects.Last().ObjectItems.Add(item);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public override string BuildJson(int indicator = 0)
        {
            /* {
                "Action":1/2/4,
                "ItemId":"PBT=[Usernames]", "ItemId": "PBT=[MCBCustomers] mcb.ID=[70c0b27f-8151-4f3a-8fcf-a8a8381823b4] mcb.DT=[2015-01-07 13:13:04.660]",
                "Properties":
                [
                {"IsNull":false,"Modified":true/false,"Name":"Username","Value":"newuser54"},
                {"IsNull":false,"Modified":true/false,"Name":"EditLevel","Value":"2"},
                {"IsNull":false,"Modified":true/false,"Name":"UserDesc","Value":"user description"},
                {"IsNull":false,"Modified":true/false,"Name":"SuperUserFlag","Value":"1"}
                ]
                }
            */
            string output = "";
            StringWriter writer = new StringWriter();
            JsonWriter jsonWriter = new JsonWriter(writer);

            if (string.IsNullOrEmpty(iResult.IDOName))
            {
                return "";
            }

            jsonWriter.BeginObject();
            jsonWriter.Name("Action").Value(indicator);
            switch (indicator)
            {
                case 1:
                    jsonWriter.Name("ItemId").Value(string.Format("PBT=[{0}]", iResult.IDOName));
                    break;
                case 2:
                case 4:
                    string.Format("PBT=[{0}] {0}.DT=[{1}] {0}.ID=[{2}]", iResult.IDOName, "", "");
                    break;
                default:
                    break;
            }
            jsonWriter.Name("Properties");
            jsonWriter.BeginArray();
            foreach (BaseIDOObject obj in iResult.Objects)
            {
                if (((indicator == 1) && (!obj.Inserted)) || ((indicator == 2) && (obj.Inserted || obj.Deleted)) || ((indicator == 4) && (!obj.Deleted)))
                {
                    continue;
                }
                foreach (BaseIDOObjectItem objitem in obj.ObjectItems)
                    {
                        jsonWriter.BeginObject();
                        jsonWriter.Name("IsNull").Value((string.IsNullOrEmpty(objitem.ItemValue) ? true : false));
                        jsonWriter.Name("Modified").Value(objitem.Updated);
                        jsonWriter.Name("Name").Value(objitem.ItemName);
                        jsonWriter.Name("Value").Value(objitem.ItemValue);
                        jsonWriter.EndObject();
                    }
            }
            jsonWriter.EndObject();
            jsonWriter.EndArray();
            jsonWriter.EndObject();
            jsonWriter.Flush();
            jsonWriter.Close();
            output = writer.ToString();
            return output;
        }
    }
}