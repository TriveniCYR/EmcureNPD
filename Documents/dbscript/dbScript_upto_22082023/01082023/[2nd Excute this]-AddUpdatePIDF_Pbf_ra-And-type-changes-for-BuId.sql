
Drop Proc AddUpdatePIDF_Pbf_ra
GO
/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 01-08-2023 13:47:05 ******/
DROP TYPE [dbo].[Type_PIDF_PBF_RA]
GO

/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 01-08-2023 13:47:05 ******/
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
	[CreatedBy] [int] NULL
)
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
                    BuId,
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
                    typetable.BuId,
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



