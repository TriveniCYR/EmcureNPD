 --  exec GetUserForReminder          
alter PROCEDURE [dbo].[GetUserForReminder]             
AS            
BEGIN            
          
select DISTINCT p.PIDFNO,p.MoleculeName,p.StatusUpdatedDate As IPDApprovedDate ,u.FullName,u.EmailAddress, B.StatusId      
from  PIDF p        
Inner Join PIDFStatusHistory As B On P.PIDFID = B.PIDFID And B.StatusId = 7             
inner join Master_UserBusinessUnitMapping UserBUMap on UserBUMap.BusinessUnitId = p.BusinessUnitId          
inner join Master_User u on u.UserId = UserBUMap.UserId             
where  dateadd(DD, 90, p.StatusUpdatedDate) > getdate()        
And p.PIDFId NOT IN (Select PIDFId from PIDFStatusHistory Where StatusId In (11,18))      
--and   p.StatusId = 7 --[IPD Approved = 7]          
--and   p.IsActive =1          
and u.UserId in (          
select u.UserId from Master_User u           
inner join RoleModulePermission rolepermission on rolepermission.RoleId = u.RoleId          
where rolepermission.ModuleId = 15 --[15 = Commercial Management]          
and (rolepermission.[Edit] =1 or  rolepermission.[Add] =1)          
)      
and u.IsActive = 1 And u.IsDeleted = 0       
And p.StatusId NOt IN (4, 8)    
and UserBUMap.BusinessUnitId not in (select BusinessUnitId from PIDF_Commercial_Master where Interested = 0)  
order by IPDApprovedDate ASC          
          

-------------- details for PIDF Submitted scheduler-------------------------------
--------Table1
select PIDFNO,PIDFID,MoleculeName,BrandName,BU.BusinessUnitName,dos.DosageFormName from PIDF P 
inner join Master_BusinessUnit BU on BU.BusinessUnitId=P.BusinessUnitId
inner join Master_DosageForm dos on dos.DosageFormId = P.DosageFormId
where StatusId =2

--------Table2
select PIDFID,Strength from PIDFProductStrength where PIDFID in (select PIDFID from PIDF where StatusId =2)

--------Table3
select PIDFID,IMSValue,IMSVolume from PIDFIMSData where PIDFID in (select PIDFID from PIDF where StatusId =2)

--------Table4
Select top 2 UserId,FullName,EmailAddress from Master_User u                   
where  u.UserId in (          
select u.UserId from Master_User u           
inner join RoleModulePermission rolepermission on rolepermission.RoleId = u.RoleId          
where rolepermission.ModuleId = 5 --[5 = PIDF -Project Identification Form]          
and (rolepermission.[Edit] =1)          
)    
-----------------------------------------------

END 