USE [OYW.OA]
GO
/****** Object:  Table [dbo].[ORG_User]    Script Date: 2018/6/23 15:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_User](
	[UserName] [nvarchar](50) NOT NULL,
	[UserPwd] [nvarchar](500) NULL,
	[EmplID] [nvarchar](50) NULL,
	[UserLogonTimes] [int] NULL,
	[UserNeedChgPwd] [int] NULL,
	[UserDisabled] [int] NULL,
	[UserLastLogonIP] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UserNeedChgPwd]  DEFAULT ((0)) FOR [UserNeedChgPwd]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UserDisabled]  DEFAULT ((0)) FOR [UserDisabled]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
