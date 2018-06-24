USE [OYW.OA]
GO
/****** Object:  Table [dbo].[ORG_User]    Script Date: 2018/6/24 11:42:26 ******/
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
/****** Object:  Table [dbo].[SYS_Menu]    Script Date: 2018/6/24 11:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_Menu](
	[ID] [uniqueidentifier] NOT NULL,
	[MenuName] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
	[App] [nvarchar](50) NULL,
	[Controller] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NULL,
	[Params] [nvarchar](50) NULL,
	[URL] [nvarchar](500) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[Sort] [int] NULL,
	[EmplID] [nvarchar](50) NULL,
	[EmplName] [nvarchar](50) NULL,
	[DeptID] [nvarchar](50) NULL,
	[DeptName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK__SYS_Menu__3214EC2703317E3D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ORG_User] ([UserName], [UserPwd], [EmplID], [UserLogonTimes], [UserNeedChgPwd], [UserDisabled], [UserLastLogonIP], [CreateTime], [UpdateTime]) VALUES (N'admin', N'21232f297a57a5a743894ae4a801fc3', N'99999999-9999-9999-9999-999999999999', 0, 0, 0, N'127.0.0.1', CAST(0x0000A90800DF6A08 AS DateTime), CAST(0x0000A90800DF6A08 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'cffde5eb-f366-4d11-9f69-11f966fe4bc6', N'职位', N'Position', N'glyphicon glyphicon-th-large', N'OA.People', N'Position', N'Index', NULL, N'/Position/Index?app=People&menu=Position', N'bb838c09-273a-4409-ac9b-376a606d218d', 3, N'', N'', N'', N'', CAST(0x0000A7B6016077F0 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'a41dadb9-57e2-465e-84ca-2d0dc8e18aaa', N'部门和人员', N'Index', N'glyphicon glyphicon-th-large', N'OA.People', N'Home', N'Index', NULL, N'/PeopleManager/Index?app=People&menu=Index', N'bb838c09-273a-4409-ac9b-376a606d218d', 1, N'', N'', N'', N'', CAST(0x0000A7B601607D57 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'814bc2a4-7588-4ace-9725-37589d7ae5a3', N'角色', N'Role', N'glyphicon glyphicon-th-large', N'OA.People', N'Role', N'Index', NULL, N'/RoleManager/Index?app=People&menu=Role', N'bb838c09-273a-4409-ac9b-376a606d218d', 4, N'', N'', N'', N'', CAST(0x0000A7B601607514 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'bb838c09-273a-4409-ac9b-376a606d218d', N'组织和人员', N'People', N'glyphicon glyphicon-user', N'OA.People', N'Home', N'Index', NULL, N'/PeopleManager/Index?app=People&menu=Index', NULL, 5, N'', N'', N'', N'', CAST(0x0000A7B601608237 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'8a03814e-6b60-45db-a4b8-57bc18998ed8', N'我的桌面', N'Welcome', N'glyphicon glyphicon-th-large', NULL, N'Welcome', N'Desktop', NULL, N'/Welcome/Desktop?app=Web&menu=Welcome', N'2ba128e7-ada4-4de7-9bf9-af3561482575', 1, N'', N'', N'', N'', CAST(0x0000A7B50103D8DE AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'92852e3f-bbf6-428b-9e71-798b5b348401', N'配置桌面', N'DesktopConfig', N'glyphicon glyphicon-th-large', NULL, N'DesktopConfig', N'Index', NULL, N'/DesktopConfig/Index?app=Web&menu=DesktopConfig', N'2ba128e7-ada4-4de7-9bf9-af3561482575', 2, N'', N'', N'', N'', CAST(0x0000A7B50103DD4D AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'45955858-6545-4569-9432-a80ef278d4e5', N'登录账号', N'User', N'glyphicon glyphicon-th-large', N'OA.People', N'User', N'Index', NULL, N'/UserManager/Index?app=People&menu=User', N'bb838c09-273a-4409-ac9b-376a606d218d', 2, N'', N'', N'', N'', CAST(0x0000A7B601607AE5 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'2ba128e7-ada4-4de7-9bf9-af3561482575', N'门户', N'Web', N'glyphicon glyphicon-th-large', NULL, N'Welcome', N'Desktop', NULL, N'/Welcome/Desktop?app=Web&menu=Welcome', NULL, 1, N'', N'', N'', N'', CAST(0x0000A7B50105CC11 AS DateTime))
GO
INSERT [dbo].[SYS_Menu] ([ID], [MenuName], [Code], [Icon], [App], [Controller], [Action], [Params], [URL], [ParentID], [Sort], [EmplID], [EmplName], [DeptID], [DeptName], [CreateTime]) VALUES (N'97592db5-11fa-419b-be16-fd909cdb8ede', N'登录日志', N'EmplLogon', N'glyphicon glyphicon-th-large', N'OA.People', N'EmplLogon', N'Index', NULL, N'/EmplLogon/Index?app=People&menu=EmplLogon', N'bb838c09-273a-4409-ac9b-376a606d218d', 5, N'', N'', N'', N'', CAST(0x0000A7CA015F9805 AS DateTime))
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UserNeedChgPwd]  DEFAULT ((0)) FOR [UserNeedChgPwd]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UserDisabled]  DEFAULT ((0)) FOR [UserDisabled]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[ORG_User] ADD  CONSTRAINT [DF_ORG_User_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MenuName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Icon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'Icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'Action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ParentID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sort' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'EmplID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'EmplID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'EmplName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'EmplName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DeptID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'DeptID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DeptName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'DeptName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CreateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_Menu', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
