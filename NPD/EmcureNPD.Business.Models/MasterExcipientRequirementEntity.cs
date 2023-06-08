using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterExcipientRequirementEntity
    {
        public long ExcipientRequirementId { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public string ExcipientRequirementName { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		public double? ExcipientRequirementCost { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
