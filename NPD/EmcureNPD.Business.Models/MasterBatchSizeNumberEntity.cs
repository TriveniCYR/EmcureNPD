using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterBatchSizeNumberEntity
    {
        public int BatchSizeNumberId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BatchSizeNumberName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string BatchSizeNumberName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}