
GO

/****** Object:  Table [dbo].[PIDF_IPD]    Script Date: 18-09-2023 10:28:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  TABLE [dbo].[PIDF_IPD_General](
	PIDF_IPD_General_Id [bigint] IDENTITY(1,1) NOT NULL,
	[IPDID] [bigint] NOT NULL,
	[BusinessUnitId] [int] NULL,
	[CountryId] [int] NULL,
	[MarketName] [nvarchar](200) NULL,
	[DataExclusivity] [nvarchar](200) NULL,
	MarketExclusivityDate datetime NULL,
	ExpectedFilingDate datetime NULL,
	ExpectedLaunchDate datetime NULL,
	[ApprovedGenetics] [nvarchar](100) NULL,
	[LaunchedGenetics] [nvarchar](100) NULL,
	[LegalStatus] [nvarchar](100) NULL,
	[CostOfLitication] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsComment] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	
 CONSTRAINT [PK__PIDF_IPD_General_Id] PRIMARY KEY CLUSTERED 
(
	[PIDF_IPD_General_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PIDF_IPD_General]  WITH CHECK ADD  CONSTRAINT [FK_PIDF_IPD_General_IPDID] FOREIGN KEY(IPDID)
REFERENCES [dbo].[PIDF_IPD] (IPDID)
GO

ALTER TABLE [dbo].[PIDF_IPD_General] CHECK CONSTRAINT [FK_PIDF_IPD_General_IPDID]
GO
--------------------------

alter table [PIDF_IPD_PatentDetails] add PIDF_IPD_General_Id [bigint]   NULL

go



