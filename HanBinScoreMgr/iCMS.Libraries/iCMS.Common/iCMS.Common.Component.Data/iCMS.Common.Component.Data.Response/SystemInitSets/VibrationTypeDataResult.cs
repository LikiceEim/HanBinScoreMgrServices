/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  VibrationTypeData
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：通用数据：振动类型
/************************************************************************************/

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region  通用数据：振动类
    /// <summary>
    /// 通用数据：振动类型
    /// </summary>
    public class VibrationTypeData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string AddDate { get; set; }

        public int IsUsable { get; set; }

        public int IsDefault { get; set; }

        /// <summary>
        ///是否存在特征值子节点 
        /// </summary>
        public bool IsExistEigenChild { get; set; }
        /// <summary>
        /// 是否存在波形长度子节点
        /// </summary>
        public bool IsExistWaveLengthChild { get; set; }
        /// <summary>
        /// 是否存在波形下限子节点
        /// </summary>
        public bool IsExistLowerLimtChild { get; set; }
        /// <summary>
        /// 是否存在波形上限子节点
        /// </summary>
        public bool IsExistUpperLimtChild { get; set; }
    }
    #endregion
}
