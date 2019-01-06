using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.OrganManage
{
    public class AddOrganParameter : BaseRequest
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public string OrganCode { get; set; }
        /// <summary>
        /// 单位全称
        /// </summary>
        public string OrganFullName { get; set; }
        /// <summary>
        /// 单位简称
        /// </summary>
        public string OrganShortName { get; set; }
        /// <summary>
        /// 单位类型
        /// </summary>
        public int OrganTypeID { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public int AddUserID { get; set; }

        /// <summary>
        /// 单位所在地区
        /// </summary>
        public int AreaID { get; set; }

        public int CurrentUserID { get; set; }
    }
}
