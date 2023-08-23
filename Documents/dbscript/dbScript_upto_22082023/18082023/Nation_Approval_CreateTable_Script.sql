USE [EmcureNPDDev]
GO
/****** Object:  Table [dbo].[Master_NationApproval]    Script Date: 8/17/2023 6:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_NationApproval](
	[NationApprovalId] [int] IDENTITY(1,1) NOT NULL,
	[NationApprovalName] [varchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NationApprovalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Master_NationApproval] ON 

INSERT [dbo].[Master_NationApproval] ([NationApprovalId], [NationApprovalName], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'Test', CAST(N'2023-08-16T12:24:27.760' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Master_NationApproval] ([NationApprovalId], [NationApprovalName], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'Test2', CAST(N'2023-08-16T12:24:38.503' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Master_NationApproval] OFF
GO
ALTER TABLE [dbo].[Master_NationApproval] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
