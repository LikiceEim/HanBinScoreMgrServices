using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 公用方法
    /// </summary>
    public static class Utilitys
    {
        /// <summary>
        /// 获取配置节点数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfig(string key)
        {
            //张辽阔 2016-12-23 注释
            //return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            //张辽阔 2016-12-23 修改
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string GetAppConfigForExe(string key)
        {
            //Modified by QXM, 2017/02/21
            //做手动推送时候，为了时修改的App.config文件能够立即起效 而不用重新启动配置文件，故而做此修改
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            KeyValueConfigurationElement connectElement = config.AppSettings.Settings["iCMS"];
            if (null == connectElement)
            {
                return string.Empty;
            }
            return connectElement.Value;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-19
        /// 创建记录：验证字符串是否等于空
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <returns></returns>
        public static bool ValidateStringEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-19
        /// 创建记录：验证字符串是否等于空或者等于0
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <returns></returns>
        public static bool ValidateStringEmptyOrZero(this string source)
        {
            return string.IsNullOrWhiteSpace(source) || source.Trim() == "0";
        }


        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证MAC地址
        /// </summary>
        /// <param name="source">验证字符串</param>
        /// <returns></returns>
        public static bool ValidateMAC(this string source)
        {
            return Regex.IsMatch(source.Trim(), @"^[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}$");
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证电话号码
        /// </summary>
        /// <param name="source">验证电话号码</param>
        /// <returns></returns>
        public static bool ValidatePhone(this string source)
        {

            var regex1 = @"^(1\d{10})$";
            // var rex=/^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/;
            //区号：前面一个0，后面跟2-3位数字 ： 0\d{2,3}
            //电话号码：7-8位数字： \d{7,8
            //分机号：一般都是3位数字： \d{3,}
            //这样连接起来就是验证电话的正则表达式了：/^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/		 
            var regex2 = @"^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$";

            //1、可以是1开头的11位数字（手机号）
            //2、可以是"区号 -电话号-分机号"或者是"(区号)电话号-分机号"格式
            //3、区号是0开头的3～4位数字，可以没有区号
            //4、电话号是5～8位数字，不能以0开头
            //5、分机号是1～8位数字，可以没有分机号

            //合法数据示例：
            //13812341234
            //010-12345678
            //(0432)1234567-1234
            var regex3 = @"^1\d{10}$|^(0\d{2,3}-?|\(0\d{ 2,3}\))?[1-9]\d{4,7}(-\d{1,8})?$";
            // return Regex.IsMatch(source, @"^(\d{3,4}-)?\d{6,8}$");
            return Regex.IsMatch(source.Trim(), regex1) || Regex.IsMatch(source.Trim(), regex2) || Regex.IsMatch(source.Trim(), regex3);
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证电话号码
        /// </summary>
        /// <param name="source">验证电话号码</param>
        /// <returns></returns>
        public static bool ValidateLength(this string source, int maxLength)
        {
            return source.Trim().Length <= maxLength;
        }
    }
}