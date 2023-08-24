 --exec stp_npd_GetIPDPatentDetailsCountryList 5  
alter proc [dbo].[stp_npd_GetIPDPatentDetailsCountryList](  
@BUId int=0  
)  
as  
begin  
  
Select DISTINCT A.CountryId, CountryName from Master_Country As A  
Inner Join Master_RegionCountryMapping As B On A.CountryID = B.CountryId  
Inner Join Master_BusinessUnitRegionMapping As C On B.RegionId = C.RegionId  
Where C.BusinessUnitId = @BUId   AND A.IsActive =1  
  
  end  


  go

  -- exec [stp_npd_GetIPDDetailByPIDF] 134     
alter PROCEDURE [dbo].[stp_npd_GetIPDDetailByPIDF]         
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
, [BasicPatentExpiry]      
, [OtherLmitingPatentDate1]    
, [OtherLmitingPatentDate2]    
, [EarliestLaunchDate]     
, [AnyPatentstobeFiled]     
, [EarliestMarketEntry]     
, [stimatedNumberofgenericsinthe]   
, [Lawfirmbeingused]      
, country.CountryName as CountryName,  
 (case when A.PatentStrategy = 6 then A.PatentStrategyOther   
  when A.PatentStrategy < 6 then (select PatentStrategyName from Master_Patent_Strategy where PatentStrategyID = A.PatentStrategy)  
 end  
 ) as PatentStrategyName  
 from PIDF_IPD_PatentDetails As A        
 Inner JOin PIDF_IPD As B On A.IPDID = B.IPDID        
 Inner Join Master_BusinessUnit As C On C.BusinessUnitId = B.BusinessUnitId   
 left join Master_Country country on country.CountryID = A.CountryId  
 Where B.PIDFId = @PIDFId        
        
 Select MoleculeName As ProjectName from PIDF        
 Where PIDFID = @PIDFId        
END 

  
  Go

  insert into Master_Patent_Strategy values('Wait for Expiry')
  insert into Master_Patent_Strategy values('Develop around')
  insert into Master_Patent_Strategy values('Limit SmPC/Carve OUT')
  insert into Master_Patent_Strategy values('Litigation')
  insert into Master_Patent_Strategy values('Negotiation')
  insert into Master_Patent_Strategy values('Other')


  go
--exec GetWishList  @PageSize = 10, @CurrentPageNumber = 0 ,@typeId=1  
alter Proc [dbo].[GetWishList]  
(            
@typeId int=0,            
@CurrentPageNumber INT = 0,                        
@PageSize INT = 25,                        
@SortColumn VARCHAR(100) = 'CreatedOn',                        
@SortDirection VARCHAR(5) = 'DESC',                        
@SearchText VARCHAR(MAX) =''            
)                    
AS                      
BEGIN                
            
SET NOCOUNT ON;   
Set @SearchText = IsNull(@SearchText, '')    
Select * from (            
SELECT Count(WishListId) OVER() TotalCount, * , ROW_NUMBER() OVER             
(      
ORDER BY             
CASE                      
WHEN @SortDirection <> 'DESC' THEN 0                      
WHEN @SortColumn = 'WishListId' THEN  WishListId                       
END DESC                       
,CASE                      
WHEN @SortDirection <> 'ASC' THEN 0                      
WHEN @SortColumn = 'WishListId' THEN WishListId            
END ASC             
, CASE                      
WHEN @SortDirection <> 'DESC' THEN null                      
WHEN @SortColumn = 'CreatedOn' THEN CreatedOn                  
END DESC                       
,CASE                      
WHEN @SortDirection <> 'ASC' THEN null                      
WHEN @SortColumn = 'CreatedOn' THEN CreatedOn                      
END ASC  
,  
CASE                      
WHEN @SortDirection <> 'DESC' THEN null                      
WHEN @SortColumn = 'MoleculeName' THEN MoleculeName                  
END DESC                       
,CASE                      
WHEN @SortDirection <> 'ASC' THEN null                      
WHEN @SortColumn = 'WishListTyp' THEN WishListTyp                      
END ASC  
,  
CASE                      
WHEN @SortDirection <> 'ASC' THEN null                      
WHEN @SortColumn = 'WishListTyp' THEN WishListTyp                      
END ASC  
)             
RowNumber from (            
SELECT  COUNT(u.WishListId) OVER() TotalRecord,WishListId,U.WishListTypeId,WT.WishListTyp,GeographyId,MBU.BusinessUnitName as Geography,MC.CountryName,u.CountryId,  
MoleculeName,Strength,IsInhouseOrInLicensed,DateOfFiling,DateOfApproval,NameofVendor,  
VendorEvaluationRemark,ReferenceDrugProduct,Remarks,U.CreatedOn,U.UpdatedOn,U.DeletedOn,u.IsActive,u.CreatedBy           
FROM Tbl_WishList As U     
LEFT join Master_Country as MC on MC.CountryID = U.CountryId            
LEFT join Master_User as us on us.UserId = u.CreatedBy         
LEFT Join Master_BusinessUnit As MBU On MBU.BusinessUnitId = U.GeographyId   
INNER JOIN Master_WishListType WT on WT.WishListTypeId=U.WishListTypeId  
)    
AS B WHERE               
(MoleculeName LIKE '%' + @SearchText + '%') or (WishListTyp LIKE '%' + @SearchText + '%') or (Strength LIKE '%' + @SearchText + '%')   
or (NameofVendor LIKE '%' + @SearchText + '%') or (CountryName LIKE '%' + @SearchText + '%') or (Geography LIKE '%' + @SearchText + '%') ) As C            
Where  (WishListTypeId=@typeId or @typeId=0) and   
RowNumber BETWEEN (@CurrentPageNumber) + 1 AND (@CurrentPageNumber) + @PageSize            
            
SET NOCOUNT OFF;   
end  
