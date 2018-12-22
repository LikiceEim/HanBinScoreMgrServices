using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.SystemManage;
using iCMS.Common.Component.Data.Response.HanBin.SystemManager;
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

    }
}
