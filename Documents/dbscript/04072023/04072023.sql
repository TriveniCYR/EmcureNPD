

alter table PIDF_PBF_RnD_Master add APIRequirementVendorName nvarchar(100)
alter table PIDF_PBF_Rnd_BatchSize Add Salt float
Go
-- exec [GetPIDFByYearAndBusinessUnit] 0, '04-01-1970', '03-31-2024',87
alter procedure [dbo].[GetPIDFByYearAndBusinessUnit] (      
@BusinessUnitId int,      
@FinancialYearStart varchar(20),      
@FinancialYearEnd varchar(20),     
@UserId int    
)      
as      
begin      
    
	Select Row_Number() Over (Partition by PIDFId Order By CreatedDate Desc) As Row_Number1,
PIDFId, StatusId,CreatedDate,ManagementApprovalStatusHistoryId into #Temptable1 from PIDF_ManagementApprovalStatusHistory 


Set @BusinessUnitId = IsNull(@BusinessUnitId, 0)    
 (   
Select PIDFStatusID, PIDFStatus, SUM((Case When B.PIDFStatusHistoryId is not null And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)   
And (D.UserId = @UserId) then 1 else 0 end)) As StatusCount  
  
, A.StatusTextColor,  A.StatusColor from Master_PIDFStatus As A    
Left Join PIDFStatusHistory As B On A.PIDFStatusID = B.StatusId And Cast(B.CreatedDate as Date) Between CAST(@FinancialYearStart AS Date ) AND CAST(@FinancialYearEnd AS Date)      
Left Join [PIDF] As C On B.PIDFID = C.PIDFID And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)    
Left Join [Master_UserBusinessUnitMapping] As D On D.BusinessUnitId = C.BusinessUnitId And (D.UserId = @UserId)    
WHERE A.IsDashboard = 1 And PIDFStatusID Not In (20,21)  
--And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)    
Group BY PIDFStatusID, PIDFStatus, A.StatusTextColor,  A.StatusColor    
 )     
union all ----------------------------------------------------------------------------  
(  
Select PIDFStatusID, PIDFStatus, SUM((Case When B.ManagementApprovalStatusHistoryId is not null And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)   
And (D.UserId = @UserId) then 1 else 0 end)) As StatusCount  
  
, A.StatusTextColor,  A.StatusColor from Master_PIDFStatus As A    
Left Join #Temptable1 As B On A.PIDFStatusID = B.StatusId  And Row_Number1 =1 And Cast(B.CreatedDate as Date) Between CAST(@FinancialYearStart AS Date ) AND CAST(@FinancialYearEnd AS Date)      
Left Join [PIDF] As C On B.PIDFID = C.PIDFID And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)    
Left Join [Master_UserBusinessUnitMapping] As D On D.BusinessUnitId = C.BusinessUnitId And (D.UserId = @UserId)    
WHERE A.IsDashboard = 1 And PIDFStatusID In (20,21)  
--And (C.BusinessUnitId = @BusinessUnitId Or @BusinessUnitId = 0)    
Group BY PIDFStatusID, PIDFStatus, A.StatusTextColor,  A.StatusColor    
)  
----------------------------------------------------------------------------  
Order By PIDFStatusID   
drop table #Temptable1
end 

GO
          
 -- exec stp_npd_GetPbfAllTabData @PIDFID=157,@BUId =1              
Alter PROCEDURE [dbo].[stp_npd_GetPbfAllTabData]                          
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
select PBFGeneralId, BatchSizeId, APIRequirementMarketPrice, PlanSupportCostRsPerDay, ManHourRate,PlantId,LineId,APIRequirementVendorName from PIDF_PBF_RnD_Master          
Where PBFGeneralId = @PBFGeneralId          
          
select PIDFPBFRNDExicipientId,PBFGeneralId, StrengthId, ActivityTypeId, ExicipientPrototype, ExicipientDevelopment, RsPerKg,           
MgPerUnitDosage from PIDF_PBF_RnD_ExicipientRequirement          
Where PBFGeneralId = @PBFGeneralId          
          
select PBFGeneralId, StrengthId, ActivityTypeId, PackingTypeId, UnitOfMeasurement,PackagingDevelopment, RsPerUnit,           
Quantity from PIDF_PBF_RnD_PackagingMaterial          
Where PBFGeneralId = @PBFGeneralId          
          
select PBFGeneralId, StrengthId, PrototypeFormulation, ScaleUpbatch,ExhibitBatch1, ExhibitBatch2,           
ExhibitBatch3,salt from PIDF_PBF_Rnd_BatchSize         
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
    
select ExcipientRequirementId,ExcipientRequirementName,ExcipientRequirementCost from Master_ExcipientRequirement    
where IsActive =1    
          
END 
