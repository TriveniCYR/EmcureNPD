USE [EmcureNPDDev]
GO
/****** Object:  UserDefinedTableType [dbo].[Type_PIDF_PBF_RA]    Script Date: 20-07-2023 15:03:11 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'Type_PIDF_PBF_RA' AND ss.name = N'dbo')
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
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL
)
GO
/****** Object:  Table [dbo].[Master_TypeOfSubmission]    Script Date: 20-07-2023 15:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_TypeOfSubmission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Master_TypeOfSubmission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfSubmission] [nvarchar](20) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__Master_T__3214EC0780AA3A6E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PIDF_PBF_RA]    Script Date: 20-07-2023 15:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PIDF_PBF_RA](
	[PIDFPBFRAId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFId] [bigint] NOT NULL,
	[PBFId] [bigint] NOT NULL,
	[CountryIdBuId] [int] NOT NULL,
	[PivotalBatchManufactured] [datetime] NULL,
	[LastDataFromRnD] [datetime] NULL,
	[BEFinalReport] [datetime] NULL,
	[CountryId] [int] NOT NULL,
	[TypeOfSubmissionId] [int] NULL,
	[DossierReadyDate] [datetime] NULL,
	[EarliestSubmissionDExcl] [datetime] NULL,
	[EarliestLaunchDExcl] [datetime] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_PIDF_PBF_RA] PRIMARY KEY CLUSTERED 
(
	[PIDFPBFRAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Master_TypeOfSubmission_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Master_TypeOfSubmission] ADD  CONSTRAINT [DF_Master_TypeOfSubmission_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_PIDF_PBF_RA_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PIDF_PBF_RA] ADD  CONSTRAINT [DF_PIDF_PBF_RA_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PIDF_PBF_RA_PIDF]') AND parent_object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]'))
ALTER TABLE [dbo].[PIDF_PBF_RA]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RA_PIDF] FOREIGN KEY([PIDFId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PIDF_PBF_RA_PIDF]') AND parent_object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]'))
ALTER TABLE [dbo].[PIDF_PBF_RA] CHECK CONSTRAINT [FK_PIDF_PBF_RA_PIDF]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PIDF_PBF_RA_PIDF_PBF]') AND parent_object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]'))
ALTER TABLE [dbo].[PIDF_PBF_RA]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RA_PIDF_PBF] FOREIGN KEY([PBFId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PIDF_PBF_RA_PIDF_PBF]') AND parent_object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RA]'))
ALTER TABLE [dbo].[PIDF_PBF_RA] CHECK CONSTRAINT [FK_PIDF_PBF_RA_PIDF_PBF]
GO
/****** Object:  StoredProcedure [dbo].[AddUpdatePIDF_Pbf_ra]    Script Date: 20-07-2023 15:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUpdatePIDF_Pbf_ra]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddUpdatePIDF_Pbf_ra] AS' 
END
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
					  UpdatedOn = typetable.CreatedOn,
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

GO
