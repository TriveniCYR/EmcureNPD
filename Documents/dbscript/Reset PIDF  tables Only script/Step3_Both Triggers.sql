----------Triggers--------------------------------------------------------------------------
    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE TRIGGER [dbo].[tgPIDFManagementApprovalStatus]    
   ON  [dbo].[PIDF]    
   AFTER INSERT,UPDATE    
AS     
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
    -- Insert statements for trigger here    
 Insert Into [dbo].PIDF_ManagementApprovalStatusHistory(StatusId, PIDFId, CreatedDate, CreatedBy,Remark)    
 SELECT    
        i.StatusId,    
        i.PIDFID,    
        GETDATE(),    
  --IsNull(StatusUpdatedDate, GETDATE()),    
        IsNull(StatusUpdatedBy, i.CreatedBy),    
  isnull(i.StatusRemark,'')    
    FROM    
        inserted i    
  Left Join [dbo].PIDF_ManagementApprovalStatusHistory As B On i.PIDFId = B.PIDFId And i.StatusID = B.StatusId And B.CreatedBy = i.StatusUpdatedBy    
  Where B.StatusID Is Null and ( i.StatusId =20 or i.StatusId =21)    
    
  IF (UPDATE(StatusId) OR UPDATE(StatusUpdatedBy))    
  Begin    
  UPDATE B    
   SET B.CreatedDate = GETDATE(), --IsNull(i.StatusUpdatedDate, GETDATE()),      
   B.CreatedBy = i.StatusUpdatedBy,    
   B.Remark=Isnull(i.StatusRemark,'')    
   FROM inserted i      
   INNER JOIN PIDF_ManagementApprovalStatusHistory As B ON i.PIDFId = B.PIDFId And i.StatusID = B.StatusId And B.CreatedBy = i.StatusUpdatedBy    
   Where ( i.StatusId =20 or i.StatusId =21)    
  End    
    
  Declare @TotalManagementUser int = (Select Count(*) from Master_User Where IsActive = 1 And IsDeleted = 0 And IsNull(IsManagement, 0) = 1)    
    
  Select PIDFID into #tmpFinalPIDF from (Select Count(*) as TotalUserApproved, i.PIDFID FROM inserted i      
  INNER JOIN PIDF_ManagementApprovalStatusHistory As B ON i.PIDFId = B.PIDFId And B.StatusId = 20    
  Group By i.PIDFID) As A Where A.TotalUserApproved >= @TotalManagementUser    
    
  Update PIDF    
  Set StatusId = 22, StatusUpdatedDate = GETDATE()    
  Where PIDFId In (Select PIDFID from #tmpFinalPIDF)    
    
END    
-------------------------------------------
    
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE TRIGGER [dbo].[tgPIDFStatus]    
   ON  [dbo].[PIDF]    
   AFTER INSERT,UPDATE    
AS     
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
    -- Insert statements for trigger here    
 Insert Into PIDFStatusHistory(StatusId, PIDFID, CreatedDate, CreatedBy,StatusRemark)    
 SELECT    
        i.StatusId,    
        i.PIDFID,    
        GETDATE(),    
  --IsNull(StatusUpdatedDate, GETDATE()),    
        IsNull(StatusUpdatedBy, i.CreatedBy),    
  (Case When i.StatusId IN (3, 4, 7, 8, 18, 19, 20, 21) Then isnull(i.StatusRemark,'') Else '' End)    
    FROM    
        inserted i    
  Left Join PIDFStatusHistory As B On i.PIDFId = B.PIDFId And i.StatusID = B.StatusId    
  Where B.StatusID Is Null And i.StatusId NOt In (20, 21)    
    
  IF (UPDATE(StatusId) OR UPDATE(StatusUpdatedBy))    
  Begin    
  UPDATE B    
   SET B.CreatedDate = GETDATE(), --IsNull(i.StatusUpdatedDate, GETDATE()),      
   B.CreatedBy = i.StatusUpdatedBy,    
   B.StatusRemark=(Case When i.StatusId IN (3, 4, 7, 8, 18, 19, 20, 21) Then isnull(i.StatusRemark,'') Else '' End)    
   FROM inserted i      
   INNER JOIN PIDFStatusHistory As B ON i.PIDFId = B.PIDFId And i.StatusID = B.StatusId    
   Where i.StatusId NOt In (20, 21)    
  End    
END 