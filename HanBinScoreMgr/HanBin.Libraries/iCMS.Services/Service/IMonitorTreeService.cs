using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IMonitorTreeService
    {
        string GetMonitorTreeDatas(MonitorTreeDatasParameter parameter);
    }
}
