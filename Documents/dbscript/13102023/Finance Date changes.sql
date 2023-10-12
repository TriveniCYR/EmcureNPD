
alter table PIDF_PBF add BatchManifacturingDate datetime 
alter table PIDF_PBF add FillingDateDate datetime 

Go
 -- exec stp_npd_GetPbfData @PIDFID=214,@BUSINESSUNITId =4     
CREATE or alter PROCEDURE [dbo].[stp_npd_GetPbfData]                  
(      
@PIDFID bigint = 0,      
@BUSINESSUNITId bigint = 0      
)                
AS                  
BEGIN            
      
 declare @PIDFPBFId as bigint       
 declare @PBFGeneralId as bigint   
 declare @OralName as nvarchar(30)  
 --declare @StrengthId as bigint   
   
  -------find PIDFId by PIDF Id    
  select @OralName = MO.OralName from PIDF   
 inner join Master_Oral as MO on MO.OralId = pidf.OralId  
 where pidf.PIDFID = @PIDFID  
 --PBF Object  
  
 -------find PIDFPBF Id by PIDF Id      
 select @PIDFPBFId=PIDFPBFId from PIDF_PBF where PIDFID= @PIDFID       
 select @PBFGeneralId = PBFGeneralId from PIDF_PBF_General where PIDFPBFId= @PIDFPBFId and BusinessUnitId=@BUSINESSUNITId      
     
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

 ,PBF.[BatchManifacturingDate]
 ,PBF.[FillingDateDate]

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
 --,PBG_G.[CreatedDate]       
 ,STUFF((SELECT ',' + cast(BusinessUnitId as nvarchar)  
           FROM PIDF_PBF_MarketMapping b     
           WHERE PIDFPBFId=PBF.PIDFPBFId    
          FOR XML PATH('')), 1, 1, '') as MarketIds  
 from PIDF_PBF as PBF   
 inner join PIDF on PBF.PIDFId=PIDF.PIDFID  
 Left join PIDF_PBF_General as PBG_G on PBG_G.PIDFPBFID = PBF.PIDFPBFId and  PBG_G.PBFGeneralId = @PBFGeneralId      
 where PBF.PIDFId = @PIDFID     
  
      
 --------------------Table[1]--PBF General Strength Object---------------      
 select * from PIDF_PBF_General_Strength where PBFGeneralId=@PBFGeneralId    
   
  --------------------Table[2]--PIDF Object---------------      
 select @OralName [OralName]    
  
END   

--------------------------------
GO



CREATE or alter Proc [dbo].[ProcGetPidfFinance]  
@PIDFId int=0  
AS   
BEGIN  
SELECT   
       [PIDFFinaceId] as PidffinaceId  
      ,pf.[PIDFId] as Pidfid  
      ,[Entity] as Entity  
      ,[Product] as Product  
      ,[ForecastDate] as ForecastDate  
      ,[Currencyid] as Currencyid  
      ,[DosageFrom] as DosageFrom  
      ,[ManufacturingSiteOrPartner] as ManufacturingSiteOrPartner  
      ,[SKUs] as Skus  
      ,[MSPersentage] as Mspersentage  
      ,[TargetPriceScenario] as TargetPriceScenario  
      ,[ProjectStartDate] as ProjectStartDate  
      ,[BatchManufacturing] as BatchManufacturing  
      ,[ExpectedFilling] as ExpectedFilling  
      ,[ApprovalPeriodinDays] as ApprovalPeriodinDays  
      ,[ApprovalDate] as ApprovalDate  
      ,[ProductLaunchDate] as ProductLaunchDate  
      ,isnull([GestationPeriodinYears],0) as GestationPeriodinYears  
      ,[MarketShareErosionrate] as MarketShareErosionrate  
      ,[PriceErosion] as PriceErosion  
   ,EscalationinCOGS  
      ,[DiscountRate] as DiscountRate  
      ,[Incometaxrate] as Incometaxrate  
      ,[Opexasapercenttosale] as Opexasapercenttosale  
      ,[ExternalProfitSharepercent] as ExternalProfitSharepercent  
      ,[CollectioninDays] as CollectioninDays  
      ,[InventoryinDays] as InventoryinDays  
      ,[CreditorinDays] as CreditorinDays  
      ,[MarketingAllowance] as MarketingAllowance  
      ,[RegulatoryMaintenanceCost] as RegulatoryMaintenanceCost  
      ,[GrosstoNet] as GrosstoNet  
      ,[Noofbatchestobemanufactured] as Noofbatchestobemanufactured  
      ,[NoofbatchestobemanufacturedPhaseEndDate] as NoofbatchestobemanufacturedPhaseEndDate  
      ,[NoSKUs] as NoSkus  
      ,[NoSKUsPhaseEndDate] as NoSkusPhaseEndDate  
      ,[RandDAnalyticalcost] as RandDanalyticalcost  
      ,[RandDAnalyticalcostPhaseEndDate]  as RandDanalyticalcostPhaseEndDate  
      ,[RLDsamplecost] as Rldsamplecost  
      ,[RLDsamplecostPhaseEndDate] as RldsamplecostPhaseEndDate  
      ,[BatchmanufacturingcostOrAPIActualsEst] as BatchmanufacturingcostOrApiactualsEst  
      ,[BatchmanufacturingcostOrAPIActualsEstPhaseEndDate] as BatchmanufacturingcostOrApiactualsEstPhaseEndDate  
      ,[Sixmonthsstabilitycost] as Sixmonthsstabilitycost  
      ,[SixmonthsstabilitycostPhaseEndDate] as SixmonthsstabilitycostPhaseEndDate  
      ,[TechTransfer] as TechTransfer  
      ,[TechTransferPhaseEndDate] as TechTransferPhaseEndDate  
      ,[BEstudies] as Bestudies  
      ,[BEstudiesPhaseEndDate] as BestudiesPhaseEndDate  
      ,[Filingfees] as Filingfees  
      ,[FilingfeesPhaseEndDate] as FilingfeesPhaseEndDate  
      ,[BioStuddyCost] as BioStuddyCost  
      ,[BioStuddyCostPhaseEndDate] as BioStuddyCostPhaseEndDate  
      ,[Capex] as Capex  
      ,[CapexPhaseEndDate] as CapexPhaseEndDate  
      ,[ToolingAndChangeParts] as ToolingAndChangeParts  
      ,[ToolingAndChangePartsPhaseEndDate] as ToolingAndChangePartsPhaseEndDate  
      ,[Total] as Total  
      ,pf.[CreatedDate] as CreatedDate  
      ,pf.[CreatedBy] as CreatedBy  
   ,isnull(p.StatusId,0) as PIDFStatusId 
   , (select ProjectInitiationDate from PIDF_PBF where PIDFID = @PIDFId) as ProjectInitiationDate
   , (select BatchManifacturingDate from PIDF_PBF where PIDFID = @PIDFId) as BatchManifacturingDate
   ,(select FillingDateDate from PIDF_PBF where PIDFID = @PIDFId) as FillingDateDate

  FROM PIDF_Finance as pf  
  inner join PIDF as p on p.PIDFID=pf.PIDFId  
  inner join Master_PIDFStatus as mps on p.StatusId=mps.PIDFStatusID  
  WHERE pf.PIDFId=@PIDFId or @PIDFId=0  
  END  

  GO