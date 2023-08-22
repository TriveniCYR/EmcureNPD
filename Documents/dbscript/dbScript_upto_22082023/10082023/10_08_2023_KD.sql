USE [EmcureNPDDev]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFOutsourceWorkflowTask]    Script Date: 10-08-2023 11:59:03 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetPBFOutsourceWorkflowTask]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFOutsourceData]    Script Date: 10-08-2023 11:59:03 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetPBFOutsourceData]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetCommercialFormData]    Script Date: 10-08-2023 11:59:03 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetCommercialFormData]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_Outsource]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_Outsource] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_Outsource_PIDFID]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource_Task]    Script Date: 10-08-2023 11:59:03 ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Outsource_Task]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Outsource]    Script Date: 10-08-2023 11:59:03 ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Outsource]
GO
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 10-08-2023 11:59:03 ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkflow_Task]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 10-08-2023 11:59:03 ******/
DROP TABLE IF EXISTS [dbo].[Master_PBFWorkFlow]
GO
/****** Object:  Table [dbo].[Master_PBFWorkFlow]    Script Date: 10-08-2023 11:59:03 ******/
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
/****** Object:  Table [dbo].[Master_PBFWorkflow_Task]    Script Date: 10-08-2023 11:59:04 ******/
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
/****** Object:  Table [dbo].[PIDF_PBF_Outsource]    Script Date: 10-08-2023 11:59:04 ******/
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
/****** Object:  Table [dbo].[PIDF_PBF_Outsource_Task]    Script Date: 10-08-2023 11:59:04 ******/
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
SET IDENTITY_INSERT [dbo].[Master_PBFWorkFlow] ON 
GO
INSERT [dbo].[Master_PBFWorkFlow] ([PBFWorkFlowId], [PBFWorkFlowName], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (1, N'workflowname1', 1, 1, CAST(N'2023-07-30T17:40:05.167' AS DateTime), 1, CAST(N'2023-07-30T17:40:05.167' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkFlow] ([PBFWorkFlowId], [PBFWorkFlowName], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (2, N'workflowname2', 1, 1, CAST(N'2023-07-30T17:40:12.097' AS DateTime), 1, CAST(N'2023-07-30T17:40:12.097' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkFlow] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkflow_Task] ON 
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (1, N'TaskName2', 1, 2, 2, 1, 1, CAST(N'2023-07-30T17:43:51.023' AS DateTime), 1, CAST(N'2023-07-30T17:43:51.023' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (2, N'TaskName1', 1, 1, 0, 1, 1, CAST(N'2023-07-30T17:44:30.483' AS DateTime), 1, CAST(N'2023-07-30T17:44:30.483' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (3, N'Purchase API', 2, 1, 0, 1, 1, CAST(N'2023-08-10T10:29:05.190' AS DateTime), 1, CAST(N'2023-08-10T10:29:05.190' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (4, N'Select API Suppliers', 2, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
INSERT [dbo].[Master_PBFWorkflow_Task] ([PBfWorkFlowTaskId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [IsActive], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (5, N'Formulation Finalized', 2, 1, 0, 1, 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime), 1, CAST(N'2023-08-10T10:32:21.673' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_PBFWorkflow_Task] OFF
GO
SET IDENTITY_INSERT [dbo].[PIDF_PBF_Outsource] ON 
GO
INSERT [dbo].[PIDF_PBF_Outsource] ([PIDFPBFOutsourceId], [PIDFID], [ProjectWorkflowId], [PBFWorkflowId], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (1, 135, 5, 2, 1, CAST(N'2023-07-30T18:03:20.487' AS DateTime), 87, CAST(N'2023-08-10T11:14:18.147' AS DateTime))
GO
INSERT [dbo].[PIDF_PBF_Outsource] ([PIDFPBFOutsourceId], [PIDFID], [ProjectWorkflowId], [PBFWorkflowId], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (4, 155, 2, 1, 87, CAST(N'2023-08-09T15:40:53.130' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PIDF_PBF_Outsource] ([PIDFPBFOutsourceId], [PIDFID], [ProjectWorkflowId], [PBFWorkflowId], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (5, 128, 2, 1, 87, CAST(N'2023-08-09T17:27:41.540' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PIDF_PBF_Outsource] OFF
GO
SET IDENTITY_INSERT [dbo].[PIDF_PBF_Outsource_Task] ON 
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (11, 4, N'TaskName1', 0, NULL, NULL, 12, N'2023-09-04', 87, CAST(N'2023-08-09T15:40:53.130' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (12, 4, N'TaskName2', 0, NULL, NULL, 34, N'2023-09-05', 87, CAST(N'2023-08-09T15:40:53.130' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (25, 5, N'TaskName1', 0, NULL, NULL, 12, N'2023-09-05', 87, CAST(N'2023-08-09T17:27:41.540' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (26, 5, N'TaskName2', 0, NULL, NULL, 34, N'2023-09-06', 87, CAST(N'2023-08-09T17:27:41.540' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (40, 1, N'Purchase API', 0, NULL, NULL, 13, N'2023-08-12', NULL, CAST(N'2023-08-10T11:14:18.147' AS DateTime), 87, CAST(N'2023-08-10T11:14:18.423' AS DateTime))
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (41, 1, N'Select API Suppliers', 0, NULL, NULL, 45, N'2023-08-31', NULL, CAST(N'2023-08-10T11:14:18.147' AS DateTime), 87, CAST(N'2023-08-10T11:14:18.437' AS DateTime))
GO
INSERT [dbo].[PIDF_PBF_Outsource_Task] ([PIDFPBFOutsourceTaskId], [PIDFPBFOutsourceId], [PBFWorkFlowTaskName], [PBfWorkFlowId], [TaskLevel], [ParentId], [Cost], [Tentative], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (42, 1, N'Formulation Finalized', 0, NULL, NULL, 66, N'2023-08-16', NULL, CAST(N'2023-08-10T11:14:18.147' AS DateTime), 87, CAST(N'2023-08-10T11:14:18.457' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[PIDF_PBF_Outsource_Task] OFF
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_Outsource] CHECK CONSTRAINT [FK_PIDF_PBF_Outsource_PIDFID]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetCommercialFormData]    Script Date: 10-08-2023 11:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec stp_npd_GetCommercialFormData 135,1       
CREATE PROCEDURE [dbo].[stp_npd_GetCommercialFormData]                    
(          
@PIDFId int,        
@UserId int        
)                  
AS                    
BEGIN              
          
select PIDFCommercialId, PIDFId, BusinessUnitId, PIDFProductStrengthId, MarketSizeInUnit, ShelfLife        
, A.PackSizeId, B.PackSizeName, B.PackSize        
from PIDF_Commercial As A        
Inner Join Master_PackSize As B On A.PackSizeId = B.PackSizeId        
Where PIDFId = @PIDFId and A.IsDeleted = 0      
        
select [PIDFCommercialYearId], A.PIDFCommercialId, YearIndex, PackagingTypeId, CurrencyId,        
[CommercialBatchSize], [PriceDiscounting], [TotalApireq], [Apireq], [Suimsvolume], [FreeOfCost],         
[MarketGrowth], [MarketSize], [PriceErosion], [FinalSelectionId], [MarketSharePercentageLow],         
[MarketSharePercentageMedium], [MarketSharePercentageHigh], [MarketShareUnitLow], [MarketShareUnitMedium],         
[MarketShareUnitHigh], [NspunitsLow], [NspunitsMedium], [NspunitsHigh], [NspLow], [NspMedium],         
[NspHigh], [BrandPrice], [GenericPrice],[TargetCostOfGood]        
, B.BusinessUnitId, B.PIDFId, B.PIDFProductStrengthId, B.PackSizeId        
from PIDF_Commercial_years As A        
Inner Join PIDF_Commercial As B On A.PIDFCommercialId = B.PIDFCommercialId        
Where PIDFId = @PIDFId and B.IsDeleted = 0 order by PIDFCommercialId,YearIndex     
    
        
Select Strength, C.UnitofMeasurementName, A.PIDFProductStrengthId from PIDFProductStrength As A        
Inner Join PIDF As B On A.PIDFId = B.PIDFId        
Inner Join Master_UnitofMeasurement As C On C.UnitofMeasurementId = A.UnitofMeasurementId        
Where A.PIDFId = @PIDFId        
        
Select StatusId,InHouses from PIDF         
Where PIDFID = @PIDFId        
        
Select BusinessUnitId, BusinessUnitName from Master_BusinessUnit        
Where IsActive = 1        
        
exec SP_GetCurrencyByUser @UserId        
        
Select PackagingTypeId, PackagingTypeName from Master_PackagingType        
Where IsActive = 1        
        
Select PackSizeId, PackSizeName, PackSize from Master_PackSize        
Where IsActive = 1        
        
Select FinalSelectionId, FinalSelectionName from Master_FinalSelection        
Where IsActive = 1        
        
select Remark,Interested,BusinessUnitId,PIDFID from PIDF_Commercial_Master where PIDFID =  @PIDFId  
                   
-------------------Get PBFOutsource data-------------------------
select PPO.PIDFID,MPWorkFlow.PBFWorkFlowName,MPWorkFlow.PBFWorkflowId,PPO_Task.PBFWorkFlowTaskName,PPO_Task.Cost,PPO.ProjectWorkflowId,
PPO_Task.Tentative,MPO_Task.TaskLevel,MPO_Task.ParentId  
from PIDF_PBF_Outsource PPO   
left join PIDF_PBF_Outsource_Task PPO_Task on PPO.PIDFPBFOutsourceId = PPO_Task.PIDFPBFOutsourceId   
left join Master_PBFWorkflow_Task MPO_Task on MPO_Task.PBFWorkFlowTaskName = PPO_Task.PBFWorkFlowTaskName  
left join Master_PBFWorkFlow MPWorkFlow on MPWorkFlow.PBFWorkflowId = PPO.PBFWorkflowId  
  
where PPO.PIDFID = @PIDFId   -- and
 --PPO.PBFWorkflowId = @PBFWorkflowId 
order by MPO_Task.TaskLevel  
-----------------------------------------------------------------------


END     
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFOutsourceData]    Script Date: 10-08-2023 11:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
 -- exec stp_npd_GetPBFOutsourceData 1

CREATE proc [dbo].[stp_npd_GetPBFOutsourceData]   
 @PBFWorkFlowID int =0    
 as  
 begin  
  
 select	MPWorkFlow.PBFWorkflowId,MPWorkFlow.PBFWorkFlowName,MPO_Task.PBFWorkFlowTaskName,MPO_Task.TaskLevel,MPO_Task.ParentId  
from   
Master_PBFWorkflow_Task MPO_Task 
inner join Master_PBFWorkFlow MPWorkFlow on MPWorkFlow.PBFWorkflowId = MPO_Task.PBFWorkflowId  
where MPO_Task.PBFWorkflowId = @PBFWorkFlowID
order by MPO_Task.TaskLevel  
  
 end

GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFOutsourceWorkflowTask]    Script Date: 10-08-2023 11:59:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  --	exec [stp_npd_GetPBFOutsourceWorkflowTask] 135,1

CREATE proc [dbo].[stp_npd_GetPBFOutsourceWorkflowTask]   
 @PIDFID bigint =0,
 @PBFWorkflowId int =0  
 as  
 begin  
  
 select PPO.PIDFID,MPWorkFlow.PBFWorkFlowName,MPWorkFlow.PBFWorkflowId,PPO_Task.PBFWorkFlowTaskName,PPO_Task.Cost,PPO_Task.Tentative,MPO_Task.TaskLevel,MPO_Task.ParentId  
from PIDF_PBF_Outsource PPO   
right join PIDF_PBF_Outsource_Task PPO_Task on PPO.PIDFPBFOutsourceId = PPO_Task.PIDFPBFOutsourceId   
inner join Master_PBFWorkflow_Task MPO_Task on MPO_Task.PBFWorkFlowTaskName = PPO_Task.PBFWorkFlowTaskName  
left join Master_PBFWorkFlow MPWorkFlow on MPWorkFlow.PBFWorkflowId = PPO.PBFWorkflowId  
  
where PPO.PIDFID = @PIDFID   and
 PPO.PBFWorkflowId = @PBFWorkflowId 
order by MPO_Task.TaskLevel 
  
 end
GO
