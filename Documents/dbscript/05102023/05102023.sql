
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPbfData]    Script Date: 10/5/2023 4:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- exec stp_npd_GetPbfDataTest @PIDFID=420,@BUSINESSUNITId =2
CREATE OR ALTER PROCEDURE [dbo].[stp_npd_GetPbfData]                
(    
@PIDFID bigint = 0,    
@BUSINESSUNITId bigint = 0    
)              
AS                
BEGIN          
    
 declare @PIDFPBFId as bigint     
 declare @PBFGeneralId as bigint 
 declare @OralName as nvarchar(30)
 declare @TradeDressProposalId as bigint    
 --declare @StrengthId as bigint 
 
  -------find PIDFId by PIDF Id  
  select @OralName = MO.OralName from PIDF 
 inner join Master_Oral as MO on MO.OralId = pidf.OralId
 where pidf.PIDFID = @PIDFID
 --PBF Object

 -------find PIDFPBF Id by PIDF Id    
 select @PIDFPBFId=PIDFPBFId from PIDF_PBF where PIDFID= @PIDFID     
 select @PBFGeneralId = PBFGeneralId from PIDF_PBF_General where PIDFPBFId= @PIDFPBFId and BusinessUnitId=@BUSINESSUNITId    
 select top 1 @TradeDressProposalId=TradeDressProposalId from dbo.pbf_general_TDP TDP where PIDFID= @PIDFID       
 --select @PIDFPBFId,@PBFAnalyticalId,@PBFClinicalId    
    
  --------------------Table[0]--PBF Object---------------    
 select     
 PBF.[PIDFPBFId]     
 ,PBF.[PIDFId]     
 ,PBF.[ProjectName]     
 ,PBF.[BusinessRelationable]     
 ,PBF.[BERequirementId]     
 ,PBF.[NumberOfApprovedANDA]     
 ,PBF.[ProductTypeId]     
 ,PBF.[PlantId]     
 ,PBF.[WorkflowId]     
 ,PBF.[DosageId]    
 ,PBF.[PatentStatus]    
 ,PBF.[SponsorBusinessPartner]     
 ,PBF.[FillingTypeId]    
 ,PBF.[ScopeObjectives]     
 ,PBF.[FormRnDDivisionId]     
 ,PBF.[ProjectInitiationDate]     
 ,PBF.[RnDHead]     
 ,PBF.[ProjectManager]    
 ,PBF.[PackagingTypeId]      
 ,PBF.[ManufacturingId]  
 ,PBG_G.[PBFGeneralId]    
 ,PBG_G.[PIDFPBFID]     
 ,PBG_G.[BusinessUnitId]    
 ,PBG_G.[Capex]     
 ,PBG_G.[TotalExpense]     
 ,PBG_G.[ProjectComplexity]     
 ,PBG_G.[ProductTypeId] as GeneralProductTypeId    
 ,PBG_G.[TestLicenseAvailability]     
 ,PBG_G.[BudgetTimelineSubmissionDate]     
 ,PBG_G.[ProjectDevelopmentInitialDate]     
 ,PBG_G.[FormulationGLId]    
 --,PBG_G.[StrengthId]     
 ,PBG_G.[AnalyticalGLId]
 ,PIDF.StatusId
 ,TDP.Approch
 ,TDP.FormulaterResponsiblePerson
 --,PBG_G.[CreatedDate]     
 ,STUFF((SELECT ',' + cast(BusinessUnitId as nvarchar)
           FROM PIDF_PBF_MarketMapping b   
           WHERE PIDFPBFId=PBF.PIDFPBFId  
          FOR XML PATH('')), 1, 1, '') as MarketIds
 from PIDF_PBF as PBF 
 inner join PIDF on PBF.PIDFId=PIDF.PIDFID
  inner join dbo.pbf_general_TDP TDP on TDP.TradeDressProposalId=@TradeDressProposalId
 Left join PIDF_PBF_General as PBG_G on PBG_G.PIDFPBFID = PBF.PIDFPBFId and  PBG_G.PBFGeneralId = @PBFGeneralId    
 where PBF.PIDFId = @PIDFID   

    
 --------------------Table[1]--PBF General Strength Object---------------    
 select * from PIDF_PBF_General_Strength where PBFGeneralId=@PBFGeneralId  
 
  --------------------Table[2]--PIDF Object---------------    
 select @OralName [OralName]  

END 
