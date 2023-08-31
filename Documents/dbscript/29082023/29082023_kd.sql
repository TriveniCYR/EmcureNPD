
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFList]    Script Date: 29-08-2023 19:04:16 ******/
DROP PROCEDURE IF EXISTS [dbo].[stp_npd_GetPIDFList]
GO
/****** Object:  StoredProcedure [dbo].[std_npd_GetIsAlloeToApprove]    Script Date: 29-08-2023 19:04:16 ******/
DROP PROCEDURE IF EXISTS [dbo].[std_npd_GetIsAlloeToApprove]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountryForBusinessUnitAndPIDF]    Script Date: 29-08-2023 19:04:16 ******/
DROP FUNCTION IF EXISTS [dbo].[GetCountryForBusinessUnitAndPIDF]
GO
/****** Object:  UserDefinedFunction [dbo].[GetBusinessUnitInterestedInPIDF]    Script Date: 29-08-2023 19:04:16 ******/
DROP FUNCTION IF EXISTS [dbo].[GetBusinessUnitInterestedInPIDF]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCanBeApprove]    Script Date: 29-08-2023 19:04:16 ******/
DROP FUNCTION IF EXISTS [dbo].[GetCanBeApprove]
GO
/****** Object:  UserDefinedFunction [dbo].[GetCanBeApprove]    Script Date: 29-08-2023 19:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- select dbo.GetCanBeApprove(186,87)
CREATE   function [dbo].[GetCanBeApprove]              
(  
@PIDFID bigint,
@USERID int =0
)            
returns bit as           
BEGIN        
     
	 declare @IsInteresbucOUNT AS INT
	 declare @MasterBUCOunt AS INT
	 declare @PIDFOlderThan15daysCount AS INT
	 declare @result AS bit

	;WITH myCTE as 
 (
 select A.BusinessUnitId as BUID  --count(distinct A.BusinessUnitId) 
 from PIDF_BusinessUnit_Interested A inner join
	 PIDF B on B.PIDFID = A.PIDFId
	 where A.PIDFID = @PIDFID
union
select  BusinessUnitId as BUID from PIDFProductStrength where PIDFID = @PIDFID
)
select @IsInteresbucOUNT=count(distinct BUID) from myCTE

	 select @MasterBUCOunt=count(BusinessUnitId) from Master_BusinessUnit where IsActive = 1 and BusinessUnitId 
	 in (select BusinessUnitId from Master_UserBusinessUnitMapping where USerId =@USERID)

 	 select @PIDFOlderThan15daysCount =count(PIDFID) from PIDF where StatusId =2 and StatusUpdatedDate < DATEADD(day,-15,getdate()) and PIDFID = @PIDFID
	
	set @result= 0

	 if(	((@MasterBUCOunt=@IsInteresbucOUNT) and @IsInteresbucOUNT>0) or (@PIDFOlderThan15daysCount=1) )
	 begin
		set @result= 1
	 end
	 else
	 begin
	 set @result= 0
	 end

	 return @result;
END    

--update PIDF set StatusUpdatedDate = DATEADD(day,-17,getdate()) where  PIDFID = 175
GO
/****** Object:  UserDefinedFunction [dbo].[GetBusinessUnitInterestedInPIDF]    Script Date: 29-08-2023 19:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from [dbo].[GetBusinessUnitInterestedInPIDF](189, 1)    
CREATE FUNCTION [dbo].[GetBusinessUnitInterestedInPIDF] (@PIDFId int,@BUId int)    
RETURNS TABLE as     
Return(  
Select BusinessUnitName,   
Case 
when IsNull((select BusinessUnitId from PIDF where PIDFID = @PIDFId),0)=@BUId then 1
When IsNull(B.PIDFBusinessUnitId, 0) = 0 Then 3   
When IsNull(B.IsInterested, 0) = 0 Then 2 Else 1 End As ActionStatus,   
--convert(date,B.CreatedDate,6) As InterestedDate,   
B.CreatedDate As InterestedDate, 
C.FullName  
from Master_BusinessUnit As A  
Left Join PIDF_BusinessUnit_Interested As B On A.BusinessUnitId = B.BusinessUnitId And B.PIDFId = @PIDFId  
Left JOin [Master_User] As C On C.UserId = B.CreatedBy  
Where A.BusinessUnitId = @BUId  
)   

--	select BusinessUnitId= from PIDF where PIDFID = 189
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountryForBusinessUnitAndPIDF]    Script Date: 29-08-2023 19:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- select * from  dbo.[GetCountryForBusinessUnitAndPIDF](180, 11)       
CREATE FUNCTION [dbo].[GetCountryForBusinessUnitAndPIDF]       
(       
 @PIDFID Bigint,      
 @BusinessUnitId int      
)      
RETURNS TABLE       
AS      
RETURN       
(      
Select DISTINCT A.CountryId, CountryName from Master_Country As A      
Inner Join Master_RegionCountryMapping As B On A.CountryID = B.CountryId      
Inner Join Master_BusinessUnitRegionMapping As C On C.RegionId = B.RegionId And C.BusinessUnitId = @BusinessUnitId    
--Inner Join PIDF As D On D.BusinessUnitId = C.BusinessUnitId And D.PIDFID = @PIDFId     
Inner Join PIDFProductStrength As E On E.BusinessUnitId = C.BusinessUnitId And E.PIDFID = @PIDFId     
Inner Join PIDFProductStrength_CountryMapping As F ON F.PIDFProductStrengthId = E.PIDFProductStrengthId  and F.CountryId = A.CountryID
Where A.IsActive = 1      
)      
   
GO
/****** Object:  StoredProcedure [dbo].[std_npd_GetIsAlloeToApprove]    Script Date: 29-08-2023 19:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



  
 --exec [std_npd_GetIsAlloeToApprove] 175  
CREATE PROCEDURE [dbo].[std_npd_GetIsAlloeToApprove]                            
(                  
@PIDFID INT = 0 ,
@USERID INT = 0  
)                          
AS                            
BEGIN  
select  dbo.GetCanBeApprove(@PIDFID,@USERID)  
end
GO
/****** Object:  StoredProcedure [dbo].[stp_npd_GetPIDFList]    Script Date: 29-08-2023 19:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec stp_npd_GetPIDFList @ScreenId =5, @SearchText = '', @PageSize = 100, @CurrentPageNumber = 0  , @UserId=1        
CREATE PROCEDURE [dbo].[stp_npd_GetPIDFList]                          
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
Declare @APIHead int = IsNull((Select top 1 APIUser from Master_User Where UserId = @UserId), 0)        
      
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
SELECT COUNT([PIDFID]) OVER() TotalRecord, PIDFID, PIDFNo,MoleculeName,BrandName,
dbo.GetCanBeApprove(PIDFID,@UserId) as IsAllowApprove,
DF.DosageFormName as DosageFormName, ME.MarketExtenstionName as marketExtension,                
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
WHERE  ((U.BusinessUnitId In (Select BusinessUnitId from Master_UserBusinessUnitMapping Where UserId = @UserId)) Or @UserId = 0 Or @ScreenId IN (1, 2, 4, 6, 8))          
          
And ((@ScreenId = 1 And U.StatusId > 0) Or (@ScreenId = 2 And U.StatusId NOT In (1,2,4))          
 Or (@ScreenId = 3 And U.StatusId NOT In (1, 2, 4) And MBU.IsDomestic = 1)          
  Or (@ScreenId = 4 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))          
   Or (@ScreenId = 5 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))          
    Or (@ScreenId = 6 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9) And U.InHouses = 1)          
  Or (@ScreenId = 7 And (select Count(PIDFStatusHistoryId) from PIDFStatusHistory where StatusId In (11, 13) And  PIDFId = U.PIDFId) > 1 And U.StatusId NOT In (1, 2, 3, 4, 5, 6, 8, 9))          
  Or (@ScreenId = 8 And U.StatusId In (18, 20, 21,22))          
  Or (@ScreenId = 9 And U.StatusId In (22))          
  )        
          
  And ((@ScreenId = 5 And ( @APIGroupLeader = 1       
 And  PIDFId In (Select PIDFId from PIDF_API_Master Where Interested = 1 And UserId = @UserId) OR @APIHead =1 )         
)      
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
GO
