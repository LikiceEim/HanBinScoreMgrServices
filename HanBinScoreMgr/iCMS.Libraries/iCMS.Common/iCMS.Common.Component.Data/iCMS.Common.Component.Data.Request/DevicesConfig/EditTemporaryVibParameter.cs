/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 *文件名：  EditTemporaryVibParameter 
 *创建人：  王颖辉
 *创建时间：2017/10/10 16:37:38 
 *描述：编辑临时振动信号
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    /// <summary>
    /// 编辑临时振动信号
    /// </summary>
    public class EditTemporaryVibParameter : BaseRequest
    {

        /// <summary>
        /// 测点ID
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动和特征值
        /// </summary>
        public List<VibAndEigenInfoForEditTemporaryVib> VibAndEigenInfo
        {
            get;
            set;
        }

    }

    public class VibAndEigenInfoForEditTemporaryVib
    {

        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int? SingalID
        {
            get;
            set;
        }

        /// <summary>
        /// 上限ID
        /// </summary>
        public int UpperLimitID
        {
            get;
            set;
        }

        /// <summary>
        /// 下限ID
        /// </summary>
        public int LowLimitID
        {
            get;
            set;
        }

        /// <summary>
        /// 波长ID
        /// </summary>
        public int WaveLengthID
        {
            get;
            set;
        }

        public int EigenWaveLengthID { get; set; }
        /// <summary>
        /// 振动信号类型
        /// </summary>
        public EnumVibSignalType VibrationSignalTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 振动和特征值
        /// </summary>
        public List<EigenValueInfoForEditTemporaryVib> EigenValueInfo
        {
            get;
            set;
        }

    }


    public class EigenValueInfoForEditTemporaryVib
    {
        /// <summary>
        /// 报警Id
        /// </summary>
        public int? SingalAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值类型ID
        /// </summary>
        public int EigenValueTypeID
        {
            get;
            set;
        }
    }
}
