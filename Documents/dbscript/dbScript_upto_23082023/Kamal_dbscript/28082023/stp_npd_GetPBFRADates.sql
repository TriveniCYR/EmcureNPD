 
--[dbo].[stp_npd_GetPBFRADates]     148,2,94,2,'20230807' 
ALTER PROCEDURE [dbo].[stp_npd_GetPBFRADates]    
(              
@PIDFId int,
@BusinessUnitId int,
@CountryId int,
@TypeOfSubmissionId int= null,
@DossierReadyDate datetime= null,
@PivotalBatchManufactured datetime = null,
@LastDataFromRnD datetime = null,
@BEFinalReport datetime = null,
@UserId INT = 0,
@SMStabilityResultsSixMonth datetime=null
)                      
AS                        
BEGIN                         
	 --SELECT 
	 --Cast(DATEADD(month, 1,GetDate())  as Date)As EarliestSubmissionDate, 
	 --Cast(DATEADD(month, 2,GetDate()) as Date) As EarliestLaunchDate, 
	 --Cast(DATEADD(month, 3,GetDate()) as Date) As LastDateToRegulatory
	--Declare @PIDFId int = 148 
	--Declare @TypeOfSubmissionId int = 1
	--Declare @CountryId int = 94

	
	DECLARE @today DATETIME = GETDATE();
	DECLARE @EarliestLaunchDate DateTime = @today, @EndOfProcedureDate DateTime = @today, @CountryApprovalDate DateTime = @today,
			@LastDateToRegulatory DateTime, @EarliestSubmissionDate DateTime,
			@TypeSubmissionEOP INT, @NationApprovalEOP int

	DECLARE @StabilityResultsSixMonth DATETIME = (SELECT TOP 1 StabilityResultsSixMonth FROM PIDF_PBF_General_RND WHERE PidfId = @PIDFId);
	
	SELECT TOP 1 @TypeSubmissionEOP = ISNULL(MaxEOP, MinEOP) 
	FROM Master_TypeOfSubmission WHERE Id = @TypeOfSubmissionId;
	
	SELECT TOP 1 @NationApprovalEOP = ISNULL(MaxEOP, MinEOP) 
	FROM Master_NationApproval As A JOIN Master_NationApproval_CountryMapping As B On A.NationApprovalId = B.NationApprovalId
										Where B.CountryId = @CountryId 

	SET @StabilityResultsSixMonth = COALESCE(@StabilityResultsSixMonth, @SMStabilityResultsSixMonth, @today)
	SET @LastDataFromRnD  = ISNULL(@LastDataFromRnD, DATEADD(MONTH, 1, @StabilityResultsSixMonth))
	
	SET @LastDateToRegulatory = @LastDataFromRnD
	SET @DossierReadyDate = DATEADD(MONTH, 2, @LastDataFromRnD); 
	SET @EarliestSubmissionDate = @DossierReadyDate;  
	SET @EndOfProcedureDate = DATEADD(MONTH, ISNULL(@TypeSubmissionEOP, 0),  @DossierReadyDate)  
	SET @CountryApprovalDate = DATEADD(MONTH, ISNULL(@NationApprovalEOP, 0), @EndOfProcedureDate)
	SET @EarliestLaunchDate  = DATEADD(MONTH, IsNUll(@TypeSubmissionEOP, 0), @CountryApprovalDate)
	
	
	SELECT	  Cast(@LastDataFromRnD as Date)  LastDataFromRnD,							Cast(@LastDateToRegulatory as Date) LastDateToRegulatory
			, Cast(@DossierReadyDate as Date) DossierReadyDate,							Cast(@EarliestSubmissionDate as Date) EarliestSubmissionDate
			, Cast( DateAdd(Month,4, @EarliestLaunchDate) as Date) EarliestLaunchDate,	Cast(@EndOfProcedureDate as Date) EndOfProcedureDate
			, CAST(@CountryApprovalDate as Date) CountryApprovalDate
END 
