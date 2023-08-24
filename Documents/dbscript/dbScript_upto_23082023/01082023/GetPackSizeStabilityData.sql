

/****** Object:  StoredProcedure [dbo].[GetPackSizeStabilityData]    Script Date: 01-08-2023 15:35:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[dbo].[GetPackSizeStabilityData] 147,5--1,
CREATE OR ALTER Proc [dbo].[GetPackSizeStabilityData]
(
 @PIDFId bigint,
 @BUId int
 )
 AS 
 BEGIN
Select PIDFProductStrengthId,Strength,a.UnitofMeasurementId from PIDFProductStrength As A
Inner Join [Master_UnitofMeasurement] As B  on a.UnitofMeasurementId=b.UnitofMeasurementId

Where PIDFId = @PIDFId 

Select D.PackSizeId, PIDFProductStrengthId,Isnull(C.Value,'')Value,Isnull(c.PackSizeStabilityId,0) PackSizeStabilityId from PIDF_Commercial as D
left join PIDF_PBF_RnD_PackSizeStability As C on c.PIDFID=D.PIDFId and d.PackSizeId=c.PackSizeId  
Where D.PIDFId = @PIDFId and d.BusinessUnitId=@BUId
END
GO


