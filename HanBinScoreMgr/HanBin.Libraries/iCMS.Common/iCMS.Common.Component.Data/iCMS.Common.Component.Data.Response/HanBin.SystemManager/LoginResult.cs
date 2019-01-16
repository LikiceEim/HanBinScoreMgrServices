using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.SystemManager
{
    /// <summary>
    /// 登陆返回结果类
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户Token
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// JWT Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public int OrganID { get; set; }

        public int OrganTypeID { get; set; }

        public int OrganCategoryID { get; set; }
    }
}
