USE [EmcureNPDDev]
GO
/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 8/18/2023 6:55:56 PM ******/
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
SET IDENTITY_INSERT [dbo].[Master_WorkFlowTasks] ON 

INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (1, 0, N'Development Phase', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (2, 0, N'Regulatory Phase', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (3, 0, N'Phase 4 Pre Launch Preparation per country', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (4, 0, N'Phase 5 Launch Activities per country', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (5, 1, N'Purchase API', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (6, 1, N'Select API suppliers', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (7, 1, N'Formulation Finalised', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (8, 1, N'Manufacture Pivotal batches', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (9, 1, N'Test and Place on Stability', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (10, 1, N'Carry out Biostudy', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (11, 1, N'3 month stability Report', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (12, 1, N'6 month stability Report', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (13, 1, N'Give last data to RA', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (14, 2, N'Dossier Ready Date', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (15, 2, N'Submit DCP in interested EU countries - DCP1', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (16, 2, N'RA Submission Dues Paid into Germany ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (17, 2, N'RA Submission Dues Paid into France ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (18, 2, N'RA Submission Dues Paid into Italy ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (19, 2, N'Submit DCP in interested EU countries - DCP2', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (23, 2, N'Submit into Nationals', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (24, 2, N'Submit Nationals into UK', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (25, 2, N'Submit Nationals into Israel', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (26, 2, N'DCP Validation', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (33, 2, N'DCP Procedure', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (34, 2, N'DCP Starts', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (35, 2, N'DCP Day 105', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (36, 2, N'DCP Day 106', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (37, 2, N'DCP End of Procedure (EOP)', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (38, 2, N'UK Validation', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (39, 2, N'Israel Validation', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (40, 2, N'Submit Nationals in other interested countries', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (41, 2, N'Pharmacovigilance requirements', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (47, 2, N'DCP Approval in IT', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (48, 2, N'Approval in UK', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (49, 2, N'Approval in Israel', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (58, 3, N'Cutter Guides sent to Artwork', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (59, 3, N'Draft Artworks completed', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (60, 3, N'Artworks submitted and approved', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (61, 3, N'Plan Batch manufacture', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (62, 4, N'Final Artworks sent to plant', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (63, 4, N'Packaging for launch stock ordered', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (64, 4, N'Manufacture Launch Bulk Stock ', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (65, 4, N'Approve Bulk product ', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (66, 4, N'Packaging for launch stock received', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (67, 4, N'Launch Bulk packed', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (68, 4, N'Approve Finished product', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 2, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (69, 4, N'Send product to Europe', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (70, 4, N'Release product in Europe', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (71, 4, N'Send product to Country', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (72, 4, N'Obtain Price in Country', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (78, 4, N'Launch in Country ', CAST(N'2023-08-17T11:59:54.410' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (79, 3, N'API ordered ', CAST(N'2023-08-17T12:00:20.077' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (80, 2, N'DCP Approval in ', CAST(N'2023-08-17T12:01:10.753' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (81, 2, N'National Phase in ', CAST(N'2023-08-17T12:01:32.800' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (82, 3, N'Purchase order raised ', CAST(N'2023-08-17T12:01:55.770' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (83, 1, N'Submit DCP2 into ', CAST(N'2023-08-17T12:02:17.687' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (84, 1, N'Validation DCP1 into ', CAST(N'2023-08-17T12:02:40.933' AS DateTime), 1, 1, 1, 2)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel]) VALUES (85, 1, N'Validation DCP2 into ', CAST(N'2023-08-17T12:02:58.297' AS DateTime), 1, 1, 1, 2)
SET IDENTITY_INSERT [dbo].[Master_WorkFlowTasks] OFF
GO
ALTER TABLE [dbo].[Master_WorkFlowTasks]  WITH CHECK ADD FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Master_Workflow] ([WorkflowId])
GO
