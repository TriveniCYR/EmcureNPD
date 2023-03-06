using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterProjectPriority
    {
        public MasterProjectPriority()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
