using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;

namespace SyteLine.Classes.Business.Admin
{
    class IDOEmployees : BaseBusinessObject
    {
        public IDOEmployees(SOAPParameters parm) : base(parm)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOEmployees(string Token) : base(Token)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLEmployees";
            parm.PropertyList = "EmpNum,Name,Nickname";
        }

        public void BuilderFileByUserName(string UserName)
        {
            parm.Filter = string.Format("Username = N'{0}'", UserName);
        }

    }
}