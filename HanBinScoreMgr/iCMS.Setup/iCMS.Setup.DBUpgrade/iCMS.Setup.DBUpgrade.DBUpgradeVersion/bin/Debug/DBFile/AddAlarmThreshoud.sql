USE [iCMSDB]
GO

INSERT INTO [dbo].[T_DATA_REALTIME_ALARMTHRESHOLD]
(
    [MeasureSiteID],
    [MeasureSiteThresholdType],
    [AlarmThresholdValue],
    [DangerThresholdValue],
    [EigenValueType],
    [AddDate],
    [SamplingDate]
)
--设备温度
SELECT *
FROM
(
    SELECT MsiteID MeasureSiteID,
           6 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_DEVICE_MSITEDATA_1
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           6 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_DEVICE_MSITEDATA_2
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           6 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_DEVICE_MSITEDATA_3
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           6 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_DEVICE_MSITEDATA_4
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
) AS TEMPE_DEVICE
--ws温度
UNION
SELECT *
FROM
(
    SELECT MsiteID MeasureSiteID,
           7 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_WS_MSITEDATA_1
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           7 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_WS_MSITEDATA_2
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           7 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_WS_MSITEDATA_3
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           7 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_TEMPE_WS_MSITEDATA_4
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
) AS TEMPE_WS
UNION
--电池电压
SELECT *
FROM
(
    SELECT MsiteID MeasureSiteID,
           8 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_VOLTAGE_WS_MSITEDATA_1
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           8 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_VOLTAGE_WS_MSITEDATA_2
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           8 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_VOLTAGE_WS_MSITEDATA_3
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
    UNION
    SELECT MsiteID MeasureSiteID,
           8 MeasureSiteThresholdType,
           WarnValue AlarmThresholdValue,
           AlmValue DangerThresholdValue,
           NULL EigenValueType,
           GETDATE() AddDate,
           MAX(SamplingDate) SamplingDate
    FROM dbo.T_DATA_VOLTAGE_WS_MSITEDATA_4
    GROUP BY MsiteID,
             AlmValue,
             WarnValue
) AS VOLTAGE_WS
UNION
SELECT *
FROM
(
    SELECT TSV.MSiteID MeasureSiteID,
           TSV.SingalType MeasureSiteThresholdType,
           TSVSS.WarnValue AlarmThresholdValue,
           TSVSS.AlmValue DangerThresholdValue,
           CASE TDEVT.Name
               WHEN '有效值' THEN
                   1
               WHEN '峰值' THEN
                   2
               ELSE
                   3
           END EigenValueType,
           GETDATE() AddDate,
           (
               SELECT MAX(TDVSCHA.SamplingDate)
               FROM dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC AS TDVSCHA
               WHERE TDVSCHA.SingalID = TSV.SingalID
           ) SamplingDate
    FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
        LEFT JOIN dbo.T_SYS_VIBSINGAL AS TSV
            ON TSV.SingalID = TSVSS.SingalID
        LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT
            ON TDEVT.ID = TSVSS.ValueType
    WHERE TSV.SingalType = 1
    UNION
    SELECT TSV.MSiteID MeasureSiteID,
           TSV.SingalType MeasureSiteThresholdType,
           TSVSS.WarnValue AlarmThresholdValue,
           TSVSS.AlmValue DangerThresholdValue,
           CASE TDEVT.Name
               WHEN '有效值' THEN
                   1
               WHEN '峰值' THEN
                   2
               ELSE
                   3
           END EigenValueType,
           GETDATE() AddDate,
           (
               SELECT MAX(TDVSCHA.SamplingDate)
               FROM dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL AS TDVSCHA
               WHERE TDVSCHA.SingalID = TSV.SingalID
           ) SamplingDate
    FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
        LEFT JOIN dbo.T_SYS_VIBSINGAL AS TSV
            ON TSV.SingalID = TSVSS.SingalID
        LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT
            ON TDEVT.ID = TSVSS.ValueType
    WHERE TSV.SingalType = 2
    UNION
    SELECT TSV.MSiteID MeasureSiteID,
           TSV.SingalType MeasureSiteThresholdType,
           TSVSS.WarnValue AlarmThresholdValue,
           TSVSS.AlmValue DangerThresholdValue,
           CASE TDEVT.Name
               WHEN '有效值' THEN
                   1
               WHEN '峰值' THEN
                   2
               ELSE
                   3
           END EigenValueType,
           GETDATE() AddDate,
           (
               SELECT MAX(TDVSCHA.SamplingDate)
               FROM dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP AS TDVSCHA
               WHERE TDVSCHA.SingalID = TSV.SingalID
           ) SamplingDate
    FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
        LEFT JOIN dbo.T_SYS_VIBSINGAL AS TSV
            ON TSV.SingalID = TSVSS.SingalID
        LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT
            ON TDEVT.ID = TSVSS.ValueType
    WHERE TSV.SingalType = 3
    UNION
    SELECT TSV.MSiteID MeasureSiteID,
           TSV.SingalType MeasureSiteThresholdType,
           TSVSS.WarnValue AlarmThresholdValue,
           TSVSS.AlmValue DangerThresholdValue,
           CASE TDEVT.Name
               WHEN '峰值' THEN
                   2
               ELSE
                   4
           END EigenValueType,
           GETDATE() AddDate,
           (
               SELECT MAX(TDVSCHA.SamplingDate)
               FROM dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL AS TDVSCHA
               WHERE TDVSCHA.SingalID = TSV.SingalID
           ) SamplingDate
    FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
        LEFT JOIN dbo.T_SYS_VIBSINGAL AS TSV
            ON TSV.SingalID = TSVSS.SingalID
        LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT
            ON TDEVT.ID = TSVSS.ValueType
    WHERE TSV.SingalType = 4
    UNION
    SELECT TSV.MSiteID MeasureSiteID,
           TSV.SingalType MeasureSiteThresholdType,
           TSVSS.WarnValue AlarmThresholdValue,
           TSVSS.AlmValue DangerThresholdValue,
           5 EigenValueType,
           GETDATE() AddDate,
           (
               SELECT MAX(TDVSCHA.SamplingDate)
               FROM dbo.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ AS TDVSCHA
               WHERE TDVSCHA.SingalID = TSV.SingalID
           ) SamplingDate
    FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
        LEFT JOIN dbo.T_SYS_VIBSINGAL AS TSV
            ON TSV.SingalID = TSVSS.SingalID
        LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT
            ON TDEVT.ID = TSVSS.ValueType
    WHERE TSV.SingalType = 5
) AS b
WHERE b.SamplingDate IS NOT NULL;
GO