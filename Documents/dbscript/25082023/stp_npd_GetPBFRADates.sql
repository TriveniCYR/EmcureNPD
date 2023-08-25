
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 25-08-2023 12:04:13 ******/
DROP PROCEDURE [dbo].[stp_npd_GetPBFRADates]
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 25-08-2023 12:04:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--[dbo].[stp_npd_GetPBFRADates]     98,16,94,2,'20230807' 
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
@UserId INT = 0
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

set @LastDataFromRnD  =  DateAdd(Month, 1, @StabilityResultsSixMonth)
Declare @LastDateToRegulatory DateTime = @LastDataFromRnD
set     @DossierReadyDate = DateAdd(Month, 2, @LastDataFromRnD)
Declare @EarliestSubmissionDate DateTime = @DossierReadyDate
Declare @EarliestLaunchDate DateTime = GetDate()

Declare @TypeSubmissionEOP int = (Select Top 1 ISNULL(MaxEOP, MinEOP) From Master_TypeOfSubmission
Where Id = @TypeOfSubmissionId)

Set @EarliestLaunchDate = DateAdd(Month, IsNUll(@TypeSubmissionEOP, 0), @EarliestSubmissionDate)

Declare @NationApprovalEOP int = (Select Top 1 ISNULL(MaxEOP, MinEOP) From Master_NationApproval As A
Inner Join Master_NationApproval_CountryMapping As B On A.NationApprovalId = B.NationApprovalId
Where B.CountryId = @CountryId)

Set @EarliestLaunchDate = DateAdd(Month, @NationApprovalEOP, @EarliestLaunchDate)

Select Cast(@LastDataFromRnD as Date)  LastDataFromRnD,  Cast(@LastDateToRegulatory as Date) LastDateToRegulatory
, Cast(@DossierReadyDate as Date) DossierReadyDate, Cast(@EarliestSubmissionDate as Date) EarliestSubmissionDate
, Cast(@EarliestLaunchDate as Date) EarliestLaunchDate
END 
GO


