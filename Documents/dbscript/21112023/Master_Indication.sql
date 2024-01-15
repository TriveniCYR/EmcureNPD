USE [EmcureNPDDev]
GO

/****** Object:  Table [dbo].[[Master_Indication]]    Script Date: 21-11-2023 14:50:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Master_Indication]') AND type in (N'U'))
DROP TABLE [dbo].[Master_Indication]
GO

/****** Object:  Table [dbo].[[Master_Indication]]    Script Date: 21-11-2023 14:50:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Master_Indication](
	[IndicationId] [int] IDENTITY(1,1) NOT NULL,
	[IndicationName] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_Master_Indication] PRIMARY KEY CLUSTERED 
(
	[IndicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


alter table PIDF_BusinessUnit add IndicationId int null
alter table PIDF add IndicationId int null


