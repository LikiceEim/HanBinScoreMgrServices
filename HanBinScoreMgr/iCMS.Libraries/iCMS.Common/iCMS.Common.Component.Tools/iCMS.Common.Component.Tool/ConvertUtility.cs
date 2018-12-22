/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  ConvertUtility
 *创建人：  LF  
 *创建时间：2016年8月1日11:18:53
 *描述：公共方法类型转换工具
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    public class ConvertUtility
    {
        #region 数据转换 Struct To Bytes
        /// <summary>
        /// 数据转换 Struct To Bytes
        /// </summary>
        /// <param name="structObj"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] StructToBytes(object structObj, int size)
        {
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        #endregion

        #region int转BCD码
        /// <summary>
        /// int转BCD码
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static byte ConvertIntToBCD(int num)
        {
            int n = num % 10;
            int m = (num / 10) % 10;
            return Convert.ToByte((Convert.ToByte(m) << 4) + (Convert.ToByte(n)));
        }
        #endregion

        #region 时间转换
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inx"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(byte[] data, int inx)
        {
            try
            {
                int Year = 2000 + Convert.ToInt32(data[inx]);
                int month = Convert.ToInt32(data[inx + 1]);
                int day = Convert.ToInt32(data[inx + 2]);
                int hour = Convert.ToInt32(data[inx + 3]);
                int minute = Convert.ToInt32(data[inx + 4]);
                int second = Convert.ToInt32(data[inx + 5]);


                return new DateTime(Year, month, day, hour, minute, second);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region BCD码转十进制
        /// <summary>
        /// BCD码转十进制
        /// </summary>
        /// <param name="bcdData"></param>
        /// <returns></returns>
        public static int ConvertBCDToInt32(byte bcdData)
        {
            return Convert.ToInt32((bcdData >> 4).ToString() + "" + (((byte)(bcdData << 4)) >> 4).ToString());
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

        #region 转换为int32型
        /// <summary>
        /// 转换为int32型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(byte[] data, int Index)
        {
            Int32[] nTemp = new Int32[1];

            Buffer.BlockCopy(data, Index, nTemp, 0, 4);

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

            //if (nTemp[0] < 0.000001)
            if (nTemp[0] < 0.000001 && nTemp[0] > -0.000001)
            {
                return 0;
            }
            return nTemp[0];
        }
        #endregion

        #region 将字符串转换为byte数组
        /// <summary>
        /// 将字符串转换为byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ConvertStringToBytes(string data)
        {
            char[] charData = data.ToCharArray();
            byte[] renData = new byte[charData.Length];
            for (int i = 0; i < charData.Length; i++)
            {
                renData[i] = Convert.ToByte(charData[i]);
            }
            return renData;
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

        #region 转换int32型为byte数组
        /// <summary>
        /// 转换int32型为byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static byte[] ConvertInt32ToBytes(int data)
        {
            byte[] nTemp = new byte[4];

            nTemp[0] = (byte)(data >> 24);
            nTemp[1] = (byte)(data >> 16);
            nTemp[2] = (byte)(data >> 8);
            nTemp[3] = (byte)(data);

            return nTemp;
        }
        #endregion

        #region 将byte型波形数据转换为float型
        /// <summary>
        /// 将byte型波形数据转换为float型
        /// </summary>
        /// <param name="waveData"></param>
        /// <param name="ConvertCoef"></param>
        /// <returns></returns>
        public static float[] ConvertToDoubleWave(byte[] waveData, float ConvertCoef)
        {
            // 验证波形数据
            if (waveData == null || waveData.Length < 2)
            {
                return null;
            }
            // 波形点数
            int nShortLen = waveData.Length / 2;
            // 临时short缓存
            short[] nTmp = new short[nShortLen];
            // 转换结果
            float[] dRslt = new float[nShortLen];
            // 循环完成转换
            for (int inx = 0; inx < nShortLen; inx++)
            {
                nTmp[inx] = ConvertToShort(waveData, inx * 2);

                dRslt[inx] = ConvertCoef * nTmp[inx];
            }

            return dRslt;
        }
        #endregion

        #region
        private static short ConvertToShort(byte[] data, int Index)
        {
            short[] nTemp = new short[1];

            System.Buffer.BlockCopy(data, Index, nTemp, 0, 2);

            return nTemp[0];
        }
        #endregion

        #region 将byte数组转换为float数组
        /// <summary>
        ///  将byte数组转换为float数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float[] ConvertByteArrToFloatArr(byte[] data)
        {
            // 验证波形数据
            if (data == null || data.Length < 4)
            {
                return null;
            }

            float[] fRslt = new float[data.Length / 4];
            for (int i = 0; i < fRslt.Length; i++)
            {
                byte[] temp = new byte[4];
                temp[0] = data[i * 4];
                temp[1] = data[i * 4 + 1];
                temp[2] = data[i * 4 + 2];
                temp[3] = data[i * 4 + 3];

                //fRslt[i] = ConvertToFloat(temp, 0);
                fRslt[i] = BitConverter.ToSingle(temp, 0);
            }
            return fRslt;
        }
        #endregion

        #region 将时间转换为byte数组
        /// <summary>
        /// 将时间转换为byte数组(年只取后两位)
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static byte[] ConvertDateTimeToBytes(DateTime dt)
        {
            byte[] reData = new byte[6];
            int index = 0;
            //取得当前时间
            reData[index++] = Convert.ToByte(dt.Year - 2000);
            reData[index++] = Convert.ToByte(dt.Month);
            reData[index++] = Convert.ToByte(dt.Day);
            reData[index++] = Convert.ToByte(dt.Hour);
            reData[index++] = Convert.ToByte(dt.Minute);
            reData[index++] = Convert.ToByte(dt.Second);

            return reData;
        }
        #endregion

        public static int Convert16To10(string value)
        {
            return GetHexadecimalValue(value.Substring(0, 1).ToUpper()) * 16 +
                GetHexadecimalValue(value.Substring(1, 1).ToUpper());
        }

        private static int GetHexadecimalValue(string value)
        {
            int rValue = 0;
            try
            {
                switch (value)
                {
                    case "A":
                        rValue = 10;
                        break;
                    case "B":
                        rValue = 11;
                        break;
                    case "C":
                        rValue = 12;
                        break;
                    case "D":
                        rValue = 13;
                        break;
                    case "E":
                        rValue = 14;
                        break;
                    case "F":
                        rValue = 15;
                        break;
                    default:
                        rValue = Convert.ToInt32(value);
                        break;
                }
            }
            catch { }
            return rValue;
        }
    }
}
