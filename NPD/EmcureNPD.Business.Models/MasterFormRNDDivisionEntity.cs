using EmcureNPD.Resource.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class MasterFormRNDDivisionEntity
    {
        public int FormRNDDivisionId { get; set; }
		[Required(ErrorMessage = "Form R&D Division Name is required")]
		[Display(Name = "FormRNDDivisionName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string FormRNDDivisionName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}