using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.SystemManager
{
    public class UserDevManagedResult
    {
        //设备ID集合
        public List<UserRelated> DeviceTree { get; set; }

        /// <summary>
        /// 无线监测树集合
        /// </summary>
        public List<UserRelated> WireLessWSTree { get; set; }

        /// <summary>
        /// 有线监测树集合
        /// </summary>
        public List<UserRelated> WiredWSTree { get; set; }

    }

    #region 监测树状态信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class UserRelated
    {
        /// <summary>
        /// 监测树当前记录ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录的父节点ID
        /// </summary>
        public int PID
        {
            get;
            set;
        }

        /// <summary>
        /// 检测树记录名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录的类型
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库ID，即数据库中的ID。如：类型为设备，则该字段表示设备ID。
        /// </summary>
        public int RecordID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户是否有管理权限 false：否 true：是
        /// </summary>
        public bool isRelated
        {
            get;
            set;
        }

    }
    #endregion
}
