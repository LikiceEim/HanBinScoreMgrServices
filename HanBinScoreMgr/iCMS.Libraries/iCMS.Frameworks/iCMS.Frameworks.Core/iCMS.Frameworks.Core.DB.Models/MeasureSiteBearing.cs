/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iPVP.Frameworks.Core.DB.Models
 *文件名：  MEASURESITEBEARING
 *创建人：  王颖辉
 *创建时间：2017/02/22 18:01:13
 *描述： 测点和轴承关系表
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 测点和轴承关系表
    /// </summary>
    [Table("T_SYS_MEASURESITE_BEARING")]
    public partial class MeasureSiteBearing : EntityBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int MeasureSiteBearingID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 测点ID
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum
        {
            get;
            set;
        }  
    
        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
        {
            get;
            set;
        }  
    
    }
}