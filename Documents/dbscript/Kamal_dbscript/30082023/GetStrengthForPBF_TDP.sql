USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[GetStrengthForPBF_TDP]    Script Date: 30-08-2023 16:22:57 ******/
DROP PROCEDURE [dbo].[GetStrengthForPBF_TDP]
GO

/****** Object:  StoredProcedure [dbo].[GetStrengthForPBF_TDP]    Script Date: 30-08-2023 16:22:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[dbo].[GetStrengthForPBF_TDP] 181
CREATE Proc [dbo].[GetStrengthForPBF_TDP]
@PIDFID bigint
As
BEGIN
select A.Strength,a.PIDFProductStrengthId,B.UnitofMeasurementName from PIDFProductStrength as A
  inner join Master_UnitofMeasurement as B on a.UnitofMeasurementId=b.UnitofMeasurementId
  inner join PIDF_Commercial As C on c.PIDFProductStrengthId=a.PIDFProductStrengthId
  where A.PIDFID=@PIDFID and c.IsDeleted=0 --and b.IsActive=0 
END
GO


