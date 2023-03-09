using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IProjectService
    {
        public ProjectTaskEntity GetDropDownsForTask();
        Task<DBOperation> AddUpdateTaskDetail(ProjectTaskEntity addTaskModel);
        public List<ProjectTaskEntity> GetTaskSubTaskList(long pidfId);
        Task<DBOperation> DeleteTaskSubTask(int id);
        Task<ProjectTaskEntity> GetById(long id);
    }
}
