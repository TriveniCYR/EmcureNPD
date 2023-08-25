-----------------run this section first---
alter table PIDF_PBF_RA
add EndOfProcedureDate datetime null
go
alter table PIDF_PBF_RA
add CountryApprovalDate datetime null
go
--------------------------------------
DROP PROCEDURE [dbo].[AddUpdatePIDF_Pbf_ra]
go
----------------------------------------
/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 25-08-2023 16:34:42 ******/
DROP TYPE [dbo].[Type_PIDF_PBF_RA]
GO

/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 25-08-2023 16:34:42 ******/
CREATE TYPE [dbo].[Type_PIDF_PBF_RA] AS TABLE(
	[PIDFPBFRAId] [bigint] NULL,
	[PIDFId] [bigint] NULL,
	[PBFId] [bigint] NULL,
	[CountryIdBuId] [int] NULL,
	[PivotalBatchManufactured] [datetime] NULL,
	[LastDataFromRnD] [datetime] NULL,
	[BEFinalReport] [datetime] NULL,
	[BuId] [int] NULL,
	[TypeOfSubmissionId] [int] NULL,
	[DossierReadyDate] [datetime] NULL,
	[EarliestSubmissionDExcl] [datetime] NULL,
	[EarliestLaunchDExcl] [datetime] NULL,
	[LasDateToRegulatory] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	EndOfProcedureDate [datetime] null,
	CountryApprovalDate [datetime] null
)
GO

---------------------ra sp-------------------------
/****** Object:  StoredProcedure [dbo].[AddUpdatePIDF_Pbf_ra]    Script Date: 25-08-2023 16:43:13 ******/
DROP PROCEDURE [dbo].[AddUpdatePIDF_Pbf_ra]
GO

/****** Object:  StoredProcedure [dbo].[AddUpdatePIDF_Pbf_ra]    Script Date: 25-08-2023 16:43:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





Create PROCEDURE [dbo].[AddUpdatePIDF_Pbf_ra] (
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
					  BuId = typetable.BuId,
					  TypeOfSubmissionId = typetable.TypeOfSubmissionId,
					  DossierReadyDate = typetable.DossierReadyDate,
					  EarliestSubmissionDExcl = typetable.EarliestSubmissionDExcl,
					  EarliestLaunchDExcl = typetable.EarliestLaunchDExcl,
					  LasDateToRegulatory = typetable.LasDateToRegulatory,
					  UpdatedOn = typetable.UpdatedOn,
					  CreatedBy=typetable.CreatedBy,
					  EndOfProcedureDate=typetable.EndOfProcedureDate,
					  CountryApprovalDate=typetable.CountryApprovalDate
					 
		WHEN NOT MATCHED 
			THEN
				INSERT (
					PIDFId,
                    PBFId,
                    CountryIdBuId,
                    PivotalBatchManufactured,
                    LastDataFromRnD,
                    BEFinalReport,
                    BuId,
                    TypeOfSubmissionId,
                    DossierReadyDate,
                    EarliestSubmissionDExcl,
                    EarliestLaunchDExcl,
					LasDateToRegulatory,
                    CreatedOn,
                    CreatedBy,
					EndOfProcedureDate,
					CountryApprovalDate
					)
				VALUES (
					typetable.PIDFId,
                    typetable.PBFId,
                    typetable.CountryIdBuId,
                    typetable.PivotalBatchManufactured,
                    typetable.LastDataFromRnD,
                    typetable.BEFinalReport,
                    typetable.BuId,
                    typetable.TypeOfSubmissionId,
                    typetable.DossierReadyDate,
                    typetable.EarliestSubmissionDExcl,
                    typetable.EarliestLaunchDExcl,
					typetable.LasDateToRegulatory,
                    typetable.CreatedOn,
                    typetable.CreatedBy,
					typetable.EndOfProcedureDate,
					typetable.CountryApprovalDate
					);

		SELECT @Success = 'success';

		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END



GO

