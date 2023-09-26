
GO
/****** Object:  StoredProcedure [dbo].[GetStrengthForPBF_TDPTest]    Script Date: 9/24/2023 1:19:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[GetStrengthForPBF_TDP] 181
CREATE OR ALTER   Proc [dbo].[GetStrengthForPBF_TDP]
@PIDFID bigint,
@BUId bigint = 0     
As
BEGIN
select distinct(A.Strength),a.PIDFProductStrengthId,B.UnitofMeasurementName from PIDFProductStrength as A
  inner join Master_UnitofMeasurement as B on a.UnitofMeasurementId=b.UnitofMeasurementId
  inner join PIDF As C on c.UnitofMeasurementId=a.UnitofMeasurementId
  where A.PIDFID=@PIDFID and A.BusinessUnitId=@BUId --and b.IsActive=0 
  order by (A.Strength)

END





ALTER TABLE [dbo].[pbf_general_TDP]
ADD PrimaryPackaging varchar(255);

ALTER TABLE [dbo].[pbf_general_TDP]
ADD SecondryPackaging varchar(255);