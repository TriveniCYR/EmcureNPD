

-- exec stp_npd_GetCommercialFormData 100 ,0            
CREATE or alter PROCEDURE [dbo].[stp_npd_GetCommercialFormData]                          
(                
@PIDFId int,              
@UserId int              
)                        
AS                          
BEGIN                    
                
select PIDFCommercialId, PIDFId, BusinessUnitId,CountryId, PIDFProductStrengthId, MarketSizeInUnit, ShelfLife              
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
, B.BusinessUnitId,B.CountryId,B.PIDFId, B.PIDFProductStrengthId, B.PackSizeId              
from PIDF_Commercial_years As A              
Inner Join PIDF_Commercial As B On A.PIDFCommercialId = B.PIDFCommercialId              
Where PIDFId = @PIDFId and B.IsDeleted = 0 order by PIDFCommercialId,YearIndex           
          
              
Select Strength, C.UnitofMeasurementName, A.PIDFProductStrengthId, A.BusinessUnitId from PIDFProductStrength As A              
Inner Join PIDF As B On A.PIDFId = B.PIDFId              
Inner Join Master_UnitofMeasurement As C On C.UnitofMeasurementId = A.UnitofMeasurementId              
Where A.PIDFId = 249              
              
Select StatusId,InHouses from PIDF               
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
              
select Remark,Interested,BusinessUnitId,PIDFID,CountryId from PIDF_Commercial_Master where PIDFID =  @PIDFId        
                         
-------------------Get PBFOutsource data-------------------------      
select PPO.PIDFID,MPWorkFlow.PBFWorkFlowName,MPWorkFlow.PBFWorkflowId,PPO_Task.PBFWorkFlowTaskName,PPO_Task.Cost,PPO.ProjectWorkflowId,      
PPO_Task.Tentative,MPO_Task.TaskLevel,MPO_Task.ParentId        
from PIDF_PBF_Outsource PPO         
left join PIDF_PBF_Outsource_Task PPO_Task on PPO.PIDFPBFOutsourceId = PPO_Task.PIDFPBFOutsourceId         
left join Master_PBFWorkflow_Task MPO_Task on MPO_Task.PBFWorkFlowTaskName = PPO_Task.PBFWorkFlowTaskName        
left join Master_PBFWorkFlow MPWorkFlow on MPWorkFlow.PBFWorkflowId = PPO.PBFWorkflowId        
        
where PPO.PIDFID = @PIDFId   -- and      
 --PPO.PBFWorkflowId = @PBFWorkflowId       
order by MPO_Task.TaskLevel        
-----------------------------------------------------------------------------------------------------------------------      
-----if any chnage made here, nned to same chnage in FUNCTION -->[dbo].[GetCountryForBusinessUnitAndPIDF] ---------------------      
Select DISTINCT A.CountryId, CountryName,(Select BusinessUnitId from  PIDF where PIDFID = @PIDFId ) as BusinessUnitId ,
E.PIDFProductStrengthId,E.Strength,MU.UnitofMeasurementName from Master_Country As A  
--select * from Master_Country As A  
Inner Join PIDFProductStrength_CountryMapping As F ON F.CountryId = A.CountryID 
      
Inner Join PIDFProductStrength As E On F.PIDFProductStrengthId = E.PIDFProductStrengthId And E.PIDFID = @PIDFId       
 
--------this addition join for Unit of Measurment  
Inner Join Master_UnitofMeasurement As MU On MU.UnitofMeasurementId = E.UnitofMeasurementId    
Where A.IsActive = 1      
------------------------------------------------------------------------------------------      
      
END   