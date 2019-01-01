/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Base
 *文件名：  BaseResponse
 *创建人：  LF
 *创建时间：2016/2/16 14:58:21
 *描述：返回结果标准类
 **=================================================================================
 *修改记录
 *修改时间：2016年10月11日09:18:52
 *修改人： LF
 *修改原因：完善结果中Reason字段赋值机制
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace HanBin.Common.Component.Data.Base
{
    #region 返回基类

    /// <summary>
    /// 返回基类
    /// </summary>
    public class BaseResponse<T>
    {
        #region Ctor.

        public BaseResponse()
        {
            this.IsSuccessful = true;
            this.Code = string.Empty;
            //张辽阔 2016-11-01 添加
            this.Result = default(T);
        }

        public BaseResponse(bool isSuccess, string reason, string code, T data)
        {
            this.IsSuccessful = isSuccess;
            this.Code = code;
            //张辽阔 2016-11-01 添加
            this.Result = data;
        }

        public BaseResponse(string code)
        {
            this.IsSuccessful = false;
            this.Code = code;

            //张辽阔 2016-11-01 添加
            this.Result = default(T);
        }

        #endregion

        /// <summary>
        /// 当前操作是否成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 不需要手动赋值
        /// 返回结果描述信息，当isSuccessfull为True此字段为空串，不需要此字段请赋值  null
        /// </summary>
        public string Reason { get;  set; }

        private string ErrorCode = string.Empty;

        /// <summary>返回结果操作代码，当isSuccessfull为True此字段为空串，不需要此字段请赋值 null
        /// </summary>
        public string Code
        {
            get
            {
                return ErrorCode;
            }
            set
            {
                ErrorCode = value;
                if (HanBin.Common.Component.Tool.ConstObject.ErrorCode.Count <= 0)
                {
                    InitErrorCode();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    object message = string.Empty;

                    if (HanBin.Common.Component.Tool.ConstObject.ErrorCode.TryGetValue(value, out message))
                    {
                        this.Reason = message.ToString();
                    }
                    else
                    {
                        this.Reason = "发生未知错误，请与管理员联系";
                    }
                }
                else
                {
                    this.Reason = null;
                }
            }
        }

        /// <summary>
        /// 返回当前操作数据信息，例如影响数据库的ID、查询结果实体对象等（或前端要求编号），不需要此字段请赋值  null
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 初始化iCMS错误代码
        /// </summary>
        private void InitErrorCode()
        {
            try
            {
                StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Resource\ErrorCode.json", System.Text.Encoding.Default);

                HanBin.Common.Component.Tool.ConstObject.ErrorCode = JsonConvert.DeserializeObject<Dictionary<string, object>>(streamReader.ReadToEnd());
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }

    #endregion
}