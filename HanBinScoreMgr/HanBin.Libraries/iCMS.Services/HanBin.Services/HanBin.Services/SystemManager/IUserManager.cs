using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Data.Request.HanBin.SystemManage;
using HanBin.Common.Component.Data.Response.HanBin.SystemManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.SystemManager
{
    public interface IUserManager
    {
        BaseResponse<LoginResult> Login(LoginParameter parameter);

        BaseResponse<bool> AddUser(AddUserParameter parameter);

        BaseResponse<bool> EditUser(EditUserParameter parameter);

        BaseResponse<GetUserInfoResult> GetUserInfo(GetUserInfoParameter parameter);

        BaseResponse<GetRoleInfoListResult> GetRoleInfoList();

        BaseResponse<bool> ChangeUseStatus(ChangeUseStatusParameter parameter);

        BaseResponse<bool> DeleteUser(DeleteUserParameter parameter);

        BaseResponse<bool> ResetPWD(ResetPWDParameter parameter);

        BaseResponse<bool> BackupDB();

        BaseResponse<GetBackupLogResult> GetBackupLogList(GetBackupLogParameter parameter);

        BaseResponse<bool> DeleteBackup(DeleteBackupParameter parameter);
    }
}
