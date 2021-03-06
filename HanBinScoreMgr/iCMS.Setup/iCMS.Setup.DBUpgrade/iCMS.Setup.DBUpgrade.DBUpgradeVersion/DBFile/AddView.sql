USE [iCMSDB]


--删除老的视图，添加新视图
DROP VIEW View_ACCHistoryData;
DROP VIEW View_DevHistoryData;
DROP VIEW View_DeviceTempHistortyData;
DROP VIEW View_DISPHistoryData;
DROP VIEW View_ENVLHistoryData;
DROP VIEW View_Get_WS_Status;
DROP VIEW View_Get_WS_Status_ForTrigger;
DROP VIEW View_LQHistoryData;
DROP VIEW View_MonitorTree;
DROP VIEW View_VELHistoryData;
DROP VIEW ViewGetMSInfo;

GO
/****** Object:  View [dbo].[View_ACCHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_ACCHistoryData]
AS
SELECT   acc.MsiteID, mst.Name AS MSiteName, acc.DevID, dev.DevName, NULL AS TempValue, NULL AS TempWarnSet, NULL 
                AS TempAlarmSet, NULL AS TempStat, NULL AS SpeedVirtualValue, NULL AS SpeedVirtualValueWarnSet, NULL 
                AS SpeedVirtualValueAlarmSet, NULL AS SpeedVirtualValueStat, acc.PeakValue AS ACCPEAKValue, 
                acc.PeakWarnValue AS ACCPEAKValueWarnSet, acc.PeakAlmValue AS ACCPEAKValueAlarmSet, 
                (CASE WHEN acc.PeakValue IS NULL OR
                acc.PeakValue <= acc.PeakWarnValue THEN 1 WHEN acc.PeakValue > acc.PeakWarnValue AND 
                acc.PeakValue <= acc.PeakAlmValue THEN 2 ELSE 3 END) AS ACCPEAKValueStat, NULL AS LQValue, NULL 
                AS LQWarnSet, NULL AS LQAlarmSet, NULL AS LQStat, NULL AS DisplacementDPEAKValue, NULL 
                AS DisplacementDPEAKValueWarnSet, NULL AS DisplacementDPEAKValueAlarmSet, NULL 
                AS DisplacementDPEAKValueStat, NULL AS EnvelopPEAKValue, NULL AS EnvelopPEAKValueWarnSet, NULL 
                AS EnvelopPEAKValueAlmSet, NULL AS EnvelopPEAKValueStat, acc.SamplingDate AS CollectitTime, 
                2 AS DataType
FROM      dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC AS acc LEFT OUTER JOIN
                dbo.T_SYS_MEASURESITE AS ms ON acc.MsiteID = ms.MSiteID LEFT OUTER JOIN
                dbo.T_DICT_MEASURE_SITE_TYPE AS mst ON ms.MSiteName = mst.ID LEFT OUTER JOIN
                dbo.T_SYS_DEVICE AS dev ON acc.DevID = dev.DevID
WHERE   EXISTS
                    (SELECT   MSiteID
                     FROM      dbo.T_SYS_MEASURESITE
                     WHERE   (MSiteID = acc.MsiteID)) AND (acc.SingalID IN
                    (SELECT   SingalID
                     FROM      dbo.T_SYS_VIBSINGAL
                     WHERE   (MSiteID = acc.MsiteID)))



GO
/****** Object:  View [dbo].[View_DeviceTempHistortyData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_DeviceTempHistortyData] AS 
select TEMP.MsiteID as MSiteID ,mst.Name as MSiteName,
ms.DevID,dev.DevName,
		TEMP.MsDataValue AS TempValue,
		TEMP.WarnValue AS TempWarnSet,
		TEMP.AlmValue AS TempAlarmSet,
		TEMP.Status AS TempStat,
null as SpeedVirtualValue,
null as SpeedVirtualValueWarnSet,
null as SpeedVirtualValueAlarmSet,
null as SpeedVirtualValueStat,
null as ACCPEAKValue,
null as ACCPEAKValueWarnSet,
null as ACCPEAKValueAlarmSet,
null as ACCPEAKValueStat,
null as LQValue,
null as LQWarnSet,
null as LQAlarmSet,
null as LQStat,
null as DisplacementDPEAKValue,
null as DisplacementDPEAKValueWarnSet,
null as DisplacementDPEAKValueAlarmSet,
null as DisplacementDPEAKValueStat,
null as EnvelopPEAKValue,
null as EnvelopPEAKValueWarnSet,
null as EnvelopPEAKValueAlmSet,
null as EnvelopPEAKValueStat,
TEMP.SamplingDate as CollectitTime,
6 as DataType

 from
(
select * from T_DATA_TEMPE_DEVICE_MSITEDATA_1 WHERE EXISTS (
		SELECT
			MSiteID
		FROM
			T_SYS_MEASURESITE WHERE MsiteID = T_DATA_TEMPE_DEVICE_MSITEDATA_1.MsiteID
	)
union all 
select * from T_DATA_TEMPE_DEVICE_MSITEDATA_2 WHERE EXISTS (
		SELECT
			MSiteID
		FROM
			T_SYS_MEASURESITE WHERE MsiteID = T_DATA_TEMPE_DEVICE_MSITEDATA_2.MsiteID
	)
union ALL
select * from T_DATA_TEMPE_DEVICE_MSITEDATA_3 WHERE EXISTS (
		SELECT
			MSiteID
		FROM
			T_SYS_MEASURESITE WHERE MsiteID = T_DATA_TEMPE_DEVICE_MSITEDATA_3.MsiteID
	)
union ALL
select * from T_DATA_TEMPE_DEVICE_MSITEDATA_4 WHERE EXISTS (
		SELECT
			MSiteID
		FROM
			T_SYS_MEASURESITE WHERE MsiteID = T_DATA_TEMPE_DEVICE_MSITEDATA_4.MsiteID
	)
) as TEMP
LEFT JOIN T_SYS_MEASURESITE ms on TEMP.MsiteID = ms.MSiteID
left join T_DICT_MEASURE_SITE_TYPE mst on ms.MSiteName = mst.ID
left join T_SYS_DEVICE dev on ms.DevID = dev.DevID

where EXISTS (select MSiteID from T_SYS_MEASURESITE where MSiteID = TEMP.MSiteID)












GO
/****** Object:  View [dbo].[View_DISPHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_DISPHistoryData] AS 
SELECT
	disp.MsiteID AS MSiteID,
	mst.Name as MSiteName,
	disp.DevID,
	dev.DevName,
	NULL AS TempValue,
	null as TempWarnSet,
	null as TempAlarmSet,
	null as TempStat,
	null as SpeedVirtualValue,
	null as SpeedVirtualValueWarnSet,
	null as SpeedVirtualValueAlarmSet,
	null as SpeedVirtualValueStat,
	null as ACCPEAKValue ,
	null as ACCPEAKValueWarnSet,
	null as ACCPEAKValueAlarmSet,
	null AS ACCPEAKValueStat,
	null as LQValue,
	null as LQWarnSet,
	null as LQAlarmSet,
	null as LQStat,
	disp.PeakPeakValue as DisplacementDPEAKValue,
	disp.PeakPeakWarnValue as DisplacementDPEAKValueWarnSet,
	disp.PeakPeakAlmValue as DisplacementDPEAKValueAlarmSet,
	(CASE WHEN disp.PeakPeakValue is null or disp.PeakPeakValue <= disp.PeakPeakWarnValue THEN 1 WHEN disp.PeakPeakValue > disp.PeakPeakWarnValue AND 
									 disp.PeakPeakValue <= disp.PeakPeakAlmValue THEN 2 ELSE 3 END)   as DisplacementDPEAKValueStat,
null as EnvelopPEAKValue,
null as EnvelopPEAKValueWarnSet,
null as EnvelopPEAKValueAlmSet,
null as EnvelopPEAKValueStat,	
disp.SamplingDate as CollectitTime,
 4 as DataType
FROM
	T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP disp
LEFT JOIN T_SYS_MEASURESITE ms ON disp.msiteid = ms.msiteid
LEFT JOIN T_DICT_MEASURE_SITE_TYPE mst ON ms.MSiteName = mst.ID
LEFT JOIN T_SYS_DEVICE dev ON disp.DevID = dev.DevID


WHERE EXISTS (SELECT MSiteID FROM T_SYS_MEASURESITE WHERE MsiteID = disp.MsiteID)
AND disp.SingalID IN (SELECT SingalID FROM T_SYS_VIBSINGAL WHERE  MsiteID = disp.MsiteID )











GO
/****** Object:  View [dbo].[View_ENVLHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_ENVLHistoryData] AS 
SELECT
	envp.MsiteID AS MSiteID,
	mst.Name as MSiteName,
	envp.DevID,
	dev.DevName,
	NULL AS TempValue,
	null as TempWarnSet,
	null as TempAlarmSet,
	null as TempStat,
	null as SpeedVirtualValue,
	null as SpeedVirtualValueWarnSet,
	null as SpeedVirtualValueAlarmSet,
	null as SpeedVirtualValueStat,
	null as ACCPEAKValue ,
	null as ACCPEAKValueWarnSet,
	null as ACCPEAKValueAlarmSet,
	null AS ACCPEAKValueStat,
	null as LQValue,
	null as LQWarnSet,
	null as LQAlarmSet,
	null as LQStat,
  null as DisplacementDPEAKValue,
	null as DisplacementDPEAKValueWarnSet,
	null as DisplacementDPEAKValueAlarmSet,
	null  as DisplacementDPEAKValueStat,
envp.PeakValue as EnvelopPEAKValue,
envp.PeakWarnValue as EnvelopPEAKValueWarnSet,
envp.PeakAlmValue as EnvelopPEAKValueAlmSet,
	(CASE WHEN envp.PeakValue is null or envp.PeakValue <= envp.PeakWarnValue THEN 1 WHEN envp.PeakValue > envp.PeakWarnValue AND 
									 envp.PeakValue <= envp.PeakAlmValue THEN 2 ELSE 3 END)  as EnvelopPEAKValueStat,	
envp.SamplingDate as CollectitTime,
 3 as DataType
FROM
	T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL envp
LEFT JOIN T_SYS_MEASURESITE ms ON envp.msiteid = ms.msiteid
LEFT JOIN T_DICT_MEASURE_SITE_TYPE mst ON ms.MSiteName = mst.ID
LEFT JOIN T_SYS_DEVICE dev ON envp.DevID = dev.DevID

WHERE EXISTS(SELECT MSiteID FROM T_SYS_MEASURESITE WHERE MSiteID = envp.MsiteID)
AND envp.SingalID IN (SELECT SingalID FROM T_SYS_VIBSINGAL WHERE  MsiteID = envp.MsiteID )











GO
/****** Object:  View [dbo].[View_LQHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_LQHistoryData] AS 
select lq.MSITEID as MSiteID, mst.Name as MSiteName,lq.DevID,dev.DevName, 
null as TempValue,
null as TempWarnSet,
null as TempAlarmSet,
null as TempStat,
null as SpeedVirtualValue,
null as SpeedVirtualValueWarnSet,
null as SpeedVirtualValueAlarmSet,
null as  SpeedVirtualValueStat,
null as ACCPEAKValue,
null as ACCPEAKValueWarnSet, 
null as ACCPEAKValueAlarmSet,
null as ACCPEAKValueStat,
lq.LQValue as LQValue,

lq.LQWarnValue as LQWarnSet,
lq.LQAlmValue as LQAlarmSet,
lq.AlmStatus as LQStat,
null as DisplacementDPEAKValue,
null as DisplacementDPEAKValueWarnSet,
null as DisplacementDPEAKValueAlarmSet,
null as DisplacementDPEAKValueStat,
null as EnvelopPEAKValue,
null as EnvelopPEAKValueWarnSet,
null as EnvelopPEAKValueAlmSet,
null as EnvelopPEAKValueStat,
lq.SamplingDate as CollectitTime,
5 as DataType

from T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ lq
left join T_SYS_MEASURESITE ms on lq.MSITEID = ms.MSiteID
left join T_DICT_MEASURE_SITE_TYPE mst on ms.MSiteName = mst.ID
LEFT JOIN T_SYS_DEVICE dev on lq.DevID = dev.DevID

WHERE EXISTS(SELECT MSiteID FROM T_SYS_MEASURESITE WHERE MSiteID = lq.MsiteID)
AND lq.SingalID IN (SELECT SingalID FROM T_SYS_VIBSINGAL WHERE  MsiteID = lq.MsiteID )











GO
/****** Object:  View [dbo].[View_VELHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_VELHistoryData] AS 
select vel.MsiteID AS MSiteID,
mst.Name as MSiteName,
vel.DevID,
dev.DevName,
null as TempValue,
null as TempWarnSet,
null as TempAlarmSet,
null as TempStat,

vel.EffValue as SpeedVirtualValue,
vel.EffWarnValue as SpeedVirtualValueWarnSet,
vel.EffAlmValue as SpeedVirtualValueAlarmSet,

	(CASE WHEN (vel.EffValue is null or vel.EffValue <= vel.EffWarnValue) THEN 1 WHEN vel.EffValue > vel.EffWarnValue AND 
									 vel.EffValue <= vel.EffAlmValue THEN 2 ELSE 3 END)  as SpeedVirtualValueStat,
	null as ACCPEAKValue ,
	null as ACCPEAKValueWarnSet,
	null as ACCPEAKValueAlarmSet,
null as  ACCPEAKValueStat,
	null as LQValue,
	null as LQWarnSet,
	null as LQAlarmSet,
	null as LQStat,
	null as DisplacementDPEAKValue,
	null as DisplacementDPEAKValueWarnSet,
	null as DisplacementDPEAKValueAlarmSet,
	null as DisplacementDPEAKValueStat,

 null as EnvelopPEAKValue,
null as EnvelopPEAKValueWarnSet,
null as EnvelopPEAKValueAlmSet,
null as EnvelopPEAKValueStat,
	vel.SamplingDate as CollectitTime,

  1 as DataType
from T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL vel
left join T_SYS_MEASURESITE ms on vel.MsiteID=  ms.MSiteID 
left join T_DICT_MEASURE_SITE_TYPE mst  on ms.MSiteName  = mst.ID
LEFT JOIN T_SYS_DEVICE dev on vel.DevID = dev.DevID

WHERE EXISTS(SELECT MSiteID FROM T_SYS_MEASURESITE WHERE MSiteID = vel.MsiteID)
AND vel.SingalID IN (SELECT SingalID FROM T_SYS_VIBSINGAL WHERE  MsiteID = vel.MsiteID )











GO
/****** Object:  View [dbo].[View_DevHistoryData]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[View_DevHistoryData] as 
select * from View_DeviceTempHistortyData 
	UNION ALL 
select * from View_LQHistoryData 
	union ALL
select * from View_VELHistoryData 
	union all 
select * from View_ACCHistoryData 
	union all 
select * from View_DISPHistoryData 
	union all 
select * from View_ENVLHistoryData



GO
/****** Object:  View [dbo].[a]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[a]
AS
SELECT * FROM dbo.T_SYS_USER AS TSU

GO
/****** Object:  View [dbo].[GetMeasureSiteInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[GetMeasureSiteInfo]
AS
    SELECT  TSM.MSiteID WID ,
            TSM.MSiteName MSiteTypeId ,
			TDMST.Name MStieTypeName,
            TSM.DevID ,
            ISNULL(TSM.WSID,-1) WSID ,
            TSW.WSName ,
            ISNULL(TSW.LinkStatus,0)  LinkStatus,
            TSM.MeasureSiteType ,
            TSM.SensorCosA ,
            TSM.SensorCosB ,
            TSM.MSiteStatus ,
            TSM.MSiteSDate ,
            TSM.WaveTime ,
            TSM.FlagTime ,
            TSM.TemperatureTime ,
            TSM.Remark ,
            TSM.Position ,
            TSM.SerialNo ,
			ISNULL(TSM.BearingID,-1) BearingID ,
            TSM.BearingType ,
            TSM.LubricatingForm ,
            TSM.AddDate ,
            '' ConfigMSDate ,
            1 Type ,
            '测量位置' TypeName ,
            TSW2.WGName,
		    ISNULL(TSW.TriggerStatus,0) TriggerStatus,
			(SELECT COUNT(1) FROM dbo.T_SYS_VIBSINGAL AS TSV WHERE TSV.MSiteID=TSM.MSiteID)
			+(SELECT COUNT(1) FROM dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM  WHERE TSTDSM.MSiteID=TSM.MSiteID)
			+(SELECT COUNT(1) FROM dbo.T_SYS_VOLTAGE_SET_MSITEALM AS TSVSM  WHERE TSVSM.MSiteID=TSM.MSiteID)
			+(SELECT COUNT(1) FROM dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM  WHERE TSTWSM.MSiteID=TSM.MSiteID)
			AS ChildrenCount,
			TSB.FactoryName,
            TSB.BearingNum,
            TSB.FactoryID,
			0 OperationStatus

			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
    FROM    dbo.T_SYS_MEASURESITE AS TSM
            LEFT JOIN dbo.T_SYS_WS AS TSW ON TSW.WSID = TSM.WSID
            LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName
            LEFT JOIN dbo.T_SYS_WG AS TSW2 ON TSW2.WGID = TSW.WGID
			LEFT JOIN dbo.T_SYS_BEARING AS TSB ON TSB.BearingID = TSM.BearingID







GO
/****** Object:  View [dbo].[GetOperationList]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[GetOperationList]
AS
    SELECT  MAX(TSO.id) id ,
            OperationResult ,
            TSO.EDate  ,
            TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID
    FROM    dbo.T_SYS_OPERATION AS TSO
            LEFT JOIN dbo.T_SYS_MEASURESITE AS TSM ON TSO.MSID = TSM.MSiteID
    GROUP BY EDate,OperationResult, TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID





GO
/****** Object:  View [dbo].[ServerTree]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ServerTree]
AS
    SELECT  TSW.WGID TreeID ,
            TSW.WGName Name ,
            0 ParentID ,
            1 [Level] ,
            TSW.WGID TrueId ,
            0 DeviceId
    FROM    dbo.T_SYS_WG AS TSW
    UNION
    SELECT  ( SELECT    MAX(WGID)
              FROM      T_SYS_WG
            ) + TSW2.WSID TreeID ,
            TSW2.WSName Name ,
            TSW2.WGID ParentID ,
            2 [Level] ,
            TSW2.WSID TrueId ,
            0 DeviceId
    FROM    dbo.T_SYS_WS AS TSW2
    UNION
    SELECT  ( SELECT    MAX(TSW.WSID) + ( SELECT    MAX(WGID)
                                          FROM      T_SYS_WG
                                        )
              FROM      dbo.T_SYS_WS AS TSW
            ) + TSM.MSiteID TreeID ,
            TDMST.Name Name ,
            ( SELECT    MAX(TSW.WSID) + ( SELECT    MAX(WGID)
                                          FROM      T_SYS_WG
                                        )
              FROM      dbo.T_SYS_WS AS TSW
            ) ParentID ,
            3 [Level] ,
            TSM.MSiteID TrueId ,
            TSM.DevID DeviceId
    FROM    dbo.T_SYS_MEASURESITE AS TSM
            LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName;








GO
/****** Object:  View [dbo].[Tree]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Tree]
AS
SELECT   TSMT.MonitorTreeID, TSMT.Name, TSMT.PID, TDMT.Describe[Level], TSMT.MonitorTreeID TrueId,1 as MTStatus,-1 as UseType
FROM      dbo.T_SYS_MONITOR_TREE AS TSMT LEFT JOIN
                dbo.T_DICT_MONITORTREE_TYPE AS TDMT ON TDMT.ID = TSMT.Type
UNION
SELECT   (SELECT   MAX(MonitorTreeID)
                 FROM      dbo.T_SYS_MONITOR_TREE) + 1 + TSD.DevID, TSD.DevName, TSD.MonitorTreeID, 5 [Level], 
                TSD.DevID TrueId,CASE TSD.RunStatus WHEN 3 THEN
			 4
			 ELSE
			 TSD.AlmStatus
			 END MTStatus,UseType
FROM      dbo.T_SYS_DEVICE AS TSD;




GO
/****** Object:  View [dbo].[View_DeviceTree]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[View_DeviceTree]
AS

SELECT  TSMT.MonitorTreeID ,
        TSMT.Name ,
        TSMT.PID ,
        TDMT.Describe [Level] ,
        TSMT.MonitorTreeID TrueId ,
        1 AS MTStatus ,
        -1 AS UseType,
		TDMT.Code
FROM    dbo.T_SYS_MONITOR_TREE AS TSMT
        LEFT JOIN dbo.T_DICT_MONITORTREE_TYPE AS TDMT ON TDMT.ID = TSMT.Type  
WHERE TDMT.IsUsable=1
UNION
SELECT  ( SELECT    MAX(MonitorTreeID)
          FROM      dbo.T_SYS_MONITOR_TREE
        ) + 1 + TSD.DevID ,
        TSD.DevName ,
        TSD.MonitorTreeID ,
       ( SELECT COUNT(1)+1 FROM T_DICT_MONITORTREE_TYPE WHERE IsUsable=1) [Level] ,
        TSD.DevID TrueId ,
        CASE TSD.RunStatus
          WHEN 3 THEN 4
          ELSE TSD.AlmStatus
        END MTStatus ,
        UseType,
		'DEVICE' Code
FROM    dbo.T_SYS_DEVICE AS TSD;



GO
/****** Object:  View [dbo].[View_Get_WS_Status]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[View_Get_WS_Status] AS
SELECT  Ws.FirmwareVersion ,
        Ws.WSID ,
        Ws.MACADDR MAC ,
        MeasureSiteType.Name MSName ,
        Ws.LinkStatus LinkStatu ,
        Ws.WSName ,
        Ws.UseStatus ,
		Operation.OperationType,
		MeasureSite.MSiteID MSID,
        CAST(Operation.DAQStyle AS NVARCHAR(8)) AS CMDType ,
        CASE Operation.OperationType
          WHEN 1 THEN ( SELECT  TSO.OperationResult
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END ConfigStatu ,
        CASE Operation.OperationType
          WHEN 2 THEN ( SELECT  TSO.OperationResult
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END UpdateStatu,
	  CASE Operation.OperationType
          WHEN 3 THEN ( SELECT  TSO.OperationResult
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END TriggerStatus,
		    CASE Operation.OperationType
          WHEN 1 THEN ( SELECT  TSO.EDate
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END EdateForConfig ,
        CASE Operation.OperationType
          WHEN 2 THEN ( SELECT  TSO.EDate
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END EdateForUpdate,
		 CASE Operation.OperationType
          WHEN 3 THEN ( SELECT  TSO.EDate
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END EdateForTrigger

FROM    dbo.T_SYS_WS Ws
        LEFT JOIN dbo.T_SYS_MEASURESITE AS MeasureSite ON MeasureSite.WSID = Ws.WSID
        LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS MeasureSiteType ON MeasureSiteType.ID = MeasureSite.MSiteName
        LEFT JOIN ( SELECT  DAQStyle ,
                            MSID ,
                            MAX(TSO.id) maxId ,
                            TSO.OperationType
                    FROM    dbo.T_SYS_OPERATION AS TSO
                    GROUP BY DAQStyle ,
                            MSID ,
                            TSO.OperationType
                  ) AS Operation ON Operation.MSID = MeasureSite.MSiteID
















GO
/****** Object:  View [dbo].[View_Get_WS_Status_ForTrigger]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[View_Get_WS_Status_ForTrigger] AS
SELECT  Ws.FirmwareVersion ,
        Ws.WSID ,
        Ws.MACADDR MAC ,
        MeasureSiteType.Name MSName ,
        Ws.LinkStatus LinkStatu ,
        Ws.WSName ,
        Ws.UseStatus ,
		Operation.OperationType,
		MeasureSite.MSiteID MSID,
        CAST(Operation.DAQStyle AS NVARCHAR(8)) AS CMDType ,

	  CASE Operation.OperationType
          WHEN 3 THEN ( SELECT  TSO.OperationResult
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END TriggerStatus,

		 CASE Operation.OperationType
          WHEN 3 THEN ( SELECT  TSO.EDate
                        FROM    dbo.T_SYS_OPERATION AS TSO
                        WHERE   TSO.id = Operation.maxId
                      )
        END EdateForTrigger

FROM    dbo.T_SYS_WS Ws
        LEFT JOIN dbo.T_SYS_MEASURESITE AS MeasureSite ON MeasureSite.WSID = Ws.WSID
        LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS MeasureSiteType ON MeasureSiteType.ID = MeasureSite.MSiteName
        LEFT JOIN ( SELECT  DAQStyle ,
                            MSID ,
                            MAX(TSO.id) maxId ,
                            TSO.OperationType
                    FROM    dbo.T_SYS_OPERATION AS TSO
                    GROUP BY DAQStyle ,
                            MSID ,
                            TSO.OperationType
                  ) AS Operation ON Operation.MSID = MeasureSite.MSiteID
 WHERE Operation.OperationType=3 OR Operation.OperationType IS NULL


















GO
/****** Object:  View [dbo].[View_GetDeviceAlarmStat]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--====================================================
--  作    者：王颖辉 
--  创建时间：2017年10月17日09:53:34
--  功能简述：获取设备报警状态统计功能
--  参数描述：无
--  修改历史：
--  修 改 人：
--  修改时间：
--  修改原因：
--====================================================
CREATE VIEW [dbo].[View_GetDeviceAlarmStat]
AS
SELECT  TSMT.MonitorTreeID ,
        TSMT.Name ,
        TSMT.Type ,
        TSD.AlmStatus ,
		TSMT.PID,
        COUNT(1) DeviceCount 
FROM    dbo.T_SYS_MONITOR_TREE AS TSMT
        CROSS APPLY F_GetDeivceByMonitorTreeId(TSMT.MonitorTreeID) F
        LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = F.DeviceId
GROUP BY TSMT.MonitorTreeID ,
        TSMT.Name ,
        TSMT.Type ,
        TSD.AlmStatus,
        TSMT.PID

GO
/****** Object:  View [dbo].[View_GetDeviceRunStat]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--====================================================
--  作    者：王颖辉 
--  创建时间：2017年10月17日09:53:34
--  功能简述：获取设备运行状态统计功能
--  参数描述：无
--  修改历史：
--  修 改 人：
--  修改时间：
--  修改原因：
--====================================================
CREATE VIEW [dbo].[View_GetDeviceRunStat]
AS
SELECT  TSMT.MonitorTreeID ,
        TSMT.Name ,
        TSMT.Type ,
        TSD.RunStatus ,
		TSMT.PID,
        COUNT(1) DeviceCount 
FROM    dbo.T_SYS_MONITOR_TREE AS TSMT
        CROSS APPLY F_GetDeivceByMonitorTreeId(TSMT.MonitorTreeID) F
        LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = F.DeviceId
GROUP BY TSMT.MonitorTreeID ,
        TSMT.Name ,
        TSMT.Type ,
        TSD.RunStatus,
        TSMT.PID

GO
/****** Object:  View [dbo].[View_GetLastWSSamplingInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE VIEW [dbo].[View_GetLastWSSamplingInfo]
 AS
    SELECT  a.LatestStartTime ,
            a.MSAlmID ,
            a.WSID ,
            a.SamplingValue,
			a.AlmStatus,
			a.MSiteID
    FROM    T_SYS_WSN_ALMRECORD a
            INNER JOIN ( SELECT MAX(b.LatestStartTime) MaxDate ,
                                b.MSAlmID ,
                                b.WSID
                         FROM   T_SYS_WSN_ALMRECORD b
                         GROUP BY b.MSAlmID ,
                                b.WSID
                       ) c ON c.MaxDate = a.LatestStartTime
                              AND c.MSAlmID = a.MSAlmID



GO
/****** Object:  View [dbo].[View_GetMeasureSiteInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 CREATE VIEW [dbo].[View_GetMeasureSiteInfo]
 AS
    SELECT  TSD.DevID DeviceID ,
            TSD.DevName DeviceName ,
            TSM.MSiteID MeasureSiteID ,
            TDMST.Name MeasureSite ,
            TSM.LubricatingForm LubricatingForm ,
            TSM.AddDate AddDate ,
            TSTDSM.MsiteAlmID DeviceTemperatureMsiteAlmID ,
            TSTDSM.WarnValue DeviceTemperatureAlarmValue ,
            TSTDSM.AlmValue DeviceTemperatureDangerValue ,
			TSTDSM.AddDate DeviceTemperatureAddDate,
            ( SELECT    Name
              FROM      dbo.T_DICT_MEASURE_SITE_MONITOR_TYPE
              WHERE     Code = 'MEASURESITEMONITORTYPE_1_SBWD'
            ) DeviceTemperatureName ,
            TSTWSM.MsiteAlmID WSTemperatureMsiteAlmID ,
            TSTWSM.WarnValue WSTemperatureAlarmValue ,
            TSTWSM.AlmValue WSTemperatureDangerValue ,
            ( SELECT    Name
              FROM      dbo.T_DICT_MEASURE_SITE_MONITOR_TYPE
              WHERE     Code = 'MEASURESITEMONITORTYPE_3_CGQWD'
            ) WSTemperatureName ,
			TSTWSM.AddDate WSTemperatureAddDate,
            TSVSM.MsiteAlmID VoltageMsiteAlmID ,
            TSVSM.WarnValue VoltageAlarmValue ,
            TSVSM.AlmValue VoltageDangerValue ,
            ( SELECT    Name
              FROM      dbo.T_DICT_MEASURE_SITE_MONITOR_TYPE
              WHERE     Code = 'MEASURESITEMONITORTYPE_2_DCDY'
            ) VoltageName ,
			TSVSM.AddDate VoltageAddDate,
            TSM.WaveTime WaveTime ,
            TSM.FlagTime FlagTime ,
            TSM.TemperatureTime TemperatureTime
    FROM    dbo.T_SYS_MEASURESITE AS TSM
            LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName
            LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = TSM.DevID
            LEFT JOIN dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM ON TSTDSM.MsiteID = TSM.MSiteID
            LEFT JOIN dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM ON TSTWSM.MsiteID = TSM.MSiteID
            LEFT JOIN dbo.T_SYS_VOLTAGE_SET_MSITEALM AS TSVSM ON TSVSM.MsiteID = TSM.MSiteID




GO
/****** Object:  View [dbo].[View_GetMonitorTree]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_GetMonitorTree] as

		--监测树
		SELECT TSMT.PID MTPid,
		       TSMT.MonitorTreeID MTId,
			   TSMT.Name MTName,
			   1 MTStatus,
			   (SELECT TDMT.Describe FROM dbo.T_DICT_MONITORTREE_TYPE AS TDMT WHERE TDMT.ID=TSMT.Type) MTType,
			   TSMT.Name Remark,
			   TSMT.MonitorTreeID RecordID
			    FROM dbo.T_SYS_MONITOR_TREE AS TSMT
	   UNION

	   --设备树
	   SELECT TSD.MonitorTreeID MTPid,
	         TSD.DevID+(SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT) MTId,
		 	 CASE TSD.RunStatus WHEN 3 THEN
			 TSD.DevName+N'(stop)'
			 ELSE
			 TSD.DevName
			 END MTName,
			 CASE TSD.RunStatus WHEN 3 THEN
			 4
			 ELSE
			 TSD.AlmStatus
			 END MTStatus,
			 5 MTType,
			 TSD.DevName Remark,
             TSD.DevID RecordID
			 FROM dbo.T_SYS_DEVICE AS TSD
	   UNION

		--测量位置
		SELECT TSM.DevID + (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT) MTPid,
		      TSM.MSiteID +(SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) MTId,
		      (SELECT TDMST.Name FROM dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST WHERE TDMST.ID =TSM.MSiteName) MTName,
			  TSM.MSiteStatus MTStatus,
			  6 MTType,
			  (SELECT TSW.WSName FROM dbo.T_SYS_WS AS TSW WHERE TSW.WSID=TSM.WSID) Remark,
			  TSM.MSiteID RecordID
		      FROM dbo.T_SYS_MEASURESITE AS TSM
		UNION

		--设备温度
		SELECT TSTWSM.MsiteID+(SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) MTPid,
		TSTWSM.MsiteAlmID+(SELECT (SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) + MAX(TSM.MSiteID) FROM dbo.T_SYS_MEASURESITE AS TSM ) MTId,
		'设备温度' MTName,
		TSTWSM.Status MTStatus,
        9 MTType,
	    '设备温度' Remark,
		TSTWSM.MsiteAlmID RecordID
		FROM dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM

		--振动信号
		UNION
		SELECT TSV.MSiteID+(SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) MTPid,
		TSV.SingalID+(SELECT (SELECT (SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) + MAX(TSM.MSiteID) FROM dbo.T_SYS_MEASURESITE AS TSM )+MAX(TSTDSM.MsiteAlmID) FROM dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM)  MTId,
		(SELECT TDVST.Name FROM dbo.T_DICT_VIBRATING_SIGNAL_TYPE AS TDVST WHERE TDVST.ID=TSV.SingalType) MTName,
		TSV.SingalStatus MTStatus,
		7 MTType,
	   (SELECT TDVST.Name FROM dbo.T_DICT_VIBRATING_SIGNAL_TYPE AS TDVST WHERE TDVST.ID=TSV.SingalType) Remark,
	   	TSV.SingalID RecordID
	    FROM dbo.T_SYS_VIBSINGAL AS TSV

		UNION
		--特征值
		SELECT TSVSS.SingalID+(SELECT (SELECT (SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) + MAX(TSM.MSiteID) FROM dbo.T_SYS_MEASURESITE AS TSM )+MAX(TSTDSM.MsiteAlmID) FROM dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM),
		TSVSS.SingalAlmID+(SELECT (SELECT (SELECT (SELECT  (SELECT MAX(TSMT.MonitorTreeID) FROM  dbo.T_SYS_MONITOR_TREE AS TSMT)+ MAX(TSD.DevID) FROM dbo.T_SYS_DEVICE AS TSD) + MAX(TSM.MSiteID) FROM dbo.T_SYS_MEASURESITE AS TSM )+MAX(TSTDSM.MsiteAlmID) FROM dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM)+MAX(TSV.SingalID) FROM dbo.T_SYS_VIBSINGAL AS TSV),
		(SELECT TDEVT.Name FROM dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT WHERE TDEVT.ID=TSVSS.ValueType) MTName,
		TSVSS.Status MTStatus,
		8 MTType,
        (SELECT TDEVT.Name FROM dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT WHERE TDEVT.ID=TSVSS.ValueType) Remark,
		TSVSS.SingalAlmID RecordID
		FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS



GO
/****** Object:  View [dbo].[View_GetOperationList]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[View_GetOperationList]
AS
    SELECT  MAX(TSO.id) id ,
            OperationResult ,
            TSO.EDate  ,
            TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID
    FROM    dbo.T_SYS_OPERATION AS TSO
            LEFT JOIN dbo.T_SYS_MEASURESITE AS TSM ON TSO.MSID = TSM.MSiteID
    GROUP BY EDate,OperationResult, TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID






GO
/****** Object:  View [dbo].[View_GetTimingMeasureSiteInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--====================================================
--  作    者：王颖辉 
--  创建时间：2017年10月28日
--  功能简述：获取定时测量位置信息
--  参数描述：无
--  修改历史：
--  修 改 人：
--  修改时间：
--  修改原因：
--====================================================
Create VIEW [dbo].[View_GetTimingMeasureSiteInfo]
AS
    SELECT  TSM.MSiteID WID ,
            TSM.MSiteName MSiteTypeId ,
			TDMST.Name MStieTypeName,
            TSM.DevID ,
            ISNULL(TSM.WSID,-1) WSID ,
            TSW.WSName ,
            ISNULL(TSW.LinkStatus,0)  LinkStatus,
            TSM.MeasureSiteType ,
            TSM.SensorCosA ,
            TSM.SensorCosB ,
            TSM.MSiteStatus ,
            TSM.MSiteSDate ,
            TSM.WaveTime ,
            TSM.FlagTime ,
            TSM.TemperatureTime ,
            TSM.Remark ,
            TSM.Position ,
            TSM.SerialNo ,
			ISNULL(TSM.BearingID,-1) BearingID ,
            TSM.BearingType ,
            TSM.LubricatingForm ,
            TSM.AddDate ,
            '' ConfigMSDate ,
            1 Type ,
            '测量位置' TypeName ,
            TSW2.WGName,
		    ISNULL(TSW.TriggerStatus,0) TriggerStatus,
			(SELECT COUNT(1) FROM dbo.T_SYS_VIBSINGAL AS TSV WHERE TSV.MSiteID=TSM.MSiteID AND TSV.DAQStyle=1)
			+(SELECT COUNT(1) FROM dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM  WHERE TSTDSM.MSiteID=TSM.MSiteID)
			+(SELECT COUNT(1) FROM dbo.T_SYS_VOLTAGE_SET_MSITEALM AS TSVSM  WHERE TSVSM.MSiteID=TSM.MSiteID)
			+(SELECT COUNT(1) FROM dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM  WHERE TSTWSM.MSiteID=TSM.MSiteID)
			AS ChildrenCount,
			TSB.FactoryName,
            TSB.BearingNum,
            TSB.FactoryID,
			0 OperationStatus

			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
    FROM    dbo.T_SYS_MEASURESITE AS TSM
            LEFT JOIN dbo.T_SYS_WS AS TSW ON TSW.WSID = TSM.WSID
            LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName
            LEFT JOIN dbo.T_SYS_WG AS TSW2 ON TSW2.WGID = TSW.WGID
			LEFT JOIN dbo.T_SYS_BEARING AS TSB ON TSB.BearingID = TSM.BearingID







GO
/****** Object:  View [dbo].[View_GetTimporayMeasureSiteInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--====================================================
--  作    者：王颖辉 
--  创建时间：2017年10月28日
--  功能简述：获取临时测量位置信息
--  参数描述：无
--  修改历史：
--  修 改 人：
--  修改时间：
--  修改原因：
--====================================================
CREATE VIEW [dbo].[View_GetTimporayMeasureSiteInfo]
AS
    SELECT  TSM.MSiteID WID ,
            TSM.MSiteName MSiteTypeId ,
			TDMST.Name MStieTypeName,
            TSM.DevID ,
            ISNULL(TSM.WSID,-1) WSID ,
            TSW.WSName ,
            ISNULL(TSW.LinkStatus,0)  LinkStatus,
            TSM.MeasureSiteType ,
            TSM.SensorCosA ,
            TSM.SensorCosB ,
            TSM.MSiteStatus ,
            TSM.MSiteSDate ,
            TSM.WaveTime ,
            TSM.FlagTime ,
            TSM.TemperatureTime ,
            TSM.Remark ,
            TSM.Position ,
            TSM.SerialNo ,
			ISNULL(TSM.BearingID,-1) BearingID ,
            TSM.BearingType ,
            TSM.LubricatingForm ,
            TSM.AddDate ,
            '' ConfigMSDate ,
            1 Type ,
            '测量位置' TypeName ,
            TSW2.WGName,
		    ISNULL(TSW.TriggerStatus,0) TriggerStatus,
			(SELECT COUNT(1) FROM dbo.T_SYS_VIBSINGAL AS TSV WHERE TSV.MSiteID=TSM.MSiteID AND TSV.DAQStyle=2)
			AS ChildrenCount,
			TSB.FactoryName,
            TSB.BearingNum,
            TSB.FactoryID,
			0 OperationStatus

			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
			--(SELECT TOP 1 TSO.OperationResult FROM dbo.T_SYS_OPERATION AS TSO WHERE TSO.MSID=tsm.MSiteID ORDER BY TSO.id DESC)  OperationStatus
    FROM    dbo.T_SYS_MEASURESITE AS TSM
            LEFT JOIN dbo.T_SYS_WS AS TSW ON TSW.WSID = TSM.WSID
            LEFT JOIN dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName
            LEFT JOIN dbo.T_SYS_WG AS TSW2 ON TSW2.WGID = TSW.WGID
			LEFT JOIN dbo.T_SYS_BEARING AS TSB ON TSB.BearingID = TSM.BearingID







GO
/****** Object:  View [dbo].[View_GetVibSignalAndEigenValue]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE VIEW [dbo].[View_GetVibSignalAndEigenValue]
 AS
 
  SELECT    TSV.SingalID ,
            TSV.SingalType VibrationSignalTypeID ,
            TDVST.Name VibrationSignalName ,
            TDVST.AddDate ,
            TSV.UpLimitFrequency UpperLimitID ,
            ( SELECT    WaveUpperLimitValue
              FROM      dbo.T_DICT_WAVE_UPPERLIMIT_VALUE
              WHERE     ID = TSV.UpLimitFrequency
            ) UpperLimitValue ,
            TSV.LowLimitFrequency LowLimitID ,
            ( SELECT    WaveLowerLimitValue
              FROM      dbo.T_DICT_WAVE_LOWERLIMIT_VALUE
              WHERE     ID = TSV.LowLimitFrequency
            ) LowLimitValue ,
            TSV.WaveDataLength WaveLengthID,
			    ( SELECT    WaveLengthValue
              FROM      dbo.T_DICT_WAVE_LENGTH_VALUE
              WHERE     ID = TSV.WaveDataLength
            ) WaveLengthValue ,
			TSV.MSiteID,
			TSV.DAQStyle
  FROM      dbo.T_SYS_VIBSINGAL AS TSV
            LEFT JOIN dbo.T_DICT_VIBRATING_SIGNAL_TYPE AS TDVST ON TDVST.ID = TSV.SingalType;


GO
/****** Object:  View [dbo].[View_MonitorTree]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_MonitorTree]
AS
WITH MonitorTree AS (SELECT   MonitorTreeID, Name, PID, Type, 0 AS Lvl
                                    FROM      dbo.T_SYS_MONITOR_TREE AS TSMT
                                    WHERE   (PID = 0)
                                    UNION ALL
                                    SELECT   TSMT.MonitorTreeID, TSMT.Name, TSMT.PID, TSMT.Type, P.Lvl + 1 AS Expr1
                                    FROM      MonitorTree AS P INNER JOIN
                                                    dbo.T_SYS_MONITOR_TREE AS TSMT ON TSMT.PID = P.MonitorTreeID)
    SELECT   MonitorTree.MonitorTreeID, MonitorTree.Name, MonitorTree.Lvl, MonitorTree.PID, TDMT.Describe, TSD.DevID, 
                    TSD.DevName, TSD.RunStatus, TSD.AlmStatus, TSD.UseType, TSM.MSiteID, TDMST.Name AS MeasureSiteName, 
                    TSM.MSiteStatus, TSW.WSName, TSTDSM.MsiteAlmID, TSTDSM.Status AS DeviceTemperatureStatus, TSV.SingalID, 
                    TSV.SingalStatus, TDVST.ID AS VibratingTypeId, TDVST.Name AS VibratingTypeName, TSVSS.SingalAlmID, 
                    TSVSS.Status AS EnginStatus, TDEVT.ID AS EigenTypeId, TDEVT.Name AS EigenTypeName
    FROM      MonitorTree LEFT OUTER JOIN
                    dbo.T_DICT_MONITORTREE_TYPE AS TDMT ON TDMT.ID = MonitorTree.Type LEFT OUTER JOIN
                    dbo.T_SYS_DEVICE AS TSD ON TSD.MonitorTreeID = MonitorTree.MonitorTreeID LEFT OUTER JOIN
                    dbo.T_SYS_MEASURESITE AS TSM ON TSM.DevID = TSD.DevID LEFT OUTER JOIN
                    dbo.T_SYS_WS AS TSW ON TSW.WSID = TSM.WSID LEFT OUTER JOIN
                    dbo.T_DICT_MEASURE_SITE_TYPE AS TDMST ON TDMST.ID = TSM.MSiteName LEFT OUTER JOIN
                    dbo.T_SYS_TEMPE_DEVICE_SET_MSITEALM AS TSTDSM ON TSTDSM.MsiteID = TSM.MSiteID LEFT OUTER JOIN
                    dbo.T_SYS_VIBSINGAL AS TSV ON TSV.MSiteID = TSM.MSiteID AND TSV.DAQStyle = 1 LEFT OUTER JOIN
                    dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS ON TSVSS.SingalID = TSV.SingalID LEFT OUTER JOIN
                    dbo.T_DICT_VIBRATING_SIGNAL_TYPE AS TDVST ON TDVST.ID = TSV.SingalType LEFT OUTER JOIN
                    dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT ON TDEVT.VibratingSignalTypeID = TDVST.ID AND 
                    TDEVT.ID = TSVSS.ValueType




GO
/****** Object:  View [dbo].[ViewGetMSInfo]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewGetMSInfo]
AS
SELECT   w.WSID, w.MACADDR, w.LinkStatus, m.MSiteID, m.MSiteName, m.DevID, m.VibScanID, m.ChannelID, 
                m.MeasureSiteType, m.SensorCosA, m.SensorCosB, m.MSiteStatus, m.MSiteSDate, m.WaveTime, m.FlagTime, 
                m.TemperatureTime, m.Remark, m.Position, m.SerialNo, m.BearingID, m.BearingType, m.BearingModel, 
                m.LubricatingForm, m.AddDate, op.id, op.OperatorKey, op.MSID, op.OperationType, op.Bdate, op.EDate, 
                op.OperationResult, op.OperationReson, op.DAQStyle
FROM      dbo.T_SYS_MEASURESITE AS m INNER JOIN
                dbo.T_SYS_WS AS w ON m.WSID = w.WSID INNER JOIN
                    (SELECT   MSID, MAX(id) AS id
                     FROM      dbo.T_SYS_OPERATION
                     WHERE   (OperationType = 1)
                     GROUP BY MSID) AS o ON m.MSiteID = o.MSID INNER JOIN
                dbo.T_SYS_OPERATION AS op ON o.id = op.id












GO
/****** Object:  View [dbo].[ViewGetOperationList]    Script Date: 2017/12/1 17:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ViewGetOperationList]
AS
    SELECT  MAX(TSO.id) id ,
            OperationResult ,
            TSO.EDate  ,
            TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID
    FROM    dbo.T_SYS_OPERATION AS TSO
            LEFT JOIN dbo.T_SYS_MEASURESITE AS TSM ON TSO.MSID = TSM.MSiteID
    GROUP BY EDate,OperationResult, TSO.MSID ,
            TSO.WSID ,
            TSO.DAQStyle ,
            TSO.OperationType,
			TSM.DevID
GO
