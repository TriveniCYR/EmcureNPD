using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class ProjectTask
    {
        public long ProjectTaskId { get; set; }
        public long Pidfid { get; set; }
        public string TaskName { get; set; }
        public int TaskOwnerId { get; set; }
        public int TaskLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int TaskDuration { get; set; }
        public double TotalPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public long? ParentId { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterProjectPriority Priority { get; set; }
        public virtual MasterProjectStatus Status { get; set; }
        public virtual MasterUser TaskOwner { get; set; }
    }
}
