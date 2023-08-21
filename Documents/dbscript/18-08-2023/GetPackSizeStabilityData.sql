USE [EmcureNPDDev]
GO
/****** Object:  StoredProcedure [dbo].[GetPackSizeStabilityData]    Script Date: 18-08-2023 17:40:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[GetPackSizeStabilityData] 128,5
ALTER Proc [dbo].[GetPackSizeStabilityData]  
(  
 @PIDFId bigint,  
 @BUId int  
 )  
 AS   
 BEGIN  

Select PIDFProductStrengthId,Strength,a.UnitofMeasurementId,B.UnitofMeasurementName from PIDFProductStrength As A  
Inner Join [Master_UnitofMeasurement] As B  on a.UnitofMeasurementId=b.UnitofMeasurementId  
  Where PIDFId = @PIDFId   
order by PIDFProductStrengthId desc  
  
Select    
ps.PackSizeId,ps.PackSizeName,PIDFProductStrengthId
,Isnull(C.Value,'')Value,Isnull(c.PackSizeStabilityId,0) PackSizeStabilityId 
from PIDF_Commercial as D  
inner join master_packsize as ps on ps.PackSizeId=d.PackSizeId and ps.IsActive=1  
left join PIDF_PBF_RnD_PackSizeStability As C on c.PIDFID=D.PIDFId and d.PackSizeId=c.PackSizeId    
Where D.PIDFId = @PIDFId and d.BusinessUnitId=@BUId  and d.IsDeleted=0
order by PIDFProductStrengthId desc  

END  