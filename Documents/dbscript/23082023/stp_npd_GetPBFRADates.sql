USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 23-08-2023 13:50:02 ******/
DROP PROCEDURE [dbo].[stp_npd_GetPBFRADates]
GO

/****** Object:  StoredProcedure [dbo].[stp_npd_GetPBFRADates]    Script Date: 23-08-2023 13:50:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--[dbo].[stp_npd_GetPBFRADates]     128,5,1,1,'20230822' 
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
	 SELECT 
	 Cast(GetDate() as Date) As EarliestSubmissionDate, 
	 Cast(GetDate() as Date) As EarliestLaunchDate, 
	 Cast(GetDate() as Date) As LastDateToRegulatory
END 
GO


