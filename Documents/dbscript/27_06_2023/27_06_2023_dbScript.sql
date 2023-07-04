     
alter proc [dbo].[ProcGetProjectNameAndStrength]      
(      
@PIDFID int=0,      
@BUID int=0      
)      
AS       
--ProcGetProjectNameAndStrength 2,10077-- 10072,      
BEGIN      
SET NOCOUNT ON;      
      
Declare @APIREQ nvarchar(20);      
Declare @ProjectName nvarchar(100);  
Declare @ProjectBudget bigint; 
------Project Budget (INR lacks)----------------------------------------------
  Select             
@ProjectBudget =sum(isnull(B.FilingCumTotal,0))    
from PIDF_PBF_PhaseWiseBudget B      
Left join PIDF_PBF_General As AA on AA.PBFGeneralId = B.PBFGeneralId      
Left Join PIDF_PBF As BB On AA.PIDFPBFId = BB.PIDFPBFId      
Left Join PIDF As CC On CC.PIDFId = BB.PIDFId And CC.BusinessUnitId = AA.BusinessUnitId      
Where CC.PIDFId = @PIDFID      
--------------------------------------------------------------
select top 1 @APIREQ = APIReq from PIDF_Commercial_Years as A      
inner join PIDF_Commercial B on A.PIDFCommercialId = B.PIDFCommercialId      
Inner join PIDF C on C.PIDFID=B.PIDFId and C.BusinessUnitId = B.BusinessUnitId      
where C.PIDFId =  @PIDFID      
      
      
select @ProjectName = MoleculeName from PIDF where (PIDFID=@PIDFID or @PIDFID=0)      
      
--select @ProjectName as projectName ,[dbo].[GetCurrencyCode](@PIDFID) as CurrencyCode,[dbo].[GetCurrencySymbol] (@PIDFID) as CurrencySymbol      
select @ProjectName as projectName ,'INR' as CurrencyCode, NCHAR(8377) as CurrencySymbol      
      
Select DISTINCT A.Strength + ' ' + F.UnitofMeasurementName As Strength, IsNull(E.ProjectCode, '') As ProjectCode,A.PIDFProductStrengthId from PIDFProductStrength As A      
Inner Join PIDF As B On A.PIDFId = B.PIDFId      
Inner Join Master_UnitofMeasurement As F On F.UnitofMeasurementId = A.UnitofMeasurementId      
Left Join PIDF_PBF As C On C.PIDFId = B.PIDFId And A.PIDFId = C.PIDFId      
Left Join PIDF_PBF_General As D On D.BusinessUnitId = B.BusinessUnitId And D.PIDFPBFId = C.PIDFPBFId      
Left Join PIDF_PBF_General_Strength As E On E.PBFGeneralId = D.PBFGeneralId And E.StrengthId = A.PIDFProductStrengthId      
Where B.PIDFId = @PIDFID      
order by A.PIDFProductStrengthId       
      
--select UserId,FullName as ManagerName,isnull(DesignationName,'NA') as DesignationName from Master_User where IsNUll(IsManagement, 0)=1      
--And IsActive = 1 And IsDeleted = 0      
select UserId,FullName as ManagerName,isnull(u.DesignationName,'') as DesignationName,isnull(pm.StatusId,0) as StatusId,isnull(pm.CreatedDate,getdate()) as CreatedDate from Master_User as u      
left join PIDF_ManagementApprovalStatusHistory pm on pm.CreatedBy=u.UserId and pm.PIDFId=@PIDFID      
where IsNUll(IsManagement, 0)=1       
And u.IsActive = 1 And IsDeleted = 0      
order by pm.CreatedDate desc      
--Head-Wise Budget (INR Lacs)--      
Select       
A.ProjectActivitiesId,       
A.ProjectActivitiesName as [BudgetsHeades],      
isnull(B.Prototype,0) [Prototype],       
isnull(B.ScaleUp,0) [ScaleUp],       
isnull(B.Exhibit,0) [Exhibit],      
isnull((B.Prototype+ B.ScaleUp + B.Exhibit),0) [Total]      
from Master_HeadWiseBudgetActivities A      
Left Join PIDF_PBF_HeadWiseBudget B ON A.ProjectActivitiesId = B.ProjectActivitiesId      
left join PIDF_PBF_General As AA on AA.PBFGeneralId = B.PBFGeneralId      
left Join PIDF_PBF As BB On AA.PIDFPBFId = BB.PIDFPBFId      
left Join PIDF As CC On CC.PIDFId = BB.PIDFId And CC.BusinessUnitId = AA.BusinessUnitId      
Where CC.PIDFId = @PIDFID      
order by B.ProjectActivitiesId      
      
select       
STUFF((SELECT ', ' + cast(MB.BusinessUnitName as nvarchar)      
           FROM PIDF_PBF_MarketMapping MM        
     inner join Master_BusinessUnit MB on MB.BusinessUnitId = MM.BusinessUnitId      
           WHERE PIDFPBFId=B.PIDFPBFId        
          FOR XML PATH('')), 1, 1, '') as Market,      
IsNull(B.SponsorBusinessPartner,'') as [SponsorBusinessPartner],      
E.FullName as [GroupLeader],      
IsNull(A.ProjectComplexity,'') as [ProjectComplexity],      
cast([dbo].[GetProjectEndDate](@PIDFId) as nvarchar )+' Days' as [TotalProjectDuration],      
IsNull(@ProjectName,'') as [API],      
--STUFF((SELECT ', ' + cast(APIName as nvarchar)+' - '+ cast(APIS.APISourcingName as nvarchar)+' - '+ cast(APIVendor as nvarchar)      
--           FROM PIDFAPIDetails PA      
--     inner join Master_APISourcing APIS on APIS.APISourcingId = PA.APISourcingId      
--           WHERE PA.PIDFID=C.PIDFID        
--          FOR XML PATH('')), 1, 1, '') as [APISource]  
  case when PAM.Interested=1 then 'In House' else 'Out Source' end as [APISource]  
 ,@APIREQ as [APICommercialQuantity],      
 IsNull((select APIRequirementMarketPrice from PIDF_PBF_RnD_Master As D where D.PBFGeneralId = A.PBFGeneralId),'') as [APIprice],      
 IsNull(@ProjectName,'') as [APIRequirement],      
 IsNull([dbo].[GetPrototypePBFCharter](A.PBFGeneralId),'') as [Prototype],      
 IsNull([dbo].[GetScaleUpPBFCharter](A.PBFGeneralId),'') as [ScaleUp],      
 IsNull([dbo].[GetExhibitPBFCharter](A.PBFGeneralId),'') as [Exhibit],      
 --deliverable columns      
--@ProjectBudget = IsNull(A.TotalExpense,''),      
@ProjectBudget as [ProjectBudget],      
IsNull(convert(datetime,dateadd(dd,[dbo].[GetProjectEndDate](@PIDFId),B.ProjectInitiationDate),108),'') as [ProjectCompletionFilingDate],      
'FTR' as [BEStudies]  ,    
plant.PlantNameName as [PlantName],    
B.ProjectInitiationDate as [ProjectInitiationDate]    
, AnaAmvCost.Remark as [Note_Remark]    
from PIDF_PBF_General As A      
Inner Join PIDF_PBF As B On A.PIDFPBFId = B.PIDFPBFId      
Inner Join PIDF As C On C.PIDFId = B.PIDFId And C.BusinessUnitId = A.BusinessUnitId      
inner Join PIDF_PBF_RnD_Master As D On D.PBFGeneralId = A.PBFGeneralId      
inner Join Master_User As E On E.UserId = A.AnalyticalGLId      
left join Master_Plant As plant on plant.PlantId = B.PlantId    
left join PIDF_PBF_Analytical_AMVCost AnaAmvCost on AnaAmvCost.PBFGeneralId = A.PBFGeneralId   
left join PIDF_API_Master PAM on PAM.PIDFId = C.PIDFId  
Where C.PIDFId = @PIDFId      
      
      
---Cumulative Phase-Wise Budget (INR Lacs)--------      
Select       
isnull(B.FeasabilityCumTotal,0) [Feasability],       
isnull(B.PrototypeCumTotal,0) [Prototype],       
isnull(B.ScaleUpCumTotal,0) [ScaleUp],       
isnull(B.AMVCumTotal,0) [AMV],       
isnull(B.ExhibitCumTotal,0) [Exhibit],       
isnull(B.FilingCumTotal,0) [Filing]      
from PIDF_PBF_PhaseWiseBudget B      
Left join PIDF_PBF_General As AA on AA.PBFGeneralId = B.PBFGeneralId      
Left Join PIDF_PBF As BB On AA.PIDFPBFId = BB.PIDFPBFId      
Left Join PIDF As CC On CC.PIDFId = BB.PIDFId And CC.BusinessUnitId = AA.BusinessUnitId      
Where CC.PIDFId = @PIDFId      
      
      
---ADDITIONAL COST TO INCUR FOR ROW AND DOMESTIC MARKET (INR Lacs) AS Per Business Unit      
      
Declare @BusinessUnitId int = (Select BusinessUnitId from PIDF Where PIDFId = @PIDFId)      
Select       
dbo.GetReferenceProductCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) [ReferenceProductCost],      
dbo.GetCapexCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) [CapexMiscCost],      
dbo.GetBioStudyCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) [BioStudyCost],       
dbo.GetFillingCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) [FillingCost],      
(dbo.GetReferenceProductCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) + dbo.GetCapexCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId) + dbo.GetBioStudyCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId)     
+ dbo.GetFillingCostPBFCharter(A.PIDFId, A.BusinessUnitId, C.PBFGeneralId))[Total],      
D.BusinessUnitId,       
D.BusinessUnitName      
from PIDF As A      
Inner Join PIDF_PBF As B On A.PIDFId = B.PIDFId And B.PIDFId = @PIDFId      
Inner Join PIDF_PBF_General As C On C.PIDFPBFId = B.PIDFPBFId       
Inner Join Master_BusinessUnit As D On D.BusinessUnitId = C.BusinessUnitId      
Where A.PIDFId = @PIDFId And C.BusinessUnitId <> @BusinessUnitId      
SET NOCOUNT OFF;      
END 