using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterProjectStatus
    {
        public MasterProjectStatus()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
