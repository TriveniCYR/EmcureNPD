﻿using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<Pidf> _repository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        private IRepository<ProjectTask> _projectTaskRepository { get; set; }
        private IRepository<MasterUser> _masterUserRepository { get; set; }
        private IRepository<MasterProjectStatus> _masterProjectStatusRepository { get; set; }
        private IRepository<MasterProjectPriority> _masterProjectPriorityRepository { get; set; }
        private IRepository<PidfMedical> _pidfMedicalrepository { get; set; }
        private IRepository<PidfMedicalFile> _pidfMedicalFilerepository { get; set; }
        public ProjectService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _projectTaskRepository = unitOfWork.GetRepository<ProjectTask>();
            _masterUserRepository = unitOfWork.GetRepository<MasterUser>();
            _masterProjectStatusRepository = unitOfWork.GetRepository<MasterProjectStatus>();
            _masterProjectPriorityRepository = unitOfWork.GetRepository<MasterProjectPriority>();
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _pidfMedicalrepository = unitOfWork.GetRepository<PidfMedical>();
            _pidfMedicalFilerepository = unitOfWork.GetRepository<PidfMedicalFile>();
            _repository = unitOfWork.GetRepository<Pidf>();
            _pidfProductStrength = unitOfWork.GetRepository<PidfproductStrength>();
        }
        public ProjectTaskEntity GetDropDownsForTask()
        {
            ProjectTaskEntity TaskAddModel = new ProjectTaskEntity();
            List<ProjectTaskEntity> mainTasks = new List<ProjectTaskEntity>();
            var mainTaskList = _projectTaskRepository.GetAll().Where(x => x.TaskLevel == 1).ToList();
            foreach (var data in mainTaskList)
            {
                ProjectTaskEntity temp = new ProjectTaskEntity();
                temp.ProjectTaskId = data.ProjectTaskId;
                temp.TaskName = data.TaskName;
                mainTasks.Add(temp);
            }
            TaskAddModel.Task = mainTasks;

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
                task.ParentId = addTaskModel.ParentId;
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
            if (entityProject == null)
                return DBOperation.NotFound;
            if (entityProject.ParentId == null)
            {
                var childs = _projectTaskRepository.GetAll().Where(x => x.ParentId == entityProject.ProjectTaskId).ToList();
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

        public async Task<PIDFMedicalViewModel> GetFiles(long id)
        {
            Expression<Func<PidfMedical, bool>> expr = u => u.Pidfid == id;
            dynamic objData = (dynamic)await _pidfMedicalrepository.FindAllAsync(expr);
            PIDFMedicalViewModel data = new PIDFMedicalViewModel();
            if (objData != null && objData.Count > 0)
            {
                data = _mapperFactory.Get<PidfMedical, PIDFMedicalViewModel>(objData[0]);
                var medicalFileData = _pidfMedicalFilerepository.GetAll().Where(x => x.PidfmedicalId == data.PidfmedicalId).ToList();
                int i = 0;
                data.FileName = new string[medicalFileData.Count];
                foreach (var item in medicalFileData)
                {
                    data.PidfmedicalId = item.PidfmedicalId;
                    data.PidfmedicalFileId = item.PidfmedicalFileId;
                    data.FileName[i] = item.FileName;
                    i++;
                }
            }
            return data;
        }

        public async Task<PIDFEntity> GetByPIDFDetailsById(long id)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", id)
            };
            var PIDFList = await _repository.GetBySP("std_npd_GetPIDFById", System.Data.CommandType.StoredProcedure, osqlParameter);
            return null;
            //var data = _mapperFactory.Get<Pidf, PIDFEntity>(await _repository.GetAsync(id));
            //data.pidfProductStregthEntities = _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(_pidfProductStrength.GetAll().Where(x => x.Pidfid == id).ToList());
            //return data;
        }
    }
}