using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 获取实时数据配置信息返回结果类
    /// </summary>
    public class GetConfigTreeDevStateResult
    {
        public List<ConfigTree> ConfigTree { get; set; }

        /// <summary>
        /// 是否显示启停机
        /// </summary>
        public bool IsShowStopDev { get; set; }

        public GetConfigTreeDevStateResult()
        {
            this.ConfigTree = new List<ConfigTree>();
        }
    }

    /// <summary>
    /// 实时数据配置
    /// </summary>
    public class ConfigTree
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
