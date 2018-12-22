USE [iCMSDB]
GO


CREATE TRIGGER TGR_DEICE_INSERTORUPDATE ON dbo.T_SYS_DEVICE
    AFTER INSERT, UPDATE
AS
    BEGIN

        DECLARE @DeviceId INT;

        SELECT  @DeviceId = s.DevID
        FROM    inserted s;
        UPDATE  dbo.T_SYS_DEVICE
        SET     LastUpdateTime = GETDATE()
        WHERE   DevID = @DeviceId;

    END;
GO


/****** Object:  Trigger [dbo].[TGR_SYS_DEV_ALMRECORD_INSERT_ALARMREMIND]    Script Date: 2017/11/27 14:06:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[TGR_SYS_DEV_ALMRECORD_INSERT_ALARMREMIND]
ON [dbo].[T_SYS_DEV_ALMRECORD]
FOR INSERT
AS
BEGIN
    IF (EXISTS (SELECT TOP 1 1 FROM inserted))
    BEGIN
        SET NOCOUNT ON;

        --主键ID
        DECLARE @ID INT;
        --设备ID
        DECLARE @DeviceID INT;
        --报警记录添加时间ID
        DECLARE @DeviceAlmTime DATETIME;
        --该操作影响的行数
        DECLARE @count INT;
        --循环索引
        DECLARE @index INT;
        --报警级别
        DECLARE @AlmStatus INT;

		DECLARE @BJTXBJSZ INT;
        DECLARE @BJTXSJSZ INT;
        DECLARE @indexUserID INT;
        DECLARE @totalUserID INT;
        DECLARE @userID INT;
        --用户ID表
        DECLARE @TEMPUSERID TABLE (UserID INT);

        --给变量赋值
        SELECT @count = COUNT(1) FROM inserted;
        SET @index = 0;

		SELECT @BJTXBJSZ = Value FROM T_DICT_CONFIG WHERE Code = 'CONFIG_151_BJTXBJSZ';
        SELECT @BJTXSJSZ = Value FROM T_DICT_CONFIG WHERE Code = 'CONFIG_150_BJTXSJSZ';
        --删除在提醒周期外的关联记录
        DELETE FROM T_SYS_USER_RELATION_DEV_ALMRECORD
			WHERE DeviceAlmTime < DATEADD(hh, -@BJTXSJSZ, GETDATE());

        WHILE (@index < @count)
        BEGIN
            SET @index = @index + 1;
            --根据索引取出数据
            SELECT @ID = sourceData.AlmRecordID,
                   @DeviceID = sourceData.DevID,
                   @AlmStatus = AlmStatus,
                   @DeviceAlmTime = sourceData.AddDate
            FROM
            (
                SELECT ROW_NUMBER() OVER (ORDER BY AlmRecordID) AS number,
                       AlmRecordID,
                       DevID,
                       AlmStatus,
                       AddDate
                FROM inserted
            ) sourceData
            WHERE sourceData.number = @index;

            --添加到 用户关联未设备报警提醒表
            --写入在提醒范围内的报警记录（1：高报+高高报；    2：高报；    3：高高报）
            IF (
                   (@AlmStatus = @BJTXBJSZ)
                   OR (
                          @BJTXBJSZ = 1
                          AND (
                                  @AlmStatus = 2
                                  OR @AlmStatus = 3
                              )
                      )
               )
            BEGIN
                INSERT INTO @TEMPUSERID
					SELECT DISTINCT UserID FROM T_SYS_USER_RELATION_DEVICE WHERE DevId = @DeviceID;
                WHILE ((SELECT COUNT(1) FROM @TEMPUSERID) > 0)
                BEGIN
                    SELECT TOP 1 @userID = UserID FROM @TEMPUSERID;

                    INSERT INTO T_SYS_USER_RELATION_DEV_ALMRECORD
                    (
                        UserID,
                        DeviceID,
                        DeviceAlmRecordID,
                        DeviceAlmTime,
                        AlmStatus,
                        AddDate
                    )
                    VALUES
                    (@userID, @DeviceID, @ID, @DeviceAlmTime, @AlmStatus, GETDATE());

                    DELETE FROM @TEMPUSERID WHERE UserID = @userID;
                END;
            END;
        END;

        SET NOCOUNT OFF;
    END;
END;
GO


/****** Object:  Trigger [dbo].[TGR_SYS_DEV_ALMRECORD_UPDATE_ALARMREMIND]    Script Date: 2017/11/27 14:07:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[TGR_SYS_DEV_ALMRECORD_UPDATE_ALARMREMIND]
ON [dbo].[T_SYS_DEV_ALMRECORD] FOR UPDATE
AS
BEGIN
	IF(EXISTS (SELECT TOP 1 1 FROM inserted))
	BEGIN
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--设备ID
		declare @deviceID int
		--测量位置ID
		declare @measureSiteID int
		--振动信号ID
		declare @signalID int
		--报警记录报警开始时间
		declare @startTime datetime
		--报警记录报警结束时间
		declare @endTime datetime
		--最后报警时间
		declare @LatestStartTime datetime
		--振动信号类型
		declare @singalType int
		--振动信号采集方式
		declare @daqStyle int
		--报警类型
		declare @msAlmID int
		--报警级别
		declare @AlmStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		declare @BJTXBJSZ int
		declare @indexUserID int
		declare @totalUserID int
		declare @userID int
		declare @TEMPUSERID table (UserID int)
		declare @delLatestStartTime datetime
		declare @c int
		select @BJTXBJSZ=Value from T_DICT_CONFIG where Code='CONFIG_151_BJTXBJSZ'

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		WHILE(@index < @count)
		BEGIN
			set @extraMessage = null
			set @index = @index + 1
			--根据索引取出数据
			SELECT @ID = sourceData.AlmRecordID,
				@deviceID = sourceData.DevID,
				@measureSiteID = sourceData.MSiteID,
				@signalID = sourceData.SingalID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate,
				@msAlmID = sourceData.MSAlmID,
				@AlmStatus=sourceData.AlmStatus,
				@LatestStartTime = sourceData.LatestStartTime
			FROM (
				SELECT ROW_NUMBER() OVER (ORDER BY AlmRecordID) AS number,
					AlmRecordID,
					DevID,
					MSiteID,
					SingalID,
					BDate,
					EDate,
					MSAlmID,
					AlmStatus,
					LatestStartTime
				FROM inserted
			) sourceData
			WHERE sourceData.number = @index
	

            --判断是否持续报警，则新增或更新用户关联设备报警提醒表
			SELECT @delLatestStartTime=LatestStartTime FROM Deleted WHERE AlmRecordID=@ID;
			IF((@startTime = @endTime) AND (@LatestStartTime>@delLatestStartTime))
			BEGIN
				--判断报警状态是否符合报警提醒设置条件
				IF((@AlmStatus=@BJTXBJSZ) OR (@BJTXBJSZ=1 AND (@AlmStatus=2 OR @AlmStatus=3)))
				BEGIN
					DELETE FROM T_SYS_USER_RELATION_DEV_ALMRECORD WHERE DeviceAlmRecordID=@ID
					DELETE FROM @TEMPUSERID
					INSERT INTO @TEMPUSERID 
						SELECT DISTINCT userid FROM T_SYS_USER_RELATION_DEVICE WHERE DevId=@DeviceID
					WHILE((SELECT COUNT(1) FROM @TEMPUSERID)>0)
					BEGIN
						SELECT TOP 1 @userID = UserID FROM @TEMPUSERID
						INSERT INTO T_SYS_USER_RELATION_DEV_ALMRECORD
							   		 (UserID,DeviceID,DeviceAlmRecordID,DeviceAlmTime,AlmStatus,AddDate)
							   VALUES(@userID,@DeviceID,@ID,@LatestStartTime,@AlmStatus,GETDATE())

						DELETE FROM @TEMPUSERID WHERE UserID=@userID
					END
				END
			END

		END
		SET NOCOUNT OFF
	END
END


GO



/****** Object:  Trigger [dbo].[TGR_SYS_WSN_ALMRECORD_INSERT_ALARMREMIND]    Script Date: 2017/11/27 14:07:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create TRIGGER [dbo].[TGR_SYS_WSN_ALMRECORD_INSERT_ALARMREMIND]
ON [dbo].[T_SYS_WSN_ALMRECORD] FOR INSERT
AS
BEGIN
	IF(EXISTS (SELECT TOP 1 1 FROM inserted))
	BEGIN
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--网关ID
		declare @WGID int
		--传感器ID
		declare @WSID int
		--报警记录添加时间ID
		declare @DeviceAlmTime datetime
		--报警类型
		declare @MSAlmID int
		--报警级别
		declare @AlmStatus int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		declare @BJTXBJSZ int
		declare @BJTXSJSZ int
		declare @indexUserID int
		declare @totalUserID int
		declare @userID int
		--用户ID表
		declare @TEMPUSERID table 
		(UserID int)
		select @BJTXBJSZ=Value from T_DICT_CONFIG where Code='CONFIG_151_BJTXBJSZ'
		select @BJTXSJSZ=Value from T_DICT_CONFIG where Code='CONFIG_150_BJTXSJSZ'
		--删除在提醒周期外的关联记录
		delete from T_SYS_USER_RELATION_WS_ALMRECORD where WSNAlmTime<dateadd(hh,-@BJTXSJSZ,getdate())

		WHILE(@index < @count)
		BEGIN
			set @index = @index + 1
			--根据索引取出数据
			SELECT @ID = sourceData.AlmRecordID,
					@WGID=sourceData.wgid,
					@WSID=sourceData.wsid,
					@DeviceAlmTime=sourceData.LatestStartTime,
					@MSAlmID=sourceData.MSAlmID,
					@AlmStatus=AlmStatus FROM (
				SELECT ROW_NUMBER() OVER (ORDER BY AlmRecordID) AS number, AlmRecordID,wgid,wsid,MSAlmID,AlmStatus,LatestStartTime
				FROM inserted
			) sourceData
			WHERE sourceData.number = @index

		
--添加到 用户关联未设备报警提醒表			
			--写入在提醒范围内的报警记录（1：高报+高高报；    2：高报；    3：高高报）
			IF((@AlmStatus=@BJTXBJSZ) OR (@BJTXBJSZ=1 AND (@AlmStatus=2 OR @AlmStatus=3)))
			BEGIN
				--网关报警
				IF(@MSAlmID = 6)
				BEGIN
					DELETE FROM @TEMPUSERID
					INSERT INTO @TEMPUSERID 
						SELECT DISTINCT userid FROM T_SYS_USER_RELATION_WS WHERE 
						WSID IN(SELECT DISTINCT wsid FROM T_SYS_WS WHERE WGID=@WGID)
					WHILE((SELECT COUNT(1) FROM @TEMPUSERID)>0)
					BEGIN
					SELECT TOP 1 @userID = UserID FROM @TEMPUSERID

					INSERT INTO T_SYS_USER_RELATION_WS_ALMRECORD
						   		 (UserID,MonitorDeviceType,MonitorDeviceID,WSNAlmRecordID,WSNAlmTime,AlmStatus,AddDate)
						   VALUES(@userID,1,@WGID,@ID,@DeviceAlmTime,@AlmStatus,GETDATE())

					DELETE FROM @TEMPUSERID WHERE UserID=@userID
				END
				END

				--传感器报警
				IF(@MSAlmID = 3 OR @MSAlmID = 4 OR @MSAlmID = 5)
				BEGIN
					DELETE FROM @TEMPUSERID
					INSERT INTO @TEMPUSERID 
						SELECT DISTINCT userid FROM T_SYS_USER_RELATION_WS WHERE WSID=@WSID
					WHILE((SELECT COUNT(1) FROM @TEMPUSERID)>0)
					BEGIN
						SELECT TOP 1 @userID = UserID FROM @TEMPUSERID
						INSERT INTO T_SYS_USER_RELATION_WS_ALMRECORD
							   		 (UserID,MonitorDeviceType,MonitorDeviceID,WSNAlmRecordID,WSNAlmTime,AlmStatus,AddDate)
							   VALUES(@userID,2,@WSID,@ID,@DeviceAlmTime,@AlmStatus,GETDATE())

						DELETE FROM @TEMPUSERID WHERE UserID=@userID
					END
				END
			END

		END

		SET NOCOUNT OFF
	END
END


GO
/****** Object:  Trigger [dbo].[TGR_SYS_WSN_ALMRECORD_UPDATE_ALARMREMIND]    Script Date: 2017/11/27 14:07:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[TGR_SYS_WSN_ALMRECORD_UPDATE_ALARMREMIND]
ON [dbo].[T_SYS_WSN_ALMRECORD] FOR UPDATE
AS
BEGIN
	IF(EXISTS (SELECT TOP 1 1 FROM inserted))
	BEGIN
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--测点ID
		declare @mSiteID int
		--无线网关ID
		declare @wgID int
		--无线传感器ID
		declare @wsID int
		--报警记录报警开始时间
		declare @startTime datetime
		--报警记录报警结束时间
		declare @endTime datetime
		--报警类型
		declare @MSAlmID int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		declare @WSNAlmTime datetime
		declare @AlmStatus int
		declare @BJTXBJSZ int
		declare @BJTXSJSZ int
		declare @indexUserID int
		declare @totalUserID int
		declare @userID int
		--用户ID表
		declare @TEMPUSERID table (UserID int)
		declare @delLatestStartTime datetime
		declare @c int
		select @BJTXBJSZ=Value from T_DICT_CONFIG where Code='CONFIG_151_BJTXBJSZ'
		select @BJTXSJSZ=Value from T_DICT_CONFIG where Code='CONFIG_150_BJTXSJSZ'

		WHILE(@index < @count)
		BEGIN
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID,
				@mSiteID = sourceData.MSiteID,
				@wgID = sourceData.WGID,
				@wsID = sourceData.WSID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate,
				@WSNAlmTime=sourceData.LatestStartTime,
				@MSAlmID = sourceData.MSAlmID,
				@AlmStatus=AlmStatus
			from (
				select row_number() over (order by AlmRecordID) as number,
					AlmRecordID,
					MSiteID,
					WGID,
					WSID,
					BDate,
					EDate,
					LatestStartTime,
					MSAlmID,
					AlmStatus
				from inserted
			) sourceData
			where sourceData.number = @index
			if(isnull(@mSiteID, 0) < 1)
			begin
				continue
			end

--判断是否持续报警，则新增或更新用户关联设备报警提醒表
			SELECT @delLatestStartTime=LatestStartTime FROM Deleted WHERE AlmRecordID=@ID;
			IF((@startTime = @endTime) AND (@WSNAlmTime>@delLatestStartTime))
			BEGIN
				--判断报警状态是否符合报警提醒设置条件
				IF((@AlmStatus=@BJTXBJSZ) OR (@BJTXBJSZ=1 AND (@AlmStatus=2 OR @AlmStatus=3)))
				BEGIN
					DELETE FROM T_SYS_USER_RELATION_WS_ALMRECORD WHERE WSNAlmRecordID=@ID
					--网关报警
					IF(@MSAlmID = 6)
					BEGIN
						DELETE FROM @TEMPUSERID
						INSERT INTO @TEMPUSERID 
						SELECT DISTINCT userid FROM T_SYS_USER_RELATION_WS WHERE 
						WSID IN(SELECT DISTINCT wsid FROM T_SYS_WS WHERE WGID=@WGID)
						WHILE((SELECT COUNT(1) FROM @TEMPUSERID)>0)
						BEGIN
							SELECT TOP 1 @userID = UserID FROM @TEMPUSERID
							INSERT INTO T_SYS_USER_RELATION_WS_ALMRECORD
					   		(UserID,MonitorDeviceType,MonitorDeviceID,WSNAlmRecordID,WSNAlmTime,AlmStatus,AddDate)
							VALUES(@userID,1,@WGID,@ID,@WSNAlmTime,@AlmStatus,GETDATE())

							DELETE FROM @TEMPUSERID WHERE UserID=@userID
						END
					END
					--传感器报警
					IF(@MSAlmID = 3 OR @MSAlmID = 4 OR @MSAlmID = 5)
					BEGIN
						DELETE FROM @TEMPUSERID
						INSERT INTO @TEMPUSERID 
						SELECT DISTINCT userid FROM T_SYS_USER_RELATION_WS WHERE WSID=@WSID
						WHILE((SELECT COUNT(1) FROM @TEMPUSERID)>0)
						BEGIN
							SELECT TOP 1 @userID = UserID FROM @TEMPUSERID
							INSERT INTO T_SYS_USER_RELATION_WS_ALMRECORD
								   		 (UserID,MonitorDeviceType,MonitorDeviceID,WSNAlmRecordID,WSNAlmTime,AlmStatus,AddDate)
								   VALUES(@userID,2,@WSID,@ID,@WSNAlmTime,@AlmStatus,GETDATE())

							DELETE FROM @TEMPUSERID WHERE UserID=@userID
						END
					END
				END
			END
		END

		SET NOCOUNT OFF
	END
END


GO




