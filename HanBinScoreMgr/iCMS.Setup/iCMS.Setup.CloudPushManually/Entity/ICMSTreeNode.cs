using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCMS.Setup.CloudPushManually.Entity
{
    public class ICMSTreeNode : TreeNode
    {
        /// <summary>
        ///  数据类型
        /// </summary>
        public EnumCloudOperationType DataType { get; set; }

        /// <summary>
        /// 数据ID
        /// </summary>
        public int TableID { get; set; }

        /// <summary>
        /// 排序号，数字越小，首先进行推送
        /// </summary>
        public EnumSortOrder Order { get; set; }

        public ICMSTreeNode(string name)
            : base(name)
        {
        }

    }
}
