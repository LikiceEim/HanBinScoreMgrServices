using iCMS.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.Statistics
{
    /// 设置实时数据当前配置请求参数
    /// </summary>
    public class SetConfigDevStateParameter : BaseRequest
    {
        /// <summary>
        /// 是否显示停机设备
        /// </summary>
        public bool IsShowStopDev { get; set; }

        public List<ConfigTreeForSet> ConfigTreeForSet { get; set; }

        public SetConfigDevStateParameter()
        {
            ConfigTreeForSet = new List<ConfigTreeForSet>();
        }
    }

    /// <summary>
    /// 实时数据配置
    /// </summary>
    public class ConfigTreeForSet
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        public int ConfigID { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public int TableID { get; set; }
        /// <summary>
        /// 类型1：振动信号 2：特征值 3：测点
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

}
