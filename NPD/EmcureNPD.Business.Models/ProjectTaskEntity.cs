using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class ProjectTaskEntity
    {
        public long ProjectTaskId { get; set; }
        public long Pidfid { get; set; }
        [Required(ErrorMessage = "Task Name is required")]
        public string TaskName { get; set; }
        [Required(ErrorMessage = "Task Owner Name is required")]
        public int TaskOwnerId { get; set; }
        public string TaskOwnerName { get; set; }
        public int EditTaskOwnerId { get; set; }
        public int TaskLevel { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
      
        public DateTime ?PlannedStartDate { get; set; }
        public DateTime ?PlannedEndDate { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public int EditTaskPriorityId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int EditTaskStatusId { get; set; }
        [Required(ErrorMessage = "Task Duration is required")]
        public int TaskDuration { get; set; }
        [Required(ErrorMessage = "Total Percentage is required")]
        public double TotalPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public long? ParentId { get; set; }
        public bool IsGanttUpdate { get; set; }
        public List<ProjectTaskEntity> Task { get; set; }
        public virtual PIDFEntity Pidf { get; set; }
        public virtual List<MasterProjectPriorityEntity> Priority { get; set; }
        public virtual List<MasterProjectStatusEntity> Status { get; set; }
        public virtual List<MasterUserEntity> TaskOwner { get; set; }
    }
}
