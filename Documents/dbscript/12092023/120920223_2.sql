
GO
/****** Object:  StoredProcedure [dbo].[GetCountryWisePackSizeStabilityData]    Script Date: 9/11/2023 11:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[GetPackSizeStabilityData] 128,5  
CREATE OR ALTER   Proc [dbo].[GetCountryWisePackSizeStabilityData]    
(    
 @PIDFId bigint,    
 @BUId int ,
 @CountryId int  
 )    
 AS     
 BEGIN    
  
Select distinct(A.PIDFProductStrengthId),Strength,a.UnitofMeasurementId,B.UnitofMeasurementName,MC.CountryName
from PIDFProductStrength As A    
inner join PIDF_Commercial D on A.PIDFProductStrengthId=D.PIDFProductStrengthId
inner join Master_Country MC on D.CountryId=MC.CountryID
Inner Join [Master_UnitofMeasurement] As B  on a.UnitofMeasurementId=b.UnitofMeasurementId    
Where A.PIDFId = @PIDFId And A.BusinessUnitId = @BUId and D.CountryId=@CountryId
order by A.PIDFProductStrengthId desc    
    
Select      
ps.PackSizeId,ps.PackSizeName,PIDFProductStrengthId  
,Isnull(C.Value,'')Value,Isnull(c.PackSizeStabilityId,0) PackSizeStabilityId,MC.CountryName   
from PIDF_Commercial as D    
inner join master_packsize as ps on ps.PackSizeId=d.PackSizeId and ps.IsActive=1 
inner join Master_Country MC on D.CountryId=MC.CountryID
left join PIDF_PBF_RnD_PackSizeStability As C on c.PIDFID=D.PIDFId and d.PackSizeId=c.PackSizeId      
Where D.PIDFId = @PIDFId and d.BusinessUnitId=@BUId  and d.IsDeleted=0 and D.CountryId=@CountryId
order by PIDFProductStrengthId desc    
  
END 