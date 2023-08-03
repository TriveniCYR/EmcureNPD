
/****** Object:  StoredProcedure [dbo].[SpGetPIDF_PBF_General_RND]    Script Date: 03-08-2023 17:15:08 ******/
DROP PROCEDURE [dbo].[SpGetPIDF_PBF_General_RND]
GO

/****** Object:  StoredProcedure [dbo].[SpGetPIDF_PBF_General_RND]    Script Date: 03-08-2023 17:15:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[SpGetPIDF_PBF_General_RND] --0,147,80
(
@PbfRndDetailsId bigint =0,
@PidfId bigint =0,
@PbfId bigint =0,
@BusinessUnitId bigint=0
)
AS BEGIN
SELECT  
       PbfRndDetailsId
      ,PidfId
      ,PbfId
      ,RndResponsiblePerson
      , TypeOfDevelopmentDate
      ,PivotalBatchesManufacturedCompleted
      ,StabilityResultsDayZero
      ,StabilityResultsThreeMonth
      ,StabilityResultsSixMonth
      ,NonStandardProduct
      ,Pivotals
      ,BatchSizes
      ,NoMOfBatchesPerStrength
      ,SiteTransferDate
      ,ApiOrderedDate
      ,ApiReceivedDate
      ,FinalFormulationApproved
      ,CreatedOn
      ,UpdatedOn
      ,DeletedOn
      ,CreatedBy
  FROM PIDF_PBF_General_RND
  where (PbfRndDetailsId=@PbfRndDetailsId or @PbfRndDetailsId=0)
        AND PidfId=@PidfId
        AND (PbfId=@PbfId or @PbfId=0) AND BusinessUnitId=@BusinessUnitId

END
GO


