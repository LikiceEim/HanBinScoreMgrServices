/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.Common
 * 文件名：  CommonInfo
 * 创建人：  王颖辉
 * 创建时间：2016-11-15
 * 描述：公共信息
/************************************************************************************/

namespace HanBin.Common.Component.Data.Response.Common
{
    #region 返回结果集合

    /// <summary>
    /// 返回结果集合
    /// </summary>
    public class CommonInfo
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public int? OrderNo { get; set; }

        public int VibrationSignalTypeID { get; set; }

        public int MeasurementDefinitionTypeID { get; set; }

        public int? PID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string AddDate { get; set; }

        public int? IsUsable { get; set; }

        public int? IsDefault { get; set; }

        public bool IsExistDeviceChild { get; set; }

        public bool IsExistChild { get; set; }

        public bool IsExistEigenChild { get; set; }

        public bool IsExistWaveLengthChild { get; set; }

        public bool IsExistLowerLimitChild { get; set; }

        public bool IsExistUpperLimitChild { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：是否有特征值波长
        /// </summary>
        public bool IsExistEigenWaveLengthChild { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：是否有包络滤波器上限
        /// </summary>
        public bool IsExistEnvlFilterUpperLimitChild { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：是否有包络滤波器下限
        /// </summary>
        public bool IsExistEnvlFilterLowerLimitChild { get; set; }
    }

    #endregion
}