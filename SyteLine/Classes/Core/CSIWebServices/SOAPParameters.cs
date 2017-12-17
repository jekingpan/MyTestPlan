using System.Data;

namespace SyteLine.Classes.Core.CSIWebServices
{
    public class SOAPParameters
    {
        public string LoadCommand { get; set; }
        public string SaveCommand { get; set; }
        public string Command { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string IDOName { get; set; }
        public string MethodName { get; set; }
        public string MethodParameters;
        public string PropertyList { get; set; }
        public string Filter { get; set; }
        public string OrderBy { get; set; }
        public string PostQueryMethod { get; set; }
        public int RecordCap { get; set; }
        public string Configuration { get; set; }
        public string OutPutString { get; set; }
        public string OutPutJsonString { get; set; }
        public string[] OutPutStrings{ get; set; }
        public DataSet OutPutDataSet { get; set; }
        public DataSet UpdateDataSet { get; set; }
        public object OutPutObject { get; set; }
        public bool RefreshAfterSave { get; set; }
        public string CustomInsert { get; set; }
        public string CustomUpdate { get; set; }
        public string CustomDelete { get; set; }
        public string UpdateJsonObject { get; set; }
        public string Url { get; set; }
        
        public SOAPParameters()
        {

        }
    }
}