using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.AdminViewModels
{
    public class PrescriberViewModel
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


        [Required]
        [Display(Name = "Telephone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter exactly 10 digits")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [EmailAddress(ErrorMessage = "The '{0}' field is not a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Doctor/Pharmacist")]
        public string IsDoctorPharmacist { get; set; }

        
        [Display(Name = "HCP Registration Number")]
        public string GMCGpHCNumber { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Specialization")]
        public string Specialization { get; set; }

        [Display(Name = "Hospital Address")]
        public string HospitalAddress { get; set; }

        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }

        public bool IsEnable { get; set; }

        public bool IsHide { get; set; }

    }
}
