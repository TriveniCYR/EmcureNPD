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
