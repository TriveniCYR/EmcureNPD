USE [EmcureNPDDev]

alter table  PIDF_PBF_RnD_Master add PlantId int, LineId int

go
 -- exec stp_npd_GetPbfAllTabData @PIDFID=19,@BUId =1        
alter PROCEDURE [dbo].[stp_npd_GetPbfAllTabData]                    
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
select PBFGeneralId, BatchSizeId, APIRequirementMarketPrice, PlanSupportCostRsPerDay, ManHourRate,PlantId,LineId from PIDF_PBF_RnD_Master    
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