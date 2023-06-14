
CREATE TABLE [dbo].[PIDFIMSData](
	[PIDFIMSDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[IMSValue] [float] NOT NULL,
	[IMSVolume] [float] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDFIMSData] PRIMARY KEY CLUSTERED 
(
	[PIDFIMSDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PIDFIMSData]  WITH CHECK ADD  CONSTRAINT [FK_PIDFIMSData_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO

ALTER TABLE [dbo].[PIDFIMSData] CHECK CONSTRAINT [FK_PIDFIMSData_PIDF]
GO
--------------------------------------------------------------------------------
CREATE TABLE [dbo].[Master_ExcipientRequirement](
	[ExcipientRequirementId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExcipientRequirementName] [nvarchar](70) NULL,
	[ExcipientRequirementCost] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExcipientRequirementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Master_ExcipientRequirement] ADD  CONSTRAINT [DF_Master_ExcipientRequirement_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


-----------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Master_PlantLine](
	[LineId] [bigint] IDENTITY(1,1) NOT NULL,
	[PlantId] [int] NOT NULL,
	[LineName] [nvarchar](70) NULL,
	[LineCost] [float] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_PlantLine] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Master_PlantLine] ADD  CONSTRAINT [DF_Master_PlantLine_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Master_PlantLine] ADD  CONSTRAINT [DF_Master_PlantLine_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Master_PlantLine]  WITH CHECK ADD  CONSTRAINT [FK_Master_PlantLine_Master_PlantLine] FOREIGN KEY([LineId])
REFERENCES [dbo].[Master_PlantLine] ([LineId])
GO

ALTER TABLE [dbo].[Master_PlantLine] CHECK CONSTRAINT [FK_Master_PlantLine_Master_PlantLine]
GO

--------------KUldip changes------------------------------------------------------------------------------------------------

alter table Master_User add APIGroupLeader bit null
-------------------------------------------------------------------

/****** Object:  Table [dbo].[PIDF_Commercial]    Script Date: 06-06-2023 16:56:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PIDF_Commercial_Master](
	[PIDFCommercialMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[BusinessUnitId] [int] NOT NULL,
	
	[Interested] [bit] NULL,
	[Remark] [nvarchar](500) NULL,
	
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	
 CONSTRAINT [PK_PIDF_Commercial_Master] PRIMARY KEY CLUSTERED 
(
	[PIDFCommercialMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PIDF_Commercial_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_Master_BusinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[Master_BusinessUnit] ([BusinessUnitId])
GO

ALTER TABLE [dbo].[PIDF_Commercial_Master] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_Master_BusinessUnit]
GO


ALTER TABLE [dbo].[PIDF_Commercial_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_Commercial_Master_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO

ALTER TABLE [dbo].[PIDF_Commercial_Master] CHECK CONSTRAINT [FK_PIDF_Commercial_Master_PIDF]
GO

----------------------------------------------------------------------------------------------------------------------------------------------
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PIDF_API_Master](
	[PIDFAPIMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[UserId] [int] NULL,
	[Interested] [bit] NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDF_API_Master] PRIMARY KEY CLUSTERED 
(
	[PIDFAPIMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[PIDF_API_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Master_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO

ALTER TABLE [dbo].[PIDF_API_Master] CHECK CONSTRAINT [FK_PIDF_API_Master_PIDF]
GO

ALTER TABLE [dbo].[PIDF_API_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_API_Master_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Master_User] ([UserId])
GO

ALTER TABLE [dbo].[PIDF_API_Master] CHECK CONSTRAINT [FK_PIDF_API_Master_Master_User]
GO

-----------------------------------------------------------------------------------------------------
-- exec stp_npd_GetCommercialFormData 117,1       
alter PROCEDURE [dbo].[stp_npd_GetCommercialFormData]                    
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
[NspHigh], [BrandPrice], [GenericPrice]        
, B.BusinessUnitId, B.PIDFId, B.PIDFProductStrengthId, B.PackSizeId        
from PIDF_Commercial_years As A        
Inner Join PIDF_Commercial As B On A.PIDFCommercialId = B.PIDFCommercialId        
Where PIDFId = @PIDFId and B.IsDeleted = 0 order by PIDFCommercialId,YearIndex     
    
        
Select Strength, C.UnitofMeasurementName, A.PIDFProductStrengthId from PIDFProductStrength As A        
Inner Join PIDF As B On A.PIDFId = B.PIDFId        
Inner Join Master_UnitofMeasurement As C On C.UnitofMeasurementId = A.UnitofMeasurementId        
Where A.PIDFId = @PIDFId        
        
Select StatusId from PIDF         
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
                   
END    
----------------------------------------------------------------------------
alter PROCEDURE [dbo].[ProcGetProjectNameAndStrength]     
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
STUFF((SELECT ', ' + cast(APIName as nvarchar)+' - '+ cast(APIS.APISourcingName as nvarchar)+' - '+ cast(APIVendor as nvarchar)    
           FROM PIDFAPIDetails PA    
     inner join Master_APISourcing APIS on APIS.APISourcingId = PA.APISourcingId    
           WHERE PA.PIDFID=C.PIDFID      
          FOR XML PATH('')), 1, 1, '') as [APISource],    
 @APIREQ as [APICommercialQuantity],    
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

----------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFList]    Script Date: 09-06-2023 19:01:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
        
-- exec stp_npd_GetPIDFList @ScreenId =1, @SearchText = '', @PageSize = 100, @CurrentPageNumber = 0  , @UserId=1
ALTER PROCEDURE [dbo].[stp_npd_GetPIDFList]                  
(        
@UserId INT = 0,        
@CurrentPageNumber INT = 0,                    
@PageSize INT = 25,                    
@SortColumn VARCHAR(100) = 'PIDFNO',                    
@SortDirection VARCHAR(5) = 'ASC',                    
@SearchText VARCHAR(MAX) ='',  
@ScreenId int = 0  
)                
AS                  
BEGIN            
        
SET NOCOUNT ON;       

Declare @APIGroupLeader int = IsNull((Select top 1 APIGroupLeader from Master_User Where UserId = @UserId), 0)

--DECLARE @IsManagement bit=(select top 1 Isnull(IsManagement,0) from master_user where Userid=@UserId);  
--Declare @AlReadyApproved bit=(select top 1 case when isnull(StatusId,0)=20 or isnull(StatusId,0)=21 then 1 else 0 end from PIDF_ManagementApprovalStatusHistory where CreatedBy=@UserId)  
Set @SearchText = IsNull(@SearchText, '')        
Select * from (        
SELECT Count(PIDFID) OVER() TotalCount, * , ROW_NUMBER() OVER         
(        
ORDER BY         
         
CASE                  
WHEN @SortDirection <> 'DESC' THEN 'DESC'                  
WHEN @SortColumn = 'PIDFNo' THEN PIDFNo             
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN 'ASC'                  
WHEN @SortColumn = 'PIDFNo' THEN PIDFNo        
END ASC         
, CASE                  
WHEN @SortDirection <> 'DESC' THEN 'Desc'                  
WHEN @SortColumn = 'MoleculeName' THEN MoleculeName        
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN 'ASC'                  
WHEN @SortColumn = 'MoleculeName' THEN MoleculeName        
END ASC         
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'BrandName' THEN BrandName              
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'BrandName' THEN BrandName                  
END ASC        
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'DosageFormName' THEN DosageFormName              
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'DosageFormName' THEN DosageFormName                  
END ASC        
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'CountryName' THEN CountryName             
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'CountryName' THEN CountryName                  
END ASC        
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'CreatedDate' THEN CreatedDate        
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'CreatedDate' THEN CreatedDate                  
END ASC        
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'BusinessUnitName' THEN BusinessUnitName        
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'BusinessUnitName' THEN BusinessUnitName                  
END ASC    
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'OralName' THEN OralName        
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'OralName' THEN OralName                  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'status' THEN [status]
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'status' THEN [status]  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'MarketExtenstionName' THEN marketExtension  
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'MarketExtenstionName' THEN marketExtension  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'applicant' THEN applicant  
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'applicant' THEN applicant  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'InHouses' THEN InHouses  
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'InHouses' THEN InHouses  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'inidication' THEN inidication  
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'inidication' THEN inidication  
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'CreatedBy' THEN CreatedBy 
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'CreatedBy' THEN CreatedBy
END ASC   
, CASE                  
WHEN @SortDirection <> 'DESC' THEN null                  
WHEN @SortColumn = 'diaName' THEN DIAName  
END DESC                   
,CASE                  
WHEN @SortDirection <> 'ASC' THEN null                  
WHEN @SortColumn = 'diaName' THEN DIAName  
END ASC   
)         
RowNumber from (        
SELECT COUNT([PIDFID]) OVER() TotalRecord, PIDFID, PIDFNo,MoleculeName,BrandName,DF.DosageFormName as DosageFormName, ME.MarketExtenstionName as marketExtension,        
U.ApprovedGenerics, U.LaunchedGenerics, U.RFDApplicant as applicant, MC.CountryName as CountryName, MD.DIAName as diaName,        
P.PackagingTypeName as ProductPackagingName,U.RFDIndication as inidication,        
us.FullName as CreatedBy,us.UserId as UserId,ISNULL(
(select top 1 case when isnull(StatusId,0)=20 or isnull(StatusId,0)=21 then 1 else 0 end from PIDF_ManagementApprovalStatusHistory as A where CreatedBy=@UserId
and A.PIDFId = U.PIDFID)
,0) as AlReadyApproved, ps.PIDFStatus as status, ps.StatusColor, PIDFStatusID        
, MO.OralName, MBU.BusinessUnitName, IsNull(U.InHouses, 0) As InHouses, U.CreatedDate, U.BusinessUnitId        
        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 6), 0) as bit) As IPD        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 11), 0) as bit) As Commercial        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 13), 0) as bit) As PBF        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 15), 0) as bit) As API        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 17), 0) as bit) As Finance        
, Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 9), 0) as bit) As Medical        
        
, cast(0 as bit) Management, RFDBrand        

, IsNull((Select top 1 Interested as APIInterested from PIDF_API_Master Where PIDFId = U.PIDFId), 0) As APIInterested
   
, (select A.Strength, B.UnitofMeasurementName from PIDFProductStrength As A        
LEFT Join Master_UnitofMeasurement As B On A.UnitofMeasurementId = B.UnitofMeasurementId        
Where PIDFID = U.PIDFID        
FOR JSON PATH, INCLUDE_NULL_VALUES) As ProductStrength        
        
, (select A.APIName, B.APISourcingName, A.APIVendor from PIDFAPIDetails As A        
LEFT Join Master_APISourcing As B On A.APISourcingId = B.APISourcingId        
Where PIDFID = U.PIDFID        
FOR JSON PATH, INCLUDE_NULL_VALUES)  As ProductAPIDetail        
        
,(  
select * from  dbo.GetPIDFHistoryStatusByPIDFID(U.PIDFID) Order By CreatedDate desc        
FOR JSON PATH, INCLUDE_NULL_VALUES)  As StatusHistory        
        
FROM PIDF As U   
LEFT join Master_DosageForm as DF on DF.DosageFormId = u.DosageFormId        
LEFT join Master_PIDFStatus as ps on ps.PIDFStatusID = u.StatusId        
LEFT join Master_Country as MC on MC.CountryID = U.RFDCountryId        
LEFT join Master_User as us on us.UserId = u.CreatedBy        
LEFT Join Master_Oral As MO On MO.OralId = U.OralId        
LEFT Join Master_BusinessUnit As MBU On MBU.BusinessUnitId = U.BusinessUnitId        
        
LEFT join Master_MarketExtenstion ME on ME.MarketExtenstionId = U.MarketExtenstionId        
LEFT join Master_DIA as MD on MD.DIAId = U.DIAId        
Left join Master_PackagingType as P on P.PackagingTypeId = u.PackagingTypeId        
WHERE  ((U.BusinessUnitId In (Select BusinessUnitId from Master_UserBusinessUnitMapping Where UserId = @UserId)) Or @UserId = 0 Or @ScreenId IN (2, 4, 6, 8))  
  
And ((@ScreenId = 1 And U.StatusId > 0) Or (@ScreenId = 2 And U.StatusId NOT In (1,2,4))  
 Or (@ScreenId = 3 And U.StatusId NOT In (1, 2, 4))  
  Or (@ScreenId = 4 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))  
   Or (@ScreenId = 5 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))  
    Or (@ScreenId = 6 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))  
  Or (@ScreenId = 7 And (select Count(PIDFStatusHistoryId) from PIDFStatusHistory where StatusId In (11, 13) And  PIDFId = U.PIDFId) > 1 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))  
  Or (@ScreenId = 8 And U.StatusId In (18, 20, 21,22))  
  Or (@ScreenId = 9 And U.StatusId In (22))  
  )
  
  And ((@ScreenId = 5 And @APIGroupLeader = 1 And 
  PIDFId In (Select PIDFId from PIDF_API_Master Where Interested = 1 And UserId = @UserId)) 
  OR (@APIGroupLeader = 0) OR (@ScreenId <> 5))

)AS B WHERE           
(BrandName LIKE '%' + @SearchText + '%' OR PIDFNo LIKE '%' + @SearchText + '%'   
OR MoleculeName LIKE '%' + @SearchText + '%' OR DosageFormName LIKE '%' + @SearchText + '%'   
OR BusinessUnitName LIKE '%' + @SearchText + '%' OR OralName LIKE '%' + @SearchText + '%'        
OR CountryName LIKE '%' + @SearchText + '%' OR status LIKE '%' + @SearchText + '%'  
 OR applicant LIKE '%' + @SearchText + '%' OR diaName LIKE '%' + @SearchText + '%'  
 OR ProductPackagingName LIKE '%' + @SearchText + '%' OR RFDBrand LIKE '%' + @SearchText + '%'  
)        
) As C Where         
RowNumber BETWEEN (@CurrentPageNumber) + 1 AND (@CurrentPageNumber) + @PageSize           
           
SET NOCOUNT OFF;        
                 
END   
