alter   PROCEDURE [dbo].[usp_PIDF_CSV]   
AS     
BEGIN      
 DECLARE @Dt DATETIME = GETDATE(), @By INT=1  
 DECLARE @mCnt TABLE(Id INT, CountryName VARCHAR(1000));  
  
 ----- Step1: Add PIDFID for table -----  
 -- ALTER TABLE [dbo].[ExcelExportPIDF] ADD PIDFID INT  
 --ALTER TABLE [dbo].PIDF_PBF_RA ADD BudgetLaunchDate datetime null
 
  --Master_BusinessUnit
  --Master_UnitofMeasurement
  --Master_ProductStrength
  --Master_Country
  --Master_DosageForm------
  --Master_PackSize
  --Master_MarketExtenstion---------
  --Master_DIA / MA
  --Master In house/InLicensed
  --Master_Manufacturing
 --===== Data update =======  
 --- Unit of measurement ----  
 INSERT INTO Master_UnitofMeasurement(UnitofMeasurementName, IsActive, CreatedBy, CreatedDate)  
SELECT --PATINDEX('%[a-z]%', Strength) StartIndex, Strength, SUBSTRING(Strength, 0, PATINDEX('%[a-z]%', Strength)) Stength,   
distinct SUBSTRING(Strength, PATINDEX('%[a-z]%', Strength), LEN(Strength)), 1, @By,@Dt  
FROM ExcelExportPIDF t  
 WHERE SUBSTRING(Strength, PATINDEX('%[a-z]%', Strength), LEN(Strength)) IS NOT NULL AND -- Replace Strength with Unit of measure   
 NOT EXISTS (SELECT TOP 1 1 FROM Master_UnitofMeasurement m1 WHERE m1.UnitofMeasurementName = SUBSTRING(Strength, PATINDEX('%[a-z]%', Strength), LEN(Strength)))  
 
   --- Strength --- 
   INSERT INTO Master_ProductStrength(ProductStrengthName, IsActive, CreatedBy, CreatedDate) 
SELECT   
distinct SUBSTRING(Strength, 0, PATINDEX('%[a-z]%', Strength))  
, 1, @By,@Dt  
FROM ExcelExportPIDF t  
 WHERE SUBSTRING(Strength, 0, PATINDEX('%[a-z]%', Strength)) IS NOT NULL AND -- Replace Strength with Unit of measure   
 NOT EXISTS (SELECT TOP 1 1 FROM Master_ProductStrength m1 WHERE m1.ProductStrengthName = SUBSTRING(Strength, 0, PATINDEX('%[a-z]%', Strength)))  
  
  
 --- update Country ----   
 INSERT INTO Master_Country(CountryName, CountryCode, IsActive, CreatedBy, CreatedDate)  
 SELECT distinct TRIM(t1.[Value]), TRIM(t1.[Value]), 1, @By,@Dt   
 FROM [dbo].[ExcelExportPIDF] t  
 CROSS APPLY [dbo].[ufn_Split](t.Country, '/') t1  
 WHERE t.Country IS NOT NULL   -- Replace Strength with Unit of measure   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_Country m1 WHERE m1.CountryCode = TRIM(t1.[Value]))   
  
 --- Dosage form ---   
 INSERT INTO Master_DosageForm(DosageFormName, IsActive, CreatedBy, CreatedDate)  
 SELECT distinct TRIM(t.[Dosage form]), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[Dosage form] IS NOT NULL   -- Replace Strength with Unit of measure   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_DosageForm m1 WHERE m1.DosageFormName = t.[Dosage form])   
  
   
 --- Pack Size ---   
 INSERT INTO Master_PackSize(PackSizeName,PackSize, IsActive, CreatedBy, CreatedDate)  
 SELECT DISTINCT 'Pack Size Name'+ TRIM(CONVERT(varchar, t.[Pack Size])),  TRIM(CONVERT(varchar, t.[Pack Size])), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[Pack Size] IS NOT NULL   --   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_PackSize m1 WHERE m1.PackSizeName = Convert(varchar, t.[Pack Size]))     
   --- Market Extenstion ---   
 INSERT INTO Master_MarketExtenstion(MarketExtenstionName, IsActive, CreatedBy, CreatedDate)  
 SELECT  distinct TRIM(t.[Market Extension]), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[Market Extension] IS NOT NULL   --    
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_MarketExtenstion m1 WHERE m1.MarketExtenstionName = t.[Market Extension])  
    
 ----Master In house/InLicensed ---> already done  
   
 --- Master_DIA / MA ---   
 INSERT INTO Master_DIA(DIAName, IsActive, CreatedBy, CreatedDate)  
 SELECT distinct TRIM(t.[MA]), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[MA] IS NOT NULL   --   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_DIA m1 WHERE m1.DIAName = t.[MA])  
  
  ---Manufacturer ---   
 INSERT INTO Master_Manufacturing(ManufacturingName, IsActive, CreatedBy, CreatedDate)  
 SELECT distinct TRIM(t.Manufacturer), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[Manufacturer] IS NOT NULL   --   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_Manufacturing m1 WHERE m1.ManufacturingName = t.Manufacturer)  
    
   --- Business Unit---   
 INSERT INTO Master_BusinessUnit(BusinessUnitName, IsActive, CreatedBy, CreatedDate)  
 SELECT distinct TRIM(t.BU), 1, @By,@Dt  
 FROM [dbo].[ExcelExportPIDF] t   
 WHERE t.[BU] IS NOT NULL   -- Replace Strength with Unit of measure   
 AND NOT EXISTS (SELECT TOP 1 1 FROM Master_BusinessUnit m1 WHERE m1.BusinessUnitName = t.[BU])  
  
  
  
 -- Step2: Do Product info -----  
 DECLARE  @name VARCHAR(1000), @buId INT = 2  
  
 DECLARE db_cursor CURSOR FOR   
 SELECT DISTINCT t.Product  
 FROM [dbo].[ExcelExportPIDF] t WHERE t.Product IS NOT NULL    
 ORDER BY t.Product  
  
 OPEN db_cursor    
 FETCH NEXT FROM db_cursor INTO @name    
  
 WHILE @@FETCH_STATUS = 0    
 BEGIN    
     DECLARE @pidfId INT  
       
  
     INSERT INTO [dbo].[PIDF] ([BusinessUnitId],[MoleculeName],[IsActive]  
           ,[CreatedDate], ModifyDate  
           ,[CreatedBy], ModifyBy   
           ,[StatusId]  
           ,[LastStatusId]  
           ,[InHouses],   
     OralId, UnitofMeasurementId, DosageFormId, PackagingTypeId, RFDCountryId, DIAId, MarketExtenstionId, StatusUpdatedBy, PIDFNO)   
     SELECT DISTINCT 31 [BusinessUnitId], t.Product, 1, GETDATE(), GETDATE(), 1, 1,  
     1 [StatusId], 1 [LastStatusId], 1 [InHouses]  
     , 1, 17 UnitofMeasurementId, 1, 1 PackagingTypeId, 1, 1 DIAId, 1, 1, 'PIDF-00'  
     FROM [dbo].[ExcelExportPIDF] t WHERE t.Product = @name  
     SET @pidfId = SCOPE_IDENTITY();   
  
     PRINT 'Added data'   
     UPDATE p SET p.PIDFNO = 'PIDF-00' + CAST(@pidfId AS varchar(1000))  
     FROM [dbo].[PIDF] p WITH(NOLOCK)  
     WHERE p.PIDFID = @pidfId  
  
    --- Update PIDF ID -----  
    UPDATE t SET t.PIDFID = @pidfId  
    FROM  [dbo].[ExcelExportPIDF] t WHERE t.Product = @name  
  
  
  
  -- Add PIDFProductStrength ---    
   SELECT DISTINCT t.PIDFID, t.Strength, PATINDEX('%[^0-9.]%', t.Strength) Ind  
   INTO #Str  
   FROM [dbo].[ExcelExportPIDF] t WITH(NOLOCK)   
   WHERE t.PIDFID = @pidfId  
  
   INSERT INTO PIDFProductStrength (PIDFID, Strength, UnitofMeasurementId, BusinessUnitId, ModifyDate, ModifyBy)  
   SELECT DISTINCT t.PIDFID, SUBSTRING(t.Strength, 0, t.Ind) Strength,  u.UnitofMeasurementId, @buId [BusinessUnitId], GETDATE(), 1  
    FROM #Str t   
   LEFT JOIN Master_UnitofMeasurement u ON u.UnitofMeasurementName = TRIM(SUBSTRING(t.Strength, t.Ind, LEN(t.Strength)))  
   WHERE t.PIDFID = @pidfId  
  
   DROP TABLE #Str;  
   PRINT 'Added PIDFProductStrength'   
  
  
  
  -- Add [PIDFProductStrength_CountryMapping]  
   INSERT INTO [PIDFProductStrength_CountryMapping] (PIDFProductStrengthId, CountryId, ModifyBy, ModifyDate)  
   SELECT p.PIDFProductStrengthId, c.CountryID, 1, GETDATE()  
   FROM PIDFProductStrength p  WITH(NOLOCK)  
   OUTER APPLY (SELECT c.CountryID FROM Master_Country c   
       JOIN [dbo].[ExcelExportPIDF] t ON t.Country = c.CountryCode   
       WHERE t.PIDFId = @pidfId  
      ) c  
   WHERE p.PIDFId = @pidfId AND c.CountryID IS NOT NULL  
   PRINT 'Added PIDFProductStrength_CountryMapping'   
  
    FETCH NEXT FROM db_cursor INTO @name   
 END   
  
 CLOSE db_cursor    
 DEALLOCATE db_cursor  

 ---step 3 insert into PBF tables-----

 ------start----Insert into --PIDF_PBF------------------------------------
-- insert into PIDF_PBF (PIDFID,ProjectName,CreatedDate,CreatedBy,ManufacturingId) 
--select PIDFID,MoleculeName,getdate(), 1,(
--select ManufacturingId from Master_Manufacturing where ManufacturingName =
--(select top 1 Manufacturer from ExcelExportPIDF where Product = MoleculeName)
--)
--from PIDF where BusinessUnitId  =2 -- 31 is Itly Country ID and 2 is EU country ID

--insert into PIDF_PBF (PIDFID,ProjectName,CreatedDate,CreatedBy,ManufacturingId) 
--select PIDFID,MoleculeName,getdate(), 1,(
--select ManufacturingId from Master_Manufacturing where ManufacturingName =
--(select top 1 Manufacturer from ExcelExportPIDF where Product = MoleculeName)
--)
--from PIDF where BusinessUnitId  =31 -- 31 is Itly Country ID and 2 is EU country ID

------End----Insert into --PIDF_PBF------------------------------------

------Start----Insert into --PIDF_PBF_RA------------------------------------

--insert into PIDF_PBF_RA (PIDFId,PBFId,CountryIdBuId,BudgetLaunchDate,EarliestLaunchDExcl,BuId,CreatedOn)
--select PIDFID
--,(select top 1 PIDFPBFID from PIDF_PBF where PIDFID = p.PIDFID)
--,(select TOP 1 CountryID from Master_Country where CountryCode = 
--	(select TOP 1 Country from ExcelExportPIDF_EU where Product =  p.MoleculeName)
--)
--,(select TOP 1 [Bud Launch] from ExcelExportPIDF_EU where Product = p.MoleculeName)
--,(select TOP 1 [Launch date] from ExcelExportPIDF_EU where Product = p.MoleculeName)
--,(select TOP 1 BusinessUnitId from Master_BusinessUnit where BusinessUnitName = 
--	(select TOP 1 BU from ExcelExportPIDF_EU where Product = p.MoleculeName)
--)
--,getdate()
--from PIDF p where BusinessUnitId  =2 -- 31 is Itly Country ID and 2 is EU country ID

--insert into PIDF_PBF_RA (PIDFId,PBFId,CountryIdBuId,BudgetLaunchDate,EarliestLaunchDExcl,BuId,CreatedOn)
--select PIDFID
--,(select top 1 PIDFPBFID from PIDF_PBF where PIDFID = p.PIDFID)
--,(select TOP 1 CountryID from Master_Country where CountryCode = 
--	(select TOP 1 Country from ExcelExportPIDF where Product =  p.MoleculeName)
--)
--,(select TOP 1 [Bud Launch] from ExcelExportPIDF where Product = p.MoleculeName)
--,(select TOP 1 [Launch date] from ExcelExportPIDF where Product = p.MoleculeName)
--,(select TOP 1 BusinessUnitId from Master_BusinessUnit where BusinessUnitName = 
--	(select TOP 1 BU from ExcelExportPIDF where Product = p.MoleculeName)
--)
--,getdate()
--from PIDF p where BusinessUnitId  =31 -- 31 is Itly Country ID and 2 is EU country ID

------End----Insert into --PIDF_PBF_RA------------------------------------

----Start----Insert into --PIDF.DIAID------------------------------------
--update  p
--set p.DIAID = md.DIAId	
--from PIDF p inner join ExcelExportPIDF_EU eu on eu.PIDFID = p.PIDFID
--inner join Master_DIA md on md.DIAName = eu.[MA]
-----------------------------------------
--update  p
--set p.DIAID = md.DIAId	
--from PIDF p inner join ExcelExportPIDF eu on eu.PIDFID = p.PIDFID
--inner join Master_DIA md on md.DIAName = eu.[MA]
---End----Insert into --PIDF.DIAID------------------------------------
--update  p set p.InHouses = case when (eu.[In house/ In-licensed] ='In-House')	 then 1 else 0 end
--from PIDF p inner join ExcelExportPIDF_EU eu on eu.PIDFID = p.PIDFID
----------------------------------
--update  p set p.InHouses = case when (eu.[In house/ In-licensed] ='In-House')	 then 1 else 0 end
--from PIDF p inner join ExcelExportPIDF eu on eu.PIDFID = p.PIDFID

-----------------------------------------------------------------------------------------
--insert into PIDF_Finance (PIDFId,Product,CreatedDate,CreatedBy,DosageFrom)
--select PIDFID,MoleculeName,getdate(), 1,
--(
--	select DosageFormId from Master_DosageForm where DosageFormName =
--	(select top 1 [Dosage Form] from ExcelExportPIDF where PIDFID = p.PIDFID)
--)
--from PIDF p where BusinessUnitId  =31 -- 31 is Itly Country ID and 2 is EU country ID

-----------------------------------------------------------------
--insert into PIDF_Finance (PIDFId,Product,CreatedDate,CreatedBy,DosageFrom)
--select PIDFID,MoleculeName,getdate(), 1,
--(
--	select DosageFormId from Master_DosageForm where DosageFormName =
--	(select top 1 [Dosage Form] from ExcelExportPIDF_EU where PIDFID = p.PIDFID)
--)
--from PIDF p where BusinessUnitId  =2 -- 31 is Itly Country ID and 2 is EU country ID
-----------------------------------------------------------------------------
----update  p
----set p.MarketExtenstionId = md.MarketExtenstionId	
------select p.MarketExtenstionId ,md.MarketExtenstionId	, eu.[Market Extension],md.MarketExtenstionName
----from PIDF p inner join ExcelExportPIDF_EU eu on eu.PIDFID = p.PIDFID
----inner join Master_MarketExtenstion md on md.MarketExtenstionName = eu.[Market Extension]


---------------------------------------------------------------------------------
 ------update PIDFProductStrength set BusinessUnitId = 31 where PIDFID > 176 
-------update PIDF_PBF_RA set buid = 2 where PIDFID <177


-- changes for Commercila screen-------------------
--update PIDFProductStrength set UnitofMeasurementId = 1 where UnitofMeasurementId  is null
-----Finance  chnage-------------------------------------
--need to insert data in below tables from Commercial-> 
--and for Pack size and Strength from Excel
--PIDF_Commercial,PIDF_Commercial_Years 
----------------------------------------------------------------------------------------------------------
--update  p
--set p.BusinessUnitId = md.BusinessUnitId	
--from PIDF p inner join ExcelExportPIDF eu on eu.PIDFID = p.PIDFID
--inner join Master_BusinessUnit md on md.BusinessUnitName = eu.[BU]
------------------------------------------------------------------------------------------------------

--update pc
--set  pc.countryId = prdc.CountryId 
--from 
--PIDF_Commercial pc inner join PIDFProductStrength prd on pc.PIDFId = prd.PIDFID 
-- inner join PIDFProductStrength_CountryMapping prdc on prdc.PIDFProductStrengthId = prd.PIDFProductStrengthId

---------------------------------------------------------------
--DECLARE @i INT = 2;
--WHILE @i <= 20
--	BEGIN
--		if(@i!=4 and @i!=8 and @i!=19)
--		begin
--		update PIDF set StatusId = @i
--		print @i
--		end
--		SET @i = @i + 1;
--	END;

END 