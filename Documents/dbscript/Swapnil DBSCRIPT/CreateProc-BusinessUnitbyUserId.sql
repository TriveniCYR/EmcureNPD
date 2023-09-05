USE [EmcureNPDDev]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetActiveBusinessUnitByUserid]    Script Date: 9/4/2023 2:23:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetActiveBusinessUnitByUserid] -- 1 
@Userid int
AS  
BEGIN  
 Select mb.BusinessUnitId , BusinessUnitName 
 from Master_BusinessUnit mb
 left join Master_UserBusinessUnitMapping mub on mb.BusinessUnitId = mub.BusinessUnitId
 Where IsActive = 1 and mub.UserId = @Userid
 order by  BusinessUnitId 
END 
GO


