
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


-- exec stp_npd_GetCommercialFormData 117,1         
alter PROCEDURE [dbo].[stp_npd_GetCommercialFormData]                      
(            
@PIDFId int,          
@UserId int          
)                    
AS                      
BEGIN                
            
select PIDFCommercialId, PIDFId, BusinessUnitId, PIDFProductStrengthId, MarketSizeInUnit, ShelfLife          
, A.PackSizeId, B.PackSizeName, B.PackSize          
from PIDF_Commercial As A          
Inner Join Master_PackSize As B On A.PackSizeId = B.PackSizeId          
Where PIDFId = @PIDFId and A.IsDeleted = 0        
          
select [PIDFCommercialYearId], A.PIDFCommercialId, YearIndex, PackagingTypeId, CurrencyId,          
[CommercialBatchSize], [PriceDiscounting], [TotalApireq], [Apireq], [Suimsvolume], [FreeOfCost],           
[MarketGrowth], [MarketSize], [PriceErosion], [FinalSelectionId], [MarketSharePercentageLow],           
[MarketSharePercentageMedium], [MarketSharePercentageHigh], [MarketShareUnitLow], [MarketShareUnitMedium],           
[MarketShareUnitHigh], [NspunitsLow], [NspunitsMedium], [NspunitsHigh], [NspLow], [NspMedium],           
[NspHigh], [BrandPrice], [GenericPrice],[TargetCostOfGood]          
, B.BusinessUnitId, B.PIDFId, B.PIDFProductStrengthId, B.PackSizeId          
from PIDF_Commercial_years As A          
Inner Join PIDF_Commercial As B On A.PIDFCommercialId = B.PIDFCommercialId          
Where PIDFId = @PIDFId and B.IsDeleted = 0 order by PIDFCommercialId,YearIndex       
      
          
Select Strength, C.UnitofMeasurementName, A.PIDFProductStrengthId from PIDFProductStrength As A          
Inner Join PIDF As B On A.PIDFId = B.PIDFId          
Inner Join Master_UnitofMeasurement As C On C.UnitofMeasurementId = A.UnitofMeasurementId          
Where A.PIDFId = @PIDFId          
          
Select StatusId from PIDF           
Where PIDFID = @PIDFId          
          
Select BusinessUnitId, BusinessUnitName from Master_BusinessUnit          
Where IsActive = 1          
          
exec SP_GetCurrencyByUser @UserId          
          
Select PackagingTypeId, PackagingTypeName from Master_PackagingType          
Where IsActive = 1          
          
Select PackSizeId, PackSizeName, PackSize from Master_PackSize          
Where IsActive = 1          
          
Select FinalSelectionId, FinalSelectionName from Master_FinalSelection          
Where IsActive = 1          
          
select Remark,Interested,BusinessUnitId,PIDFID from PIDF_Commercial_Master where PIDFID =  @PIDFId    
                     
END 

