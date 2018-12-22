using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_DEVICE_RELATION_WG")]
    public class DeviceRelationWG : EntityBase
    {
        #region Model
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevId { get; set; }

        /// <summary>
        /// wgID
        /// </summary>
        public int WGID { get; set; }

        #endregion
    }
}