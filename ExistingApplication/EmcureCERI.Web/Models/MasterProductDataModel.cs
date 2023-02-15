using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class MasterProductDataModel
    {
        public Int64 UPID { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Generic Name")]
        public string GenericName { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Formulation")]
        public int? FormulationID { get; set; }
        
        [Display(Name = "Formulation")]
        public string FormName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strength")]
        public int? StrengthID { get; set; }
        
        [Display(Name = "Strength")]
        public string Strength { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Style")]
        public int? PackStyleID { get; set; }
        
        [Display(Name = "Pack Style")]
        public string PackStyle { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Size")]
        public int? PackSizeID { get; set; }
        
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Plant")]
        public int? PlantID { get; set; }
        
        [Display(Name = "Plant")]
        public string PlantName { get; set; }
        public bool IsActive { get; set; }       
    }
}
