using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterBusinessUnitEntity
    {
        public int BusinessUnitId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BusinessUnitName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string BusinessUnitName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RegionIds { get; set; } 
        public string MasterBusinessRegionMappingIds { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
		public string EncBusinessUnitId { get; set; }
	}

    public class BusinessUnitViewModel
    {
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
    }
}
