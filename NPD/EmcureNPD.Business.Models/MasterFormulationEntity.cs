using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterFormulationEntity
    {
        public int FormulationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "FormulationName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string FormulationName { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}