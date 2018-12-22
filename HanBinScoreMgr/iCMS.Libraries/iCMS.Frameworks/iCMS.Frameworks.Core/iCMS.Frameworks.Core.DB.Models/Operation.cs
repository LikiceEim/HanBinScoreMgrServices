/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 记录对于WS的操作记录表
    /// </summary>
    [Table("T_SYS_OPERATION")]
    public class Operation : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 操作主键
        /// </summary>
        public string OperatorKey { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSID { get; set; }

        /// <summary>
        /// 操作类型，1=下发测量定义；2=升级；3=触发式上传
        /// </summary>
        public int OperationType { get; set; }

        /// <summary>
        /// 开始操作时间
        /// </summary>
        public DateTime Bdate { get; set; }

        /// <summary>
        /// 结束操作时间
        /// </summary>
        public DateTime? EDate { get; set; }

        /// <summary>
        /// 操作返回信息
        /// 0:初始无状态
        /// 1:升级成功
        /// 2:下层设备反馈升级失败
        /// 3:操作中
        /// 4:超时状态
        /// 5:网络正忙
        /// 6:应用层执行失败
        /// </summary>
        public string OperationResult { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string OperationReson { get; set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        public int? DAQStyle { get; set; }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int? WSID { get; set; }
        

        #endregion Model
    }
}