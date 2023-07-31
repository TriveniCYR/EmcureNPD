
--[dbo].[GetPackSizeStabilityData] 147--1,
Create Proc [dbo].[GetPackSizeStabilityData]
(
 @PIDFId bigint 
 )
 AS 
 BEGIN
Select PIDFProductStrengthId,Strength,a.UnitofMeasurementId from PIDFProductStrength As A
Inner Join [Master_UnitofMeasurement] As B  on a.UnitofMeasurementId=b.UnitofMeasurementId

Where PIDFId = @PIDFId

Select D.PackSizeId, PIDFProductStrengthId,Isnull(C.Value,'')Value,Isnull(c.PackSizeStabilityId,0) PackSizeStabilityId from PIDF_Commercial as D
left join PIDF_PBF_RnD_PackSizeStability As C on c.PIDFID=D.PIDFId and d.PackSizeId=c.PackSizeId  
Where D.PIDFId = @PIDFId
END
