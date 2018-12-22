using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using iCMS.Common.Component.Data.Request;
using iCMS.Presentation.Server;
using iCMS.Service.Web.DiagnosticAnalysis;
using iCMS.Service.Web.Utility;
using iCMS.Common.Component.Data.Base;
using iCMS.Presentation.Common;
using iCMS.Common.Component.Tool;
using System.ServiceModel;

namespace iCMS.Presentation.Server
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class OtherService : BaseService, IOtherService
    {
        public string DoWork()
        {
            return "Test Well";
        }
    }
}