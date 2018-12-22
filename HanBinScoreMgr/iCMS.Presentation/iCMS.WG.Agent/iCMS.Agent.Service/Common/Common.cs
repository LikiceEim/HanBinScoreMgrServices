using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iCMS.WG.AgentServer
{
    public static class Common
    {
        public static iCMS.WG.Agent.SyncTools   syncTools;

        public static iCMS.WG.Agent.AsyncTools asyncTools;

       

        public static void Init()
        {
            if (syncTools==null)
            syncTools = new  Agent.SyncTools();
            if (asyncTools == null)
                asyncTools = new   Agent.AsyncTools();
        }
    }
}