using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBin.OfficerManager
{
    /// <summary>
    /// 获取干部基础信息返回结果类
    /// </summary>
    public class GetOfficerDetailInfoResult
    {
        /// <summary>
        /// 干部ID
        /// </summary>
        public int OfficerID { get; set; }
        /// <summary>
        /// 干部姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别 1：男；2：女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentifyNumber { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public int PositionID { get; set; }
        /// <summary>
        /// 级别ID
        /// </summary>
        public int LevelID { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime OnOfficeDate { get; set; }
        /// <summary>
        /// 分管工作
        /// </summary>
        public string Duty { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public int AddUserID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate { get; set; }
    }
}
