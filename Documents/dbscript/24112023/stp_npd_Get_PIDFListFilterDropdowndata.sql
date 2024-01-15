

alter table PIDF_PBF_RA add [BudgetLaunchDate] [datetime] NULL


CREATE or alter PROCEDURE [dbo].[stp_npd_Get_PIDFListFilterDropdowndata]                          
(                
@UserId int             
)                        
AS                      
BEGIN 

select CountryID,CountryName from Master_Country	--0
select MarketExtenstionId,MarketExtenstionName from Master_MarketExtenstion	--1
select BusinessUnitId,BusinessUnitName from Master_BusinessUnit		--2
select ManufacturingId,ManufacturingName from Master_Manufacturing	--3
select distinct CONVERT(VARCHAR(10), BudgetLaunchDate, 103) as BudgetLaunchDate from PIDF_PBF_RA where BudgetLaunchDate is not null	--4
-- convert(varchar, BudgetLaunchDate, 106) -->30 Dec 2022
end	
