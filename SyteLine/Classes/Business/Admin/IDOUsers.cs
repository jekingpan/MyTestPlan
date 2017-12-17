using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;

namespace SyteLine.Classes.Business.Admin
{
    class IDOUsers : BaseBusinessObject
    {
        public IDOUsers(SOAPParameters parm) : base(parm)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOUsers(string Token) : base(Token)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "UserNames";
            parm.PropertyList = "UserId,Username,UserDesc";
        }

        public void BuilderFileByUserName(string UserName)
        {
            parm.Filter = string.Format("Username = N'{0}'", UserName);
        }

    }
}