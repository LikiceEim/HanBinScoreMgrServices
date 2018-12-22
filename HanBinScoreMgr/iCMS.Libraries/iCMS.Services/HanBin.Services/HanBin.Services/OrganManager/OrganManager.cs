using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.OrganManager
{
    public class OrganManager : IOrganManager
    {
        [Dependency]
        public IRepository<Organization> organRepository { get; set; }

        #region 添加单位
        public BaseResponse<bool> AddOrganizationRecord(AddOrganParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                Organization organ = new Organization();
                organ.OrganCode = param.OrganCode;
                organ.OrganFullName = param.OrganFullName;
                organ.OrganShortName = param.OrganShortName;
                organ.OrganTypeID = param.OrganTypeID;
                organ.AddUserID = param.AddUserID;
                organ.LastUpdateDate = DateTime.Now;
                organ.LastUpdateUserID = param.AddUserID;

                var operResult = organRepository.AddNew<Organization>(organ);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("单位添加发生异常！");
                }
                response.IsSuccessful = true;
                response.Result = true;
                return response;

            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                response.Reason = e.Message;

                return response;
            }
        }
        #endregion
    }
}
