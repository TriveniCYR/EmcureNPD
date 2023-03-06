using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterProjectStatusEntity
    {
        public MasterProjectStatusEntity()
        {
            ProjectTasks = new HashSet<ProjectTaskEntity>();
        }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<ProjectTaskEntity> ProjectTasks { get; set; }
    }
}
