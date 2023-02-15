using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.ManageViewModels
{
    public class SQViewModel
    {
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

        public string StatusMessage { get; set; }
    }
}
