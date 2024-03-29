go

 -- exec [stp_npd_Get_PIDFListFilterDropdowndata] @UserId =1
  
  
  
 -- exec [stp_npd_Get_PIDFListFilterDropdowndata] @UserId =1  
CREATE or alter  PROCEDURE [dbo].[stp_npd_Get_PIDFListFilterDropdowndata]                              
(                    
@UserId int                 
)                            
AS                          
BEGIN     
    
select CountryID,CountryName from Master_Country --0    
select MarketExtenstionId,MarketExtenstionName from Master_MarketExtenstion --1    
select BusinessUnitId,BusinessUnitName from Master_BusinessUnit  --2    
select ManufacturingId,ManufacturingName from Master_Manufacturing --3  
  
select  
distinct CONCAT(FORMAT(BudgetLaunchDate, 'MMM', 'en-US'),'-',year(BudgetLaunchDate)) as BudgetLaunchDateText  
,convert(varchar(7),BudgetLaunchDate, 126)  as BudgetLaunchDate  
from PIDF_PBF_RA where BudgetLaunchDate is not null --4    
  
-- convert(varchar, BudgetLaunchDate, 106) -->30 Dec 2022    
end       
go
-------------------------------------------------------------------------------------------------------------

go

-- exec stp_npd_GetPIDFList @ScreenId =5, @SearchText = '', @PageSize = 100, @CurrentPageNumber = 0  , @UserId=1   ,@buid='1'       
CREATE or  alter PROCEDURE [dbo].[stp_npd_GetPIDFList]                            
(                  
 @UserId INT = 0,                  
 @CurrentPageNumber INT = 0,                              
 @PageSize INT = 25,                              
 @SortColumn VARCHAR(100) = 'PIDFNO',                              
 @SortDirection VARCHAR(5) = 'ASC',                              
 @SearchText VARCHAR(MAX) ='',            
 @ScreenId int = 0,  
 @countryid VARCHAR(100) = NULL,      
 @marketextenstionid VARCHAR(100) = NULL,      
 @buid VARCHAR(100) = NULL,      
 @manufacturingid VARCHAR(100) = NULL,      
 @budgetlaunchdate VARCHAR(100) = NULL,      
 @inhouse VARCHAR(100) = NULL  
)                          
AS                            
BEGIN                      
                  
 SET NOCOUNT ON;                 
          
 Declare @APIGroupLeader int = IsNull((Select top 1 APIGroupLeader from Master_User Where UserId = @UserId), 0)          
 Declare @APIHead int = IsNull((Select top 1 APIUser from Master_User Where UserId = @UserId), 0)          
        
 SET @SearchText = IsNull(@SearchText, '')    
   
 SELECT * FROM   
 (                  
  SELECT Count(PIDFID) OVER() TotalCount, * , ROW_NUMBER() OVER                   
  (                  
   ORDER BY      
   CASE WHEN @SortDirection <> 'DESC' THEN 'DESC' WHEN @SortColumn = 'PIDFNo' THEN PIDFNo END DESC,                             
   CASE WHEN @SortDirection <> 'ASC' THEN 'ASC'   WHEN @SortColumn = 'PIDFNo' THEN PIDFNo END ASC,                  
   CASE                            
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
  RowNumber FROM   
  (SELECT COUNT([PIDFID]) OVER() TotalRecord, PIDFID, PIDFNo,MoleculeName,BrandName,  
   dbo.GetCanBeApprove(PIDFID,@UserId) as IsAllowApprove,  
   DF.DosageFormName as DosageFormName, ME.MarketExtenstionName as marketExtension,                  
   U.ApprovedGenerics, U.LaunchedGenerics, U.RFDApplicant as applicant, MC.CountryName as CountryName, MD.DIAName as diaName,                  
   P.PackagingTypeName as ProductPackagingName,U.RFDIndication as inidication,                  
   us.FullName as CreatedBy,us.UserId as UserId,  
   ISNULL((select top 1 case when isnull(StatusId,0)=20 or isnull(StatusId,0)=21 then 1 else 0 end from PIDF_ManagementApprovalStatusHistory as A where CreatedBy=@UserId          
     and A.PIDFId = U.PIDFID),0) as AlReadyApproved,   
   ps.PIDFStatus as status, ps.StatusColor, PIDFStatusID, MO.OralName, MBU.BusinessUnitName, IsNull(U.InHouses, 0) As InHouses, U.CreatedDate, U.BusinessUnitId,      
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 6), 0) as bit) As IPD ,                 
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 11), 0) as bit) As Commercial ,                 
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 13), 0) as bit) As PBF  ,                
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 15), 0) as bit) As API ,                 
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 17), 0) as bit) As Finance,              
   Cast(IsNull((Select top 1 1 from PIDFStatusHistory Where PIDFId = U.PIDFId And StatusId = 9), 0) as bit) As Medical,   
   cast(0 as bit) Management, RFDBrand,    
   IsNull((Select top 1 Interested as APIInterested from PIDF_API_Master Where PIDFId = U.PIDFId), 0) As APIInterested,     
   (select A.Strength, B.UnitofMeasurementName from PIDFProductStrength As A                  
    LEFT Join Master_UnitofMeasurement As B On A.UnitofMeasurementId = B.UnitofMeasurementId                  
    Where PIDFID = U.PIDFID                  
    FOR JSON PATH, INCLUDE_NULL_VALUES  
   ) As ProductStrength,                 
                  
   (select A.APIName, B.APISourcingName, A.APIVendor from PIDFAPIDetails As A                  
    LEFT Join Master_APISourcing As B On A.APISourcingId = B.APISourcingId                  
    Where PIDFID = U.PIDFID                  
    FOR JSON PATH, INCLUDE_NULL_VALUES)  As ProductAPIDetail,                  
                  
   (select * from  dbo.GetPIDFHistoryStatusByPIDFID(U.PIDFID) Order By CreatedDate desc                  
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
   LEFT join Master_PackagingType as P on P.PackagingTypeId = u.PackagingTypeId     
   WHERE   ((U.BusinessUnitId In (Select BusinessUnitId from Master_UserBusinessUnitMapping Where UserId = @UserId)) Or @UserId = 0 Or @ScreenId IN (1, 2, 4, 6, 8))     
    AND ((@ScreenId = 1 AND U.StatusId > 0)   
      Or (@ScreenId = 2 And U.StatusId NOT In (1,2,4))          
      Or (@ScreenId = 3 And U.StatusId NOT In (1, 2, 4) And MBU.IsDomestic = 1)            
      Or (@ScreenId = 4 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))            
      Or (@ScreenId = 5 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))            
      Or (@ScreenId = 6 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9) And U.InHouses = 1)            
      Or (@ScreenId = 7 And (select Count(PIDFStatusHistoryId) from PIDFStatusHistory where StatusId In (11, 13) And  PIDFId = U.PIDFId) > 1   
            And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9)  
              )            
      Or (@ScreenId = 8 And U.StatusId In (18, 20, 21,22))            
      Or (@ScreenId = 9 And U.StatusId In (22)  
       )   
     )  
            
    And ( (@ScreenId = 5 And ( @APIGroupLeader = 1 And  PIDFId In (Select PIDFId from PIDF_API_Master Where Interested = 1 And UserId = @UserId) OR @APIHead =1))  
      OR (@APIGroupLeader = 0) OR (@ScreenId <> 5)  
     )  
  
  
    --AND (@countryid IS NULL    OR (MC.CountryID = @countryid))    
    --AND (@inhouse IS NULL    OR (U.InHouses = @inhouse))     
    --AND (@buid IS NULL     OR (U.BusinessUnitId = @buid))    
    --AND (@manufacturingid IS NULL  OR (U.DIAId = @manufacturingid)) 

    AND (@countryid IS NULL or @countryid =''   OR (MC.CountryID  in (SELECT value FROM STRING_SPLIT(@countryid, ','))  ))  
    AND (@inhouse IS NULL OR @inhouse =''    OR (U.InHouses in (SELECT value FROM STRING_SPLIT(@inhouse, ','))  ))   
    AND (@buid IS NULL OR @buid=''    OR (U.BusinessUnitId in (SELECT value FROM STRING_SPLIT(@buid, ','))  ))  
    AND (@manufacturingid IS NULL  OR @manufacturingid='' OR (U.DIAId in (SELECT value FROM STRING_SPLIT(@manufacturingid, ','))  ))  
    
	AND (@budgetlaunchdate IS NULL  OR 
	PIDFID in
	(
	select PIDFId from PIDF_PBF_RA where month(BudgetLaunchDate) in (SELECT value FROM STRING_SPLIT(@budgetlaunchdate, '-')) 
	and year(BudgetLaunchDate) in (SELECT value FROM STRING_SPLIT(@budgetlaunchdate, '-')) 
	)
	)  
    AND (@marketextenstionid IS NULL OR (ME.MarketExtenstionId = @marketextenstionid))  
      
          
   ) AS B   
  WHERE   (BrandName LIKE '%' + @SearchText + '%'    OR PIDFNo LIKE '%' + @SearchText + '%'             
     OR MoleculeName LIKE '%' + @SearchText + '%'  OR DosageFormName LIKE '%' + @SearchText + '%'             
     OR BusinessUnitName LIKE '%' + @SearchText + '%'  OR OralName LIKE '%' + @SearchText + '%'                  
     OR CountryName LIKE '%' + @SearchText + '%'  OR status LIKE '%' + @SearchText + '%'            
     OR applicant LIKE '%' + @SearchText + '%'   OR diaName LIKE '%' + @SearchText + '%'            
     OR ProductPackagingName LIKE '%' + @SearchText + '%' OR RFDBrand LIKE '%' + @SearchText + '%'            
     )   
  ) As C   
  WHERE  RowNumber BETWEEN (@CurrentPageNumber) + 1 AND (@CurrentPageNumber) + @PageSize                     
                     
 SET NOCOUNT OFF;                  
                           
END 