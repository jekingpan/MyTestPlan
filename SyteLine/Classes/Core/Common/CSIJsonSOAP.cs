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
    class CSIJsonSOAP : CSIJson
    {
        public CSIJsonSOAP(BaseIDOResult result = null) : base(result, "SOAP")
        {
            ;
        }
        public override BaseIDOResult PasreJson(string JsonString)
        {
            try
            {
                ReadSOAPJsonHead(new JsonReader(new Java.IO.StringReader(JsonString)));
            }
            catch (Exception Ex)
            {
                iResult = null;
                //return iResult;
                throw Ex;
            }
            return iResult;
        }

        private void ReadSOAPJsonHead(JsonReader jReader)
        {
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("IDOName"))
                {
                    iResult.IDOName = jReader.NextString();
                }
                else if (name.Equals("Items"))
                {
                    ReadSOAPItemsArray(jReader);
                }
                else if (name.Equals("PropertyList"))
                {
                    jReader.BeginArray();
                    while (jReader.HasNext)
                    {
                        iResult.ObjectNames.Add(jReader.NextString());
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

        private void ReadSOAPItemsArray(JsonReader jReader)
        {
            jReader.BeginArray();
            while (jReader.HasNext)
            {
                ReadSOAPItem(jReader);
            }
            jReader.EndArray();
        }

        private void ReadSOAPItem(JsonReader jReader)
        {
            BaseIDOObject col = new BaseIDOObject();
            jReader.BeginObject();
            while (jReader.HasNext)
            {
                string name = jReader.NextName();
                if (name.Equals("EditStatus"))
                {
                    col.EditStatus = jReader.NextInt();
                }
                else if (name.Equals("ID"))
                {
                    col.ID = jReader.NextString();
                }
                else if (name.Equals("Properties"))
                {
                    ReadSOAPPropertiesArray(jReader, ref col);
                }
                else
                {
                    jReader.SkipValue();
                }
            }
            jReader.EndObject();
            iResult.Objects.Add(col);
        }

        private void ReadSOAPPropertiesArray(JsonReader jReader, ref BaseIDOObject col)
        {
            jReader.BeginArray();

            while (jReader.HasNext)
            {
                BaseIDOObjectItem propertyValues = new BaseIDOObjectItem();
                jReader.BeginObject();
                while (jReader.HasNext)
                {
                    string name = jReader.NextName();
                    if (name.Equals("Property"))
                    {
                        JsonToken peek = jReader.Peek();
                        if (peek == JsonToken.Null)
                        {
                            jReader.SkipValue();
                        }
                        else
                        {
                            propertyValues.ItemValue = jReader.NextString();
                        }
                    }
                    else if (name.Equals("Updated"))
                    {
                        propertyValues.Updated = jReader.NextBoolean();
                    }
                    else
                    {
                        jReader.SkipValue();
                    }
                }
                jReader.EndObject();
                col.ObjectItems.Add(propertyValues);
            }
            jReader.EndArray();
        }

        public override string BuildJson(int indicator = 0)
        {
            /* {
	            "IDOName": "UserNames",
	            "Items": [{
			            "EditStatus": 0,
			            "ID": "PBT=[UserNames] UserNames.DT=[20171223 20:19:13.103] UserNames.ID=[4c0a3eb1-5492-4785-bb2b-db0522a92a6e]",
			            "Properties": [{
					            "Property": "2",
					            "Updated": false
				            }, {
					            "Property": "sa",
					            "Updated": false
				            }, {
					            "Property": "",
					            "Updated": false
				            }
			            ]
		            }
	            ],
	            "PropertyList": ["UserId", "Username", "UserDesc"]
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
            jsonWriter.Name("IDOName").Value(iResult.IDOName);
            jsonWriter.Name("Items");
            jsonWriter.BeginArray();
            foreach (BaseIDOObject obj in iResult.Objects)
            {
                jsonWriter.BeginObject();
                if (obj.Inserted)
                {
                    jsonWriter.Name("EditStatus").Value(1);
                }
                else if (obj.Updated)
                {
                    jsonWriter.Name("EditStatus").Value(2);
                }
                else if (obj.Deleted)
                {
                    jsonWriter.Name("EditStatus").Value(4);
                }
                else
                {
                    jsonWriter.Name("EditStatus").Value(0);
                }
                jsonWriter.Name("ID").Value(string.Format("PBT=[{0}] {0}.DT=[{1}] {0}.ID=[{2}]", iResult.IDOName, "",""));
                jsonWriter.Name("Properties");
                jsonWriter.BeginArray();
                foreach (BaseIDOObjectItem objitem in obj.ObjectItems)
                {
                    jsonWriter.BeginObject();
                    jsonWriter.Name("Property").Value(objitem.ItemValue);
                    jsonWriter.Name("Updated").Value(objitem.Updated);
                    jsonWriter.EndObject();
                }
                jsonWriter.EndArray();
                jsonWriter.EndObject();
            }
            jsonWriter.EndArray();
            jsonWriter.Name("PropertyList");
            jsonWriter.BeginArray();
            foreach (string propertyName in iResult.ObjectNames)
            {
                jsonWriter.Value(propertyName);
            }
            jsonWriter.EndArray();
            jsonWriter.EndObject();
            jsonWriter.Flush();
            jsonWriter.Close();
            output = writer.ToString();
            return output;
        }
    }
}