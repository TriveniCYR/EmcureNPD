using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public  class MasterProjectPriorityEntity
    {
        public MasterProjectPriorityEntity()
        {
            ProjectTasks = new HashSet<ProjectTaskEntity>();
        }
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public virtual ICollection<ProjectTaskEntity> ProjectTasks { get; set; }
    }
}
