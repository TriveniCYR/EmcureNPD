using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [EmailAddress(ErrorMessage = "The '{0}' field is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
