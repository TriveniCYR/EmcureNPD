USE [EmcureNPDDev]
GO

ALTER TABLE [dbo].[Master_WorkFlowTasks] DROP CONSTRAINT [FK__Master_Wo__Workf__4066405D]
GO

/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 8/18/2023 10:10:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_WorkFlowTasks]') AND type in (N'U'))
DROP TABLE [dbo].[Master_WorkFlowTasks]
GO

/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 8/18/2023 10:10:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Master_WorkFlowTasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[TaskName] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[WorkflowId] [int] NULL,
	[Country] [bit] NULL,
	[TaskLevel] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Master_WorkFlowTasks]  WITH CHECK ADD FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Master_Workflow] ([WorkflowId])
GO


