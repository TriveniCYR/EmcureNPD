alter table PIDF_IPD_PatentDetails 
Add CountryId int
,PatentStrategy int
,PatentStrategyOther nvarchar(100)
, BasicPatentExpiry date
, OtherLmitingPatentDate1 date
, OtherLmitingPatentDate2 date
, EarliestLaunchDate date
, AnyPatentstobeFiled bit
, EarliestMarketEntry date
, stimatedNumberofgenericsinthe nvarchar(100)
, Lawfirmbeingused nvarchar(100)


GO

/****** Object:  Table [dbo].[Master_Country]    Script Date: 07-07-2023 10:37:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Master_Patent_Strategy](
	[PatentStrategyID] [int] IDENTITY(1,1) NOT NULL,
	[PatentStrategyName] [nvarchar](100) NULL,
	
 CONSTRAINT [PK_Master_Patent_Strategy] PRIMARY KEY CLUSTERED 
(
	[PatentStrategyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


