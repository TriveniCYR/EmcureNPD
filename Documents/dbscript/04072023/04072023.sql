
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