using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterDosageEntity
    {
        public int DosageId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "DosageName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string DosageName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}