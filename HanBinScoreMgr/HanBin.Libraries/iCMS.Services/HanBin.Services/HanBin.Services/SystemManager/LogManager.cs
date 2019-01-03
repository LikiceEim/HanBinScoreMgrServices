using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using HanBin.Common.Component.Tool;
using HanBin.Core.DB.Models;
using HanBin.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.SystemManager
{
    public class LogManager : ILogManager
    {
        [Dependency]
        public IRepository<SysLog> syslogRepsoitory { get; set; }

        public LogManager()
        {
            this.syslogRepsoitory = new Repository<SysLog>();
        }

        public BaseResponse<QueryLogResult> QueryLog(QueryLogParameter param)
        {
            BaseResponse<QueryLogResult> response = new BaseResponse<QueryLogResult>();
            QueryLogResult result = new QueryLogResult();
            try
            {
                var logQuerable = syslogRepsoitory.GetDatas<SysLog>(t => true, true);
                if (param.RoleID.HasValue && param.RoleID.Value > 0)
                {
                    logQuerable = logQuerable.Where(t => t.RoleID == param.RoleID.Value);
                }
                if (param.BeginTime.HasValue && param.BeginTime.Value != DateTime.MinValue)
                {
                    logQuerable = logQuerable.Where(t => t.OperationDate >= param.BeginTime.Value);
                }
                if (param.EndTime.HasValue && param.EndTime.Value != DateTime.MinValue)
                {
                    logQuerable = logQuerable.Where(t => t.OperationDate <= param.EndTime.Value);
                }
                if (!string.IsNullOrEmpty(param.Keyword))
                {
                    logQuerable = logQuerable.Where(t => (!string.IsNullOrEmpty(t.UserToken) && t.UserToken.ToUpper().Contains(param.Keyword.ToUpper())) || (!string.IsNullOrEmpty(t.OrganName) && t.OrganName.ToUpper().Contains(param.Keyword.ToUpper())));
                }

                int total = logQuerable.Count();
                if (param.Page > 0)
                {
                    logQuerable = logQuerable
                        .Skip((param.Page - 1) * param.PageSize)
                        .Take(param.PageSize);
                }

                logQuerable.ToList().ForEach(l =>
                {
                    LogInfo logInfo = new LogInfo();
                    logInfo.ID = l.ID;
                    logInfo.OperationUserID = l.OperationUserID;
                    logInfo.UserToken = l.UserToken;
                    logInfo.RoleID = l.RoleID;
                    logInfo.RoleName = l.RoleName;
                    logInfo.Content = l.Content;
                    logInfo.OrganID = l.OrganID;
                    logInfo.OrganName = l.OrganName;
                    logInfo.IP = l.IP;
                    logInfo.HTTPType = l.HTTPType;
                    logInfo.OperationDate = l.OperationDate;

                    result.LogList.Add(logInfo);
                });

                result.Total = total;
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = e.Message;

                return response;
            }
        }
    }
}
