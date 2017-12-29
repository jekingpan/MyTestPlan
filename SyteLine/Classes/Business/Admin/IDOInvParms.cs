using SyteLine.Classes.Core.Common;
using SyteLine.Classes.Core.CSIWebServices;

namespace SyteLine.Classes.Business.Admin
{
    class IDOInvParms : BaseBusinessObject
    {
        public IDOInvParms(SOAPParameters parm) : base(parm)
        {
            this.parm = parm;
            DefaultParm();
        }

        public IDOInvParms(string Token) : base(Token)
        {
            DefaultParm();
        }

        protected override void DefaultParm()
        {
            base.DefaultParm();
            parm.IDOName = "SLInvparms";
            parm.PropertyList = "DefLoc,DefWhse";
            parm.Filter = string.Format("parm_key = N'0'");
        }
        

    }
}