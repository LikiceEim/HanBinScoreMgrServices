/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.DiagnosticAnalysis
 *文件名：  Validate
 *创建人：  LF
 *创建时间：2016-08-04
 *描述：诊断分析请求参数验证类
 *
 *修改人：张辽阔
 *修改时间：2016-11-14
 *描述：增加错误编码
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;

namespace iCMS.Service.Web.DiagnosticAnalysis
{
    #region 验证
    /// <summary>
    /// 验证
    /// </summary>
    public class Validate
    {
        /// <summary>
        /// 创建人：LF
        /// 创建时间：2016-07-26
        /// 创建记录：验证“报警提醒”的参数是否有效
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="devAlmStat">设备报警级别</param>
        /// <param name="bDate">开始时间</param>
        /// <param name="eDate">结束时间</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        internal static BaseResponse<DevAlarmRemindDataResult> ValidateQueryDevAlarmRemindDataByUserIdParams(QueryDevWarningDataParameter Parameter)
        {
            BaseResponse<DevAlarmRemindDataResult> result = new BaseResponse<DevAlarmRemindDataResult>();
            if (Parameter.UserID <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "002472";
                return result;
            }
            if (Parameter.DevAlmStat <= 0)
            {
                result.IsSuccessful = false;
                result.Code = "002482";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.BDate))
            {
                result.IsSuccessful = false;
                result.Code = "002492";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.EDate))
            {
                result.IsSuccessful = false;
                result.Code = "002502";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Sort))
            {
                result.IsSuccessful = false;
                result.Code = "002512";
                return result;
            }
            if (string.IsNullOrWhiteSpace(Parameter.Order))
            {
                result.IsSuccessful = false;
                result.Code = "002522";
                return result;
            }
            result.IsSuccessful = true;
            return result;
        }
    }
    #endregion
}