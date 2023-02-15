using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmcureCERI.Web.Models
{
    public class ContactVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Queries")]
        public string Query { get; set; }
    }

}