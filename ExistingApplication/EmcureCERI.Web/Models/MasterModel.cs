using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class MasterModel
    {

        public class FormulationMaster
        {
            public int? FormulationID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Formulation Name")]
            public string FormulationName { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            //[Display(Name = "Formulation Name")]
            public bool IsActive { get; set; }
        }

        public class ProductManufactureMaster
        {
            public int? ProductManufactureID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Product Manufacture Name")]
            public string ProductManufacture { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }


        public class PackSizeMaster
        {
            public int? PackSizeID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Pack Size")]
            public string PackSize { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class PackStyleMaster
        {
            public int? PackStyleID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Pack Style")]
            public string PackStyle { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class StrengthMaster
        {
            public int? StrengthID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Strength")]
            public string Strength { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class ModeofshipmentMaster
        {
            public int? ModeofshipmentID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Mode of Shipment")]
            public string Modeofshipment { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class ModeofFeesPaymentMaster
        {
            public int? ModeofFeesPaymentID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Mode of Fees Payment")]
            public string ModeofFeesPayment { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class IncotermsMaster
        {
            public int? IncotermsID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Incoterms")]
            public string Incoterms { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }


        public class DossierTemplateMaster
        {
            public int? DossierTemplateID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Dossier Template")]
            public string DossierTemplate { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class APISiteMaster
        {
            public int? APISiteID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "API Site")]
            public string APISite { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class ManufacturingSiteMaster
        {
            public int? ManufacturingSiteID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Manufacturing Site")]
            public string ManufacturingSite { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }


        public class ArtworkTypeMaster
        {
            public int? ArtworkTypeID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "Artwork Type")]
            public string ArtworkType { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

        public class GMPAvailabilityMaster
        {
            public int? GMPAvailabilityID { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            [Display(Name = "GMP Availability")]
            public string GMPAvailability { get; set; }

            [Required(ErrorMessage = "The '{0}' field is required.")]
            public bool IsActive { get; set; }
        }

    }
}
