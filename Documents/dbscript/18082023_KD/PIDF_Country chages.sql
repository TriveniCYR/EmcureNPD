

alter table PIDF_Commercial add CountryId int
alter table PIDF_Commercial_Master add CountryId int
go

alter table PIDFProductStrength	add BusinessUnitId int 

ALTER TABLE [dbo].[PIDFProductStrength]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].Master_BusinessUnit ([BusinessUnitId])


ALTER TABLE [dbo].[PIDFProductStrength] CHECK CONSTRAINT [FK_PIDFProductStrength_Master_BussinessUnit]
---------

alter table PIDFIMSData	add BusinessUnitId int 

ALTER TABLE [dbo].PIDFIMSData  WITH CHECK ADD  CONSTRAINT [FK_PIDFIMSData_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].Master_BusinessUnit ([BusinessUnitId])


ALTER TABLE [dbo].PIDFIMSData CHECK CONSTRAINT [FK_PIDFIMSData_Master_BussinessUnit]
----------------------
alter table PIDFAPIDetails add BusinessUnitId int 

ALTER TABLE [dbo].PIDFAPIDetails  WITH CHECK ADD  CONSTRAINT [FK_PIDFAPIDetails_Master_BussinessUnit] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].Master_BusinessUnit ([BusinessUnitId])


ALTER TABLE [dbo].PIDFAPIDetails CHECK CONSTRAINT [FK_PIDFAPIDetails_Master_BussinessUnit]


---------------------------------------------------------------------------------------------------

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PIDFProductStrength_CountryMapping]') AND type in (N'U'))
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] DROP CONSTRAINT IF EXISTS [FK_PIDFProductStrength_CountryMapping_Master_Country]
GO
/****** Object:  Table [dbo].[PIDFProductStrength_CountryMapping]    Script Date: 19-08-2023 15:27:45 ******/
DROP TABLE IF EXISTS [dbo].[PIDFProductStrength_CountryMapping]
GO
/****** Object:  Table [dbo].[PIDFProductStrength_CountryMapping]    Script Date: 19-08-2023 15:27:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PIDFProductStrength_CountryMapping](
	[PIDFProductStrengthCountryId] [bigint] IDENTITY(1,1) NOT NULL,
	[PIDFProductStrengthId] [bigint] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyBy] [int] NOT NULL,
 CONSTRAINT [PK_PIDFProductStrength_CountryMapping] PRIMARY KEY CLUSTERED 
(
	[PIDFProductStrengthCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_CountryMapping_Master_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Master_Country] ([CountryID])
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] CHECK CONSTRAINT [FK_PIDFProductStrength_CountryMapping_Master_Country]
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength] FOREIGN KEY([PIDFProductStrengthId])
REFERENCES [dbo].[PIDFProductStrength] ([PIDFProductStrengthId])
GO
ALTER TABLE [dbo].[PIDFProductStrength_CountryMapping] CHECK CONSTRAINT [FK_PIDFProductStrength_CountryMapping_PIDFProductStrength]
GO





