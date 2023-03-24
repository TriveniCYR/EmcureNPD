using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterAPISourcingEntity
    {
        public int APISourcingId { get; set; }
		[Required(ErrorMessage = "API Sourcing Name is required")]
		[Display(Name = "APISourcingName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string APISourcingName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}