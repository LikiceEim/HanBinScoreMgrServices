/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Tool
 * 文件名：  CommonObject
 * 创建人：  LF  
 * 创建时间：2016年7月23日16:06:40
 * 描述：定义常量信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    public class ConstObject
    {
        /// <summary>
        /// 峰值
        /// </summary>
        public const string Peak_Value = "峰值";
        /// <summary>
        /// 峰峰值
        /// </summary>
        public const string Peak_Peak_Value = "峰峰值";
        /// <summary>
        /// 有效值
        /// </summary>
        public const string Effectivity_Value = "有效值";

        /// <summary>
        /// 低频能量值
        /// </summary>
        public const string LPE_Value = "低频能量值";
        /// <summary>
        /// 中频能量值
        /// </summary>
        public const string MPE_Value = "中频能量值";
        /// <summary>
        /// 高频能量值
        /// </summary>
        public const string HPE_Value = "高频能量值";

        public const string Mean_Value = "均值";
        /// <summary>
        /// 地毯值
        /// </summary>
        public const string Carpet_Value = "地毯值";
        /// <summary>
        /// 轴承状态
        /// </summary>
        public const string LQ_Value = "轴承状态";

        /// <summary>
        /// 速度名称
        /// </summary>
        public const string Velocity = "速度";
        /// <summary>
        /// 加速度名称
        /// </summary>
        public const string Accelerated = "加速度";
        /// <summary>
        /// 包络名称
        /// </summary>
        public const string Envelope = "包络";
        /// <summary>
        /// 位移名称
        /// </summary>
        public const string Displacement = "位移";

        /// <summary>
        /// LQ
        /// </summary>
        public const string LQ = "设备状态";

        /// <summary>
        /// 删除设备
        /// </summary>
        public const string SQL_DeleteDevice = "exec SP_DeleteDevice @DevID ";
        /// <summary>
        /// 删除测量位置
        /// </summary>
        public const string SQL_DeleteMeasureSite = "exec SP_DeleteMeasureSite @MSiteID";
        /// <summary>
        /// 删除诊断信号
        /// </summary>
        public const string SQL_DeleteVibSingal = "exec SP_DeleteVibSingal @SingalID";

        /// <summary>
        /// 操作类型，发送测量定义
        /// </summary>
        public const int Operation_Type_MeasureDefine = 1;
        /// <summary>
        /// 操作类型，WS升级
        /// </summary>
        public const int Operation_Type_Update_WS = 2;

        /// <summary>
        /// 操作类型，触发式上传测量定义
        /// </summary>
        public const int Operation_Type_Upload_Trigger = 3;

        /// <summary>
        /// 操作结果，成功
        /// </summary>
        public const string Operation_Result_Success = "成功";
        /// <summary>
        /// 操作结果，失败
        /// </summary>
        public const string Operation_Result_Failed = "失败";
        /// <summary>
        /// 操作结果，进行中
        /// </summary>
        public const string Operation_Result_Operating = "进行中";

        public const float Measure_Define_SamplingFrequency_256 = 2.56F;

        public const string HTTP_Request_Para_Content_Name = "&content=";

        /// <summary>
        /// 启停机
        /// </summary>
        public static bool availabilityCriticalValue = false;
        /// <summary>
        /// 报警确认
        /// </summary>
        public static bool AlarmsConfirmed = false;
        /// <summary>
        /// 趋势报警
        /// </summary>
        public static bool TrendAlarms = false;

        public static float TrendAlarmsPercentage = 0.2F;

        #region Get latest data for dev history data

        //设备历史数据查询——最近一次
        //--监测树节点条件
        //--状态条件
        //--用户UserID条件
        public const string SQL_TOTAL_LAST = @"SELECT count(*) FROM {0} a
            RIGHT JOIN (
                SELECT  MSiteID ,MAX(CollectitTime) CollectitTime
                FROM  {0} GROUP BY MSiteID ) b ON b.MSiteID=a.MSiteID AND b.CollectitTime=a.CollectitTime
            where 1=1 
            {1}
            {2}
            {3}";

        public const string SQL_CURRENT_LAST = @"select * from (
            SELECT row_number() over(ORDER BY a.{0} {1}) as RowNumber, a.* 
            FROM {2} a
            RIGHT JOIN (
                SELECT  MSiteID ,MAX(CollectitTime) CollectitTime
                FROM  {2} GROUP BY MSiteID ) b ON b.MSiteID=a.MSiteID AND b.CollectitTime=a.CollectitTime
            where 1=1 
            {3}
            {4}
            {5}
            ) tt where RowNumber BETWEEN {6} and {7};";

        //设备历史数据查询——非最近一次
        //--监测树节点条件
        //--时间条件  
        //--状态条件
        //--用户UserID条件
        public const string SQL_TOTAL = @"SELECT COUNT(*) FROM {0} a WHERE 1=1 
                                                                    {1} 
                                                                    {2}
                                                                    {3}
                                                                    {4}";
        public const string SQL_CURRENT = @"select * from (
            SELECT row_number() over(ORDER BY {0} {1} ) as RowNumber, *
            FROM {2} a WHERE 1=1
                        {3}
                        {4}
                        {5}
                        {6}
            ) tt where RowNumber BETWEEN {7} and {8} ";
        #endregion

        public const string SQL_HistoryData = "select * from View_DevHistoryData";
        public const string SQL_View_MonitorTree = "select * from View_MonitorTree";

        #region 设备报警记录查询语句

        public const string SQL_ViewStatusCondition =
            @",(case (select count(0) from T_SYS_USER_RELATION_DEV_ALMRECORD where UserID={0} and DeviceAlmRecordID=almr.AlmRecordID)
            when 0 then 0 else 1 end) ViewStatus";
        //{0}：ViewStatus字段，当需要根据ViewStatus字段查询时，再处理
        public const string SQL_View_CountDevAlarmRecord =
            @"SELECT * from
            (SELECT 
            almr.AlmRecordID, almr.DevID, almr.MSiteID,
            (select Name from T_DICT_MEASURE_SITE_TYPE where id=(select MSiteName from T_SYS_MEASURESITE where MSiteID=almr.MSiteID)) as MSiteName,
            almr.SingalID, 
            almr.SingalAlmID,
            (select Name from T_DICT_EIGEN_VALUE_TYPE where id=(select ValueType from T_SYS_VIBRATING_SET_SIGNALALM where SingalAlmID=almr.SingalAlmID)) as SingalValue,
            almr.MonitorTreeID, almr.MSAlmID, almr.AlmStatus,
            almr.BDate, almr.EDate, almr.LatestStartTime
            FROM dbo.T_SYS_DEV_ALMRECORD AS almr) almr1
            where (almr1.SingalValue is not null or (almr1.SingalAlmID=0 and almr1.MSiteName is not null) )";

        //Sort,EnumAlarmRecordType.TrendAlarm,UserID
        public const string SQL_View_QueryDevAlarmRecord =
            @"SELECT * from
            (SELECT 
            almr.AlmRecordID, almr.DevID, 
            (select DevNO from T_SYS_DEVICE where DevID=almr.DevID) as DevNO,
            (select DevName from T_SYS_DEVICE where DevID=almr.DevID) as DevName,
            almr.MSiteID, (select Name from T_DICT_MEASURE_SITE_TYPE where id=(select MSiteName from T_SYS_MEASURESITE where MSiteID=almr.MSiteID)) as MSiteName,
            almr.SingalID, (select Name from T_DICT_VIBRATING_SIGNAL_TYPE where id=(select SingalType from T_SYS_VIBSINGAL where SingalID=almr.SingalID)) as SingalName,
            almr.SingalAlmID, (select Name from T_DICT_EIGEN_VALUE_TYPE where id=(select ValueType from T_SYS_VIBRATING_SET_SIGNALALM where SingalAlmID=almr.SingalAlmID)) as SingalValue,
            almr.MonitorTreeID, almr.MSAlmID, almr.AlmStatus,
            almr.SamplingValue, almr.WarningValue, almr.DangerValue, almr.DangerValue as ThrendAlarmPrvalue ,almr.BDate, almr.EDate, almr.AddDate, almr.[Content], 
            almr.LatestStartTime
            ,(case (select count(0) from T_SYS_USER_RELATION_DEV_ALMRECORD where UserID={0} and DeviceAlmRecordID=almr.AlmRecordID)
            when 0 then 0 else 1 end) ViewStatus
            FROM dbo.T_SYS_DEV_ALMRECORD AS almr) almr1
            where (almr1.SingalValue is not null or (almr1.SingalAlmID=0 and almr1.MSiteName is not null) )";

        public const string SQL_TOTAL_DEVALMRECORD_LAST = @"with a as({0})
            SELECT count(0) from a
            RIGHT JOIN (
                SELECT  DevID,MSAlmID ,MAX(LatestStartTime) LatestStartTime
                FROM  a GROUP BY DevID,MSAlmID ) b 
                    ON a.DevID=b.DevID AND b.MSAlmID=a.MSAlmID AND b.LatestStartTime=a.LatestStartTime
            where 1=1 {1}";

        public const string SQL_DEVALMRECORD_LAST_PAGE = @"with a as({0})
            SELECT * from
            (SELECT row_number() over(ORDER BY a.{1} {2}) as RowNumber, a.* FROM a
            RIGHT JOIN (
            SELECT  DevID,MSAlmID ,MAX(LatestStartTime) LatestStartTime
            FROM a GROUP BY DevID,MSAlmID ) b 
                ON a.DevID=b.DevID AND b.MSAlmID=a.MSAlmID AND b.LatestStartTime=a.LatestStartTime
                where 1=1 {3}) tt
            where RowNumber BETWEEN {4} and {5};";

        public const string SQL_DEVALMRECORD_LAST = @"with a as({0})
            select a.* from a
            RIGHT JOIN (
            SELECT  DevID,MSAlmID ,MAX(LatestStartTime) LatestStartTime
            FROM  a GROUP BY DevID,MSAlmID ) b 
                ON a.DevID=b.DevID AND b.MSAlmID=a.MSAlmID AND b.LatestStartTime=a.LatestStartTime
            where 1=1 {1};";

        public const string SQL_TOTAL_DEVALMRECORD = @"with a as({0})
            select count(0) from a where 1=1 {1}";

        public const string SQL_DEVALMRECORD_PAGE = @"with a as({0})
            SELECT * FROM (
            SELECT row_number() over(ORDER BY {1} {2} ) as RowNumber, a.*
            FROM a WHERE 1=1 {3}
            ) tt where RowNumber BETWEEN {4} and {5} ";

        public const string SQL_DEVALMRECORD = @"with a as({0})
            SELECT a.* FROM a where 1=1 {1}";

        #endregion

        /// <summary>
        /// 转频的默认值/缺省值
        /// </summary>
        public const string RotationFrequency_Default = "-";

        public const string DevHistoryDisplayStr = "历史数据显示相关";

        public static Dictionary<string, object> ErrorCode = new Dictionary<string, object>();

        public const string SQL_TerminateAllAlarmsOverDevice = "exec SP_TerminateAllAlarmsOverDevice @DevId";

        public const string SQL_GetWSMaintainSimpleInfo = @"SELECT  T.*
            FROM    ( SELECT    a.MaintainReportID ,
                                a.MaintainReportName ,
                                a.ReportType ,
                                a.UpdateDate ,
                                b.UserID
                      FROM      dbo.T_SYS_MAINTAIN_REPORT a
                                LEFT JOIN dbo.T_SYS_USER_RELATION_WS b ON a.DeviceID = b.WSID
                      WHERE     a.ReportType = 2
                                AND b.UserID = {0}
                                AND a.IsTemplate = 0
                    ) T
            WHERE   NOT EXISTS ( SELECT 1
                                 FROM   dbo.T_SYS_USER_RELATION_MAINTAIN_REPORT
                                 WHERE  UserID = T.UserID
                                        AND MaintainReportID = T.MaintainReportID);";
    }
}