USE [EmcureNPDDev]
GO

ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT [DF__pbf_gener__Creat__3D54C988]
GO

ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT [DF__pbf_gener__IsEmc__3C60A54F]
GO

ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT [DF__pbf_gener__IsSec__3B6C8116]
GO

ALTER TABLE [dbo].[pbf_general_TDP] DROP CONSTRAINT [DF__pbf_gener__IsPri__3A785CDD]
GO

/****** Object:  Table [dbo].[pbf_general_TDP]    Script Date: 29-08-2023 11:07:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pbf_general_TDP]') AND type in (N'U'))
DROP TABLE [dbo].[pbf_general_TDP]
GO

/****** Object:  Table [dbo].[pbf_general_TDP]    Script Date: 29-08-2023 11:07:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pbf_general_TDP](
	[TradeDressProposalId] [bigint] IDENTITY(1,1) NOT NULL,
	[Approch] [varchar](max) NULL,
	[PIDFID] [bigint] NULL,
	[PbfId] [bigint] NULL,
	[PIDFPbfGeneralId] [bigint] NULL,
	[PIDFProductStrngthId] [bigint] NULL,
	[Description] [varchar](max) NULL,
	[Shape] [varchar](200) NULL,
	[Color] [varchar](20) NULL,
	[Engraving] [varchar](200) NULL,
	[Packaging] [varchar](max) NULL,
	[IsPrimaryPackaging] [bit] NULL,
	[IsSecondryPackaging] [bit] NULL,
	[Shelf_Life] [varchar](200) NULL,
	[Storage_Handling] [varchar](200) NULL,
	[IsEmcure] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[FormulaterResponsiblePerson] [nvarchar](100) NULL,
 CONSTRAINT [PK__pbf_gene__26222D7F3860C311] PRIMARY KEY CLUSTERED 
(
	[TradeDressProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsPri__3A785CDD]  DEFAULT ((0)) FOR [IsPrimaryPackaging]
GO

ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsSec__3B6C8116]  DEFAULT ((0)) FOR [IsSecondryPackaging]
GO

ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__IsEmc__3C60A54F]  DEFAULT ((0)) FOR [IsEmcure]
GO

ALTER TABLE [dbo].[pbf_general_TDP] ADD  CONSTRAINT [DF__pbf_gener__Creat__3D54C988]  DEFAULT (getdate()) FOR [CreatedDate]
GO


