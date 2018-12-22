using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_DATA_SPEED_DEVICE_MSITEDATA_1")]
    public class SpeedDeviceMSitedata_1 : EntityBase
    {
        #region Model

        /// <summary>
        /// 转速数据表ID
        /// </summary>
        [Key]
        public int MsiteDataID { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 转速波形文件地址
        /// </summary>
        public string WaveDataPath { get; set; }

        /// <summary>
        /// 采集值
        /// </summary>
        public float MsDataValue { get; set; }

        /// <summary>
        /// 线数
        /// </summary>
        public int LineCnt { get; set; }

        /// <summary>
        /// 月份时间
        /// </summary>
        public int MonthDate { get; set; }

        /// <summary>
        /// 1.有线、2.无线、3.三轴
        /// </summary>
        public int SamplingDataType { get; set; }

        #endregion
    }
}