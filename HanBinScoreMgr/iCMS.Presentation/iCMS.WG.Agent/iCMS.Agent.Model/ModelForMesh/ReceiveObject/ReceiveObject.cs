using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Model.Receive
{
    public class ReceiveObject
    {
        /// <summary>
        /// WS ID
        /// </summary>
        public Byte WSNO { get; set; }
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MAC { get; set; }
        /// <summary>
        /// 主命令
        /// </summary>
        public int MainCommand { get; set; }
        
        /// <summary>
        /// 子命令
        /// </summary>
        public int SubCommand { get; set; }
        /// <summary>
        /// 是否请求
        /// </summary>
        public int Request { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; set; }

       
    }

    ///// <summary>
    ///// 版本号
    ///// </summary>
    //public class tVersion
    //{
    //    // 主版本号
    //    public byte u8Main { get; set; }
    //    // 子版本号
    //    public byte u8Sub { get; set; }
    //    // 修订版本号
    //    public byte u8Rev { get; set; }
    //    // 构建版本号
    //    public byte u8Build { get; set; }
    //}

    /// <summary>
    /// 定时采集间隔时间
    /// </summary>
    public class tTimingTime
    {
        // 时
        public byte u8Hour { get; set; }
        // 分
        public byte u8Min { get; set; }
    }

    /// <summary>
    /// RTC时间
    /// </summary>
    public class tRtcTime
    {
        // 时
        public byte u8Hour { get; set; }
        // 分
        public byte u8Min { get; set; }
        // 秒
        public byte u8Sec { get; set; }
    }
}
