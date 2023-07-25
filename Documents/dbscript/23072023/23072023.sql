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

go


GO
/****** Object:  StoredProcedure [dbo].[AddUpdatePIDF_Pbf_ra]    Script Date: 24-07-2023 18:03:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddUpdatePIDF_Pbf_ra] (
	@table Type_PIDF_PBF_RA Readonly
	,@Success NVARCHAR(12) = '' OUTPUT
	)
AS
BEGIN
	BEGIN TRAN

	BEGIN TRY
		MERGE PIDF_PBF_RA AS dbPIDF_PBF_RA
		USING @table AS typetable
			ON (
					dbPIDF_PBF_RA.PIDFPBFRAId = typetable.PIDFPBFRAId
					AND typetable.PIDFPBFRAId > 0 
					)
		WHEN MATCHED 
			THEN
				UPDATE
				  SET PIDFId = typetable.PIDFId,
				      PBFId = typetable.PBFId,
					  CountryIdBuId = typetable.CountryIdBuId,
					  PivotalBatchManufactured = typetable.PivotalBatchManufactured,
					  LastDataFromRnD = typetable.LastDataFromRnD,
					  BEFinalReport = typetable.BEFinalReport,
					  CountryId = typetable.CountryId,
					  TypeOfSubmissionId = typetable.TypeOfSubmissionId,
					  DossierReadyDate = typetable.DossierReadyDate,
					  EarliestSubmissionDExcl = typetable.EarliestSubmissionDExcl,
					  EarliestLaunchDExcl = typetable.EarliestLaunchDExcl,
					  LasDateToRegulatory = typetable.LasDateToRegulatory,
					  UpdatedOn = typetable.UpdatedOn,
					  CreatedBy=typetable.CreatedBy
					 
		WHEN NOT MATCHED 
			THEN
				INSERT (
					PIDFId,
                    PBFId,
                    CountryIdBuId,
                    PivotalBatchManufactured,
                    LastDataFromRnD,
                    BEFinalReport,
                    CountryId,
                    TypeOfSubmissionId,
                    DossierReadyDate,
                    EarliestSubmissionDExcl,
                    EarliestLaunchDExcl,
					LasDateToRegulatory,
                    CreatedOn,
                    CreatedBy
					)
				VALUES (
					typetable.PIDFId,
                    typetable.PBFId,
                    typetable.CountryIdBuId,
                    typetable.PivotalBatchManufactured,
                    typetable.LastDataFromRnD,
                    typetable.BEFinalReport,
                    typetable.CountryId,
                    typetable.TypeOfSubmissionId,
                    typetable.DossierReadyDate,
                    typetable.EarliestSubmissionDExcl,
                    typetable.EarliestLaunchDExcl,
					typetable.LasDateToRegulatory,
                    typetable.CreatedOn,
                    typetable.CreatedBy
					);

		SELECT @Success = 'success';

		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

------------------------
GO

/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 24-07-2023 18:04:01 ******/
DROP TYPE [dbo].[Type_PIDF_PBF_RA]
GO

/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 24-07-2023 18:04:01 ******/
CREATE TYPE [dbo].[Type_PIDF_PBF_RA] AS TABLE(
	[PIDFPBFRAId] [bigint] NULL,
	[PIDFId] [bigint] NULL,
	[PBFId] [bigint] NULL,
	[CountryIdBuId] [int] NULL,
	[PivotalBatchManufactured] [datetime] NULL,
	[LastDataFromRnD] [datetime] NULL,
	[BEFinalReport] [datetime] NULL,
	[CountryId] [int] NULL,
	[TypeOfSubmissionId] [int] NULL,
	[DossierReadyDate] [datetime] NULL,
	[EarliestSubmissionDExcl] [datetime] NULL,
	[EarliestLaunchDExcl] [datetime] NULL,
	[LasDateToRegulatory] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL
)
GO

INSERT INTO [dbo].[Master_TypeOfSubmission]([TypeOfSubmission])VALUES('DCP')
INSERT INTO [dbo].[Master_TypeOfSubmission]([TypeOfSubmission])VALUES('CP')
INSERT INTO [dbo].[Master_TypeOfSubmission]([TypeOfSubmission])VALUES('RUP')
INSERT INTO [dbo].[Master_TypeOfSubmission]([TypeOfSubmission])VALUES('National')