using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmcureNPD.Business.Models
{
    public class MasterPackingTypeEntity
    {
        public int PackingTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "PackingTypeName", ResourceType = typeof(Master))]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
        public string PackingTypeName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public double? PackingCost { get; set; }
        public int? Ref { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
