using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterCountryEntity
    {
        public int CountryId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "CountryName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string CountryName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		[Display(Name = "CountryName", ResourceType = typeof(Master))]
		public string CountryCode { get; set; }
		[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
		[Display(Name = "CountryName", ResourceType = typeof(Master))]
		public string IsdcountryCode { get; set; }
    }
}
