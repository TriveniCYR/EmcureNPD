
GO
/****** Object:  StoredProcedure [dbo].[GetTaskSubTaskFileProject]    Script Date: 10/16/2023 11:49:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================     
-- exec [GetTaskSubTaskFileProject] 291, 4
ALTER PROCEDURE [dbo].[GetTaskSubTaskFileProject] --57,4     
 @PIDFID INT,      
 @FilterId INT=0--0 all,1 monthly,2 quatrely,3 half yearly,4 Annually      
 AS       
 BEGIN      
 
	 SELECT top 1 MoleculeName AS ProjectName,mpt.ProductTypeName AS ProductType,mp.PlantNameName AS PlantName,'Formulation' AS Formulation ,wf.WorkflowName as WorkFlow 
	 FROM PIDF as p      
	 left join PIDF_PBF as Pb on p.PIDFID=pb.PIDFId       
	 left join Master_ProductType mpt on pb.PackagingTypeId=mpt.ProductTypeId      
	 left join Master_Workflow wf     on pb.WorkflowId=wf.WorkflowId      
	 left join Master_Plant      mp   on pb.PlantId=mp.PlantId      
	 WHERE (p.PIDFID=@PIDFID )       
        
      
	 SELECT FileName FROM PIDF_Medical_File mfile join PIDF_Medical m on mfile.PIDFMedicalId = m.PIDFMedicalId where(PIDFId = @PIDFID)      
	 union all                                
	 select MarketDetailsFileName as FileName from PIDF_API_IPD as ai join PIDF    on ai.PIDFID =PIDF.PIDFID   where(ai.PIDFID=@PIDFID)      
 
	 SELECT DISTINCT ps.Strength, mu.UnitofMeasurementName FROM PIDFProductStrength ps join Master_UnitofMeasurement mu on ps.UnitofMeasurementId=mu.UnitofMeasurementId      
	 WHERE (PIDFID=@PIDFID)  
 
	 SELECT BusinessUnitName, BusinessUnitId from Master_BusinessUnit where IsActive=1      
 
     DECLARE @dd DATE, @range INT = 0;
	 SELECT @dd = CAST(GETDATE() AS DATE);
	 SELECT @range = CASE @FilterId WHEN 1 THEN -1 WHEN 2 THEN -3 WHEN 3 THEN -6 WHEN 4 THEN -12 END; 
	 
    
     ;WITH TaskHierarchy AS 
	 (    
		SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.PlannedStartDate, pt.PlannedEndDate, pt.TaskLevel, 
			pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate, pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, 
			pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName, mu.FullName, 0 AS HierarchyLevel, 
			CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
		FROM ProjectTask pt    
		JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
		JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
		JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
		WHERE (PIDFID = @PIDFID) AND DATEPART(YEAR, ISNULL(pt.ModifyDate, pt.CreatedDate)) = DATEPART(YEAR, GETDATE())    
			AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
		UNION ALL    
    
		SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.PlannedStartDate, pt.PlannedEndDate, pt.TaskLevel, 
			pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate, pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, 
			pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName, mu.FullName, 0 AS HierarchyLevel,    
			CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
		FROM ProjectTask pt    
		JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
		JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
		JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
		JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
	)    

	SELECT ProjectTaskId, TaskName, StartDate, PlannedStartDate, EndDate, PlannedEndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
		   TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
	FROM TaskHierarchy    
	ORDER BY TaskPath;    
    
    
	/*
		if(@FilterId=0)      
		 BEGIN      
		 --SELECT pt.ProjectTaskId,pt.TaskName,pt.StartDate,pt.EndDate,pt.TaskLevel,      
		 --pt.TaskDuration,pt.TotalPercentage,pt.CreatedDate,pt.TaskOwnerId,      
		 --pt.ParentId,pt.ModifyBy,pt.ModifyDate,pt.PriorityId,pp.PriorityName,      
		 --mps.StatusName,      
		 --mu.FullName FROM ProjectTask pt      
      
		 --join Master_User mu ON pt.TaskOwnerId = mu.UserId       
		 --join Master_Project_Status mps ON pt.StatusId = mps.StatusId       
		 --join Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId      
		 --WHERE (PIDFID=@PIDFID)       
    
		 WITH TaskHierarchy AS (    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			WHERE (PIDFID = @PIDFID) AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
			UNION ALL    
    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
		)    
		SELECT ProjectTaskId, TaskName, StartDate, EndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
		TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
		FROM TaskHierarchy    
		ORDER BY TaskPath;    
    
    
		 --and pt.ModifiedDate= case when @FilterId=1 then   cast(pt.ModifiedDate as date) between DATEADD(month,-1, GETDATE()) and cast(GETDATE() as date)       
		 --                          when @FilterId=2 then   cast(pt.ModifiedDate as date) between DATEADD(month,-3, GETDATE()) and cast(GETDATE() as date)      
		 --                          when @FilterId=3 then   cast(pt.ModifiedDate as date) between DATEADD(month,-6, GETDATE()) and cast(GETDATE() as date)       
		 --          when @FilterId=4 then   DATEPART(year, pt.ModifiedDate) =  DATEPART(year, GETDATE()) else null END      
		 END      
		 ELSE IF(@FilterId=1)      
		 BEGIN      
		 --SELECT pt.ProjectTaskId,pt.TaskName,pt.StartDate,pt.EndDate,pt.TaskLevel,      
		 --pt.TaskDuration,pt.TotalPercentage,pt.CreatedDate,pt.TaskOwnerId,      
		 --pt.ParentId,pt.ModifyBy,pt.ModifyDate,pt.PriorityId,pp.PriorityName,      
		 --mps.StatusName,      
		 --mu.FullName FROM ProjectTask pt      
      
		 --join Master_User mu ON pt.TaskOwnerId = mu.UserId       
		 --join Master_Project_Status mps ON pt.StatusId = mps.StatusId       
		 --join Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId      
		 --WHERE (PIDFID=@PIDFID) and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-1, GETDATE()) and cast(GETDATE() as date)      
   
   
		  WITH TaskHierarchy AS (    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			WHERE (PIDFID=@PIDFID) 
			and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-1, GETDATE()) and cast(GETDATE() as date)      
			AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
			UNION ALL    
    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
		)    
		SELECT ProjectTaskId, TaskName, StartDate, EndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
		TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
		FROM TaskHierarchy    
		ORDER BY TaskPath;    
   
   
		 END       
		 ELSE IF(@FilterId=2)      
		 BEGIN      
		 --SELECT pt.ProjectTaskId,pt.TaskName,pt.StartDate,pt.EndDate,pt.TaskLevel,      
		 --pt.TaskDuration,pt.TotalPercentage,pt.CreatedDate,pt.TaskOwnerId,      
		 --pt.ParentId,pt.ModifyBy,pt.ModifyDate,pt.PriorityId,pp.PriorityName,      
		 --mps.StatusName,      
		 --mu.FullName FROM ProjectTask pt      
      
		 --join Master_User mu ON pt.TaskOwnerId = mu.UserId       
		 --join Master_Project_Status mps ON pt.StatusId = mps.StatusId       
		 --join Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId      
		 --WHERE (PIDFID=@PIDFID) and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-3, GETDATE()) and cast(GETDATE() as date)      
  
   
		  WITH TaskHierarchy AS (    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			WHERE (PIDFID=@PIDFID) 
			and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-3, GETDATE()) and cast(GETDATE() as date)      
			AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
			UNION ALL    
    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
		)    
		SELECT ProjectTaskId, TaskName, StartDate, EndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
		TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
		FROM TaskHierarchy    
		ORDER BY TaskPath;    
		 END       
		 ELSE IF(@FilterId=3)      
		 BEGIN      
		 --SELECT pt.ProjectTaskId,pt.TaskName,pt.StartDate,pt.EndDate,pt.TaskLevel,      
		 --pt.TaskDuration,pt.TotalPercentage,pt.CreatedDate,pt.TaskOwnerId,      
		 --pt.ParentId,pt.ModifyBy,pt.ModifyDate,pt.PriorityId,pp.PriorityName,      
		 --mps.StatusName,      
		 --mu.FullName FROM ProjectTask pt      
      
		 --join Master_User mu ON pt.TaskOwnerId = mu.UserId       
		 --join Master_Project_Status mps ON pt.StatusId = mps.StatusId       
		 --join Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId      
		 --WHERE (PIDFID=@PIDFID) and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-6, GETDATE()) and cast(GETDATE() as date)    
   
		  WITH TaskHierarchy AS 
		  (    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			WHERE (PIDFID=@PIDFID) 
			and cast(Isnull(pt.ModifyDate,pt.CreatedDate) as date) between DATEADD(month,-6, GETDATE()) and cast(GETDATE() as date)     
			  AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
			UNION ALL    
    
			SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.TaskLevel, pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate,    
				pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName,    
				mu.FullName, 0 AS HierarchyLevel,    
				CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
			FROM ProjectTask pt    
			JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
			JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
			JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
			JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
		 )    
		 SELECT ProjectTaskId, TaskName, StartDate, EndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
		 TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
		 FROM TaskHierarchy    
		 ORDER BY TaskPath;    
  
		 END       
		 ELSE IF(@FilterId=4)      
		 BEGIN      
			 --SELECT pt.ProjectTaskId,pt.TaskName,pt.StartDate,pt.EndDate,pt.TaskLevel,      
			 --pt.TaskDuration,pt.TotalPercentage,pt.CreatedDate,pt.TaskOwnerId,      
			 --pt.ParentId,pt.ModifyBy,pt.ModifyDate,pt.PriorityId,pp.PriorityName,      
			 --mps.StatusName,      
			 --mu.FullName FROM ProjectTask pt      
      
			 --join Master_User mu ON pt.TaskOwnerId = mu.UserId       
			 --join Master_Project_Status mps ON pt.StatusId = mps.StatusId       
			 --join Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId      
			 --WHERE (PIDFID=@PIDFID) and DATEPART(year, Isnull(pt.ModifyDate,pt.CreatedDate)) =  DATEPART(year, GETDATE())      
    
			 WITH TaskHierarchy AS 
			 (    
				SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.PlannedStartDate, pt.PlannedEndDate, pt.TaskLevel, 
					pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate, pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, 
					pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName, mu.FullName, 0 AS HierarchyLevel, 
					CAST(pt.ProjectTaskId AS VARCHAR(MAX)) AS TaskPath    
				FROM ProjectTask pt    
				JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
				JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
				JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
				WHERE (PIDFID = @PIDFID) AND DATEPART(YEAR, ISNULL(pt.ModifyDate, pt.CreatedDate)) = DATEPART(YEAR, GETDATE())    
					AND (pt.ParentId  = 0 or pt.ParentId IS NULL)    
    
				UNION ALL    
    
				SELECT pt.ProjectTaskId, pt.TaskName,pt.StartDate, pt.EndDate, pt.PlannedStartDate, pt.PlannedEndDate, pt.TaskLevel, 
					pt.TaskDuration, pt.TotalPercentage, pt.CreatedDate, pt.TaskOwnerId, pt.ParentId, pt.ModifyBy, 
					pt.ModifyDate, pt.PriorityId, pp.PriorityName, mps.StatusName, mu.FullName, 0 AS HierarchyLevel,    
					CONCAT(th.TaskPath, '.', pt.ProjectTaskId) AS TaskPath    
				FROM ProjectTask pt    
				JOIN Master_User mu ON pt.TaskOwnerId = mu.UserId    
				JOIN Master_Project_Status mps ON pt.StatusId = mps.StatusId    
				JOIN Master_Project_Priority pp ON pt.PriorityId = pp.PriorityId    
				JOIN TaskHierarchy th ON pt.ParentId = th.ProjectTaskId    
			)    

			SELECT ProjectTaskId, TaskName, StartDate, EndDate, TaskLevel, TaskDuration, TotalPercentage, CreatedDate,    
			TaskOwnerId, ParentId, ModifyBy, ModifyDate, PriorityId, PriorityName, StatusName, FullName, HierarchyLevel, TaskPath    
			FROM TaskHierarchy    
			ORDER BY TaskPath;    
	*/ 
END 
