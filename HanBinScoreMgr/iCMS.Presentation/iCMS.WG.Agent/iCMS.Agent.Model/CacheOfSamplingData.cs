/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnumCacheType
 *创建人：  LF
 *创建时间：2016/6/23 10:10:19
 *描述：主要用于Agent处理判断重复数据，实体类
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Model
{
    public class CacheOfSamplingData
    {
        /// <summary>
        /// 数据对应的WS的MAC地址
        /// </summary>
        public string Mac { set; get; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingTime { set; get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public EnumCacheType CacheType { set; get; }
    }
}
