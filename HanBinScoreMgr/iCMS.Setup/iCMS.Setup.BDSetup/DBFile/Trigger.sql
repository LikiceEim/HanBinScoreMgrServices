USE [iCMSDB]
GO
/****** Object:  StoredProcedure [dbo].[GetDevAlarmRemindDataByUserId]    Script Date: 2017/1/4 16:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------监测树触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树添加触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_INSERT') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_INSERT
end
go
create trigger TGR_SYS_MONITOR_TREE_INSERT
on T_SYS_MONITOR_TREE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @monitorTreeType = sourceData.[Type] from (
				select row_number() over (order by MonitorTreeID) as number, MonitorTreeID, [Type]
				from inserted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MONITOR_TREE', @ID, 1, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树修改触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_UPDATE
end
go
create trigger TGR_SYS_MONITOR_TREE_UPDATE
on T_SYS_MONITOR_TREE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的监测树状态
		declare @oldStatus int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @oldStatus = sourceData.[Status], @monitorTreeType = sourceData.[Type] from (
				select row_number() over (order by MonitorTreeID) as number, MonitorTreeID, [Status], [Type]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + ',"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MONITOR_TREE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树删除触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_DELETE') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_DELETE
end
go
create trigger TGR_SYS_MONITOR_TREE_DELETE
on T_SYS_MONITOR_TREE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @monitorTreeType = sourceData.[Type] from (
				select row_number() over (order by MonitorTreeID) as number, MonitorTreeID, [Type]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MONITOR_TREE', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------监测树触发器 End-----------------------------

--------------------------设备树触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树添加触发器
if(OBJECT_ID('TGR_SYS_DEVICE_INSERT') is not null)
begin
	drop trigger TGR_SYS_DEVICE_INSERT
end
go
create trigger TGR_SYS_DEVICE_INSERT
on T_SYS_DEVICE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID from (
				select row_number() over (order by DevID) as number, DevID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEVICE', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树修改触发器
if(OBJECT_ID('TGR_SYS_DEVICE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_DEVICE_UPDATE
end
go
create trigger TGR_SYS_DEVICE_UPDATE
on T_SYS_DEVICE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldAlmStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID, @oldAlmStatus = sourceData.AlmStatus from (
				select row_number() over (order by DevID) as number, DevID, AlmStatus
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"AlmStatus":' + cast(@oldAlmStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEVICE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树删除触发器
if(OBJECT_ID('TGR_SYS_DEVICE_DELETE') is not null)
begin
	drop trigger TGR_SYS_DEVICE_DELETE
end
go
create trigger TGR_SYS_DEVICE_DELETE
on T_SYS_DEVICE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID from (
				select row_number() over (order by DevID) as number, DevID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEVICE', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备树触发器 End-----------------------------

--------------------------测量位置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置添加触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_INSERT') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_INSERT
end
go
create trigger TGR_SYS_MEASURESITE_INSERT
on T_SYS_MEASURESITE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID from (
				select row_number() over (order by MSiteID) as number, MSiteID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MEASURESITE', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置修改触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_UPDATE
end
go
create trigger TGR_SYS_MEASURESITE_UPDATE
on T_SYS_MEASURESITE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的无线传感器ID
		declare @oldWSID int
		--旧的测量位置状态
		declare @oldMSiteStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID, @oldWSID = sourceData.WSID, @oldMSiteStatus = sourceData.MSiteStatus from (
				select row_number() over (order by MSiteID) as number, MSiteID, WSID, MSiteStatus
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"WSID":' + cast(@oldWSID as nvarchar(10)) + ',"MSiteStatus":' + cast(@oldMSiteStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MEASURESITE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置删除触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_DELETE') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_DELETE
end
go
create trigger TGR_SYS_MEASURESITE_DELETE
on T_SYS_MEASURESITE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的无线传感器ID
		declare @WSID int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID, @WSID = sourceData.WSID from (
				select row_number() over (order by MSiteID) as number, MSiteID, WSID
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"WSID":' + cast(@WSID as nvarchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MEASURESITE', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------测量位置触发器 End-----------------------------

--------------------------振动信号触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号添加触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_INSERT') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_INSERT
end
go
create trigger TGR_SYS_VIBSINGAL_INSERT
on T_SYS_VIBSINGAL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID from (
				select row_number() over (order by SingalID) as number, SingalID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBSINGAL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号修改触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_UPDATE
end
go
create trigger TGR_SYS_VIBSINGAL_UPDATE
on T_SYS_VIBSINGAL for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的振动信号报警状态
		declare @oldSingalStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID, @oldSingalStatus = sourceData.SingalStatus from (
				select row_number() over (order by SingalID) as number, SingalID, SingalStatus
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"SingalStatus":' + cast(@oldSingalStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBSINGAL', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号删除触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_DELETE') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_DELETE
end
go
create trigger TGR_SYS_VIBSINGAL_DELETE
on T_SYS_VIBSINGAL for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--采集方式
		declare @daqStyle int
		--振动信号类型
		declare @singalType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID, @daqStyle = sourceData.DAQStyle, @singalType = sourceData.SingalType from (
				select row_number() over (order by SingalID) as number, SingalID, DAQStyle, SingalType
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ',"DAQStyle":' + cast(@daqStyle as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBSINGAL', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------振动信号触发器 End-----------------------------

--------------------------无线网关触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关添加触发器
if(OBJECT_ID('TGR_SYS_WG_INSERT') is not null)
begin
	drop trigger TGR_SYS_WG_INSERT
end
go
create trigger TGR_SYS_WG_INSERT
on T_SYS_WG for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID from (
				select row_number() over (order by WGID) as number, WGID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WG', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关修改触发器
if(OBJECT_ID('TGR_SYS_WG_UPDATE') is not null)
begin
	drop trigger TGR_SYS_WG_UPDATE
end
go
create trigger TGR_SYS_WG_UPDATE
on T_SYS_WG for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的无线网关运行状态
		declare @oldRunStatus int
		--旧的无线网关连接状态
		declare @oldLinkStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID, @oldRunStatus = sourceData.RunStatus, @oldLinkStatus = sourceData.LinkStatus from (
				select row_number() over (order by WGID) as number, WGID, RunStatus, LinkStatus
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"RunStatus":' + cast(@oldRunStatus as varchar(10)) + ',"LinkStatus":' + cast(@oldLinkStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WG', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关删除触发器
if(OBJECT_ID('TGR_SYS_WG_DELETE') is not null)
begin
	drop trigger TGR_SYS_WG_DELETE
end
go
create trigger TGR_SYS_WG_DELETE
on T_SYS_WG for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID from (
				select row_number() over (order by WGID) as number, WGID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WG', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线网关触发器 End-----------------------------

--------------------------无线传感器温度阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT
on T_SYS_TEMPE_WS_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE
on T_SYS_TEMPE_WS_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status] from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID, [Status]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE
on T_SYS_TEMPE_WS_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器温度阈值设置触发器 End-----------------------------

--------------------------无线传感器电池电压阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT
on T_SYS_VOLTAGE_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE
on T_SYS_VOLTAGE_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status] from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID, [Status]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE
on T_SYS_VOLTAGE_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器电池电压阈值设置触发器 End-----------------------------

--------------------------设备温度阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status] from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID, [Status]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备温度阈值设置触发器 End-----------------------------

--------------------------振动信号报警阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT
on T_SYS_VIBRATING_SET_SIGNALALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID from (
				select row_number() over (order by SingalAlmID) as number, SingalAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE
on T_SYS_VIBRATING_SET_SIGNALALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID, @oldStatus = sourceData.[Status] from (
				select row_number() over (order by SingalAlmID) as number, SingalAlmID, [Status]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE
on T_SYS_VIBRATING_SET_SIGNALALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--振动信号ID
		declare @signalID int
		--振动信号类型
		declare @singalType int
		--振动信号采集方式
		declare @daqStyle int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID, @signalID = sourceData.SingalID from (
				select row_number() over (order by SingalAlmID) as number, SingalAlmID, SingalID
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出振动信号类型的信号类型、采集方式
			select @singalType = SingalType, @daqStyle = DAQStyle from T_SYS_VIBSINGAL where SingalID = @signalID
			--生成附加信息数据
			set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ',"DAQStyle":' + cast(@daqStyle as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------振动信号报警阈值设置触发器 End-----------------------------

--------------------------传感器报警记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加传感器报警记录添加触发器
if(OBJECT_ID('TGR_SYS_WSN_ALMRECORD_INSERT') is not null)
begin
	drop trigger TGR_SYS_WSN_ALMRECORD_INSERT
end
go
create trigger TGR_SYS_WSN_ALMRECORD_INSERT
on T_SYS_WSN_ALMRECORD for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID from (
				select row_number() over (order by AlmRecordID) as number, AlmRecordID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WSN_ALMRECORD', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加传感器报警记录修改触发器
if(OBJECT_ID('TGR_SYS_WSN_ALMRECORD_UPDATE') is not null)
begin
	drop trigger TGR_SYS_WSN_ALMRECORD_UPDATE
end
go
create trigger TGR_SYS_WSN_ALMRECORD_UPDATE
on T_SYS_WSN_ALMRECORD for update
as
begin
	if(exists (select top 1 1 from inserted))
	begin
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
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID,
				@mSiteID = sourceData.MSiteID,
				@wgID = sourceData.WGID,
				@wsID = sourceData.WSID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate
			from (
				select row_number() over (order by AlmRecordID) as number,
					AlmRecordID,
					MSiteID,
					WGID,
					WSID,
					BDate,
					EDate
				from inserted
			) sourceData
			where sourceData.number = @index
			if(isnull(@mSiteID, 0) < 1)
			begin
				continue
			end

			--生成附加信息数据
			set @extraMessage = '{"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
				+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WSN_ALMRECORD', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------传感器报警记录触发器 End-----------------------------

--------------------------设备报警记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备报警记录添加触发器
if(OBJECT_ID('TGR_SYS_DEV_ALMRECORD_INSERT') is not null)
begin
	drop trigger TGR_SYS_DEV_ALMRECORD_INSERT
end
go
create trigger TGR_SYS_DEV_ALMRECORD_INSERT
on T_SYS_DEV_ALMRECORD for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID from (
				select row_number() over (order by AlmRecordID) as number, AlmRecordID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEV_ALMRECORD', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备报警记录修改触发器
if(OBJECT_ID('TGR_SYS_DEV_ALMRECORD_UPDATE') is not null)
begin
	drop trigger TGR_SYS_DEV_ALMRECORD_UPDATE
end
go
create trigger TGR_SYS_DEV_ALMRECORD_UPDATE
on T_SYS_DEV_ALMRECORD for update
as
begin
	if(exists (select top 1 1 from inserted))
	begin
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
		--振动信号类型
		declare @singalType int
		--振动信号采集方式
		declare @daqStyle int
		--报警类型
		declare @msAlmID int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @extraMessage = null
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID,
				@deviceID = sourceData.DevID,
				@measureSiteID = sourceData.MSiteID,
				@signalID = sourceData.SingalID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate,
				@msAlmID = sourceData.MSAlmID
			from (
				select row_number() over (order by AlmRecordID) as number,
					AlmRecordID,
					DevID,
					MSiteID,
					SingalID,
					BDate,
					EDate,
					MSAlmID
				from inserted
			) sourceData
			where sourceData.number = @index
			--振动报警结束
			if(@msAlmID = 1)
			begin
				--取出振动信号类型的信号类型、采集方式
				select @singalType = SingalType, @daqStyle = DAQStyle from T_SYS_VIBSINGAL where SingalID = @signalID
				--生成附加信息数据
				set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ','
					+ '"DAQStyle":' + cast(@daqStyle as varchar(10)) + ','
					+ '"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
					+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'
			end
			--温度报警结束
			else if(@msAlmID = 2)
			begin
				--生成附加信息数据
				set @extraMessage = '{"SingalType":0,"DAQStyle":0,'
					+ '"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
					+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'
			end

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEV_ALMRECORD', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备报警记录触发器 End-----------------------------

--------------------------无线传感器温度采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器温度采集数据触发器 End-----------------------------

--------------------------无线传感器电池电压采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器电池电压采集数据触发器 End-----------------------------

--------------------------设备温度采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备温度采集数据触发器 End-----------------------------

--------------------------特征值采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加加速度特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加速度特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加位移特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加包络特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加LQ特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------特征值采集数据触发器 End-----------------------------

--------------------------操作记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加操作记录添加触发器
if(OBJECT_ID('TGR_SYS_OPERATION_INSERT') is not null)
begin
	drop trigger TGR_SYS_OPERATION_INSERT
end
go
create trigger TGR_SYS_OPERATION_INSERT
on T_SYS_OPERATION for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_OPERATION', @ID, 1, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加操作记录修改触发器
if(OBJECT_ID('TGR_SYS_OPERATION_UPDATE') is not null)
begin
	drop trigger TGR_SYS_OPERATION_UPDATE
end
go
create trigger TGR_SYS_OPERATION_UPDATE
on T_SYS_OPERATION for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_OPERATION', @ID, 2, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------操作记录触发器 End-----------------------------

--------------------------云通讯配置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置添加触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_INSERT') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_INSERT
end
go
create trigger TGR_SYS_CLOUDCONFIG_INSERT
on T_SYS_CLOUDCONFIG for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 1, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置修改触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_UPDATE') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_UPDATE
end
go
create trigger TGR_SYS_CLOUDCONFIG_UPDATE
on T_SYS_CLOUDCONFIG for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 2, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置删除触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_DELETE') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_DELETE
end
go
create trigger TGR_SYS_CLOUDCONFIG_DELETE
on T_SYS_CLOUDCONFIG for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 3, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------云通讯配置触发器 End-----------------------------

--------------------------推送表触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加推送表添加触发器
if(OBJECT_ID('TGR_DATA_CLOUDPUSH_INSERT') is not null)
begin
	drop trigger TGR_DATA_CLOUDPUSH_INSERT
end
go
create trigger TGR_DATA_CLOUDPUSH_INSERT
on T_DATA_CLOUDPUSH for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		declare @serviceUrl as varchar(1000)
		declare @object int
		declare @platformId int
		declare @parameter nvarchar(500)
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0
		--通过http协议调用的接口地址
		set @serviceUrl = 'http://localhost:2893/CloudProxy/ProxyNotifyService/ReceiveTriggerAndRequestCloudCommunication'

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @platformId = sourceData.PlatformId from (
				select row_number() over (order by PlatformId) as number, PlatformId
				from inserted
			) sourceData
			where sourceData.number = @index

			set @parameter = '{"Key":"5bcbc178cf70e1ec7ca1586a1eaac1d3","PlatformId":' + cast(@platformId as varchar(50)) + '}'
			set @parameter = '{"Key":"5bcbc178cf70e1ec7ca1586a1eaac1d3","PlatformId":' + cast(@platformId as varchar(50))
				+ ',"Sign":"' + lower(convert(varchar(max), hashbytes('MD5', @parameter + '252a7d7582a39c899de71efa8b6fb368'), 2)) + '"}'
			
			exec sp_OACreate 'Msxml2.ServerXMLHTTP.3.0', @object out
			exec sp_OAMethod @object, 'open', NULL, 'post', @serviceUrl
			exec sp_OAMethod @object, 'send', NULL, @parameter
			exec sp_OADestroy @object
		end
	end
end
go
--------------------------推送表触发器 End-----------------------------


--------------------------监测树触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树添加触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_INSERT') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_INSERT
end
go
create trigger TGR_SYS_MONITOR_TREE_INSERT
on T_SYS_MONITOR_TREE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @monitorTreeType = sourceData.[Type] from (
				select row_number() over (order by MonitorTreeID) as number, MonitorTreeID, [Type]
				from inserted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MONITOR_TREE', @ID, 1, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树修改触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_UPDATE
end
go
create trigger TGR_SYS_MONITOR_TREE_UPDATE
on T_SYS_MONITOR_TREE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的监测树状态
		declare @oldStatus int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--老的父ID
		declare @oldPID int
		--老的OID
		declare @oldOID int
		--老的是否默认
		declare @oldIsDefault int
		--老的名称
		declare @oldName nvarchar(50)
		--老的备注
		declare @oldDes nvarchar(200)
		--新的父ID
		declare @newPID int
		--新的OID
		declare @newOID int
		--新的是否默认
		declare @newIsDefault int
		--新的名称
		declare @newName nvarchar(50)
		--新的备注
		declare @newDes nvarchar(200)
		--新的类型
		declare @newType int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @oldPID = sourceData.PID,
				@oldOID = sourceData.OID, @oldIsDefault = sourceData.IsDefault,
				@oldName = sourceData.Name, @oldDes = sourceData.[Des],
				@oldStatus = sourceData.[Status],
				@monitorTreeType = sourceData.[Type]
			from (
				select row_number() over (order by MonitorTreeID) as number,
					MonitorTreeID, PID, OID, IsDefault, Name, [Des], [Status], [Type]
				from deleted
			) sourceData
			where sourceData.number = @index
			--根据索引取出修改后的数据
			select @newPID = sourceData.PID, @newOID = sourceData.OID,
				@newIsDefault = sourceData.IsDefault,
				@newName = sourceData.Name, @newDes = sourceData.[Des],
				@newType = sourceData.[Type]
			from (
				select row_number() over (order by MonitorTreeID) as number,
					PID, OID, IsDefault, Name, [Des], [Type]
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldPID != @newPID or @oldOID != @newOID
				or @oldIsDefault != @newIsDefault
				or @oldName != @newName or @oldDes != @newDes
				or @monitorTreeType != @newType)
			begin
				--生成附加信息数据
				set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + ',"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_MONITOR_TREE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加监测树删除触发器
if(OBJECT_ID('TGR_SYS_MONITOR_TREE_DELETE') is not null)
begin
	drop trigger TGR_SYS_MONITOR_TREE_DELETE
end
go
create trigger TGR_SYS_MONITOR_TREE_DELETE
on T_SYS_MONITOR_TREE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--监测树类型
		declare @monitorTreeType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MonitorTreeID, @monitorTreeType = sourceData.[Type] from (
				select row_number() over (order by MonitorTreeID) as number, MonitorTreeID, [Type]
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"Type":' + cast(@monitorTreeType as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MONITOR_TREE', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------监测树触发器 End-----------------------------

--------------------------设备树触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树添加触发器
if(OBJECT_ID('TGR_SYS_DEVICE_INSERT') is not null)
begin
	drop trigger TGR_SYS_DEVICE_INSERT
end
go
create trigger TGR_SYS_DEVICE_INSERT
on T_SYS_DEVICE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID from (
				select row_number() over (order by DevID) as number, DevID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEVICE', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树修改触发器
if(OBJECT_ID('TGR_SYS_DEVICE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_DEVICE_UPDATE
end
go
create trigger TGR_SYS_DEVICE_UPDATE
on T_SYS_DEVICE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldAlmStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--旧的监测树ID
		declare @oldMonitorTreeID int
		--旧的设备名称
		declare @oldDevName nvarchar(100)
		--旧的设备编号
		declare @oldDevNO int
		--旧的长
		declare @oldLength real
		--旧的宽
		declare @oldWidth real
		--旧的高
		declare @oldHeight real
		--旧的转速
		declare @oldRotate real
		--旧的负责人
		declare @oldPersonInCharge nvarchar(50)
		--旧的生产厂家
		declare @oldDevManufacturer nvarchar(100)
		--旧的厂家型号
		declare @oldDevModel nvarchar(100)
		--旧的车间位置
		declare @oldPosition nvarchar(100)
		--旧的额定电压
		declare @oldRatedVoltage real
		--旧的额定电流
		declare @oldRatedCurrent real
		--旧的功率
		declare @oldPower real
		--旧的联轴器形式
		declare @oldCouplingType nvarchar(100)
		--旧的排出最大压力
		declare @oldOutputMaxPressure real
		--旧的介质
		declare @oldMedia nvarchar(100)
		--旧的扬程
		declare @oldHeadOfDelivery real
		--旧的流量
		declare @oldOutputVolume real
		--旧的备注
		declare @oldDevMark nvarchar(200)
		--旧的运行状态
		declare @oldRunStatus int
		--新的监测树ID
		declare @newMonitorTreeID int
		--新的设备名称
		declare @newDevName nvarchar(100)
		--新的设备编号
		declare @newDevNO int
		--新的长
		declare @newLength real
		--新的宽
		declare @newWidth real
		--新的高
		declare @newHeight real
		--新的转速
		declare @newRotate real
		--新的负责人
		declare @newPersonInCharge nvarchar(50)
		--新的生产厂家
		declare @newDevManufacturer nvarchar(100)
		--新的厂家型号
		declare @newDevModel nvarchar(100)
		--新的车间位置
		declare @newPosition nvarchar(100)
		--新的额定电压
		declare @newRatedVoltage real
		--新的额定电流
		declare @newRatedCurrent real
		--新的功率
		declare @newPower real
		--新的联轴器形式
		declare @newCouplingType nvarchar(100)
		--新的排出最大压力
		declare @newOutputMaxPressure real
		--新的介质
		declare @newMedia nvarchar(100)
		--新的扬程
		declare @newHeadOfDelivery real
		--新的流量
		declare @newOutputVolume real
		--新的备注
		declare @newDevMark nvarchar(200)
		--新的运行状态
		declare @newRunStatus int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID,
				@oldMonitorTreeID = sourceData.MonitorTreeID,
				@oldDevName = sourceData.DevName, @oldDevNO = sourceData.DevNO,
				@oldLength = sourceData.[Length], @oldWidth = sourceData.Width,
				@oldHeight = sourceData.Height, @oldRotate = sourceData.Rotate,
				@oldPersonInCharge = sourceData.PersonInCharge,
				@oldDevManufacturer = sourceData.DevManufacturer,
				@oldDevModel = sourceData.DevModel,
				@oldPosition = sourceData.Position,
				@oldRatedVoltage = sourceData.RatedVoltage,
				@oldRatedCurrent = sourceData.RatedCurrent,
				@oldPower = sourceData.[Power],
				@oldCouplingType = sourceData.CouplingType,
				@oldOutputMaxPressure = sourceData.OutputMaxPressure,
				@oldMedia = sourceData.Media,
				@oldHeadOfDelivery = sourceData.HeadOfDelivery,
				@oldOutputVolume = sourceData.OutputVolume,
				@oldDevMark = sourceData.DevMark,
				@oldRunStatus = sourceData.RunStatus,
				@oldAlmStatus = sourceData.AlmStatus
			from (
				select row_number() over (order by DevID) as number, *
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newMonitorTreeID = sourceData.MonitorTreeID,
				@newDevName = sourceData.DevName, @newDevNO = sourceData.DevNO,
				@newLength = sourceData.[Length], @newWidth = sourceData.Width,
				@newHeight = sourceData.Height, @newRotate = sourceData.Rotate,
				@newPersonInCharge = sourceData.PersonInCharge,
				@newDevManufacturer = sourceData.DevManufacturer,
				@newDevModel = sourceData.DevModel,
				@newPosition = sourceData.Position,
				@newRatedVoltage = sourceData.RatedVoltage,
				@newRatedCurrent = sourceData.RatedCurrent,
				@newPower = sourceData.[Power],
				@newCouplingType = sourceData.CouplingType,
				@newOutputMaxPressure = sourceData.OutputMaxPressure,
				@newMedia = sourceData.Media,
				@newHeadOfDelivery = sourceData.HeadOfDelivery,
				@newOutputVolume = sourceData.OutputVolume,
				@newDevMark = sourceData.DevMark,
				@newRunStatus = sourceData.RunStatus
			from (
				select row_number() over (order by DevID) as number, *
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldMonitorTreeID != @newMonitorTreeID or
				@oldDevName != @newDevName or @oldDevNO != @newDevNO or
				@oldLength != @newLength or @oldWidth != @newWidth or
				@oldHeight != @newHeight or @oldRotate != @newRotate or
				@oldPersonInCharge != @newPersonInCharge or
				@oldDevManufacturer != @newDevManufacturer or
				@oldDevModel != @newDevModel or
				@oldPosition != @newPosition or
				@oldRatedVoltage != @newRatedVoltage or
				@oldRatedCurrent != @newRatedCurrent or
				@oldPower != @newPower or
				@oldCouplingType != @newCouplingType or
				@oldOutputMaxPressure != @newOutputMaxPressure or
				@oldMedia != @newMedia or
				@oldHeadOfDelivery != @newHeadOfDelivery or
				@oldOutputVolume != @newOutputVolume or
				@oldDevMark != @newDevMark or
				@oldRunStatus != @newRunStatus)
			begin
				--生成附加信息数据
				set @extraMessage = '{"AlmStatus":' + cast(@oldAlmStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_DEVICE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备树删除触发器
if(OBJECT_ID('TGR_SYS_DEVICE_DELETE') is not null)
begin
	drop trigger TGR_SYS_DEVICE_DELETE
end
go
create trigger TGR_SYS_DEVICE_DELETE
on T_SYS_DEVICE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.DevID from (
				select row_number() over (order by DevID) as number, DevID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEVICE', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备树触发器 End-----------------------------

--------------------------测量位置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置添加触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_INSERT') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_INSERT
end
go
create trigger TGR_SYS_MEASURESITE_INSERT
on T_SYS_MEASURESITE for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID from (
				select row_number() over (order by MSiteID) as number, MSiteID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MEASURESITE', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置修改触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_UPDATE') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_UPDATE
end
go
create trigger TGR_SYS_MEASURESITE_UPDATE
on T_SYS_MEASURESITE for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的测量位置状态
		declare @oldMSiteStatus int
		--附加信息
		declare @extraMessage nvarchar(500)
		
		--旧的无线传感器
		declare @oldWSID int
		--旧的轴承形式
		declare @oldBearingType nvarchar(100)
		--旧的润滑形式
		declare @oldLubricatingForm nvarchar(100)
		--旧的轴承
		declare @oldBearingID int
		--旧的温度、电池电压采集时间间隔
		declare @oldTemperatureTime nvarchar(50)
		--旧的特征值采集时间间隔
		declare @oldFlagTime nvarchar(50)
		--旧的波形采集时间间隔
		declare @oldWaveTime nvarchar(50)
		--新的无线传感器
		declare @newWSID int
		--新的轴承形式
		declare @newBearingType nvarchar(100)
		--新的润滑形式
		declare @newLubricatingForm nvarchar(100)
		--新的轴承
		declare @newBearingID int
		--新的温度、电池电压采集时间间隔
		declare @newTemperatureTime nvarchar(50)
		--新的特征值采集时间间隔
		declare @newFlagTime nvarchar(50)
		--新的波形采集时间间隔
		declare @newWaveTime nvarchar(50)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID, @oldMSiteStatus = sourceData.MSiteStatus,
				@oldWSID = sourceData.WSID, @oldBearingType = sourceData.BearingType,
				@oldLubricatingForm = sourceData.LubricatingForm,
				@oldBearingID = sourceData.BearingID,
				@oldTemperatureTime = sourceData.TemperatureTime,
				@oldFlagTime = sourceData.FlagTime, @oldWaveTime = sourceData.WaveTime
			from (
				select row_number() over (order by MSiteID) as number,
					MSiteID, WSID, MSiteStatus, BearingType, LubricatingForm,
					BearingID, TemperatureTime, FlagTime, WaveTime
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newWSID = sourceData.WSID, @newBearingType = sourceData.BearingType,
				@newLubricatingForm = sourceData.LubricatingForm,
				@newBearingID = sourceData.BearingID,
				@newTemperatureTime = sourceData.TemperatureTime,
				@newFlagTime = sourceData.FlagTime, @newWaveTime = sourceData.WaveTime
			from (
				select row_number() over (order by MSiteID) as number,
					WSID, BearingType, LubricatingForm,
					BearingID, TemperatureTime, FlagTime, WaveTime
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldWSID != @newWSID or @oldBearingType != @newBearingType or
				@oldLubricatingForm != @newLubricatingForm or
				@oldBearingID != @newBearingID or @oldTemperatureTime != @newTemperatureTime or
				@oldFlagTime != @newFlagTime or @oldWaveTime != @newWaveTime)
			begin
				--生成附加信息数据
				set @extraMessage = '{"WSID":' + cast(@oldWSID as nvarchar(10)) + ',"MSiteStatus":' + cast(@oldMSiteStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_MEASURESITE', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加测量位置删除触发器
if(OBJECT_ID('TGR_SYS_MEASURESITE_DELETE') is not null)
begin
	drop trigger TGR_SYS_MEASURESITE_DELETE
end
go
create trigger TGR_SYS_MEASURESITE_DELETE
on T_SYS_MEASURESITE for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的无线传感器ID
		declare @WSID int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MSiteID, @WSID = sourceData.WSID from (
				select row_number() over (order by MSiteID) as number, MSiteID, WSID
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"WSID":' + cast(@WSID as nvarchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_MEASURESITE', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------测量位置触发器 End-----------------------------

--------------------------振动信号触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号添加触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_INSERT') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_INSERT
end
go
create trigger TGR_SYS_VIBSINGAL_INSERT
on T_SYS_VIBSINGAL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID from (
				select row_number() over (order by SingalID) as number, SingalID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBSINGAL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号修改触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_UPDATE
end
go
create trigger TGR_SYS_VIBSINGAL_UPDATE
on T_SYS_VIBSINGAL for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的振动信号报警状态
		declare @oldSingalStatus int
		--附加信息
		declare @extraMessage nvarchar(500)
		
		--旧的波长
		declare @oldWaveDataLength int
		--旧的上限频率
		declare @oldUpLimitFrequency int
		--旧的下限频率
		declare @oldLowLimitFrequency int
		--旧的包络带宽
		declare @oldEnlvpBandW int
		--旧的包络滤波器
		declare @oldEnlvpFilter int
		--新的波长
		declare @newWaveDataLength int
		--新的上限频率
		declare @newUpLimitFrequency int
		--新的下限频率
		declare @newLowLimitFrequency int
		--新的包络带宽
		declare @newEnlvpBandW int
		--新的包络滤波器
		declare @newEnlvpFilter int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID, @oldSingalStatus = sourceData.SingalStatus,
				@oldWaveDataLength = sourceData.WaveDataLength,
				@oldUpLimitFrequency = sourceData.UpLimitFrequency,
				@oldLowLimitFrequency = sourceData.LowLimitFrequency,
				@oldEnlvpBandW = sourceData.EnlvpBandW,
				@oldEnlvpFilter = sourceData.EnlvpFilter
			from (
				select row_number() over (order by SingalID) as number,
					SingalID, SingalStatus, WaveDataLength, UpLimitFrequency,
					LowLimitFrequency, EnlvpBandW, EnlvpFilter
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newWaveDataLength = sourceData.WaveDataLength,
				@newUpLimitFrequency = sourceData.UpLimitFrequency,
				@newLowLimitFrequency = sourceData.LowLimitFrequency,
				@newEnlvpBandW = sourceData.EnlvpBandW,
				@newEnlvpFilter = sourceData.EnlvpFilter
			from (
				select row_number() over (order by SingalID) as number,
					WaveDataLength, UpLimitFrequency,
					LowLimitFrequency, EnlvpBandW, EnlvpFilter
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldWaveDataLength != @newWaveDataLength or
				@oldUpLimitFrequency != @newUpLimitFrequency or
				@oldLowLimitFrequency != @newLowLimitFrequency or
				@oldEnlvpBandW != @newEnlvpBandW or
				@oldEnlvpFilter != @newEnlvpFilter)
			begin
				--生成附加信息数据
				set @extraMessage = '{"SingalStatus":' + cast(@oldSingalStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_VIBSINGAL', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号删除触发器
if(OBJECT_ID('TGR_SYS_VIBSINGAL_DELETE') is not null)
begin
	drop trigger TGR_SYS_VIBSINGAL_DELETE
end
go
create trigger TGR_SYS_VIBSINGAL_DELETE
on T_SYS_VIBSINGAL for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--采集方式
		declare @daqStyle int
		--振动信号类型
		declare @singalType int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalID, @daqStyle = sourceData.DAQStyle, @singalType = sourceData.SingalType from (
				select row_number() over (order by SingalID) as number, SingalID, DAQStyle, SingalType
				from deleted
			) sourceData
			where sourceData.number = @index
			--生成附加信息数据
			set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ',"DAQStyle":' + cast(@daqStyle as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBSINGAL', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------振动信号触发器 End-----------------------------

--------------------------无线网关触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关添加触发器
if(OBJECT_ID('TGR_SYS_WG_INSERT') is not null)
begin
	drop trigger TGR_SYS_WG_INSERT
end
go
create trigger TGR_SYS_WG_INSERT
on T_SYS_WG for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID from (
				select row_number() over (order by WGID) as number, WGID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WG', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关修改触发器
if(OBJECT_ID('TGR_SYS_WG_UPDATE') is not null)
begin
	drop trigger TGR_SYS_WG_UPDATE
end
go
create trigger TGR_SYS_WG_UPDATE
on T_SYS_WG for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的无线网关运行状态
		declare @oldRunStatus int
		--旧的无线网关连接状态
		declare @oldLinkStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--旧的监测树ID
		declare @oldMonitorTreeID int
		--旧的网关名称
		declare @oldWGName nvarchar(100)
		--旧的Agent地址
		declare @oldAgentAddress nvarchar(100)
		--旧的网关类型
		declare @oldWGType int
		--旧的负责人
		declare @oldPersonInCharge nvarchar(50)
		--旧的负责人电话
		declare @oldPersonInChargeTel nvarchar(50)
		--新的监测树ID
		declare @newMonitorTreeID int
		--新的网关名称
		declare @newWGName nvarchar(100)
		--新的Agent地址
		declare @newAgentAddress nvarchar(100)
		--新的网关类型
		declare @newWGType int
		--新的负责人
		declare @newPersonInCharge nvarchar(50)
		--新的负责人电话
		declare @newPersonInChargeTel nvarchar(50)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID, @oldRunStatus = sourceData.RunStatus,
				@oldLinkStatus = sourceData.LinkStatus,
				@oldMonitorTreeID = sourceData.MonitorTreeID,
				@oldWGName = sourceData.WGName,
				@oldAgentAddress = sourceData.AgentAddress,
				@oldWGType = sourceData.WGType,
				@oldPersonInCharge = sourceData.PersonInCharge,
				@oldPersonInChargeTel = sourceData.PersonInChargeTel
			from (
				select row_number() over (order by WGID) as number,
					WGID, RunStatus, LinkStatus, MonitorTreeID, WGName, AgentAddress,
					WGType, PersonInCharge, PersonInChargeTel
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newMonitorTreeID = sourceData.MonitorTreeID,
				@newWGName = sourceData.WGName,
				@newAgentAddress = sourceData.AgentAddress,
				@newWGType = sourceData.WGType,
				@newPersonInCharge = sourceData.PersonInCharge,
				@newPersonInChargeTel = sourceData.PersonInChargeTel
			from (
				select row_number() over (order by WGID) as number,
					MonitorTreeID, WGName, AgentAddress,
					WGType, PersonInCharge, PersonInChargeTel
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldMonitorTreeID != @newMonitorTreeID or
				@oldWGName != @newWGName or
				@oldAgentAddress != @newAgentAddress or
				@oldWGType != @newWGType or
				@oldPersonInCharge != @newPersonInCharge or
				@oldPersonInChargeTel != @newPersonInChargeTel)
			begin
				--生成附加信息数据
				set @extraMessage = '{"RunStatus":' + cast(@oldRunStatus as varchar(10)) + ',"LinkStatus":' + cast(@oldLinkStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_WG', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线网关删除触发器
if(OBJECT_ID('TGR_SYS_WG_DELETE') is not null)
begin
	drop trigger TGR_SYS_WG_DELETE
end
go
create trigger TGR_SYS_WG_DELETE
on T_SYS_WG for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.WGID from (
				select row_number() over (order by WGID) as number, WGID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WG', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线网关触发器 End-----------------------------

--------------------------无线传感器温度阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_INSERT
on T_SYS_TEMPE_WS_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_UPDATE
on T_SYS_TEMPE_WS_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)
		
		--旧的高报值
		declare @oldWarnValue real
		--旧的高高报值
		declare @oldAlmValue real
		--新的高报值
		declare @newWarnValue real
		--新的高高报值
		declare @newAlmValue real

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status],
				@oldWarnValue = sourceData.WarnValue, @oldAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number,
					MsiteAlmID, [Status], WarnValue, AlmValue
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newWarnValue = sourceData.WarnValue, @newAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number, WarnValue, AlmValue
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldWarnValue != @newWarnValue or @oldAlmValue != @newAlmValue)
			begin
				--生成附加信息数据
				set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_TEMPE_WS_SET_MSITEALM_DELETE
on T_SYS_TEMPE_WS_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_WS_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器温度阈值设置触发器 End-----------------------------

--------------------------无线传感器电池电压阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_INSERT
on T_SYS_VOLTAGE_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_UPDATE
on T_SYS_VOLTAGE_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)
		
		--旧的高报值
		declare @oldWarnValue real
		--旧的高高报值
		declare @oldAlmValue real
		--新的高报值
		declare @newWarnValue real
		--新的高高报值
		declare @newAlmValue real

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status],
				@oldWarnValue = sourceData.WarnValue, @oldAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number,
					MsiteAlmID, [Status], WarnValue, AlmValue
				from deleted
			) sourceData
			where sourceData.number = @index
			--根据索引取出新的高报值和高高报值
			select @newWarnValue = sourceData.WarnValue, @newAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number, WarnValue, AlmValue
				from inserted
			) sourceData
			where sourceData.number = @index

			if(@oldWarnValue != @newWarnValue or @oldAlmValue != @newAlmValue)
			begin
				--生成附加信息数据
				set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_VOLTAGE_SET_MSITEALM_DELETE
on T_SYS_VOLTAGE_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VOLTAGE_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器电池电压阈值设置触发器 End-----------------------------

--------------------------设备温度阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_INSERT
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_UPDATE
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)

		--旧的高报值
		declare @oldWarnValue real
		--旧的高高报值
		declare @oldAlmValue real
		--新的高报值
		declare @newWarnValue real
		--新的高高报值
		declare @newAlmValue real

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID, @oldStatus = sourceData.[Status],
				@oldWarnValue = sourceData.WarnValue, @oldAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number,
					MsiteAlmID, [Status], WarnValue, AlmValue
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newWarnValue = sourceData.WarnValue, @newAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by MsiteAlmID) as number, WarnValue, AlmValue
				from inserted
			) sourceData
			where sourceData.number = @index
			
			if(@oldWarnValue != @newWarnValue or @oldAlmValue != @newAlmValue)
			begin
				--生成附加信息数据
				set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE
end
go
create trigger TGR_SYS_TEMPE_DEVICE_SET_MSITEALM_DELETE
on T_SYS_TEMPE_DEVICE_SET_MSITEALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteAlmID from (
				select row_number() over (order by MsiteAlmID) as number, MsiteAlmID
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_TEMPE_DEVICE_SET_MSITEALM', @ID, 3, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备温度阈值设置触发器 End-----------------------------

--------------------------振动信号报警阈值设置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置添加触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_INSERT
on T_SYS_VIBRATING_SET_SIGNALALM for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID from (
				select row_number() over (order by SingalAlmID) as number, SingalAlmID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置修改触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_UPDATE
on T_SYS_VIBRATING_SET_SIGNALALM for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--旧的报警状态
		declare @oldStatus int
		--附加信息
		declare @extraMessage nvarchar(500)
		
		--旧的高报值
		declare @oldWarnValue real
		--旧的高高报值
		declare @oldAlmValue real
		--新的高报值
		declare @newWarnValue real
		--新的高高报值
		declare @newAlmValue real

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID, @oldStatus = sourceData.[Status],
				@oldWarnValue = sourceData.WarnValue, @oldAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by SingalAlmID) as number,
					SingalAlmID, [Status], WarnValue, AlmValue
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出修改后的数据
			select @newWarnValue = sourceData.WarnValue, @newAlmValue = sourceData.AlmValue
			from (
				select row_number() over (order by SingalAlmID) as number, WarnValue, AlmValue
				from inserted
			) sourceData
			where sourceData.number = @index
			
			if(@oldWarnValue != @newWarnValue or @oldAlmValue != @newAlmValue)
			begin
				--生成附加信息数据
				set @extraMessage = '{"Status":' + cast(@oldStatus as varchar(10)) + '}'

				--添加到云通讯推送数据表
				insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
				select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 2, ID, 1, 3, @extraMessage, getdate()
				from T_SYS_CLOUDCONFIG
				where IsUse = 1 and [Type] = 2
			end
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加振动信号报警阈值设置删除触发器
if(OBJECT_ID('TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE') is not null)
begin
	drop trigger TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE
end
go
create trigger TGR_SYS_VIBRATING_SET_SIGNALALM_DELETE
on T_SYS_VIBRATING_SET_SIGNALALM for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int
		--振动信号ID
		declare @signalID int
		--振动信号类型
		declare @singalType int
		--振动信号采集方式
		declare @daqStyle int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.SingalAlmID, @signalID = sourceData.SingalID from (
				select row_number() over (order by SingalAlmID) as number, SingalAlmID, SingalID
				from deleted
			) sourceData
			where sourceData.number = @index
			--取出振动信号类型的信号类型、采集方式
			select @singalType = SingalType, @daqStyle = DAQStyle from T_SYS_VIBSINGAL where SingalID = @signalID
			--生成附加信息数据
			set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ',"DAQStyle":' + cast(@daqStyle as varchar(10)) + '}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_VIBRATING_SET_SIGNALALM', @ID, 3, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------振动信号报警阈值设置触发器 End-----------------------------

--------------------------传感器报警记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加传感器报警记录添加触发器
if(OBJECT_ID('TGR_SYS_WSN_ALMRECORD_INSERT') is not null)
begin
	drop trigger TGR_SYS_WSN_ALMRECORD_INSERT
end
go
create trigger TGR_SYS_WSN_ALMRECORD_INSERT
on T_SYS_WSN_ALMRECORD for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID from (
				select row_number() over (order by AlmRecordID) as number, AlmRecordID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WSN_ALMRECORD', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加传感器报警记录修改触发器
if(OBJECT_ID('TGR_SYS_WSN_ALMRECORD_UPDATE') is not null)
begin
	drop trigger TGR_SYS_WSN_ALMRECORD_UPDATE
end
go
create trigger TGR_SYS_WSN_ALMRECORD_UPDATE
on T_SYS_WSN_ALMRECORD for update
as
begin
	if(exists (select top 1 1 from inserted))
	begin
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
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID,
				@mSiteID = sourceData.MSiteID,
				@wgID = sourceData.WGID,
				@wsID = sourceData.WSID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate
			from (
				select row_number() over (order by AlmRecordID) as number,
					AlmRecordID,
					MSiteID,
					WGID,
					WSID,
					BDate,
					EDate
				from inserted
			) sourceData
			where sourceData.number = @index
			if(isnull(@mSiteID, 0) < 1)
			begin
				continue
			end

			--生成附加信息数据
			set @extraMessage = '{"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
				+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_WSN_ALMRECORD', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------传感器报警记录触发器 End-----------------------------

--------------------------设备报警记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备报警记录添加触发器
if(OBJECT_ID('TGR_SYS_DEV_ALMRECORD_INSERT') is not null)
begin
	drop trigger TGR_SYS_DEV_ALMRECORD_INSERT
end
go
create trigger TGR_SYS_DEV_ALMRECORD_INSERT
on T_SYS_DEV_ALMRECORD for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID from (
				select row_number() over (order by AlmRecordID) as number, AlmRecordID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEV_ALMRECORD', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备报警记录修改触发器
if(OBJECT_ID('TGR_SYS_DEV_ALMRECORD_UPDATE') is not null)
begin
	drop trigger TGR_SYS_DEV_ALMRECORD_UPDATE
end
go
create trigger TGR_SYS_DEV_ALMRECORD_UPDATE
on T_SYS_DEV_ALMRECORD for update
as
begin
	if(exists (select top 1 1 from inserted))
	begin
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
		--振动信号类型
		declare @singalType int
		--振动信号采集方式
		declare @daqStyle int
		--报警类型
		declare @msAlmID int
		--附加信息
		declare @extraMessage nvarchar(500)

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @extraMessage = null
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.AlmRecordID,
				@deviceID = sourceData.DevID,
				@measureSiteID = sourceData.MSiteID,
				@signalID = sourceData.SingalID,
				@startTime = sourceData.BDate,
				@endTime = sourceData.EDate,
				@msAlmID = sourceData.MSAlmID
			from (
				select row_number() over (order by AlmRecordID) as number,
					AlmRecordID,
					DevID,
					MSiteID,
					SingalID,
					BDate,
					EDate,
					MSAlmID
				from inserted
			) sourceData
			where sourceData.number = @index
			--振动报警结束
			if(@msAlmID = 1)
			begin
				--取出振动信号类型的信号类型、采集方式
				select @singalType = SingalType, @daqStyle = DAQStyle from T_SYS_VIBSINGAL where SingalID = @signalID
				--生成附加信息数据
				set @extraMessage = '{"SingalType":' + cast(@singalType as varchar(10)) + ','
					+ '"DAQStyle":' + cast(@daqStyle as varchar(10)) + ','
					+ '"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
					+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'
			end
			--温度报警结束
			else if(@msAlmID = 2)
			begin
				--生成附加信息数据
				set @extraMessage = '{"SingalType":0,"DAQStyle":0,'
					+ '"BDate":"' + convert(varchar(20), @startTime, 20) + '",'
					+ '"EDate":"' + convert(varchar(20), @endTime, 20) + '"}'
			end

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_DEV_ALMRECORD', @ID, 2, ID, 1, 3, @extraMessage, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备报警记录触发器 End-----------------------------

--------------------------无线传感器温度采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_1_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_2_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_3_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_TEMPE_WS_MSITEDATA_4_INSERT
on T_DATA_TEMPE_WS_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_WS_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器温度采集数据触发器 End-----------------------------

--------------------------无线传感器电池电压采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_1_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_2_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_3_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加无线传感器电池电压采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_VOLTAGE_WS_MSITEDATA_4_INSERT
on T_DATA_VOLTAGE_WS_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VOLTAGE_WS_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------无线传感器电池电压采集数据触发器 End-----------------------------

--------------------------设备温度采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_1_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_1 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_1', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_2_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_2 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_2', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_3_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_3 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_3', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加设备温度采集数据添加触发器
if(OBJECT_ID('TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT') is not null)
begin
	drop trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT
end
go
create trigger TGR_DATA_TEMPE_DEVICE_MSITEDATA_4_INSERT
on T_DATA_TEMPE_DEVICE_MSITEDATA_4 for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.MsiteDataID from (
				select row_number() over (order by MsiteDataID) as number, MsiteDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_TEMPE_DEVICE_MSITEDATA_4', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------设备温度采集数据触发器 End-----------------------------

--------------------------特征值采集数据触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加加速度特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加速度特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加位移特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加包络特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加LQ特征值采集数据添加触发器
if(OBJECT_ID('TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT') is not null)
begin
	drop trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT
end
go
create trigger TGR_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ_INSERT
on T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.HisDataID from (
				select row_number() over (order by HisDataID) as number, HisDataID
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ', @ID, 1, ID, 1, 3, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------特征值采集数据触发器 End-----------------------------

--------------------------操作记录触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加操作记录添加触发器
if(OBJECT_ID('TGR_SYS_OPERATION_INSERT') is not null)
begin
	drop trigger TGR_SYS_OPERATION_INSERT
end
go
create trigger TGR_SYS_OPERATION_INSERT
on T_SYS_OPERATION for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_OPERATION', @ID, 1, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加操作记录修改触发器
if(OBJECT_ID('TGR_SYS_OPERATION_UPDATE') is not null)
begin
	drop trigger TGR_SYS_OPERATION_UPDATE
end
go
create trigger TGR_SYS_OPERATION_UPDATE
on T_SYS_OPERATION for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select 'T_SYS_OPERATION', @ID, 2, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------操作记录触发器 End-----------------------------

--------------------------云通讯配置触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置添加触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_INSERT') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_INSERT
end
go
create trigger TGR_SYS_CLOUDCONFIG_INSERT
on T_SYS_CLOUDCONFIG for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from inserted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 1, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置修改触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_UPDATE') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_UPDATE
end
go
create trigger TGR_SYS_CLOUDCONFIG_UPDATE
on T_SYS_CLOUDCONFIG for update
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 2, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--创建人：张辽阔
--创建时间：2016-12-13
--创建记录：添加云通讯配置删除触发器
if(OBJECT_ID('TGR_SYS_CLOUDCONFIG_DELETE') is not null)
begin
	drop trigger TGR_SYS_CLOUDCONFIG_DELETE
end
go
create trigger TGR_SYS_CLOUDCONFIG_DELETE
on T_SYS_CLOUDCONFIG for delete
as
begin
	if(exists (select top 1 1 from deleted))
	begin
		set nocount on

		--主键ID
		declare @ID int
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from deleted
		set @index = 0

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @ID = sourceData.id from (
				select row_number() over (order by id) as number, id
				from deleted
			) sourceData
			where sourceData.number = @index

			--添加到云通讯推送数据表
			insert into T_DATA_CLOUDPUSH (TableName, TableNameId, OperationStatus, PlatformId, DataStatus, [Priority], ExtraMessage, AddDate)
			select top 1 'T_SYS_CLOUDCONFIG', @ID, 3, ID, 1, 1, null, getdate()
			from T_SYS_CLOUDCONFIG
			where IsUse = 1 and [Type] = 2
		end

		set nocount off
	end
end
go
--------------------------云通讯配置触发器 End-----------------------------

--------------------------推送表触发器 Start-----------------------------
--创建人：张辽阔
--创建时间：2016-12-06
--创建记录：添加推送表添加触发器
if(OBJECT_ID('TGR_DATA_CLOUDPUSH_INSERT') is not null)
begin
	drop trigger TGR_DATA_CLOUDPUSH_INSERT
end
go
create trigger TGR_DATA_CLOUDPUSH_INSERT
on T_DATA_CLOUDPUSH for insert
as
begin
	if(exists (select top 1 1 from inserted))
	begin
		declare @serviceUrl as varchar(1000)
		declare @object int
		declare @platformId int
		declare @parameter nvarchar(500)
		--该操作影响的行数
		declare @count int
		--循环索引
		declare @index int

		--给变量赋值
		select @count = count(1) from inserted
		set @index = 0
		--通过http协议调用的接口地址
		set @serviceUrl = 'http://localhost:2893/CloudProxy/ProxyNotifyService/ReceiveTriggerAndRequestCloudCommunication'

		while(@index < @count)
		begin
			set @index = @index + 1
			--根据索引取出数据
			select @platformId = sourceData.PlatformId from (
				select row_number() over (order by PlatformId) as number, PlatformId
				from inserted
			) sourceData
			where sourceData.number = @index

			set @parameter = '{"Key":"5bcbc178cf70e1ec7ca1586a1eaac1d3","PlatformId":' + cast(@platformId as varchar(50)) + '}'
			set @parameter = '{"Key":"5bcbc178cf70e1ec7ca1586a1eaac1d3","PlatformId":' + cast(@platformId as varchar(50))
				+ ',"Sign":"' + lower(convert(varchar(max), hashbytes('MD5', @parameter + '252a7d7582a39c899de71efa8b6fb368'), 2)) + '"}'
			
			exec sp_OACreate 'Msxml2.ServerXMLHTTP.3.0', @object out
			exec sp_OAMethod @object, 'open', NULL, 'post', @serviceUrl
			exec sp_OAMethod @object, 'send', NULL, @parameter
			exec sp_OADestroy @object
		end
	end
end
go
--------------------------推送表触发器 End-----------------------------


