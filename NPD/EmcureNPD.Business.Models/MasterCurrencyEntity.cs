using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterCurrencyEntity
    {
        public int CurrencyId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "CurrencyName", ResourceType = typeof(Master))]
        public string CurrencyName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]

        public string CurrencyCode { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]

        public string CurrencySymbol { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CountryIds { get; set; } 
        public string MasterBusinessCountryMappingIds { get; set; }
    }
}
