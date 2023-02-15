using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.PrescriberViewModels
{
    public class PatientViewModel
    {

        public int Id { get; set; }

        public string UniqueId { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Date")]
        public DateTime Point1Date { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point1 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point2 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point3 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point4 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point5 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point6 { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool Point7 { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string RFirstName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250)]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string RLastName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public bool? IsApproved { get; set; }

        public string CStatus { get; set; }

        public string PdfName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Reason for Rejection")]
        public string Reason { get; set; }

        [Display(Name = "Declaration : I hereby confirm, that the information provided by prescriber on the above form is true and correct to the best of my knowledge and belief.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        public bool IsConsentFcheckByAdmin { get; set; }

        public bool IsConsentFcheckByHcp { get; set; }


        public string BStatus { get; set; }

        public string FStatus { get; set; }

        [Display(Name = "IsBaselineDataByAdmin")]
        public bool IsBaselineDataByAdmin { get; set; }

        public string Action { get; set; }
    }
}
