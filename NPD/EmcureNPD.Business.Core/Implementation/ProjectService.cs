using Dapper;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IConfiguration _configuration;
        private readonly IExceptionService _ExceptionService;
        private IRepository<Pidf> _repository { get; set; }
        private IRepository<ProjectTask> _projectTaskRepository { get; set; }
        private IRepository<MasterUser> _masterUserRepository { get; set; }
        private IRepository<MasterProjectStatus> _masterProjectStatusRepository { get; set; }
        private IRepository<MasterProjectPriority> _masterProjectPriorityRepository { get; set; }

        public ProjectService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IConfiguration configuration, IExceptionService exceptionService)
        {
            _projectTaskRepository = unitOfWork.GetRepository<ProjectTask>();
            _masterUserRepository = unitOfWork.GetRepository<MasterUser>();
            _masterProjectStatusRepository = unitOfWork.GetRepository<MasterProjectStatus>();
            _masterProjectPriorityRepository = unitOfWork.GetRepository<MasterProjectPriority>();
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = unitOfWork.GetRepository<Pidf>();
            _configuration = configuration;
            _ExceptionService = exceptionService;
        }

        public ProjectTaskEntity GetDropDownsForTask()
        {
            ProjectTaskEntity TaskAddModel = new ProjectTaskEntity();
            List<ProjectTaskEntity> mainTasks = new List<ProjectTaskEntity>();
            var mainTaskList = _projectTaskRepository.GetAllQuery().Where(x => x.TaskLevel == 1).ToList();
            foreach (var data in mainTaskList)
            {
                ProjectTaskEntity temp = new ProjectTaskEntity();
                temp.ProjectTaskId = data.ProjectTaskId;
                temp.TaskName = data.TaskName;
                mainTasks.Add(temp);
            }
            TaskAddModel.Task = mainTasks;

            List<MasterUserEntity> taskOwner = new List<MasterUserEntity>();
            var taskOwnerList = _masterUserRepository.GetAllQuery().Where(x => x.IsActive == true && x.IsDeleted == false).ToList();

            foreach (var data in taskOwnerList)
            {
                MasterUserEntity temp = new MasterUserEntity();
                temp.UserId = data.UserId;
                temp.FullName = data.FullName;
                taskOwner.Add(temp);
            }

            TaskAddModel.TaskOwner = taskOwner;

            List<MasterProjectStatusEntity> projectStatus = new List<MasterProjectStatusEntity>();
            //List<int> notIDS = new List<int> { 2, 3, 4, 5, 6, 7, 9, 10,12};//status ID array
            var projectStatusList = _masterProjectStatusRepository.GetAllQuery().ToList();

            foreach (var data in projectStatusList)
            {
                MasterProjectStatusEntity temp = new MasterProjectStatusEntity();
                temp.StatusId = data.StatusId;
                temp.StatusName = data.StatusName;
                projectStatus.Add(temp);
            }

            TaskAddModel.Status = projectStatus;

            List<MasterProjectPriorityEntity> priority = new List<MasterProjectPriorityEntity>();
            var projectPriorityList = _masterProjectPriorityRepository.GetAllQuery().ToList();

            foreach (var data in projectPriorityList)
            {
                MasterProjectPriorityEntity temp = new MasterProjectPriorityEntity();
                temp.PriorityId = data.PriorityId;
                temp.PriorityName = data.PriorityName;
                priority.Add(temp);
            }

            TaskAddModel.Priority = priority;

            return TaskAddModel;
            //return View();
        }

        public async Task<DBOperation> AddUpdateTaskDetail(ProjectTaskEntity addTaskModel)
        {
            ProjectTask task;

            if (addTaskModel.ProjectTaskId > 0)
            {
                task = await _projectTaskRepository.GetAsync(addTaskModel.ProjectTaskId);
                if (task != null)
                {
                    task = _mapperFactory.Get<ProjectTaskEntity, ProjectTask>(addTaskModel);
                    task.TaskName = addTaskModel.TaskName;
                    task.Pidfid = addTaskModel.Pidfid;
                    task.StartDate = addTaskModel.StartDate;
                    task.EndDate = addTaskModel.EndDate;
                    task.PlannedStartDate = addTaskModel.PlannedStartDate;
                    task.PlannedEndDate = addTaskModel.PlannedEndDate;
                    task.PriorityId = addTaskModel.PriorityId;
                    task.StatusId = addTaskModel.StatusId;
                    task.TaskDuration = addTaskModel.TaskDuration;
                    task.TotalPercentage = addTaskModel.TotalPercentage;
                    task.TaskOwnerId = addTaskModel.TaskOwnerId;
                    if (addTaskModel.IsGanttUpdate)
                    {
                        if (await UpdateMainTaskProgressByParentId(task.ParentId.ToString(), addTaskModel.TotalPercentage))
                        {
                            _projectTaskRepository.UpdateAsync(task);
                            try
                            {
                                await _unitOfWork.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            _projectTaskRepository.UpdateAsync(task);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        _projectTaskRepository.UpdateAsync(task);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    return DBOperation.Success;
                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else if (addTaskModel.ProjectTaskId == 0)
            {
                task = _mapperFactory.Get<ProjectTaskEntity, ProjectTask>(addTaskModel);
                task.TaskName = addTaskModel.TaskName;
                task.Pidfid = addTaskModel.Pidfid;
                task.StartDate = addTaskModel.StartDate;
                task.EndDate = addTaskModel.EndDate;
                task.PlannedStartDate = addTaskModel.PlannedStartDate;
                task.PlannedEndDate = addTaskModel.PlannedEndDate;
                task.PriorityId = addTaskModel.PriorityId;
                task.StatusId = addTaskModel.StatusId;
                task.TaskDuration = addTaskModel.TaskDuration;
                task.TotalPercentage = addTaskModel.TotalPercentage;
                task.TaskOwnerId = addTaskModel.TaskOwnerId==0? addTaskModel.CreatedBy: addTaskModel.TaskOwnerId;
                task.ParentId = addTaskModel.ParentId;
                task.CreatedBy = addTaskModel.CreatedBy;
                task.CreatedDate = addTaskModel.CreatedDate;
                _projectTaskRepository.AddAsync(task);
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                return DBOperation.Success;
            }
            else
            {
                return DBOperation.Error;
            }
        }

        public List<ProjectTaskEntity> GetTaskSubTaskList(long pidfId)
        {
            List<ProjectTask> projectTasks = _projectTaskRepository.GetAllQuery().Where(x => x.Pidfid == pidfId).ToList();
            var result = _mapperFactory.GetList<ProjectTask, ProjectTaskEntity>(projectTasks);
            result.ForEach(pt =>
            {
                pt.TaskOwnerName = pt.TaskLevel == 1 ? _masterUserRepository.Get(pt.TaskOwnerId).FullName : "";
                pt.StatusName = _masterProjectStatusRepository.Get(pt.StatusId).StatusName;
                pt.PriorityName = _masterProjectPriorityRepository.Get(pt.PriorityId).PriorityName;
            }
            );
            return result;
        }

        public async Task<DBOperation> DeleteTaskSubTask(int id)
        {
            var entityProject = _projectTaskRepository.Get(x => x.ProjectTaskId == id);
            if (entityProject == null)
                return DBOperation.NotFound;
            if (entityProject.ParentId == null)
            {
                var childs = _projectTaskRepository.GetAllQuery().Where(x => x.ParentId == entityProject.ProjectTaskId).ToList();
                if (childs.Count > 0)
                {
                    foreach (var child in childs)
                    {
                        _projectTaskRepository.Remove(child);
                    }
                }
            }
            _projectTaskRepository.Remove(entityProject);
            await _unitOfWork.SaveChangesAsync();
            return DBOperation.Success;
        }

        public async Task<ProjectTaskEntity> GetById(long id)
        {
            ProjectTaskEntity TaskAddModel = new ProjectTaskEntity();
            var task = _mapperFactory.Get<ProjectTask, ProjectTaskEntity>(await _projectTaskRepository.GetAsync(id));
            task.TaskOwnerId = _masterUserRepository.Get(task.TaskOwnerId).UserId;
            task.StatusId = _masterProjectStatusRepository.Get(task.StatusId).StatusId;
            task.PriorityId = _masterProjectPriorityRepository.Get(task.PriorityId).PriorityId;
            TaskAddModel = GetDropDownsForTask();
            TaskAddModel.TaskName = task.TaskName;
            TaskAddModel.EditTaskOwnerId = task.TaskOwnerId;
            TaskAddModel.EditTaskPriorityId = task.PriorityId;
            TaskAddModel.EditTaskStatusId = task.StatusId;
            TaskAddModel.StartDate = task.StartDate;
            TaskAddModel.EndDate = task.EndDate;
            TaskAddModel.TaskDuration = task.TaskDuration;
            TaskAddModel.TotalPercentage = task.TotalPercentage;
            TaskAddModel.ProjectTaskId = task.ProjectTaskId;
            TaskAddModel.Pidfid = task.Pidfid;
            TaskAddModel.TaskLevel = task.TaskLevel;
            TaskAddModel.ParentId = task.ParentId;
            return TaskAddModel;
        }

        public async Task<dynamic> GetTaskSubTaskAndProjectDetails(long id, long? FilterId = 0)
        {
            //id = 1;
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", id),
            new SqlParameter("@FilterId", FilterId)
            };
            DataSet TaskSubAndprojDetails = await _repository.GetDataSetBySP("GetTaskSubTaskFileProject", System.Data.CommandType.StoredProcedure, osqlParameter);
            return TaskSubAndprojDetails;
        }

        public async Task<dynamic> GetBusinessunitDetails(long buid, long pidfid)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@BUID", buid),
                 new SqlParameter("@PIDFID", pidfid)
            };
            DataSet BusinesUnitDetails = await _repository.GetDataSetBySP("GetBusinessUnitDetails", System.Data.CommandType.StoredProcedure, osqlParameter);
            return BusinesUnitDetails;
        }

        public async Task<dynamic> UpdateMainTaskProgressByParentId(string ParentId, double TotalPercentage)
        {
            try
            {
                if (TotalPercentage > 0)
                {
                    SqlConnection con = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
                    DynamicParameters data = new DynamicParameters();
                    data.Add("@ParentId", Convert.ToInt64(ParentId));
                    data.Add("@TotalPercentage", TotalPercentage);
                    data.Add("@Success", "", direction: ParameterDirection.Output);
                    await con.ExecuteAsync("ProcUpdateParentTaskProgressByParentId", data, commandType: CommandType.StoredProcedure);
                    var result = data.Get<string>("Success").Trim();
                    if (result == "success") { return true; }
                }
                return false;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return false;
            }
        }
    }
}