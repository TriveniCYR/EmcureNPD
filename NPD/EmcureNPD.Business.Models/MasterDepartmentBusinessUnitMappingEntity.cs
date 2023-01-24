using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterDepartmentBusinessUnitMappingEntity
    {
        public int DepartmentBusinessUnitMappingId { get; set; }
        public int DepartmentId { get; set; }
        public int BusinessUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MasterDepartmentEntity MasterDepartmentEntity{ get; set; }
        public MasterBusinessUnitEntity MasterBusinessUnitEntity { get; set; }
    }
}
