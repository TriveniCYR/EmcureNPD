

/****** Object:  StoredProcedure [dbo].[GetPackSizeStabilityData]    Script Date: 02-08-2023 18:20:33 ******/
DROP PROCEDURE [dbo].[GetPackSizeStabilityData]
GO

/****** Object:  StoredProcedure [dbo].[GetPackSizeStabilityData]    Script Date: 02-08-2023 18:20:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[dbo].[GetPackSizeStabilityData] 1,1,
CREATE Proc [dbo].[GetPackSizeStabilityData]
(
 @PIDFId bigint,
 @BUId int
 )
 AS 
 BEGIN
Select PIDFProductStrengthId,Strength,a.UnitofMeasurementId,B.UnitofMeasurementName from PIDFProductStrength As A
Inner Join [Master_UnitofMeasurement] As B  on a.UnitofMeasurementId=b.UnitofMeasurementId

Where PIDFId = @PIDFId 

Select  
D.PackSizeId,ps.PackSizeName,PIDFProductStrengthId,Isnull(C.Value,'')Value,Isnull(c.PackSizeStabilityId,0) PackSizeStabilityId from PIDF_Commercial as D
left join PIDF_PBF_RnD_PackSizeStability As C on c.PIDFID=D.PIDFId and d.PackSizeId=c.PackSizeId  
inner join master_packsize as ps on ps.PackSizeId=d.PackSizeId
Where D.PIDFId = @PIDFId and d.BusinessUnitId=@BUId
END
GO


