GO

INSERT INTO [dbo].[Master_PIDFStatus]
           ([PIDFStatusID]
           ,[PIDFStatus]
           ,[IsActive]
           ,[Createdby]
           ,[CreatedDate]
           ,[ModifyBy]
           ,[ModifyDate]
           ,[StatusColor]
           ,[IsDashboard]
           ,[ModuleId]
           ,[StatusTextColor])
     VALUES
           (24
           ,'PBF Rejected'
           ,1
           ,1
           ,GETDATE()
           ,null
           ,null
           ,'#ffc9c7'
           ,1
           ,6
           ,'#ff0000')
GO


update  [dbo].[RoleModulePermission] 
set Approve=1
where RoleId=1 and ModuleId=6 