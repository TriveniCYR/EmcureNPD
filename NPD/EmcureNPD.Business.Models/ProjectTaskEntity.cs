using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class ProjectTaskEntity
    {
        public long ProjectTaskId { get; set; }
        public long Pidfid { get; set; }
        public string TaskName { get; set; }
        public int TaskOwnerId { get; set; }
        public string TaskOwnerName { get; set; }
        public int EditTaskOwnerId { get; set; }
        public int TaskLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public int EditTaskPriorityId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int EditTaskStatusId { get; set; }
        public int TaskDuration { get; set; }
        public double TotalPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public long? ParentId { get; set; }
        public List<ProjectTaskEntity> Task { get; set; }
        public virtual PIDFEntity Pidf { get; set; }
        public virtual List<MasterProjectPriorityEntity> Priority { get; set; }
        public virtual List<MasterProjectStatusEntity> Status { get; set; }
        public virtual List<MasterUserEntity> TaskOwner { get; set; }
    }
}
