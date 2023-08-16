USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[GetEmailNotification]    Script Date: 16-08-2023 15:36:29 ******/
DROP PROCEDURE [dbo].[GetEmailNotification]
GO

/****** Object:  StoredProcedure [dbo].[GetEmailNotification]    Script Date: 16-08-2023 15:36:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE Proc [dbo].[GetEmailNotification]
As 
Begin
  

Select DISTINCT 
A.UserId, NotificationId, F.PIDFID, A.FullName as SendToName, A.EmailAddress, D.PIDFStatus, F.PIDFNO as PidfNo,
G.FullName as CreatedByName, G.CreatedDate  
from [Master_User] As A
Inner Join [Master_Role] As B On A.RoleId = B.RoleId
Inner Join RoleModulePermission As C On B.RoleId = C.RoleId And (C.[Add] = 1 Or C.Edit = 1 Or C.Approve = 1)
And ModuleId In (5, 6, 7, 8, 9, 13, 15, 16)
Inner Join Master_PIDFStatus As D On D.ModuleId = C.ModuleId
Inner Join Master_Notification As E On E.StatusId = D.PIDFStatusId
Inner Join [PIDF] As F On F.PIDFId = E.PIDFId
Inner Join [Master_User] As G On G.UserId = E.CreatedBy --And G.IsActive = 1 And G.IsDeleted = 0
Where A.isActive = 1 And A.IsDeleted = 0 And E.IsEmailSent = 0
End
GO


