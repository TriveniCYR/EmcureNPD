USE [EmcureNPDDev]

go

Alter table PIDF_PBF_General
Add BEStudyResults nvarchar(50)
go
alter table PIDF add TradeNameRequired bit null,TradeNameDate datetime null

GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 31-07-2023 17:35:03 ******/
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
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 31-07-2023 17:35:03 ******/
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
/****** Object:  Table [dbo].[PIDF_PBF_Outsource]    Script Date: 31-07-2023 17:35:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Outsource](
	[PIDFPBFOutsourceId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[ProjectWorkflowId] [int] NOT NULL,
	[PBFWorkflowId] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF_Outsource] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFOutsourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource_Task]    Script Date: 31-07-2023 17:35:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Outsource_Task](
	[PIDFPBFOutsourceTaskId] [int] IDENTITY(1,1) NOT NULL,
	[PIDFPBFOutsourceId] [int] NOT NULL,
	[PBFWorkFlowTaskName] [nvarchar](100) NULL,
	[PBfWorkFlowId] [int] NOT NULL,
	[TaskLevel] [int] NULL,
	[ParentId] [int] NULL,
	[Cost] [float] NULL,
	[Tentative] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_PIDF_PBF_Outsource_Task] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFOutsourceTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource] CHECK CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID]
GO
