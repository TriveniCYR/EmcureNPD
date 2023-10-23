--  exec ProcGetSUIMSVolumeYearWiseByPackSize    214,4,871,2        
            
CREATE or alter   Proc [dbo].[ProcGetSUIMSVolumeYearWiseByPackSize] --135,16,5,2                  
@PIDFId int=0,                  
@Buid int=0,                  
@StrengthId int=0,                  
@PackSize int=0                  
AS                  
BEGIN                  
select                  
 Convert(numeric(18,2),isnull(pcy.BrandPrice,0)) as BrandPrice                  
,Convert(numeric(18,2),isnull(pcy.GenericPrice,0)) as GenericPrice                  
,Convert(numeric(18,2),isnull(pcy.PriceDiscounting,0)) as PriceDiscounting                   
,Convert(numeric(18,2),ISNULL(pcy.SUIMSVolume,0)) as SUIMSVolume                  
,Convert(numeric(18,2),ISNULL(pcy.CommercialBatchSize,0)) as CommercialBatchSize               
,Convert(numeric(18,2),isnull(MarketSharePercentageLow,0)) As MarketSharePercentageLow              
,Convert(numeric(18,2),isnull(MarketSharePercentageMedium,0)) As MarketSharePercentageMedium              
,Convert(numeric(18,2),isnull(MarketSharePercentageHigh,0)) As MarketSharePercentageHigh              
,Convert(numeric(18,2),isnull(NSPUnitsLow,0)) As NSPUnitsLow              
,Convert(numeric(18,2),isnull(NSPUnitsMedium,0)) As NSPUnitsMedium              
,Convert(numeric(18,2),isnull(NSPUnitsHigh,0)) As NSPUnitsHigh      
,mps.PackSize [PackSizeValue]  
    
from PIDF_Commercial as pc                  
inner join Master_PackSize mps on mps.PackSizeId=pc.PackSizeId                  
inner join PIDF_Commercial_Years as pcy on pcy.PIDFCommercialId=pc.PIDFCommercialId                  
where pc.PIDFId=@PIDFId and pc.BusinessUnitId=@Buid and pc.PIDFProductStrengthId = @StrengthId        
and mps.PackSizeId=@PackSize and pc.IsDeleted =0 and pcy.YearIndex =1           
            
select Expiries,AnnualConfirmatoryRelease,[Year] from PIDF_Finance_Projection where BusinessUnitId =@Buid and PIDFID = @PIDFId order by [Year]            
       
select    
PC.BusinessUnitId  
,Convert(numeric(18,2),isnull(MarketSharePercentageLow,0)) As MarketSharePercentageLow              
,Convert(numeric(18,2),isnull(MarketSharePercentageMedium,0)) As MarketSharePercentageMedium              
,Convert(numeric(18,2),isnull(MarketSharePercentageHigh,0)) As MarketSharePercentageHigh              
,Convert(numeric(18,2),isnull(NSPUnitsLow,0)) As NSPUnitsLow              
,Convert(numeric(18,2),isnull(NSPUnitsMedium,0)) As NSPUnitsMedium              
,Convert(numeric(18,2),isnull(NSPUnitsHigh,0)) As NSPUnitsHigh   
--,pd.Strength  
from PIDF_Commercial_Years PCY   
inner join PIDF_Commercial PC on PC.PIDFCommercialId = PCY.PIDFCommercialId  
--left join PIDFProductStrength pd on pd.PIDFID = pc.PIDFId  
where PC.PIDFId = @PIDFId  
and PCY.YearIndex =1   
  
  
END   

go
--  exec ProcGetCommercialPackSizeForManagmentApproval  214,2                                    
CREATE or alter   Proc [dbo].[ProcGetCommercialPackSizeForManagmentApproval] --135,16,5,2                                
@PIDFId int=0,           
@BusinessUnitId int=0           
AS                                
BEGIN                           
                    
  declare @PIDFFinaceId as int        
  select @PIDFFinaceId = PIDFFinaceId from PIDF_Finance where PIDFId = @PIDFId        
          
select  mps.PackSizeId,mps.PackSizeName,batch.EmcureCOGs_pack ,strn.Strength [SkusName],strn.PIDFProductStrengthId      
from PIDF_Finance finance                    
inner join PIDF_Finance_BatchSizeCoating batch on batch.PIDFFinaceId = finance.PIDFFinaceId --and batch.PakeSize =  pc.PackSizeId      
inner join Master_PackSize mps on mps.PackSizeId=batch.PakeSize      
inner join PIDFProductStrength strn on  strn.PIDFProductStrengthId = batch.SKus   --And strn.BusinessUnitId = @BusinessUnitId    
where finance.PIDFFinaceId=@PIDFFinaceId       
      
select   --batch.Marketinpacks      
pcy.SUIMSVolume as Marketinpacks                          
--,Convert(numeric(18,2),isnull(pcy.BrandPrice,0)) as BrandPrice                                
--,Convert(numeric(18,2),isnull(pcy.GenericPrice,0)) as GenericPrice                                
--,Convert(numeric(18,2),isnull(pcy.PriceDiscounting,0)) as PriceDiscounting                                 
--,Convert(numeric(18,2),ISNULL(pcy.SUIMSVolume,0)) as SUIMSVolume                                
--,Convert(numeric(18,2),ISNULL(pcy.CommercialBatchSize,0)) as CommercialBatchSize                             
,Convert(numeric(18,2),isnull(MarketSharePercentageLow,0)) As MarketSharePercentageLow                            
,Convert(numeric(18,2),isnull(MarketSharePercentageMedium,0)) As MarketSharePercentageMedium                            
,Convert(numeric(18,2),isnull(MarketSharePercentageHigh,0)) As MarketSharePercentageHigh                            
,Convert(numeric(18,2),isnull(NSPUnitsLow,0)) As NSPUnitsLow                            
,Convert(numeric(18,2),isnull(NSPUnitsMedium,0)) As NSPUnitsMedium                            
,Convert(numeric(18,2),isnull(NSPUnitsHigh,0)) As NSPUnitsHigh                            
from PIDF_Commercial as pc                                                         
inner join PIDF_Commercial_Years as pcy on pcy.PIDFCommercialId=pc.PIDFCommercialId                        
where  pc.IsDeleted =0  and pcy.YearIndex =1 and pc.BusinessUnitId = @BusinessUnitId and pc.PIDFId = @PIDFId                     
                          
select Expiries,AnnualConfirmatoryRelease,[Year] from PIDF_Finance_Projection projection                                  
where projection.PIDFFinaceId=@PIDFFinaceId and projection.BusinessUnitId=@BusinessUnitId  order by [Year]                          
      
------------------------------------------------      
select    
PC.BusinessUnitId  
,Convert(numeric(18,2),isnull(MarketSharePercentageLow,0)) As MarketSharePercentageLow              
,Convert(numeric(18,2),isnull(MarketSharePercentageMedium,0)) As MarketSharePercentageMedium              
,Convert(numeric(18,2),isnull(MarketSharePercentageHigh,0)) As MarketSharePercentageHigh              
,Convert(numeric(18,2),isnull(NSPUnitsLow,0)) As NSPUnitsLow              
,Convert(numeric(18,2),isnull(NSPUnitsMedium,0)) As NSPUnitsMedium              
,Convert(numeric(18,2),isnull(NSPUnitsHigh,0)) As NSPUnitsHigh   
--,pd.Strength  
from PIDF_Commercial_Years PCY   
inner join PIDF_Commercial PC on PC.PIDFCommercialId = PCY.PIDFCommercialId  
--left join PIDFProductStrength pd on pd.PIDFID = pc.PIDFId  
where PC.PIDFId = @PIDFId  
and PCY.YearIndex =1   
  
END 


