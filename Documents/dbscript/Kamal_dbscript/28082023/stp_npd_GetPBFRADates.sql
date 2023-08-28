USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 28-08-2023 10:45:54 ******/
DROP PROCEDURE [dbo].[stp_npd_GetPBFRADates]
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 28-08-2023 10:45:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  
--[dbo].[stp_npd_GetPBFRADates]     148,2,94,2,'20230807'   
CREATE PROCEDURE [dbo].[stp_npd_GetPBFRADates]      
(                
@PIDFId int,  
@BusinessUnitId int,  
@CountryId int,  
@TypeOfSubmissionId int,  
@DossierReadyDate datetime,  
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
  
Declare @StabilityResultsSixMonth DateTime = (Select top 1 StabilityResultsSixMonth from PIDF_PBF_General_RND  
Where PidfId = @PIDFId)  
SET @StabilityResultsSixMonth = COALESCE(@StabilityResultsSixMonth, @SMStabilityResultsSixMonth, GETDATE())  
set @LastDataFromRnD  = isnull(@LastDataFromRnD, DateAdd(Month, 1, @StabilityResultsSixMonth))  
Declare @LastDateToRegulatory DateTime = @LastDataFromRnD  
set     @DossierReadyDate = DateAdd(Month, 2, @LastDataFromRnD)  
Declare @EarliestSubmissionDate DateTime = @DossierReadyDate  
Declare @EarliestLaunchDate DateTime = GetDate()  
Declare @EndOfProcedureDate DateTime = GetDate()  
Declare @CountryApprovalDate DateTime = GetDate()  
Declare @TypeSubmissionEOP int = (Select Top 1 ISNULL(MaxEOP, MinEOP) From Master_TypeOfSubmission  
Where Id = @TypeOfSubmissionId)  
  
  
Set @EndOfProcedureDate=DateAdd(Month, IsNUll(@TypeSubmissionEOP, 0), @DossierReadyDate)  
Declare @NationApprovalEOP int = (Select Top 1 ISNULL(MaxEOP, MinEOP) From Master_NationApproval As A  
Inner Join Master_NationApproval_CountryMapping As B On A.NationApprovalId = B.NationApprovalId  
Where B.CountryId = @CountryId)  
  
--Set @EarliestLaunchDate = DateAdd(Month, @NationApprovalEOP, @EarliestSubmissionDate)  
set @CountryApprovalDate=DateAdd(Month, @NationApprovalEOP, @EndOfProcedureDate)  
Set @EarliestLaunchDate = DateAdd(Month, IsNUll(@TypeSubmissionEOP, 0), @CountryApprovalDate)  
Select Cast(@LastDataFromRnD as Date)  LastDataFromRnD,  Cast(@LastDateToRegulatory as Date) LastDateToRegulatory  
, Cast(@DossierReadyDate as Date) DossierReadyDate, Cast(@EarliestSubmissionDate as Date) EarliestSubmissionDate  
, Cast( DateAdd(Month,4,@EarliestLaunchDate) as Date) EarliestLaunchDate,Cast(@EndOfProcedureDate as Date) EndOfProcedureDate  
,CAST(@CountryApprovalDate as Date) CountryApprovalDate  
END   
GO


