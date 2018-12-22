using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUService
{
    /// <summary>
    /// 通过采集单元id读取所有传感器 返回结果类
    /// </summary>
    public class GetAllWSInfoByDAUIDResult
    {
        /// <summary>
        /// WG信息   
        /// </summary>
        public List<WGInfoItem> WGInfoList { get; set; }

        public GetAllWSInfoByDAUIDResult()
        {
            this.WGInfoList = new List<WGInfoItem>();
        }
    }

    public class WGInfoItem
    {
        /// <summary>
        /// DAUID
        /// </summary>
        public int DAUID { get; set; }
        /// <summary>
        /// DAU名称
        /// </summary>
        public string DAUName { get; set; }
        /// <summary>
        /// WS结果集合
        /// </summary>
        public List<WSInfoItem> WSInfoList { get; set; }

        public WGInfoItem()
        {
            this.WSInfoList = new List<WSInfoItem>();
        }
    }

    public class WSInfoItem
    {
        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID { get; set; }
        /// <summary>
        /// WS名称
        /// </summary>
        public string WSName { get; set; }

        /// <summary>
        /// WS关联的通道ID
        /// </summary>
        public int? ChannelId { get; set; }
        /// <summary>
        /// 是否是转速通道：true是；false，否
        /// </summary>
        public bool IsSpeed { get; set; }

        /// <summary>
        /// 是否被使用：false：否 true：是
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// 是否是功率传感器 false：否； true：是
        /// </summary>
        public bool IsPower { get; set; }
        /// <summary>
        /// 是否是温度传感器 false：否； true：是
        /// </summary>
        public bool IsTemperature { get; set; }
        /// <summary>
        /// 是否是振动传感器 false：否； true：是
        /// </summary>
        public bool IsVibration { get; set; }
        /// <summary>
        /// 挂靠设备名称
        /// </summary>
        public string DevName { get; set; }
        /// <summary>
        /// 挂靠设备ID
        /// </summary>
        public int? DevID { get; set; }
        /// <summary>
        /// 挂靠测点名称
        /// </summary>
        public string MesureSiteName { get; set; }
        /// <summary>
        /// 挂靠测点ID
        /// </summary>
        public int? MesureSiteID { get; set; }
        /// <summary>
        /// 通道对应的振动信号信息
        /// </summary>
        public List<VibsignalInfoItem> VibsignalInfoList { get; set; }

        public WSInfoItem()
        {
            this.VibsignalInfoList = new List<VibsignalInfoItem>();
        }
    }

    public class VibsignalInfoItem
    {
        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int VibsignalID { get; set; }
        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int SignalType { get; set; }
        /// <summary>
        /// 特征值列表
        /// </summary>
        public List<EigenValueInfoItem> EigenValueInfoList { get; set; }

        public VibsignalInfoItem()
        {
            this.EigenValueInfoList = new List<EigenValueInfoItem>();
        }
    }

    public class EigenValueInfoItem
    {
        /// <summary>
        /// 特征值ID
        /// </summary>
        public int EigenValueID { get; set; }
        /// <summary>
        /// 特征值类型ID
        /// </summary>
        public int EigenValueTypeID { get; set; }
        /// <summary>
        /// 特征值名称
        /// </summary>
        public string EigenValueTypeName { get; set; }
    }
}
