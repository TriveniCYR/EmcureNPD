
GO
/****** Object:  StoredProcedure [dbo].[SP_Fill_ddl_PBF]    Script Date: 9/11/2023 12:13:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
-- exec [SP_Fill_ddl_PBF] 14, 21  
Create OR ALTER PROCEDURE [dbo].[SP_Fill_ddl_PBF]   
 @UserId int,  
 @selectedbusinessunit int,
 @PIDFId int  
AS  
BEGIN  
   
Select BusinessUnitId, BusinessUnitName from Master_BusinessUnit  
Where IsActive = 1  
  
Select BERequirementId, BERequirementName from Master_BERequirement  
Where IsActive = 1  
  
Select PlantId, PlantNameName from Master_Plant  
Where IsActive = 1  
  
Select WorkflowId, WorkflowName from Master_Workflow  
Where IsActive = 1  
  
Select DosageId, DosageName from Master_Dosage  
Where IsActive = 1  
  
Select FilingTypeId, FilingTypeName from Master_FilingType  
Where IsActive = 1  
  
Select FormRnDDivisionId, FormRnDDivisionName from Master_FormRnDDivision  
Where IsActive = 1  
  
Select PackagingTypeId, PackagingTypeName from Master_PackagingType  
Where IsActive = 1  
  
Select ManufacturingId, ManufacturingName from Master_Manufacturing  
Where IsActive = 1  
  
Select distinct A.CountryID, CountryName,MB.BusinessUnitId from Master_Country As A  
Inner Join Master_UserCountryMapping As B On A.CountryID = B.CountryId And B.UserId = @UserId 
Inner Join Master_RegionCountryMapping As MR On A.CountryID = MR.CountryId
Inner Join Master_BusinessUnitRegionMapping As MB On MR.RegionId = MB.RegionId
Where A.IsActive = 1 and MB.BusinessUnitId=@selectedbusinessunit
 
Select ProductTypeId, ProductTypeName from Master_ProductType  
Where IsActive = 1  
  
Select TestLicenseId, TestLicenseName from Master_TestLicense  
Where IsActive = 1  
  
Select UserId, FullName from Master_User  
Where IsActive = 1 And IsDeleted = 0 And IsNUll(FormulationGL,0) = 1  
  
Select UserId, FullName from Master_User  
Where IsActive = 1 And IsDeleted = 0 And IsNUll(AnalyticalGL,0) = 1  
  
select top 1 MoleculeName,RFDBrand,RFDCountryId,RFDApplicant,RFDIndication from PIDF  
where PIDFID = @PIDFId  
  
select top 1 PatentStatus from PIDF_IPD  
where PIDFID = @PIDFId  
  
  
--Select PIDFProductStrengthId,Strength,B.UnitofMeasurementName  
--from PIDFProductStrength as A  
--inner join Master_UnitofMeasurement As B on A.UnitofMeasurementId = B.UnitofMeasurementId  
--Where PIDFId = @PIDFId
Select PIDFProductStrengthId, Strength, UnitofMeasurementName from(
Select PIDFProductStrengthId,Strength,B.UnitofMeasurementName
, ROW_NUMBER() OVER(Partition BY Strength, B.UnitofMeasurementName Order By PIDFProductStrengthId) As rowNumber, A.BusinessUnitId
from PIDFProductStrength as A  
inner join Master_UnitofMeasurement As B on A.UnitofMeasurementId = B.UnitofMeasurementId  
inner join Master_BusinessUnit As MB on A.BusinessUnitId=MB.BusinessUnitId
Where PIDFId = @PIDFId and MB.BusinessUnitId=@selectedbusinessunit) As A Where rowNumber = 1

  
Select TestTypeId, TestTypeName from Master_TestType  
Where IsActive = 1  
   
END  