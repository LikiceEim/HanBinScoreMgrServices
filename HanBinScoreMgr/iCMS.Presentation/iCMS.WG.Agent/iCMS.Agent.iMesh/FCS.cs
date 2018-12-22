using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    class FCS
    {
        public const UInt16 FCS_INITIAL_FCS16 = ((UInt16)0xffff);
        static UInt16[] fcstab = new UInt16[256]
        {
            (UInt16)0x0000, (UInt16)0x1189, (UInt16)0x2312, (UInt16)0x329b, (UInt16)0x4624, (UInt16)0x57ad, (UInt16)0x6536, (UInt16)0x74bf,
            (UInt16)0x8c48, (UInt16)0x9dc1, (UInt16)0xaf5a, (UInt16)0xbed3, (UInt16)0xca6c, (UInt16)0xdbe5, (UInt16)0xe97e, (UInt16)0xf8f7,
            (UInt16)0x1081, (UInt16)0x0108, (UInt16)0x3393, (UInt16)0x221a, (UInt16)0x56a5, (UInt16)0x472c, (UInt16)0x75b7, (UInt16)0x643e,
            (UInt16)0x9cc9, (UInt16)0x8d40, (UInt16)0xbfdb, (UInt16)0xae52, (UInt16)0xdaed, (UInt16)0xcb64, (UInt16)0xf9ff, (UInt16)0xe876,
            (UInt16)0x2102, (UInt16)0x308b, (UInt16)0x0210, (UInt16)0x1399, (UInt16)0x6726, (UInt16)0x76af, (UInt16)0x4434, (UInt16)0x55bd,
            (UInt16)0xad4a, (UInt16)0xbcc3, (UInt16)0x8e58, (UInt16)0x9fd1, (UInt16)0xeb6e, (UInt16)0xfae7, (UInt16)0xc87c, (UInt16)0xd9f5,
            (UInt16)0x3183, (UInt16)0x200a, (UInt16)0x1291, (UInt16)0x0318, (UInt16)0x77a7, (UInt16)0x662e, (UInt16)0x54b5, (UInt16)0x453c,
            (UInt16)0xbdcb, (UInt16)0xac42, (UInt16)0x9ed9, (UInt16)0x8f50, (UInt16)0xfbef, (UInt16)0xea66, (UInt16)0xd8fd, (UInt16)0xc974,
            (UInt16)0x4204, (UInt16)0x538d, (UInt16)0x6116, (UInt16)0x709f, (UInt16)0x0420, (UInt16)0x15a9, (UInt16)0x2732, (UInt16)0x36bb,
            (UInt16)0xce4c, (UInt16)0xdfc5, (UInt16)0xed5e, (UInt16)0xfcd7, (UInt16)0x8868, (UInt16)0x99e1, (UInt16)0xab7a, (UInt16)0xbaf3,
            (UInt16)0x5285, (UInt16)0x430c, (UInt16)0x7197, (UInt16)0x601e, (UInt16)0x14a1, (UInt16)0x0528, (UInt16)0x37b3, (UInt16)0x263a,
            (UInt16)0xdecd, (UInt16)0xcf44, (UInt16)0xfddf, (UInt16)0xec56, (UInt16)0x98e9, (UInt16)0x8960, (UInt16)0xbbfb, (UInt16)0xaa72,
            (UInt16)0x6306, (UInt16)0x728f, (UInt16)0x4014, (UInt16)0x519d, (UInt16)0x2522, (UInt16)0x34ab, (UInt16)0x0630, (UInt16)0x17b9,
            (UInt16)0xef4e, (UInt16)0xfec7, (UInt16)0xcc5c, (UInt16)0xddd5, (UInt16)0xa96a, (UInt16)0xb8e3, (UInt16)0x8a78, (UInt16)0x9bf1,
            (UInt16)0x7387, (UInt16)0x620e, (UInt16)0x5095, (UInt16)0x411c, (UInt16)0x35a3, (UInt16)0x242a, (UInt16)0x16b1, (UInt16)0x0738,
            (UInt16)0xffcf, (UInt16)0xee46, (UInt16)0xdcdd, (UInt16)0xcd54, (UInt16)0xb9eb, (UInt16)0xa862, (UInt16)0x9af9, (UInt16)0x8b70,
            (UInt16)0x8408, (UInt16)0x9581, (UInt16)0xa71a, (UInt16)0xb693, (UInt16)0xc22c, (UInt16)0xd3a5, (UInt16)0xe13e, (UInt16)0xf0b7,
            (UInt16)0x0840, (UInt16)0x19c9, (UInt16)0x2b52, (UInt16)0x3adb, (UInt16)0x4e64, (UInt16)0x5fed, (UInt16)0x6d76, (UInt16)0x7cff,
            (UInt16)0x9489, (UInt16)0x8500, (UInt16)0xb79b, (UInt16)0xa612, (UInt16)0xd2ad, (UInt16)0xc324, (UInt16)0xf1bf, (UInt16)0xe036,
            (UInt16)0x18c1, (UInt16)0x0948, (UInt16)0x3bd3, (UInt16)0x2a5a, (UInt16)0x5ee5, (UInt16)0x4f6c, (UInt16)0x7df7, (UInt16)0x6c7e,
            (UInt16)0xa50a, (UInt16)0xb483, (UInt16)0x8618, (UInt16)0x9791, (UInt16)0xe32e, (UInt16)0xf2a7, (UInt16)0xc03c, (UInt16)0xd1b5,
            (UInt16)0x2942, (UInt16)0x38cb, (UInt16)0x0a50, (UInt16)0x1bd9, (UInt16)0x6f66, (UInt16)0x7eef, (UInt16)0x4c74, (UInt16)0x5dfd,
            (UInt16)0xb58b, (UInt16)0xa402, (UInt16)0x9699, (UInt16)0x8710, (UInt16)0xf3af, (UInt16)0xe226, (UInt16)0xd0bd, (UInt16)0xc134,
            (UInt16)0x39c3, (UInt16)0x284a, (UInt16)0x1ad1, (UInt16)0x0b58, (UInt16)0x7fe7, (UInt16)0x6e6e, (UInt16)0x5cf5, (UInt16)0x4d7c,
            (UInt16)0xc60c, (UInt16)0xd785, (UInt16)0xe51e, (UInt16)0xf497, (UInt16)0x8028, (UInt16)0x91a1, (UInt16)0xa33a, (UInt16)0xb2b3,
            (UInt16)0x4a44, (UInt16)0x5bcd, (UInt16)0x6956, (UInt16)0x78df, (UInt16)0x0c60, (UInt16)0x1de9, (UInt16)0x2f72, (UInt16)0x3efb,
            (UInt16)0xd68d, (UInt16)0xc704, (UInt16)0xf59f, (UInt16)0xe416, (UInt16)0x90a9, (UInt16)0x8120, (UInt16)0xb3bb, (UInt16)0xa232,
            (UInt16)0x5ac5, (UInt16)0x4b4c, (UInt16)0x79d7, (UInt16)0x685e, (UInt16)0x1ce1, (UInt16)0x0d68, (UInt16)0x3ff3, (UInt16)0x2e7a,
            (UInt16)0xe70e, (UInt16)0xf687, (UInt16)0xc41c, (UInt16)0xd595, (UInt16)0xa12a, (UInt16)0xb0a3, (UInt16)0x8238, (UInt16)0x93b1,
            (UInt16)0x6b46, (UInt16)0x7acf, (UInt16)0x4854, (UInt16)0x59dd, (UInt16)0x2d62, (UInt16)0x3ceb, (UInt16)0x0e70, (UInt16)0x1ff9,
            (UInt16)0xf78f, (UInt16)0xe606, (UInt16)0xd49d, (UInt16)0xc514, (UInt16)0xb1ab, (UInt16)0xa022, (UInt16)0x92b9, (UInt16)0x8330,
            (UInt16)0x7bc7, (UInt16)0x6a4e, (UInt16)0x58d5, (UInt16)0x495c, (UInt16)0x3de3, (UInt16)0x2c6a, (UInt16)0x1ef1, (UInt16)0x0f78
        };

        /// <summary>
        /// 计算一个byte的fcs值
        /// </summary>
        /// <param name="fcs">初始fcs值</param>
        /// <param name="data">要计算的byte数据</param>
        /// <returns>计算后的fcs值</returns>
        public static UInt16 Fcs16CalcByte(UInt16 fcs, byte data)
        {
            return (UInt16)((fcs >> 8) ^ fcstab[(fcs ^ data) & 0xff]);
        }

        /// <summary>
        /// 计算一组byte的fcs值
        /// </summary>
        /// <param name="buf">要计算一组byte数组</param>
        /// <param name="len">数组长度</param>
        /// <returns>计算后的fcs值</returns>
        public static UInt16 Fcs16CalcArray(byte[] buf, int len)
        {
            UInt16 fcs;
            UInt32 i;

            fcs = FCS_INITIAL_FCS16;
            for (i = 0; i < len; i++)
            {
                fcs = Fcs16CalcByte(fcs, buf[i]);
            }

            return (UInt16)(fcs ^ 0xffff); /* return complement */
        }

    }
}
