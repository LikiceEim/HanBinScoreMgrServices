using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanBin.Services.SystemManager;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.SystemManage;
using iCMS.Common.Component.Data.Response.HanBin.SystemManager;

namespace HanBin.Presentation.Service.SystemManage
{
   public class HanBinSystemManageService : IHanBinSystemManageService
    {
        private IUserManager userManager;
        public HanBinSystemManageService(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public BaseResponse<LoginResult> Login(LoginParameter parameter)
        {
            return userManager.Login(parameter);
        }


        public BaseResponse<bool> AddUser(AddUserParameter parameter)
        {
            return userManager.AddUser(parameter);
        }

        public BaseResponse<bool> EditUser(EditUserParameter parameter)
        {
            return userManager.EditUser(parameter);
        }

        public BaseResponse<GetUserInfoResult> GetUserInfo(GetUserInfoParameter parameter)
        {
            return userManager.GetUserInfo(parameter);
        }
    }
}
