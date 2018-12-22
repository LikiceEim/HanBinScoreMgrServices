USE [iCMSDB]

GO

execute sp_rename   'T_SYS_USER_RALATION_DEVICE' ,'T_SYS_USER_RELATION_DEVICE';
GO



--T_DICT_CONFIG
alter table [T_DICT_CONFIG] add [Code] nvarchar(100) null;
GO

alter table [T_DICT_CONFIG] alter column [ParentId] int null;
GO

alter table [T_DICT_CONFIG] add [OrderNo] int null;
GO

alter table [T_DICT_CONFIG] add [CommonDataType] int null;
GO

alter table [T_DICT_CONFIG] add [CommonDataCode] nvarchar(50) null;
GO

ALTER TABLE [T_DICT_CONFIG] DROP CONSTRAINT [PK_T_SYS_CONFIG]
GO

ALTER TABLE [T_DICT_CONFIG] ADD CONSTRAINT [PK_T_DICT_CONFIG] PRIMARY KEY CLUSTERED([ID])
GO

--T_DICT_CONNECT_STATUS_TYPE

alter table [T_DICT_CONNECT_STATUS_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_CONNECT_STATUS_TYPE] add [OrderNo] int null;
GO


--T_DICT_DEVICE_TYPE
alter table [T_DICT_DEVICE_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_DEVICE_TYPE] add [OrderNo] int null;
GO

--T_DICT_EIGEN_VALUE_TYPE
alter table [T_DICT_EIGEN_VALUE_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_EIGEN_VALUE_TYPE] add [OrderNo] int null;
GO


--T_DICT_MEASURE_SITE_MONITOR_TYPE
alter table [T_DICT_MEASURE_SITE_MONITOR_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_MEASURE_SITE_MONITOR_TYPE] add [OrderNo] int  null;
GO

--T_DICT_MEASURE_SITE_TYPE
alter table [T_DICT_MEASURE_SITE_TYPE] add [COde] nvarchar(50) null;
GO

alter table [T_DICT_MEASURE_SITE_TYPE] add [OrderNo] int  null;
GO

--T_DICT_MONITORTREE_TYPE
alter table [T_DICT_MONITORTREE_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_MONITORTREE_TYPE] add [OrderNo] int  null;
GO

--T_DICT_SENSOR_TYPE
alter table [T_DICT_SENSOR_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_SENSOR_TYPE] add [OrderNo] int null;
GO

--T_DICT_VIBRATING_SIGNAL_TYPE
alter table [T_DICT_VIBRATING_SIGNAL_TYPE] add [Code] nvarchar(50) null;
GO

alter table [T_DICT_VIBRATING_SIGNAL_TYPE] add [OrderNo] int null;
GO

--T_DICT_WAVE_LENGTH_VALUE
alter table [T_DICT_WAVE_LENGTH_VALUE] add [OrderNo] int  null;
GO

alter table [T_DICT_WAVE_LENGTH_VALUE] add [Code] nvarchar(50) null;
GO

--T_DICT_WAVE_LOWERLIMIT_VALUE
alter table [T_DICT_WAVE_LOWERLIMIT_VALUE] add [OrderNo] int null;
GO

alter table [T_DICT_WAVE_LOWERLIMIT_VALUE] add [Code] nvarchar(100) null;
GO

--T_DICT_WAVE_UPPERLIMIT_VALUE
alter table [T_DICT_WAVE_UPPERLIMIT_VALUE] add [OrderNo] int null;
GO

alter table [T_DICT_WAVE_UPPERLIMIT_VALUE] add [Code] nvarchar(100) null;
GO

--T_DICT_WIRELESS_GATEWAY_TYPE
alter table [T_DICT_WIRELESS_GATEWAY_TYPE] add [OrderNo] int  null;
GO

alter table [T_DICT_WIRELESS_GATEWAY_TYPE] add [Code] nvarchar(100) null;
GO

--T_SYS_DEVICE
alter table [T_SYS_DEVICE] add [OperationDate] datetime null;
GO

alter table [T_SYS_DEVICE] add [WGID] int null;
GO

alter table [T_SYS_DEVICE] add [MonitorDevType] int null;
GO

alter table [T_SYS_DEVICE] add [LastUpdateTime] datetime null;
GO

alter table [T_SYS_DEVICE] add [DeviceStopDate] datetime null;
GO

--T_SYS_MEASURESITE
alter table [T_SYS_MEASURESITE] alter column [BearingType] nvarchar(100) null;
GO

alter table [T_SYS_MEASURESITE] alter column [LubricatingForm] nvarchar(100) null;
GO

--T_SYS_MODULE
alter table [T_SYS_MODULE] alter column [Code] nvarchar(50);
GO

alter table [T_SYS_MODULE] alter column [Code] nvarchar(50) not null;
GO

alter table [T_SYS_MODULE] add [CommonDataType] int null;
GO

alter table [T_SYS_MODULE] add [CommonDataCode] nvarchar(50) null;
GO

alter table [T_SYS_MODULE] add [Describe] nvarchar(200) null;
GO

--T_SYS_MONITOR_TREE_PROPERTY
alter table [T_SYS_MONITOR_TREE_PROPERTY] alter column [Address] nvarchar(100) null;
GO

--T_SYS_ROLE
alter table [T_SYS_ROLE] add [RoleCode] nvarchar(50) null;
GO

--T_SYS_ROLEMODULE
alter table [T_SYS_ROLEMODULE] add [RoleCode] nvarchar(50)  null;
GO

alter table [T_SYS_ROLEMODULE] add [ModuleCode] nvarchar(50) null;
GO

alter table [T_SYS_ROLEMODULE] drop column [RoleID];
GO

alter table [T_SYS_ROLEMODULE] drop column [ModuleID];
GO

--T_SYS_WG
alter table [T_SYS_WG] add [PowerSupplyModeTypeID] int null;
GO

alter table [T_SYS_WG] add [GateWayMAC] nvarchar(16) null;
GO

alter table [T_SYS_WG] add [LastSleepTime] datetime null;
GO

alter table [T_SYS_WG] add [Duration] real null;
GO

alter table [T_SYS_WG] add [IsOnLine] bit null;
GO


