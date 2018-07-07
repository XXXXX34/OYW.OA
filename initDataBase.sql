USE [OYW.OA]
GO
/****** Object:  Table [dbo].[ORG_Department]    Script Date: 2018/7/7 11:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Department](
	[DeptID] [nvarchar](50) NOT NULL,
	[DeptName] [nvarchar](50) NULL,
	[DeptDescr] [nvarchar](250) NULL,
	[ParentID] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[DeptHierarchyCode] [nvarchar](50) NULL,
 CONSTRAINT [Departments_PK] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ORG_User]    Script Date: 2018/7/7 11:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_User](
	[ID] [uniqueidentifier] NOT NULL,
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
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ORG_UserLogon]    Script Date: 2018/7/7 11:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_UserLogon](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[IP] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[LogonTime] [datetime] NULL,
 CONSTRAINT [PK_ORG_EmplLogon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SYS_Menu]    Script Date: 2018/7/7 11:09:40 ******/
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
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'4206d17c-04c1-41cb-85f9-dc016d3884ba', N'华东大区', N'华东大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A7890160AC02 AS DateTime), CAST(0x0000A7890160AC04 AS DateTime), N'00000007')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'4298086e-f15c-4240-b302-f740ea16c40d', N'长春分公司', N'长春分公司', N'a43ed460-827a-4eff-b3ab-a514ad69e7ee', CAST(0x0000A78901611BEB AS DateTime), CAST(0x0000A78901611BEC AS DateTime), N'000000030001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'446b5457-45df-453d-a4e7-bbe539913648', N'北京分公司-运营部', N'北京分公司-运营部', N'46e3ddb7-b05f-4a2b-94ac-7fff6cbce160', CAST(0x0000A789016543C0 AS DateTime), CAST(0x0000A789016543C2 AS DateTime), N'0000000200010002')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'46e3ddb7-b05f-4a2b-94ac-7fff6cbce160', N'北京分公司', N'北京分公司', N'8367fc28-e2be-4591-9a11-a86a2f1b79b9', CAST(0x0000A7890164C224 AS DateTime), CAST(0x0000A7890164C225 AS DateTime), N'000000020001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'5194056c-78ea-4182-8ad4-ae894f404280', N'北京分公司-业务部', N'北京分公司-业务部', N'46e3ddb7-b05f-4a2b-94ac-7fff6cbce160', CAST(0x0000A789016528C8 AS DateTime), CAST(0x0000A789016528CA AS DateTime), N'0000000200010001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'5b92ffb3-154a-42f4-90aa-04494ae31b92', N'石家庄分公司', N'石家庄分公司', N'8367fc28-e2be-4591-9a11-a86a2f1b79b9', CAST(0x0000A7890164DB3D AS DateTime), CAST(0x0000A7890164DB40 AS DateTime), N'000000020002')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'6bbaedd7-9646-4e8d-b62d-6023c9f5ab20', N'上海总部-财务部', N'上海总部-财务部', N'abf300c3-0294-440a-b0b9-0ae3763ee10a', CAST(0x0000A7890160FE95 AS DateTime), CAST(0x0000A7890160FE97 AS DateTime), N'000000010002')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'7ec08cc7-e2cb-4e85-bfae-eac3d72855bc', N'上海总部-业务发展部', N'上海总部-业务发展部', N'abf300c3-0294-440a-b0b9-0ae3763ee10a', CAST(0x0000A7890160EAD7 AS DateTime), CAST(0x0000A7890160EAD8 AS DateTime), N'000000010001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'8367fc28-e2be-4591-9a11-a86a2f1b79b9', N'京津冀区', N'京津冀区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A789016054B1 AS DateTime), CAST(0x0000A789016054B2 AS DateTime), N'00000002')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'8cba13c3-b76f-445f-9937-430bb2b3ccf1', N'天津分公司', N'天津分公司', N'8367fc28-e2be-4591-9a11-a86a2f1b79b9', CAST(0x0000A78901650B23 AS DateTime), CAST(0x0000A78901650B24 AS DateTime), N'000000020003')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'8f5d3a93-4894-4a7a-830a-79e6e1642f48', N'长春分公司-运营部', N'长春分公司-运营部', N'4298086e-f15c-4240-b302-f740ea16c40d', CAST(0x0000A78901612F33 AS DateTime), CAST(0x0000A78901612F35 AS DateTime), N'0000000300010001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'90ec8dfe-6c52-4cba-b275-3e9846a758d6', N'深圳分公司', N'深圳分公司', N'98caa10d-e999-45ec-b2b7-33e81a1fcbf7', CAST(0x0000A7B900C89309 AS DateTime), CAST(0x0000A7B900C89311 AS DateTime), N'000000040001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'98caa10d-e999-45ec-b2b7-33e81a1fcbf7', N'华南大区', N'华南大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A78901606F5C AS DateTime), CAST(0x0000A78901606F5E AS DateTime), N'00000004')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'9b745a3b-c0e8-4bb7-b399-4a2a38d23015', N'西北大区', N'西北大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A78901609DE3 AS DateTime), CAST(0x0000A78901609DE5 AS DateTime), N'00000006')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'9c5fadf6-de6d-4942-a0f6-358ecb2020d0', N'西南大区', N'西南大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A78901607AB3 AS DateTime), CAST(0x0000A78901607AB5 AS DateTime), N'00000005')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'a43ed460-827a-4eff-b3ab-a514ad69e7ee', N'东北大区', N'东北大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A789016063B6 AS DateTime), CAST(0x0000A789016063B8 AS DateTime), N'00000003')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'abf300c3-0294-440a-b0b9-0ae3763ee10a', N'上海总部', N'上海总部', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A7890160164F AS DateTime), CAST(0x0000A78901602136 AS DateTime), N'00000001')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'dedd0290-f839-48fb-be67-4244b890918e', N'华中大区', N'华中大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A7890160C478 AS DateTime), CAST(0x0000A7890160C47A AS DateTime), N'00000009')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'efd7aa48-7a39-4d44-ba9d-182ebbd35e4d', N'华北大区', N'华北大区', N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', CAST(0x0000A7890160BA30 AS DateTime), CAST(0x0000A7890160BA31 AS DateTime), N'00000008')
GO
INSERT [dbo].[ORG_Department] ([DeptID], [DeptName], [DeptDescr], [ParentID], [CreateTime], [UpdateTime], [DeptHierarchyCode]) VALUES (N'fe6258ca-1ade-4a62-ae9f-dca5d7715da0', N'总部', N'总部', N'0', CAST(0x0000A6EE00000000 AS DateTime), CAST(0x0000A6EE00000000 AS DateTime), N'0')
GO
INSERT [dbo].[ORG_User] ([ID], [UserName], [UserPwd], [EmplID], [UserLogonTimes], [UserNeedChgPwd], [UserDisabled], [UserLastLogonIP], [CreateTime], [UpdateTime]) VALUES (N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'admin', N'21232f297a57a5a743894ae4a801fc3', N'99999999-9999-9999-9999-999999999999', 0, 0, 0, N'127.0.0.1', CAST(0x0000A90800DF6A08 AS DateTime), CAST(0x0000A90800DF6A08 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'd6e4fe49-97ef-443a-a701-0718230749aa', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00F390F5 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'17bcc4ab-d81c-4c58-aa8c-1471db804f97', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600B692A3 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'81eab7a2-8880-4e42-8fad-1a5414522d1f', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91000E6DA3F AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'55a55af9-b6b6-451b-9be6-1d72c78f4b14', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A9100098DC1C AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'49b4a855-060f-4dcf-98e4-2e37e5fd040b', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A9100094FDFA AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'5b9e2b88-d009-4bd8-94ea-31309c073331', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A910009392FB AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'ed29b420-d0c5-4bc4-9c81-3a020adc5368', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00D80C29 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'426435d7-6290-449c-b614-3a1e6b0e023c', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00D734EB AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'87e98e3a-6293-4b20-bca2-3b771cf08208', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A910008B6060 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'af389923-cb34-46b1-91a0-3be4757b4080', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00F0C4A1 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'13773665-ca09-4b48-ad97-3f41b75a7f3f', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600B4FAAB AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'454e2c30-cca5-4cc6-b433-40a687469bdc', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00FF1750 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'1053a789-0c18-46ab-9384-42e5f37881fd', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91000E70B25 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'b10c9e3e-dd91-49d8-82d0-48224ad0676b', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00F174A5 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'e611df15-2331-4238-b577-4fb7d84e4b43', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600B5B407 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'b6cd95be-c202-42cb-8ef0-501010c20888', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600B4608D AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'721cf9a6-d1a7-4231-aabb-52091214775d', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A910009899BE AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'87ee68f5-f536-4b8b-b6e0-836de6ec1770', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600AD5F24 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'8f157f4c-5da5-4304-9bf5-866ae3601396', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A91000914D10 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'33b8e526-82df-46c8-b79b-881b085322d6', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00D75718 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'1060887f-08e6-42f5-9811-8bc3294c7a8e', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00FF0E49 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'e582669c-1efc-4c54-8ccb-a8a8332961d1', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A910009841FD AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'05ebeb7b-50b3-4393-accc-cce1f028501a', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A90F00D2A332 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'e672950f-1c2f-436e-9920-d2fbbedc40d1', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91600B71193 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'91f9124e-50fd-472d-a39f-f67ac736d4e2', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A9100093EFDF AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'796085dc-551f-4121-aeb7-fa1a5d4f037c', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'localhost:61630', N'', CAST(0x0000A910009784C0 AS DateTime))
GO
INSERT [dbo].[ORG_UserLogon] ([ID], [UserID], [IP], [Location], [LogonTime]) VALUES (N'9726d30a-19a0-45c9-a1e8-fd083f948ea6', N'4a8f70a5-bd59-431c-bf71-addd1c5ee8bb', N'::1', N'', CAST(0x0000A91000A1E5FE AS DateTime))
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
ALTER TABLE [dbo].[ORG_Department] ADD  CONSTRAINT [DF_ORG_Department_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
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
