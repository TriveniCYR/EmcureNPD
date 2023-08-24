USE [EmcureNPDDev]
GO
/****** Object:  Table [dbo].[Master_WorkFlowTasks]    Script Date: 8/24/2023 10:41:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP Table [dbo].[Master_WorkFlowTasks]
CREATE TABLE [dbo].[Master_WorkFlowTasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[TaskName] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[WorkflowId] [int] NULL,
	[Country] [bit] NULL,
	[TaskLevel] [int] NULL,
	[StartDateOffset] [int] NULL,
	[EndDateOffset] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Master_WorkFlowTasks] ON 

INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (1, 0, N'Development Phase', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1, 0, 490)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (2, 0, N'Regulatory Phase', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1, 520, 807)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (3, 0, N'Phase 4 Pre Launch Preparation per country', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1, 567, 821)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (4, 0, N'Phase 5 Launch Activities per country', CAST(N'2023-08-14T16:38:01.207' AS DateTime), 1, NULL, 0, 1, 731, 927)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (5, 1, N'Purchase API', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 0, 0)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (6, 1, N'Select API suppliers', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 90, 91)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (7, 1, N'Formulation Finalised', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 181, 182)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (8, 1, N'Manufacture Pivotal batches', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 212, 219)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (9, 1, N'Test and Place on Stability', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 220, 250)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (10, 1, N'Carry out Biostudy', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 340, 400)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (11, 1, N'3 month stability Report', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 370, 370)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (12, 1, N'6 month stability Report', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 460, 460)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (13, 1, N'Give last data to RA', CAST(N'2023-08-14T16:41:12.193' AS DateTime), 1, 1, 0, 2, 490, 490)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (14, 2, N'Dossier Ready Date', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 520, 520)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (15, 2, N'Submit DCP in interested EU countries - DCP1', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 527, 527)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (16, 2, N'RA Submission Dues Paid into ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 527, 528)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (17, 2, N'Submit DCP in interested EU countries - DCP2', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 528, 529)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (18, 2, N'Submit DCP2 into ', CAST(N'2023-08-17T12:02:17.687' AS DateTime), 1, 1, 1, 2, 542, 543)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (19, 2, N'Submit into Nationals', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 548, 548)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (20, 2, N'Submit Nationals into ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 1, 2, 548, 548)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (21, 2, N'DCP Validation', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 555, 590)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (22, 2, N'Validation DCP1 into ', CAST(N'2023-08-17T12:02:40.933' AS DateTime), 1, 1, 1, 2, 555, 569)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (23, 2, N'Validation DCP2 into ', CAST(N'2023-08-17T12:02:58.297' AS DateTime), 1, 1, 1, 2, 576, 590)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (24, 2, N'DCP Procedure', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 591, 800)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (25, 2, N'DCP Starts', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 591, 591)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (26, 2, N'DCP Day 105', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 695, 695)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (27, 2, N'DCP Day 106', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 696, 696)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (28, 2, N'DCP End of Procedure (EOP)', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 800, 800)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (29, 2, N'Validation ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 1, 2, 548, 562)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (30, 2, N'Submit Nationals in other interested countries', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 610, 770)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (31, 2, N'Pharmacovigilance requirements', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 610, 770)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (32, 2, N'National Phase in ', CAST(N'2023-08-17T12:01:32.800' AS DateTime), 1, 1, 1, 2, 800, 807)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (33, 2, N'DCP Approval in ', CAST(N'2023-08-17T12:01:10.753' AS DateTime), 1, 1, 1, 2, 807, 807)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (34, 2, N'DCP Approval in IT', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 0, 2, 807, 807)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (35, 2, N'Approval in ', CAST(N'2023-08-14T16:51:25.010' AS DateTime), 1, 1, 1, 2, 569, 569)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (36, 3, N'Purchase order raised ', CAST(N'2023-08-17T12:01:55.770' AS DateTime), 1, 1, 1, 2, 567, 567)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (37, 3, N'API ordered ', CAST(N'2023-08-17T12:00:20.077' AS DateTime), 1, 1, 1, 2, 567, 567)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (38, 3, N'Cutter Guides sent to Artwork', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2, 770, 770)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (39, 3, N'Draft Artworks completed', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2, 795, 795)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (40, 3, N'Artworks submitted and approved', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2, 801, 801)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (41, 3, N'Plan Batch manufacture', CAST(N'2023-08-14T16:55:53.677' AS DateTime), 1, 1, 0, 2, 807, 821)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (42, 4, N'Final Artworks sent to plant', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 731, 731)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (43, 4, N'Packaging for launch stock ordered', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 761, 761)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (44, 4, N'Manufacture Launch Bulk Stock ', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 807, 821)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (45, 4, N'Approve Bulk product ', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 807, 849)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (46, 4, N'Packaging for launch stock received', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 821, 821)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (47, 4, N'Launch Bulk packed', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 807, 828)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (48, 4, N'Approve Finished product', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 828, 842)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (49, 4, N'Send product to Europe', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 842, 870)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (50, 4, N'Release product in Europe', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 870, 884)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (51, 4, N'Send product to Country', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 884, 887)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (52, 4, N'Obtain Price in Country', CAST(N'2023-08-14T17:01:02.190' AS DateTime), 1, 1, 0, 2, 807, 807)
INSERT [dbo].[Master_WorkFlowTasks] ([TaskId], [ParentId], [TaskName], [CreatedDate], [isActive], [WorkflowId], [Country], [TaskLevel], [StartDateOffset], [EndDateOffset]) VALUES (53, 4, N'Launch in Country ', CAST(N'2023-08-17T11:59:54.410' AS DateTime), 1, 1, 1, 2, 927, 927)
SET IDENTITY_INSERT [dbo].[Master_WorkFlowTasks] OFF
GO
ALTER TABLE [dbo].[Master_WorkFlowTasks]  WITH CHECK ADD FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Master_Workflow] ([WorkflowId])
GO


