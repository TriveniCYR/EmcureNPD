USE [EmcureNPDDev]

alter table PIDF_IPD_PatentDetails add PatentType smallint NULL
update PIDF_IPD_PatentDetails set PatentType = 1
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPbfAllTabData]    Script Date: 14-06-2023 19:34:31 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetPbfAllTabData]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetIPDDetailByPIDF]    Script Date: 14-06-2023 19:34:31 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetIPDDetailByPIDF]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Reference_Product_detail]    Script Date: 14-06-2023 19:34:31 ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_Reference_Product_detail]
GO
/****** Object:  Table [dbo].[PIDF_PBF_Reference_Product_detail]    Script Date: 14-06-2023 19:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_Reference_Product_detail](
	[PIDFPBFReferenceProductdetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[BusinessUnitId] [bigint] NOT NULL,
	[RFDBrand] [nvarchar](100) NULL,
	[RFDApplicant] [nvarchar](100) NULL,
	[RFDCountryId] [int] NULL,
	[RFDIndication] [nvarchar](100) NULL,
	[RFDInnovators] [nvarchar](100) NULL,
	[RFDInitialRevenuePotential] [nvarchar](100) NULL,
	[RFDPriceDiscounting] [nvarchar](100) NULL,
	[RFDCommercialBatchSize] [nvarchar](100) NULL,
 CONSTRAINT [PK_PIDFPBFReferenceProductdetail] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFReferenceProductdetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetIPDDetailByPIDF]    Script Date: 14-06-2023 19:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec [stp_npd_GetIPDDetailByPIDF] 129  
CREATE PROCEDURE [dbo].[stp_npd_GetIPDDetailByPIDF]     
 @PIDFId int     
AS    
BEGIN    
 select B.IPDID, A.MoleculeName As ProjectName, B.MarketName, B.DataExclusivity, B.FillingType, B.ApprovedGenetics, B.LaunchedGenetics    
 , B.LegalStatus, B.CostOfLitication, B.Comments,B.Innovators, B.PatentStatus    
 , STUFF((SELECT ', ' + y.CountryName FROM PIDF_IPD_Country x     
 Inner Join Master_Country y on x.CountryId = y.CountryID    
 WHERE x.IPDID = B.IPDID FOR XML PATH('')), 1, 2, '') As Country,    
 C.BusinessUnitName    
 from PIDF As A    
 Inner JOin PIDF_IPD As B On A.PIDFID = B.PIDFID    
 Inner Join Master_BusinessUnit As C On C.BusinessUnitId = B.BusinessUnitId    
 Where A.PIDFId = @PIDFId    
    
 select B.IPDID, C.BusinessUnitName, A.PatentNumber, A.Type, A.OriginalExpiryDate, A.ExtensionExpiryDate,    
 A.Comments, A.Strategy,A.PatentType    
 from PIDF_IPD_PatentDetails As A    
 Inner JOin PIDF_IPD As B On A.IPDID = B.IPDID    
 Inner Join Master_BusinessUnit As C On C.BusinessUnitId = B.BusinessUnitId    
 Where B.PIDFId = @PIDFId    
    
 Select MoleculeName As ProjectName from PIDF    
 Where PIDFID = @PIDFId    
END    
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPbfAllTabData]    Script Date: 14-06-2023 19:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
 -- exec stp_npd_GetPbfAllTabData @PIDFID=19,@BUId =1      
CREATE PROCEDURE [dbo].[stp_npd_GetPbfAllTabData]                  
(      
@PIDFID bigint = 0,      
@BUId bigint = 0      
)                
AS                  
BEGIN            
      
     
 -------find PIDFPBF Id by PIDF Id      
       
Declare @PIDFPBFId int = (Select top 1 PIdfPBFId from PIDF_PBF Where PIDFId = @PIDFId)  
Declare @PBFGeneralId int = (Select top 1 PBFGeneralId from PIDF_PBF_General Where PIDFPBFId = @PIDFPBFId and BusinessUnitId=@BUId)  
  
Select TestTypeId, TestTypeName, TestTypeCode, TestTypePrice from Master_TestType  
Where IsActive = 1  
  
Select PackingTypeId, PackingTypeName, PackingCost, Unit from Master_PackingType  
Where IsActive = 1  
  
Select BatchSizeNumberId, BatchSizeNumberName from Master_BatchSizeNumber  
Where IsActive = 1  
  
Select BusinessUnitId, BusinessUnitName from Master_BusinessUnit  
Where IsActive = 1  
  
select DISTINCT PBFGeneralId, StrengthId, ProjectCode, ImprintingEmbossingCode  
from PIDF_PBF_General_Strength where PBFGeneralId = @PBFGeneralId  
  
-- Clinical tables  
select PBFGeneralId, StrengthId, BioStudyTypeId, FastingOrFed, NumberofVolunteers, ClinicalCostAndVolume,   
BioAnalyticalCostAndVolume, DocCostandStudy from PIDF_PBF_Clinical  
Where PBFGeneralId = @PBFGeneralId  
  
-- analyticals tables  
select A.PBFGeneralId, A.StrengthId,A.TestTypeId, A.ActivityTypeId, A.Numberoftests, A.PrototypeDevelopment, A.CostPerTest,   
A.PrototypeCost  
from PIDF_PBF_Analytical as A   
Where A.PBFGeneralId = @PBFGeneralId  
  
--analytical cost and strength mapping  
Select A.TotalAMVCost,A.TotalAMVTitle,A.Remark,B.StrengthId,B.IsChecked  
from PIDF_PBF_Analytical_AMVCost As A  
left join PIDF_PBF_Analytical_AMVCost_StrengthMapping B on A.TotalAMVCostId = B.TotalAMVCostId  
Where PBFGeneralId = @PBFGeneralId  
  
  
-- rnd tables  
select PBFGeneralId, BatchSizeId, APIRequirementMarketPrice, PlanSupportCostRsPerDay, ManHourRate from PIDF_PBF_RnD_Master  
Where PBFGeneralId = @PBFGeneralId  
  
select PIDFPBFRNDExicipientId,PBFGeneralId, StrengthId, ActivityTypeId, ExicipientPrototype, ExicipientDevelopment, RsPerKg,   
MgPerUnitDosage from PIDF_PBF_RnD_ExicipientRequirement  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, ActivityTypeId, PackingTypeId, UnitOfMeasurement,PackagingDevelopment, RsPerUnit,   
Quantity from PIDF_PBF_RnD_PackagingMaterial  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, PrototypeFormulation, ScaleUpbatch,ExhibitBatch1, ExhibitBatch2,   
ExhibitBatch3 from PIDF_PBF_Rnd_BatchSize  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, Prototype, ScaleUp,ExhibitBatch1, ExhibitBatch2,   
ExhibitBatch3,PrototypeCost,ScaleUpCost,ExhibitBatchCost,TotalCost from PIDF_PBF_RnD_APIRequirement  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, ActivityTypeId, Prototype, StrengthUnitQuantity,Cost  
 from PIDF_PBF_RnD_ToolingChangepart  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, ActivityTypeId, MiscellaneousDevelopment,StrengthMiscellaneousExpense from PIDF_PBF_RnD_CapexMiscellaneousExpenses  
Where PBFGeneralId = @PBFGeneralId  
  
  
select PBFGeneralId, StrengthId, ScaleUp, Exhibit  
from PIDF_PBF_RnD_PlantSupportCost  
Where PBFGeneralId = @PBFGeneralId  
  
  
select PBFGeneralId, StrengthId, UnitCostOfReferenceProduct, FormulationDevelopment,PilotBE,PharmasuiticalEquivalence  
,PivotalBio,TotalCost from PIDF_PBF_RnD_ReferenceProductDetail  
Where PBFGeneralId = @PBFGeneralId  
  
select PBFGeneralId, StrengthId, BusinessUnitId, IsChecked,TotalCost from PIDF_PBF_RnD_FillingExpenses  
Where PBFGeneralId = @PBFGeneralId  
  
Select A.ProjectActivitiesId, A.ProjectActivitiesName,  
B.DurationInDays , B.ManPowerInDays   
from Master_ProjectActivities A  
Left Join PIDF_PBF_RnD_ManPowerCost B  
ON A.ProjectActivitiesId = B.ProjectActivitiesId  
And B.PBFGeneralId = @PBFGeneralId  
  
Select CostOfLitication, BusinessUnitId,([dbo].[GetCurrencySymbol](@PIDFId)) As CurrencySymbol from PIDF_IPD  
Where PIDFId = @PIDFId  
  
Select A.ProjectActivitiesId, A.ProjectActivitiesName,  
isnull(B.Prototype,'') [Prototype] , isnull(B.ScaleUp,'') [ScaleUp], isnull(B.Exhibit,'') [Exhibit]  
from Master_HeadWiseBudgetActivities A  
Left Join PIDF_PBF_HeadWiseBudget B  
ON A.ProjectActivitiesId = B.ProjectActivitiesId  
And B.PBFGeneralId = @PBFGeneralId  


select * from PIDF_PBF_Reference_Product_detail where BusinessUnitId = @BUId and  PIDFID = @PIDFID
  
END   
  
GO
