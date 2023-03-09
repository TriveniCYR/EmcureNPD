using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class ProjectService :IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<ProjectTask> _projectTaskRepository { get; set; }
        private IRepository<MasterUser> _masterUserRepository { get; set; }
        private IRepository<MasterProjectStatus> _masterProjectStatusRepository { get; set; }
        private IRepository<MasterProjectPriority> _masterProjectPriorityRepository { get; set; }

        public ProjectService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _projectTaskRepository = unitOfWork.GetRepository<ProjectTask>();
            _masterUserRepository = unitOfWork.GetRepository<MasterUser>();
            _masterProjectStatusRepository = unitOfWork.GetRepository<MasterProjectStatus>();
            _masterProjectPriorityRepository = unitOfWork.GetRepository<MasterProjectPriority>();
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
        }
        public ProjectTaskEntity GetDropDownsForTask()
        {
            ProjectTaskEntity TaskAddModel = new ProjectTaskEntity();
            List<MasterUserEntity> taskOwner = new List<MasterUserEntity>();
            var taskOwnerList = _masterUserRepository.GetAll().ToList();

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
            var projectStatusList = _masterProjectStatusRepository.GetAll().ToList();

            foreach (var data in projectStatusList)
            {
                MasterProjectStatusEntity temp = new MasterProjectStatusEntity();
                temp.StatusId = data.StatusId;
                temp.StatusName = data.StatusName;
                projectStatus.Add(temp);
            }

            TaskAddModel.Status = projectStatus;

            List<MasterProjectPriorityEntity> priority = new List<MasterProjectPriorityEntity>();
            var projectPriorityList = _masterProjectPriorityRepository.GetAll().ToList();

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
                    task.PriorityId = addTaskModel.PriorityId;
                    task.StatusId = addTaskModel.StatusId;
                    task.TaskDuration = addTaskModel.TaskDuration;
                    task.TotalPercentage = addTaskModel.TotalPercentage;
                    task.TaskOwnerId = addTaskModel.TaskOwnerId;
                    _projectTaskRepository.UpdateAsync(task);
                    await _unitOfWork.SaveChangesAsync();
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
                task.PriorityId = addTaskModel.PriorityId;
                task.StatusId = addTaskModel.StatusId;
                task.TaskDuration = addTaskModel.TaskDuration;
                task.TotalPercentage = addTaskModel.TotalPercentage;
                task.TaskOwnerId = addTaskModel.TaskOwnerId;
                _projectTaskRepository.AddAsync(task);
                await _unitOfWork.SaveChangesAsync();
                return DBOperation.Success;
            }
            else
            {
                return DBOperation.Error;
            }
        }

        public List<ProjectTaskEntity> GetTaskSubTaskList(long pidfId)
        {
            List<ProjectTask> projectTasks = _projectTaskRepository.GetAll().Where(x => x.Pidfid == pidfId).ToList();
            var result = _mapperFactory.GetList<ProjectTask, ProjectTaskEntity>(projectTasks);
            result.ForEach(pt =>
            {
                pt.TaskOwnerName = _masterUserRepository.Get(pt.TaskOwnerId).FullName;
                pt.StatusName = _masterProjectStatusRepository.Get(pt.StatusId).StatusName;
                pt.PriorityName = _masterProjectPriorityRepository.Get(pt.PriorityId).PriorityName;
            }
            );
            return result;
        }

        public async Task<DBOperation> DeleteTaskSubTask(int id)
        {
            var entityProject = _projectTaskRepository.Get(x => x.ProjectTaskId == id);
            //var entityBusinessUnitRegionMapping = _repositoryMasterBusinessUnitRegionMapping.GetAllQuery().Where(x => x.BusinessUnitId == id);
            if (entityProject == null)
                return DBOperation.NotFound;
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
            return TaskAddModel;
        }
    }
}
