USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [emcurenpddev_dbUser].[GetStrengthForPBF_TDP]    Script Date: 29-08-2023 19:23:58 ******/
DROP PROCEDURE [emcurenpddev_dbUser].[GetStrengthForPBF_TDP]
GO

/****** Object:  StoredProcedure [emcurenpddev_dbUser].[GetStrengthForPBF_TDP]    Script Date: 29-08-2023 19:23:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--[dbo].[GetStrengthForPBF_TDP] 181
Create Proc [dbo].[GetStrengthForPBF_TDP]
@PIDFID bigint
As
BEGIN
select A.Strength,B.UnitofMeasurementName from PIDFProductStrength as A
  inner join Master_UnitofMeasurement as B on a.UnitofMeasurementId=b.UnitofMeasurementId
  inner join PIDF_Commercial As C on c.PIDFProductStrengthId=a.PIDFProductStrengthId
  where A.PIDFID=@PIDFID and c.IsDeleted=0 --and b.IsActive=0 
END
GO


