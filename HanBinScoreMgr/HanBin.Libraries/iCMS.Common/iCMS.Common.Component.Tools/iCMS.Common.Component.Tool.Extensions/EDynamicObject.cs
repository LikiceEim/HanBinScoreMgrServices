/************************************************************************************
 * Copyright (c) 2016 iLine All Rights Reserved.
 *命名空间：DynamicExpand
 *文件名：  Result
 *创建人：  LF
 *创建时间：2016年9月5日
 *描述：用于动态创建类并序列化为Json（注：此类可能存在效率问题）
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool.Extensions
{
    /// <summary>
    /// 动态类，基类
    /// </summary>
    class DynamicObjectNew : DynamicObject, IDisposable
    {


        public Dictionary<string, object> Properties = new Dictionary<string, object>();
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                Properties.Add(binder.Name, value);
            }
            return true;
        }

        public override bool TryInvokeMember(System.Dynamic.InvokeMemberBinder binder, object[] args, out object result)
        {
            if (binder.Name == "set" && binder.CallInfo.ArgumentCount == 2)
            {
                string name = args[0] as string;
                if (name == null)
                {
                    //throw new ArgumentException("name");
                    result = null;
                    return false;
                }
                object value = args[1];
                Properties.Add(name, value);
                result = value;
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {

            return Properties.TryGetValue(binder.Name, out result);

        }


        public void Dispose()
        {

            Properties.Clear();
            this.Dispose();
        }
    }


    /// <summary>
    /// iCMS 1.4 专用返回结果类
    /// </summary>
    public class EDynamicObject : IDisposable
    {
        private dynamic returnObject;
        public EDynamicObject()
        {
            if (returnObject == null)
                returnObject = new DynamicObjectNew();
        }


        /// <summary>
        /// 当前操作是否成功
        /// </summary>
        public bool? IsSuccessful
        {
            get { return returnObject.IsSuccessful; }

            set { returnObject.IsSuccessful = value; }
        }


        /// <summary>返回结果描述信息，当isSuccessfull为True此字段为空串
        /// </summary>
        public string Reason
        {
            get { return returnObject.Reason; }
            set { returnObject.Reason = value; }
        }

        /// <summary>返回结果操作代码，当isSuccessfull为True此字段为空串
        /// </summary>
        public string Code
        {
            get { return returnObject.Code; }
            set { returnObject.Code = value; }
        }


        /// <summary>
        /// 返回当前操作数据信息，例如影响数据库的ID、查询结果实体对象等（或前端要求编号）
        /// </summary>
        public Object Result 
        {
            get { return returnObject.Result; }
            set { returnObject.Result = value; }
        }



        protected dynamic BackObject
        {
            get
            {
                return returnObject;
            }
        }

        public void AddPropertie(string name, object value)
        {
            returnObject.set(name, value);
        }



        public override string ToString()
        {
            return Json.JsonSerializer(returnObject.Properties);
        }

        public void Dispose()
        {
            this.IsSuccessful = true;
            this.Code = string.Empty;
            this.Reason = string.Empty;
            this.Result = null;
            this.Dispose();
        }
    }
}
