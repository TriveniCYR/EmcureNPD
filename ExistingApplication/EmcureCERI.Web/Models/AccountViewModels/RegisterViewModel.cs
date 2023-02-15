using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Region")]
        public int GeographyId { get; set; }
        public IEnumerable<SelectListItem> Geographys { get; set; }


        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{10}", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } 



        //[Display(Name = "Doctor or Pharmacist")]
        //public bool IsDoctorPharmacist { get; set; }

        //public int? SpecializationId { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Roles")]
        public string Specialization { get; set; }
        public IEnumerable<SelectListItem> Specializations { get; set; }

      //  [Required(ErrorMessage = "The '{0}' field is required.")]
        //[StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        //[Display(Name = "Other Specialisation")]
        //public string OtherSpecialization { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //[StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        //[Display(Name = "HCP Registration Number")]
        //public string GmcgpHcnumber { get; set; }

      //  [Required(ErrorMessage = "The '{0}' field is required.")]
        //[StringLength(5000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        //[Display(Name = "Hospital Address")]
        //public string HospitalAddress { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Telephone Number")]
        [Range(1, 999999999999999, ErrorMessage = "Invalid Number")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter exactly 10 digits")]
        public string TelephoneNumber { get; set; }


        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Question")]
        public string Question1 { get; set; }
        public IEnumerable<SelectListItem> Question1s { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Question")]
        public string Question2 { get; set; }
        public IEnumerable<SelectListItem> Question2s { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Question")]
        public string Question3 { get; set; }
        public IEnumerable<SelectListItem> Question3s { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Answer")]
        public string Answer1 { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Answer")]
        public string Answer2 { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Answer")]
        public string Answer3 { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [EmailAddress(ErrorMessage = "The '{0}' field is not a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
