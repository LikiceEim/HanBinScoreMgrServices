/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Tool
 *文件名：  EnumHelper
 *创建人：  LF  
 *创建时间：2016年7月23日13:10:55
 *描述：枚举工具类
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    public static class EnumHelper
    {
        /// <summary>
        /// 返回枚举项的描述信息
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项</param>
        /// <returns>枚举想的描述信息</returns>
        public static string GetDescription(Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    //获取描述信息属性
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 转换成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<EnumberEntity> EnumToList<T>()
        {
            IList<EnumberEntity> list = new List<EnumberEntity>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumberEntity m = new EnumberEntity();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    m.Desction = da.Description;
                }
                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }
            return list;
        }
    }

    /// <summary>
    /// 枚举实体
    /// </summary>
    public class EnumberEntity
    {
        /// <summary>  
        /// 枚举的描述  
        /// </summary>  
        public string Desction { set; get; }

        /// <summary>  
        /// 枚举名称  
        /// </summary>  
        public string EnumName { set; get; }

        /// <summary>  
        /// 枚举对象的值  
        /// </summary>  
        public int EnumValue { set; get; }
    }
}
