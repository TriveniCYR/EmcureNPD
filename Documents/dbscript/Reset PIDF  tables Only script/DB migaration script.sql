-- changes for Commercila screen
update PIDFProductStrength set UnitofMeasurementId = 1 where UnitofMeasurementId  is null
-----Finance  chnage-------------------------------------
need to insert data in below tables from Commercial-> 
and for Pack size and Strength from Excel
PIDF_Commercial,PIDF_Commercial_Years 


ALTER   PROCEDURE [dbo].[usp_PIDF_CSV] 
AS   
BEGIN    
	DECLARE @Dt DATETIME = GETDATE(), @By INT=1
	DECLARE @mCnt TABLE(Id INT, CountryName VARCHAR(1000));

	----- Step1: Add PIDFID for table -----
	-- ALTER TABLE [dbo].[ExcelExportPIDF] ADD PIDFID INT

	--===== Data update =======
	--- Unit of measurement ----
	INSERT INTO Master_UnitofMeasurement(UnitofMeasurementName, IsActive, CreatedBy, CreatedDate)
	SELECT distinct t.Strength, 1, @By, @Dt
	FROM [dbo].[ExcelExportPIDF] t
	WHERE t.Strength IS NOT NULL AND -- Replace Strength with Unit of measure 
	NOT EXISTS (SELECT TOP 1 1 FROM Master_UnitofMeasurement m1 WHERE m1.UnitofMeasurementName = t.Strength)
		
	 	--- Strength --- 
	--	parshob will share new sheet with coumn [Strengt] and [UNit of Measurment]
	--INSERT INTO Master_ProductStrength(ProductStrengthName, IsActive, CreatedBy, CreatedDate)
	--SELECT distinct TRIM(t.[Strength]), 1, @By,@Dt
	--FROM [dbo].[ExcelExportPIDF] t 
	--WHERE t.[Strength] IS NOT NULL   -- 
	--AND NOT EXISTS (SELECT TOP 1 1 FROM Master_ProductStrength m1 WHERE m1.ProductStrengthName = t.[Strength])

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
	-- parshob will share new sheet with coumn [Pack Size Name] and [Pack Size]

	--INSERT INTO Master_PackSize(PackSizeName,PackSize, IsActive, CreatedBy, CreatedDate)
	--SELECT 'Pack Size Name',TRIM(t.[Pack Size]), 1, @By,@Dt
	--FROM [dbo].[ExcelExportPIDF] t 
	--WHERE t.[Pack Size] IS NOT NULL   -- 
	--AND NOT EXISTS (SELECT TOP 1 1 FROM Master_PackSize m1 WHERE m1.PackSizeName = t.[Pack Size Name]) 
	 
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
		   SELECT DISTINCT 2 [BusinessUnitId], t.Product, 1, GETDATE(), GETDATE(), 1, 1,
					1 [StatusId], 1 [LastStatusId], 1 [InHouses]
					, 1, 1 UnitofMeasurementId, 1, 1 PackagingTypeId, 1, 1 DIAId, 1, 1, 'PIDF-00'
		   FROM [dbo].[ExcelExportPIDF] t WHERE t.Product = @name
		   SET @pidfId = SCOPE_IDENTITY(); 

		   PRINT 'Added data' 
		   --UPDATE p SET p.PIDFNO = 'PIDF-00' + CAST(@pidfId AS varchar(1000))
		   --FROM [dbo].[PIDF] p WITH(NOLOCK)
		   --WHERE p.PIDFID = @pidfId

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
END   