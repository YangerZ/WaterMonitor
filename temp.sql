USE [temp]
GO
/****** Object:  Table [dbo].[DataTab]    Script Date: 2018/7/30 16:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataTab](
	[rtu_id] [nchar](20) NOT NULL,
	[rtu_time] [smalldatetime] NULL,
	[water_level] [nchar](10) NULL,
	[water_flow] [nchar](10) NULL,
	[water_pressure] [nchar](10) NULL,
	[water_tem] [nchar](10) NULL,
	[water_ph] [nchar](10) NULL,
	[water_turb] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StationTab]    Script Date: 2018/7/30 16:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationTab](
	[rtu_id] [nchar](20) NOT NULL,
	[rtu_name] [nvarchar](50) NULL,
	[rtu_region] [nvarchar](50) NULL,
	[sta_name] [nvarchar](50) NOT NULL,
	[SIM] [nvarchar](50) NULL,
	[rtu_type] [nvarchar](50) NULL,
	[rtu_unit] [nvarchar](50) NULL,
 CONSTRAINT [PK_StationTab] PRIMARY KEY CLUSTERED 
(
	[rtu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersTab]    Script Date: 2018/7/30 16:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTab](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[user_password] [nvarchar](50) NOT NULL,
	[user_region] [nvarchar](50) NULL,
	[user_unit] [nvarchar](50) NOT NULL,
	[user_tel] [nvarchar](50) NULL,
	[user_mail] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[user_role] [nvarchar](50) NULL,
 CONSTRAINT [PK_UsersTab] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsersTab'
GO
