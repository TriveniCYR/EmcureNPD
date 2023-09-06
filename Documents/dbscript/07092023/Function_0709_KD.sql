
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountryForBusinessUnitAndPIDF]    Script Date: 07-09-2023 00:48:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- select * from  dbo.[GetCountryForBusinessUnitAndPIDF](180, 11)       
Create or ALTER FUNCTION [dbo].[GetCountryForBusinessUnitAndPIDF]       
(       
 @PIDFID Bigint,      
 @BusinessUnitId int      
)      
RETURNS TABLE       
AS      
RETURN       
( -- if any chnage made here, nned to same chnage in SP-->[stp_npd_GetCommercialFormData] For BU wise COuntry Fetching 
Select DISTINCT A.CountryId, CountryName from Master_Country As A      
Inner Join Master_RegionCountryMapping As B On A.CountryID = B.CountryId      
Inner Join Master_BusinessUnitRegionMapping As C On C.RegionId = B.RegionId And C.BusinessUnitId = @BusinessUnitId    
--Inner Join PIDF As D On D.BusinessUnitId = C.BusinessUnitId And D.PIDFID = @PIDFId     
Inner Join PIDFProductStrength As E On E.BusinessUnitId = C.BusinessUnitId And E.PIDFID = @PIDFId     
Inner Join PIDFProductStrength_CountryMapping As F ON F.PIDFProductStrengthId = E.PIDFProductStrengthId  --and F.CountryId = A.CountryID
Where A.IsActive = 1     
)      
   