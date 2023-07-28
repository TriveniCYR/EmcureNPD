USE [EmcureNPDDev]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] DROP CONSTRAINT [DF_PIDF_PBF_RnD_PackSizeStability_CreatedOn]
GO

/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackSizeStability]    Script Date: 28-07-2023 19:38:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDF_PBF_RnD_PackSizeStability]') AND type in (N'U'))
DROP TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]
GO

/****** Object:  Table [dbo].[PIDF_PBF_RnD_PackSizeStability]    Script Date: 28-07-2023 19:38:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability](
	[PackSizeStabilityId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFID] [bigint] NOT NULL,
	[PBFGeneralId] [bigint] NOT NULL,
	[StrengthId] [int] NULL,
	[PackSizeId] [int] NULL,
	[Value] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK__PIDF_PBF__5C057E65FAB7B4EF] PRIMARY KEY CLUSTERED 
(
	[PackSizeStabilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] ADD  CONSTRAINT [DF_PIDF_PBF_RnD_PackSizeStability_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF] FOREIGN KEY([PIDFID])
REFERENCES [dbo].[PIDF] ([PIDFID])
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF]
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General] FOREIGN KEY([PBFGeneralId])
REFERENCES [dbo].[PIDF_PBF_General] ([PBFGeneralId])
GO

ALTER TABLE [dbo].[PIDF_PBF_RnD_PackSizeStability] CHECK CONSTRAINT [FK_PIDF_PBF_RnD_PackSizeStability_PIDF_PBF_General]
GO


