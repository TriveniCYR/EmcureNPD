using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public class MasterPlantLineEntity
	{
		public long LineId { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public int PlantId { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public string LineName { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public double? LineCost { get; set; }
		public bool IsActive { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? ModifyBy { get; set; }
		public DateTime? ModifyDate { get; set; }
	}
}
