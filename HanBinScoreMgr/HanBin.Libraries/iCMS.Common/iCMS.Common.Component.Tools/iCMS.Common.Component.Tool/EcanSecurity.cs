using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    public static class EcanSecurity
    {

        /// <summary>
        /// 密钥
        /// </summary>
        private const string key = "0iL2in9e";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetKey(string secret)
        {
            string newKey = Encode(secret, key);
            return newKey;
        }


        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// 对称加密法加密函数
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string Encode(string encryptString, string encryptKey=key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// 对称加密法解密函数
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>

        public static string Decode(string decryptString, string decryptKey=key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #region 获取客户端生成的key
        ///<summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-10-19
        /// 创建记录：获取客户端生成的key
        /// </summary>
        /// <param name="str">需要加密码的字符串</param>
        /// <returns>MD5(str)</returns>
        public static string GetClientKey(string str)
        {
            return MD5Helper.GetMD5(str);
        }
        #endregion

        #region 获取客户端生成的key
        ///<summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-10-19
        /// 创建记录：获取客户端生成的key
        /// </summary>
        /// <param name="str">需要加密码的字符串</param>
        /// <returns>两次MD5</returns>
        public static string GetClientSecret(string str)
        {
            return MD5Helper.GetMD5(MD5Helper.GetMD5(str) + key);
        }
        #endregion
     
    }
}
