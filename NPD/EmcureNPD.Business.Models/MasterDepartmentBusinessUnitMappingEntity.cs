using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterDepartmentBusinessUnitMappingEntity
    {
		[Required(ErrorMessage = "Business Unit Name is required")]
		public int DepartmentBusinessUnitMappingId { get; set; }
        public int DepartmentId { get; set; }
        public int BusinessUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MasterDepartmentEntity MasterDepartmentEntity{ get; set; }
        public MasterBusinessUnitEntity MasterBusinessUnitEntity { get; set; }
    }
}
