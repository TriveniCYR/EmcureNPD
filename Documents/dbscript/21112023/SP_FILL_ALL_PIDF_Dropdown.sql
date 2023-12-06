  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
-- exec [SP_Fill_ddl_PIDF]  
CREATE or alter PROCEDURE [dbo].[SP_Fill_ddl_PIDF]   
 @UserId int  
AS  
BEGIN  
   
 --Oral Master   
 Select OralId, OralName from [dbo].[Master_Oral] As Oral where IsActive=1  
   
 --Unit Measurement  
 Select UnitofMeasurementId, UnitofMeasurementName from [dbo].[Master_UnitofMeasurement] where IsActive=1  
   
 --Dosage Form  
 Select DosageFormId, DosageFormName from [dbo].[Master_DosageForm] where IsActive=1  
   
 --Market Extension   
 Select MarketExtenstionId, MarketExtenstionName from [dbo].[Master_MarketExtenstion] where IsActive=1  
   
 --Product packaging Type   
 Select PackagingTypeId, PackagingTypeName from [dbo].[Master_PackagingType] where IsActive=1  
   
 --In Licenses  
 Select BusinessUnitId, BusinessUnitName from [dbo].[Master_BusinessUnit] where IsActive=1  
 And BusinessUnitId IN (Select BusinessUnitId from Master_UserBusinessUnitMapping Where UserId = @UserId)  
   
 Select APISourcingId, APISourcingName from [dbo].[Master_APISourcing] where IsActive=1  
  
 Select DIAId, DIAName from [dbo].[Master_DIA] where IsActive=1  
  
 Select IndicationId, IndicationName from [dbo].[Master_Indication] where IsActive=1  
   
END  