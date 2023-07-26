
GO
/****** Object:  StoredProcedure [dbo].[SpGetPIDF_PBF_General_RND]    Script Date: 25-07-2023 18:06:37 ******/
DROP PROCEDURE IF EXISTS [dbo].[SpGetPIDF_PBF_General_RND]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_RND_PIDF_PBF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [FK_PIDF_PBF_General_RND_PIDF]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_General_RND]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDF_PBF_General_RND] DROP CONSTRAINT IF EXISTS [DF_PIDF_PBF_General_RND_CreatedOn]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_RND]    Script Date: 25-07-2023 18:06:37 ******/
DROP TABLE IF EXISTS [dbo].[PIDF_PBF_General_RND]
GO
/****** Object:  Table [dbo].[PIDF_PBF_General_RND]    Script Date: 25-07-2023 18:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDF_PBF_General_RND](
	[PbfRndDetailsId] [bigint] IDENTITY(1,1) NOT NULL,
	[PidfId] [bigint] NOT NULL,
	[PbfId] [bigint] NOT NULL,
	[RndResponsiblePerson] [nvarchar](100) NULL,
	[TypeOfDevelopmentDate] [datetime] NULL,
	[PivotalBatchesManufacturedCompleted] [datetime] NULL,
	[StabilityResultsDayZero] [datetime] NULL,
	[StabilityResultsThreeMonth] [datetime] NULL,
	[StabilityResultsSixMonth] [datetime] NULL,
	[NonStandardProduct] [bit] NULL,
	[Pivotals] [nvarchar](100) NULL,
	[BatchSizes] [nvarchar](100) NULL,
	[NoMOfBatchesPerStrength] [bigint] NULL,
	[SiteTransferDate] [datetime] NULL,
	[ApiOrderedDate] [datetime] NULL,
	[ApiReceivedDate] [datetime] NULL,
	[FinalFormulationApproved] [datetime] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[DeletedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__PIDF_PBF__5350EF231EB5A49E] PRIMARY KEY CLUSTERED 
(
	[PbfRndDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] ADD  CONSTRAINT [DF_PIDF_PBF_General_RND_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF] FOREIGN KEY([PidfId])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] CHECK CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF]
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF_PBF] FOREIGN KEY([PbfId])
REFERENCES [dbo].[PIDF_PBF] ([PIDFPBFId])
GO
ALTER TABLE [dbo].[PIDF_PBF_General_RND] CHECK CONSTRAINT [FK_PIDF_PBF_General_RND_PIDF_PBF]
GO
/****** Object:  StoredProcedure [dbo].[SpGetPIDF_PBF_General_RND]    Script Date: 25-07-2023 18:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SpGetPIDF_PBF_General_RND] --0,147,80
(
@PbfRndDetailsId bigint =0,
@PidfId bigint =0,
@PbfId bigint =0
)
AS BEGIN
SELECT  
       PbfRndDetailsId
      ,PidfId
      ,PbfId
      ,RndResponsiblePerson
      , TypeOfDevelopmentDate
      ,PivotalBatchesManufacturedCompleted
      ,StabilityResultsDayZero
      ,StabilityResultsThreeMonth
      ,StabilityResultsSixMonth
      ,NonStandardProduct
      ,Pivotals
      ,BatchSizes
      ,NoMOfBatchesPerStrength
      ,SiteTransferDate
      ,ApiOrderedDate
      ,ApiReceivedDate
      ,FinalFormulationApproved
      ,CreatedOn
      ,UpdatedOn
      ,DeletedOn
      ,CreatedBy
  FROM PIDF_PBF_General_RND
  where (PbfRndDetailsId=@PbfRndDetailsId or @PbfRndDetailsId=0)
        AND PidfId=@PidfId
        AND (PbfId=@PbfId or @PbfId=0)
END
GO
