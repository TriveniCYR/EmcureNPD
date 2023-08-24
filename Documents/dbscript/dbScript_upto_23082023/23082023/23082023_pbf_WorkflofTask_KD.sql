USE [EmcureNPDDev]
GO
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 22-08-2023 16:36:19 ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkflow_Task]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 22-08-2023 16:36:19 ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkFlow]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 22-08-2023 16:36:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PBFWorkFlow](
	[PBFWorkFlowId] [int] IDENTITY(1,1) NOT NULL,
	[PBFWorkFlowName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PBFWorkFlow] PRIMARY KEY CLUSTERED 
(
	[PBFWorkFlowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 22-08-2023 16:36:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_PBFWorkflow_Task](
	[PBfWorkFlowTaskId] [int] IDENTITY(1,1) NOT NULL,
	[PBFWorkFlowTaskName] [nvarchar](100) NULL,
	[PBfWorkFlowId] [int] NOT NULL,
	[TaskLevel] [int] NULL,
	[ParentId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PBFWorkflow_Task] PRIMARY KEY CLUSTERED 
(
	[PBfWorkFlowTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkFlow] ON 
GO
INSERT [dbo].[Master_PBFWorkFlow] ([PBFWorkFlowId], [PBFWorkFlowName], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (1, N'workflowname1', 1, 1, CAST(N'2023-07-30T17:40:05.167' AS DateTime), 1, CAST(N'2023-07-30T17:40:05.167' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkFlow] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkflow_Task] ON 
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (3, N'Purchase API', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:29:05.190' AS DateTime), 1, CAST(N'2023-08-10T10:29:05.190' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (4, N'Select API Suppliers', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (5, N'Manufacture Pivotal batches', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (15, N'Test and Place on Stability', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (16, N'Carry out Biostudy', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (17, N'3 month stability Report', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (18, N'6 month stability Report', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (19, N'Give last data to RA', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (20, N'Regulatory', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (21, N'Dossier Ready Date', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (22, N'Submission Dues and timelines', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (23, N'DCP Procedure', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (24, N'Pharmacovigilance requirements', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (25, N'Pre Launch Preparation per country', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (26, N'Launch Activities per country', 1, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkflow_Task] OFF
GO
