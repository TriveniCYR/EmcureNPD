USE [EmcureNPDDev]
GO


/****** Object:  Table [emcurenpddev_dbUser].[Master_EmailLog]    Script Date: 18-08-2023 13:06:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[emcurenpddev_dbUser].[Master_EmailLog]') AND type in (N'U'))
DROP TABLE [emcurenpddev_dbUser].[Master_EmailLog]
GO

/****** Object:  Table [emcurenpddev_dbUser].[Master_EmailLog]    Script Date: 18-08-2023 13:06:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Master_EmailLog](
	[EmailLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ToEmailAddress] [nvarchar](1000) NULL,
	[Subject] [nvarchar](1000) NULL,
	[SentSuccessfully] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Master_EmailLog] ADD  DEFAULT ((0)) FOR [SentSuccessfully]
GO

ALTER TABLE [dbo].[Master_EmailLog] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


