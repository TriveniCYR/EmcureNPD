ALTER TABLE PIDF_PBF_PhaseWiseBudget ADD 
		FeasabilityCumTotalDate DATE,
		PrototypeCumTotalDate DATE,
		ScaleUpCumTotalDate DATE,
		AMVCumTotalDate DATE,
		ExhibitCumTotalDate DATE,
		FilingCumTotalDate DATE 
GO



-- [dbo].[stp_PBF_PhasewiseBudgetData] 421
CREATE OR ALTER Proc [dbo].[stp_PBF_PhasewiseBudgetData]  
@PIDFId int=0
AS 
BEGIN
	  DECLARE @fillDate DATE, @manufactorDate DATE, @intdate DATE;
	  DECLARE @data TABLE (Include BIT, [Terms] VARCHAR(100), Cost DECIMAL(18, 2), [EndDate] DATETIME)

	  SELECT @manufactorDate = p.BatchManifacturingDate, @fillDate = p.FillingDateDate, @intdate = p.ProjectInitiationDate
	  FROM dbo.PIDF_PBF p
	  WHERE p.PIDFId = @PIDFId

	  SELECT TOP 1 b.BusinessUnitName, b.BusinessUnitId, pb.*
	  INTO #tmpD
	  FROM PIDF_PBF_PhaseWiseBudget pb
	  JOIN PIDF_PBF_General g ON g.PBFGeneralId = pb.PBFGeneralId
	  JOIN PIDF_PBF p ON  g.PIDFPBFId = p.PIDFPBFId
	  JOIN Master_BusinessUnit b ON b.BusinessUnitId = g.BusinessUnitId
	  WHERE p.PIDFId = @PIDFId 

	  INSERT INTO @data
	  SELECT 1,  'Feasability' [Terms], t.FeasabilityCumTotal [Cost],			ISNULL(t.FeasabilityCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 1,  'Prototype development' [Terms], t.PrototypeCumTotal [Cost],	ISNULL(t.PrototypeCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 1, 'R&D Scale Up	' [Terms], t.ScaleUpCumTotal [Cost],			ISNULL(t.ScaleUpCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 1, 'AMV / AMT' [Terms], t.AMVCumTotal [Cost],						ISNULL(t.AMVCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 1, 'Exhibit and Scalability' [Terms], t.ExhibitCumTotal [Cost],	ISNULL(t.ExhibitCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 1, 'Filing' [Terms], t.FilingCumTotal [Cost],						ISNULL(t.FilingCumTotalDate, @fillDate) [EndDate] 
	  FROM #tmpD t 
	  UNION
	  SELECT 0, 'MFD', NULL, @manufactorDate
	  UNION
	  SELECT 0, 'FD', NULL, @fillDate
	  UNION
	  SELECT 0, 'IND', NULL, @intdate
	   
	  DROP TABLE #tmpD;

	  SELECT * FROM @data
  END





