using EmcureNPD.Business.Models;
using System.Collections.Generic;
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

        Task<dynamic> GetTaskSubTaskAndProjectDetails(long id, long? FilterId = 0);

        Task<dynamic> GetBusinessunitDetails(long buid, long pidfid);
    }
}