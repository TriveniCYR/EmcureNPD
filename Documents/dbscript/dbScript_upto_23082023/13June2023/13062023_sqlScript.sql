USE [EmcureNPDDev]


ALTER TABLE [dbo].[PIDF_API_Master] drop constraint FK_PIDF_API_Master_Master_User 
update Master_PIDFStatus set PIDFStatus = 'Budget Approved' where PIDFStatusID = 20
update Master_PIDFStatus set PIDFStatus = 'Budget Rejected' where PIDFStatusID = 21




GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFAPICharterSummaryData]    Script Date: 13-06-2023 11:57:42 ******/
DROP PROCEDURE [dbo].[stp_npd_GetPIDFAPICharterSummaryData]
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFAPICharterData]    Script Date: 13-06-2023 11:57:42 ******/
DROP PROCEDURE [dbo].[stp_npd_GetPIDFAPICharterData]
GO
/****** Object:  StoredProcedure [dbo].[ProcGetProjectNameAndStrength]    Script Date: 13-06-2023 11:57:42 ******/
DROP PROCEDURE [dbo].[ProcGetProjectNameAndStrength]

GO
/****** Object:  Table [dbo].[Tbl_SessionManager]    Script Date: 13-06-2023 11:57:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_SessionManager](
	[TokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TokenIssuedAt] [datetime] NULL,
	[VallidTo] [datetime] NULL,
	[UserToken] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tbl_SessionManager] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tbl_SessionManager]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_SessionManager_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Tbl_SessionManager] CHECK CONSTRAINT [FK_Tbl_SessionManager_Master_User]
GO
/****** Object:  StoredProcedure [dbo].[ProcGetProjectNameAndStrength]    Script Date: 13-06-2023 11:57:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec ProcGetProjectNameAndStrength 122    
CREATE proc [dbo].[ProcGetProjectNameAndStrength]    
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
IsNull(A.TotalExpense,0) as [ProjectBudget],    
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
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFAPICharterData]    Script Date: 13-06-2023 11:57:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec stp_npd_GetPIDFAPICharterData @PIDFID=129    
CREATE   PROCEDURE [dbo].[stp_npd_GetPIDFAPICharterData]                
(    
@PIDFID bigint = 0    
)              
AS                
BEGIN          
    
 declare @PIDFAPICharterId as bigint    
  declare @PIDFProjectInitiationDate as datetime    
 select @PIDFAPICharterId=PIDF_API_CharterId from PIDF_API_Charter where PIDFID= @PIDFID    
 select top 1 @PIDFProjectInitiationDate = ProjectInitiationDate from PIDF_PBF where PIDFId = @PIDFID  
 --select @PIDFAPICharterId    
 --------------------Table[0]--Charter Object---------------    
 select C.PIDF_API_CharterId,C.ProjectComplexityId,C.ManHourRates,    
(select top 1 FullName from Master_User where UserId in (select UserId from PIDF_API_Master where PIDFID = @PIDFID)) as APIGroupLeader,
 (select CONVERT(NVARCHAR,@PIDFProjectInitiationDate,106))[ProjectInitiationDate],   
 (SELECT CONVERT(NVARCHAR, DATEADD(DAY, [dbo].[GetProjectEndDate](@PIDFID),@PIDFProjectInitiationDate),106) ProjectEndDate)[ProjectEndDate], -- End Date Hasrcoded to Next 1 Months, --> this may need to be Change on Client feedback  
 A.MoleculeName [ProjectName],    
 B.BusinessUnitName [Market]    
 from PIDF A left join Master_BusinessUnit B on A.BusinessUnitId=B.BusinessUnitId     
 left join PIDF_API_Charter C on C.PIDFId = A.PIDFID    
 where A.PIDFID=@PIDFID    
 --------------------Table[1]----TimelineInMonths-------------    
   Select A.Name,A.Master_API_Charter_TimelineInMonthsId [TimelineInMonthsId],  B.TimelineInMonthsValue from Master_API_Charter_TimelineInMonths As A    
Left join PIDF_API_Charter_TimelineInMonths As B On A.Master_API_Charter_TimelineInMonthsId =    
B.TimelineInMonthsId  And B.PIDF_API_CharterId = @PIDFAPICharterId    
Order By A.SortOrder    
    
--------------------Table[2]----AnalyticalDepartment-------------    
 Select A.Name,A.Master_API_Charter_AnalyticalDepartmentId [AnalyticalDepartmentId],     
 B.AnalyticalDepartmentARDValue,    
 B.AnalyticalDepartmentImpurityValue,    
 B.AnalyticalDepartmentStabilityValue,    
 B.AnalyticalDepartmentAMVValue,    
 B.AnalyticalDepartmentAMTValue    
 from Master_API_Charter_AnalyticalDepartment As A    
Left join PIDF_API_Charter_AnalyticalDepartment As B On A.Master_API_Charter_AnalyticalDepartmentId =     
B.AnalyticalDepartmentId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder    
    
    
--------------------Table[3]----PRDDepartment-------------    
 Select A.Name,A.Master_API_Charter_PRDDepartmentId [PRDDepartmentId],     
 B.PRDDepartmentRawMaterialValue    
 from Master_API_Charter_PRDDepartment As A    
Left join PIDF_API_Charter_PRDDepartment As B On A.Master_API_Charter_PRDDepartmentId =     
B.PRDDepartmentId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder    
    
--------------------Table[4]----CapitalOtherExpenditure-------------    
 Select A.Name,A.Master_API_Charter_CapitalOtherExpenditureId [CapitalOtherExpenditureId],     
 B.CapitalOtherExpenditureAmountValue,    
 B.CapitalOtherExpenditureRemarkValue    
 from Master_API_Charter_CapitalOtherExpenditure As A    
Left join PIDF_API_Charter_CapitalOtherExpenditure As B On A.Master_API_Charter_CapitalOtherExpenditureId =     
B.CapitalOtherExpenditureId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder    
    
    
--------------------Table[5]----ManhourEstimates-------------    
 Select A.Name,A.Master_API_Charter_ManhourEstimatesId [ManhourEstimatesId],  B.ManhourEstimatesNoOfEmployeeValue,B.ManhourEstimatesMonthsValue,    
  B.ManhourEstimatesHoursValue,B.ManhourEstimatesCostValue from Master_API_Charter_ManhourEstimates As A    
Left join PIDF_API_Charter_ManhourEstimates As B On A.Master_API_Charter_ManhourEstimatesId =     
B.ManhourEstimatesId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder    
    
--------------------Table[6]----HeadwiseBudget-------------    
 Select A.Name,A.Master_API_Charter_HeadwiseBudgetId [HeadwiseBudgetId],     
 B.HeadwiseBudgetValue    
 from Master_API_Charter_HeadwiseBudget As A    
Left join PIDF_API_Charter_HeadwiseBudget As B On A.Master_API_Charter_HeadwiseBudgetId =     
B.HeadwiseBudgetId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder    
    
--------------------Table[7]----ManagementTeam-------------      
 Select FullName,DesignationName from Master_User where APIUser =1 and IsDeleted=0 and IsActive=1   
  
  
END       
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFAPICharterSummaryData]    Script Date: 13-06-2023 11:57:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
    
-- exec [stp_npd_GetPIDFAPICharterSummaryData] @PIDFID=115        
CREATE   PROCEDURE [dbo].[stp_npd_GetPIDFAPICharterSummaryData]                        
(            
@PIDFID bigint = 0            
)                      
AS                        
BEGIN                 
            
declare @CurrencySymbol  as nvarchar            
set @CurrencySymbol = [dbo].[GetCurrencySymbol](@PIDFId)         
     
      
            
 declare @PIDFAPICharterId as bigint          
  declare @PIDFProjectInitiationDate as datetime          
 select @PIDFAPICharterId=PIDF_API_CharterId from PIDF_API_Charter where PIDFID= @PIDFID         
  select top 1 @PIDFProjectInitiationDate = ProjectInitiationDate from PIDF_PBF where PIDFId = @PIDFID        
 --select @PIDFAPICharterId            
 --------------------Table[0]--Charter Object---------------            
 select C.PIDF_API_CharterId,C.ProjectComplexityId, Replace(FORMAT(cast(C.ManHourRates as decimal(28,2)), 'C', 'en-US'),'$',@CurrencySymbol) 'ManHourRates' ,            
         (select top 1 FullName from Master_User where UserId in (select UserId from PIDF_API_Master where PIDFID = @PIDFID)) as APIGroupLeader, 
 (select CONVERT(NVARCHAR,@PIDFProjectInitiationDate,106))[ProjectInitiationDate],         
 (SELECT CONVERT(NVARCHAR, DATEADD(DAY, [dbo].[GetProjectEndDate](@PIDFID), @PIDFProjectInitiationDate),106) ProjectEndDate)[ProjectEndDate], -- End Date Hasrcoded to Next 1 Months, --> this may need to be Change on Client feedback        
        
 A.MoleculeName [ProjectName],            
 B.BusinessUnitName [Market]            
 from PIDF A left join Master_BusinessUnit B on A.BusinessUnitId=B.BusinessUnitId             
 left join PIDF_API_Charter C on C.PIDFId = A.PIDFID            
 where A.PIDFID=@PIDFID            
 --------------------Table[1]----TimelineInMonths-------------            
   Select A.Name,A.Master_API_Charter_TimelineInMonthsId [TimelineInMonthsId],  B.TimelineInMonthsValue from Master_API_Charter_TimelineInMonths As A            
Left join PIDF_API_Charter_TimelineInMonths As B On A.Master_API_Charter_TimelineInMonthsId =            
B.TimelineInMonthsId             
And B.PIDF_API_CharterId = @PIDFAPICharterId            
Order By A.SortOrder            
            
--------------------Table[2]----AnalyticalDepartment-------------            
 Select A.Name,A.Master_API_Charter_AnalyticalDepartmentId [AnalyticalDepartmentId]            
            
 , Replace(FORMAT(cast( B.AnalyticalDepartmentARDValue as numeric),   'C', 'en-US'),'$',@CurrencySymbol    ) 'AnalyticalDepartmentARDValue'            
 , Replace(FORMAT(cast( B.AnalyticalDepartmentImpurityValue as numeric),'C', 'en-US'),'$',@CurrencySymbol     ) 'AnalyticalDepartmentImpurityValue'            
 , Replace(FORMAT(cast( B.AnalyticalDepartmentStabilityValue as numeric), 'C', 'en-US'),'$',@CurrencySymbol    ) 'AnalyticalDepartmentStabilityValue'            
 , Replace(FORMAT(cast( B.AnalyticalDepartmentAMVValue as numeric),   'C', 'en-US'),'$',@CurrencySymbol    ) 'AnalyticalDepartmentAMVValue'            
 , Replace(FORMAT(cast( B.AnalyticalDepartmentAMTValue as numeric),  'C', 'en-US'),'$',@CurrencySymbol     ) 'AnalyticalDepartmentAMTValue'            
 from Master_API_Charter_AnalyticalDepartment As A            
Left join PIDF_API_Charter_AnalyticalDepartment As B On A.Master_API_Charter_AnalyticalDepartmentId =             
B.AnalyticalDepartmentId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder            
            
            
--------------------Table[3]----PRDDepartment-------------            
 Select A.Name,A.Master_API_Charter_PRDDepartmentId [PRDDepartmentId],             
 Replace(FORMAT(cast( B.PRDDepartmentRawMaterialValue as numeric), 'C', 'en-US'),'$',@CurrencySymbol) 'PRDDepartmentRawMaterialValue'            
 from Master_API_Charter_PRDDepartment As A            
Left join PIDF_API_Charter_PRDDepartment As B On A.Master_API_Charter_PRDDepartmentId =             
B.PRDDepartmentId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder            
            
--------------------Table[4]----CapitalOtherExpenditure-------------            
 Select A.Name,A.Master_API_Charter_CapitalOtherExpenditureId [CapitalOtherExpenditureId],             
 Replace(FORMAT(cast( B.CapitalOtherExpenditureAmountValue as numeric), 'C', 'en-US'),'$',@CurrencySymbol) 'CapitalOtherExpenditureAmountValue',            
 B.CapitalOtherExpenditureRemarkValue            
 from Master_API_Charter_CapitalOtherExpenditure As A            
Left join PIDF_API_Charter_CapitalOtherExpenditure As B On A.Master_API_Charter_CapitalOtherExpenditureId =             
B.CapitalOtherExpenditureId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder                
            
--------------------Table[5]----ManhourEstimates-------------            
 Select A.Name,A.Master_API_Charter_ManhourEstimatesId [ManhourEstimatesId],  B.ManhourEstimatesNoOfEmployeeValue,B.ManhourEstimatesMonthsValue,            
  B.ManhourEstimatesHoursValue,            
   Replace(FORMAT(cast( B.ManhourEstimatesCostValue as numeric), 'C', 'en-US'),'$',@CurrencySymbol) 'ManhourEstimatesCostValue'             
  from Master_API_Charter_ManhourEstimates As A            
Left join PIDF_API_Charter_ManhourEstimates As B On A.Master_API_Charter_ManhourEstimatesId =             
B.ManhourEstimatesId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder            
            
--------------------Table[6]----HeadwiseBudget-------------            
 Select A.Name,A.Master_API_Charter_HeadwiseBudgetId [HeadwiseBudgetId],             
             
   Replace(FORMAT(cast( B.HeadwiseBudgetValue as numeric), 'C', 'en-US'),'$',@CurrencySymbol) 'HeadwiseBudgetValue'            
            
 from Master_API_Charter_HeadwiseBudget As A            
Left join PIDF_API_Charter_HeadwiseBudget As B On A.Master_API_Charter_HeadwiseBudgetId =             
B.HeadwiseBudgetId And B.PIDF_API_CharterId = @PIDFAPICharterId Order By A.SortOrder            
          
--------------------Table[7]----ManagementTeam-------------            
 Select FullName,DesignationName from Master_User where APIUser =1 and IsDeleted=0 and IsActive=1            
            
END 
GO
