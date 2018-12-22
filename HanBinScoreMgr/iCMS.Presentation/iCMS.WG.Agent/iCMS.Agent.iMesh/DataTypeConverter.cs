using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    public class DataTypeConverter
    {
        /// <summary>
        /// 翻转byte数组字节数据位置顺序
        /// </summary>
        /// <param name="data">进行饭庄操作的字节数组</param>
        private static void ReverseByteArray(byte[] data)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("ReverseByteArray take invalid parameter");

            byte temp = 0;
            for (int i = 0; i < (data.Length / 2); i++)
            {
                temp = data[i];
                data[i] = data[data.Length - i - 1];
                data[data.Length - i - 1] = temp;
            }
        }

        /// <summary>
        /// UInt16数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">UInt16数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void UInt16ToMeshByteArr(UInt16 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("UInt16ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(UInt16)))
                throw new Exception("UInt16ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(UInt16));
        }

        /// <summary>
        /// Int16数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">Int16数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void Int16ToMeshByteArr(Int16 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("Int16ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(Int16)))
                throw new Exception("Int16ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(Int16));
        }

        /// <summary>
        /// 网络字节流转换成UInt16数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static UInt16 MeshByteArrToUInt16(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToUInt16 take invalid parameter");
            if (data.Length < (index + sizeof(UInt16)))
                throw new Exception("MeshByteArrToUInt16 take invalid Index");

            byte[] extractArr = new byte[sizeof(UInt16)];
            Array.Copy(data, index, extractArr, 0, sizeof(UInt16));
            ReverseByteArray(extractArr);

            return BitConverter.ToUInt16(extractArr, 0);
        }

        /// <summary>
        /// 网络字节流转换成Int16数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static Int16 MeshByteArrToInt16(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToInt16 take invalid parameter");
            if (data.Length < (index + sizeof(Int16)))
                throw new Exception("MeshByteArrToInt16 take invalid Index");

            byte[] extractArr = new byte[sizeof(Int16)];
            Array.Copy(data, index, extractArr, 0, sizeof(Int16));
            ReverseByteArray(extractArr);

            return BitConverter.ToInt16(extractArr, 0);
        }

        /// <summary>
        /// UInt32数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">UInt32数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void UInt32ToMeshByteArr(UInt32 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("UInt32ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(Int32)))
                throw new Exception("UInt32ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(UInt32));
        }

        /// <summary>
        /// Int32数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">Int32数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void Int32ToMeshByteArr(Int32 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("Int32ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(Int32)))
                throw new Exception("Int32ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(Int32));
        }

        /// <summary>
        /// float数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">float数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void FloatToMeshByteArr(float data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("FloatToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(float)))
                throw new Exception("FloatToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);            
            ReverseByteArray(ret);
            Array.Copy(ret, 0, stream, offset, sizeof(float));
        }
        /// <summary>
        /// 网络字节流转换成UInt32数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static UInt32 MeshByteArrToUInt32(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToUInt32 take invalid parameter");
            if (data.Length < (index + sizeof(UInt32)))
                throw new Exception("MeshByteArrToUInt32 take invalid Index");

            byte[] extractArr = new byte[sizeof(UInt32)];
            Array.Copy(data, index, extractArr, 0, sizeof(UInt32));

            ReverseByteArray(extractArr);

            return BitConverter.ToUInt32(extractArr, 0);
        }

        /// <summary>
        /// 网络字节流转换成Int32数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static Int32 MeshByteArrToInt32(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToInt32 take invalid parameter");
            if (data.Length < (index + sizeof(Int32)))
                throw new Exception("MeshByteArrToInt32 take invalid Index");

            byte[] extractArr = new byte[sizeof(Int32)];
            Array.Copy(data, index, extractArr, 0, sizeof(Int32));

            ReverseByteArray(extractArr);

            return BitConverter.ToInt32(extractArr, 0);
        }

        /// <summary>
        /// UInt64数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">UInt64数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void UInt64ToMeshByteArr(UInt64 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("UInt64ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(UInt64)))
                throw new Exception("UInt64ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(UInt64));
        }

        public static void UInt64ToByteArr(UInt64 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("UInt64ToByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(UInt64)))
                throw new Exception("UInt64ToByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(UInt64));
        }

        /// <summary>
        /// Int64数据类型转换成网络字节流
        /// </summary>
        /// <param name="data">Int64T数据</param>
        /// <param name="stream">填充的字节数组</param>
        /// <param name="offset">填充的字节数组其实偏移地址</param>
        public static void Int64ToMeshByteArr(Int64 data, byte[] stream, int offset = 0)
        {
            if (stream == null || stream.Length <= 0)
                throw new Exception("Int64ToMeshByteArr take invalid parameter");
            if (stream.Length < (offset + sizeof(Int64)))
                throw new Exception("Int64ToMeshByteArr take invalid offset");

            byte[] ret = BitConverter.GetBytes(data);
            ReverseByteArray(ret);

            Array.Copy(ret, 0, stream, offset, sizeof(Int64));
        }

        /// <summary>
        /// 网络字节流转换成UInt64数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static UInt64 MeshByteArrToUInt64(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToUInt64 take invalid parameter");
            if (data.Length < (index + sizeof(UInt64)))
                throw new Exception("MeshByteArrToUInt64 take invalid Index");

            byte[] extractArr = new byte[sizeof(UInt64)];
            Array.Copy(data, index, extractArr, 0, sizeof(UInt64));
            ReverseByteArray(extractArr);

            return BitConverter.ToUInt64(extractArr, 0);
        }

        /// <summary>
        /// 网络字节流转换成Int64数据类型
        /// </summary>
        /// <param name="data">网络字节流</param>
        /// <param name="index">转换起始字节</param>
        /// <returns>转换后的数据</returns>
        public static Int64 MeshByteArrToInt64(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToInt64 take invalid parameter");
            if (data.Length < (index + sizeof(Int64)))
                throw new Exception("MeshByteArrToInt64 take invalid Index");

            byte[] extractArr = new byte[sizeof(Int64)];
            Array.Copy(data, index, extractArr, 0, sizeof(Int64));
            ReverseByteArray(extractArr);

            return BitConverter.ToInt64(extractArr, 0);
        }

        public static float MeshByteArrToFloat(byte[] data, int index = 0)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("MeshByteArrToFloat take invalid parameter");
            if (data.Length < (index + sizeof(float)))
                throw new Exception("MeshByteArrToFloat take invalid Index");

            byte[] extractArr = new byte[sizeof(float)];
            Array.Copy(data, index, extractArr, 0, sizeof(float));

            ReverseByteArray(extractArr);

            return BitConverter.ToSingle(extractArr, 0);
        }
    }
}
