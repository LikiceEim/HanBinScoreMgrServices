using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Frameworks.Core.DB;
using iCMS.Frameworks.Core.DB.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    #region 监测树
    /// <summary>
    /// 监测树
    /// </summary>
    public class MonitorTreeService : IMonitorTreeService
    {
        #region 变量
        private readonly IRepository<MonitorTree> _monitorTreeRepository;
        #endregion

        #region 构造函数
        public MonitorTreeService(IRepository<MonitorTree> monitorTreeRepository)
        {
            _monitorTreeRepository = monitorTreeRepository;
        }
        #endregion

        #region 实现方法
        public string GetMonitorTreeDatas(MonitorTreeDatasParameter parameter)
        {
            int pid = parameter.Pid; 
            var mts = _monitorTreeRepository.GetDatas<MonitorTree>(t => t.PID == pid, false);
            using (iCMSDbContext dbContext = new iCMSDbContext())
            {
                var linq = from mt in mts.ToList()
                           join mtType in dbContext.MonitorTreeType on mt.Type equals mtType.ID into mtGroup
                           from mtg in mtGroup
                           from mtProperty in dbContext.MonitorTreeProperty.ToList()
                           where mt.MonitorTreePropertyID == mtProperty.MonitorTreePropertyID
                           select new 
                           {
                               MonitorTreeID = mt.MonitorTreeID,
                               PID = mt.PID,
                               OID = mt.OID,
                               IsDefault = mt.IsDefault,
                               Name = mt.Name,
                               Des = mt.Des,
                               Type = mt.Type,
                               TypeName = mtg.Name,
                               ImageID = mt.ImageID ?? -1,
                               Status = mt.Status,
                               AddDate = mt.AddDate.ToString(),

                               //其他属性

                               Address = mtProperty.Address,
                               URL = mtProperty.URL,
                               TelphoneNO = mtProperty.TelphoneNO,
                               FaxNO = mtProperty.FaxNO,
                               Latitude = mtProperty.Latitude.HasValue ? mtProperty.Latitude.Value.ToString() : "",
                               Longtitude = mtProperty.Longtitude.HasValue ? mtProperty.Longtitude.Value.ToString() : "",
                               Length = mtProperty.Length.HasValue ? mtProperty.Length.Value.ToString() : "",
                               Width = mtProperty.Width.HasValue ? mtProperty.Width.Value.ToString() : "",
                               Area = mtProperty.Area.HasValue ? mtProperty.Area.Value.ToString() : "",
                               PersonInCharge = mtProperty.PersonInCharge,
                               PersonInChargeTel = mtProperty.PersonInChargeTel,
                               Remark = mtProperty.Remark,
                               ChildCount = -1
                           };
            }


            var monitor = _monitorTreeRepository.GetByKey(pid);
            return "";
        }
        #endregion
    }
    #endregion
}
