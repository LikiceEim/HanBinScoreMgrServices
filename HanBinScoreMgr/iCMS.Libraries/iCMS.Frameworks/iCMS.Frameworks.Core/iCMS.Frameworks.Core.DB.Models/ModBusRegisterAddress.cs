using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_ModBusRegisterAddress")]
    public class ModBusRegisterAddress : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ModBusRegisterID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：测量定义主键ID
        /// </summary>
        public int MDFID { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：测量定义来源表
        /// </summary>
        public string MDFResourceTable { get; set; }

        /// <summary>
        /// 寄存器的地址
        /// </summary>
        public int RegisterAddress { get; set; }

        /// <summary>
        /// 寄存器存储方式（默认：2）
        /// </summary>
        public int StrEnumRegisterStorageMode { get; set; }

        /// <summary>
        /// 寄存器存储字节序列模式（默认：2）
        /// </summary>
        public int StrEnumRegisterStorageSequenceMode { get; set; }

        /// <summary>
        /// 寄存器类型（默认：1）
        /// </summary>
        public int StrEnumRegisterType { get; set; }

        /// <summary>
        /// 寄存器的描述信息（默认：0）
        /// </summary>
        public int StrEnumRegisterInformation { get; set; }

        #endregion
    }
}