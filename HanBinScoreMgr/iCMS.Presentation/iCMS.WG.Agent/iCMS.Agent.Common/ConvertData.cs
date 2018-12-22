using iCMS.Common.Component.Tool;
/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  ConvertData
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：数据转化类
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iCMS.WG.Agent.Common
{
    public class ConvertData
    {
        #region 数据转换

        #region 时间转换
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inx"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(byte[] data, int inx)
        {
            int Year = 2000 + data[inx];
            byte month = data[inx + 1];
            byte day = data[inx + 2];
            byte hour = data[inx + 3];
            byte minute = data[inx + 4];
            byte second = data[inx + 5];

            return new DateTime(Year, month, day, hour, minute, second);
        }
        #endregion

        #region 转换为int型
        /// <summary>
        /// 转换为int型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static int ConvertToInt(byte[] data, int Index)
        {
            int[] nTemp = new int[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 4);

            return nTemp[0];
        }
        #endregion

        #region 转换为int16型
        /// <summary>
        /// 转换为int16型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static Int16 ConvertToInt16(byte[] data, int Index)
        {
            Int16[] nTemp = new Int16[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 2);

            return nTemp[0];
        }
        #endregion

        #region 转换为Uint16型
        /// <summary>
        /// 转换为Uint16型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static UInt16 ConvertToUInt16(byte[] data, int Index)
        {
            UInt16[] nTemp = new UInt16[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 2);

            return nTemp[0];
        }
        #endregion

        #region 转换为int64型
        /// <summary>
        /// 转换为int64型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static Int64 ConvertToInt64(byte[] data, int Index)
        {
            Int64[] nTemp = new Int64[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 8);

            return nTemp[0];
        }
        #endregion

        #region 转换为float型
        /// <summary>
        /// 转换为float型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static float ConvertToFloat(byte[] data, int Index)
        {
            float[] nTemp = new float[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 4);

            return nTemp[0];
        }
        #endregion

        #endregion

        public static object lockobject = new object();
        #region
        public static void PrintDataToFile(string fileName, byte[] data, string dataSendDirection)
        {
            try
            {
                lock (lockobject)
                {
                    int length = data.Length;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);

                    DateTime dt = DateTime.Now;
                    if (data.Length > 0)
                    {
                        sw.Write(dt.ToString() + ":" + dt.Millisecond.ToString() + "：" + dataSendDirection + " ");
                        for (int i = 0; i < length; i++)
                        {
                            if ((i == length - 1) || (i > 0 && data[i] == 0x7E))
                            {
                                sw.WriteLine(data[i].ToString("X2") + " ");
                                break;
                            }
                            else
                            {
                                sw.Write(data[i].ToString("X2") + " ");
                            }
                        }
                    }
                    else
                    {
                        sw.WriteLine(dt.ToString() + ":" + dt.Millisecond.ToString() + "：" + dataSendDirection + " ");
                    }

                    sw.Close();
                }
            }
            catch 
            {
                
            }
        }
        #endregion

        #region byte数组逆转
        /// <summary>
        /// byte数组逆转
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] TransposeByteArray(byte[] data)
        {
            byte[] temp = new byte[data.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = data[data.Length - i - 1];
            }
            return temp;
        }
        #endregion

        #region 转换int16型为byte数组
        /// <summary>
        /// 转换int16型为byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static byte[] ConvertInt16ToBytes(Int16 data)
        {
            byte[] nTemp = new byte[2];

            nTemp[0] = (byte)(data >> 8);
            nTemp[1] = (byte)(data);

            return nTemp;
        }
        #endregion

        #region 将字符型mac转换成byte型
        /// <summary>
        /// 将字符型mac转换成byte型
        /// </summary>
        /// <param name="macStr"></param>
        /// <returns></returns>
        public static byte[] GetBytesMACFromString(string macStr)
        {
            byte[] MAC = new byte[8];
            for (int i = 0; i < macStr.Length; i += 2)
            {
                MAC[i / 2] = Convert.ToByte(ConvertUtility.Convert16To10(macStr.Substring(i, 2)));
            }
            return MAC;
        }
        #endregion

        #region add by masu 2016年2月17日16:11:26 将定义类型tDateTime转化为系统DateTime
        //public DateTime ConvertToDateTime(tDateTime dt)
        //{
        //    DateTime dateTime = Convert.ToDateTime(dt.u8Year.ToString() + "-" + dt.u8Month.ToString() + "-" + dt.u8Day.ToString() + " " + dt.u8Hour.ToString() + ":" + dt.u8Min.ToString() + ":" + dt.u8Sec.ToString());
        //    return dateTime;
        //}
        #endregion
    }
}
