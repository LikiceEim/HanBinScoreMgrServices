using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iCMS.WG.Agent.Common;

namespace iCMS.WG.Agent.Model
{
    public class TaskModelBase
    {
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime addDate { set; get; }

        /// <summary>
        ///是否需要Agent响应
        /// </summary>
        public bool isNeedResponse { set; get; }

        bool _isRightNow = false;
        /// <summary>
        /// 是否需要立即执行
        /// </summary>
        public bool isRightNow
        {
            get { return _isRightNow; }
            set { _isRightNow = value; }
        }

        /// <summary>
        /// Operator类名
        /// </summary>
       public virtual string operatorName { set; get; }

       /// <summary>
       /// 命令成功
       /// </summary>
       public Action CommandSuccessed { get; set; }

       /// <summary>
       /// 命令失败
       /// </summary>
       public Action CommandFailed { get; set; }
      
        /// <summary>
        /// 下发iWSN测量定义时候，需要WG MAC, Added by QXM, 2018/05/17
        /// </summary>
        public string GateWayMAC { get; set; }
    }
}