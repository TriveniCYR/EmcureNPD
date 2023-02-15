using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.PrescriberViewModels
{
    public class DReasonViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Reason for Deactivation")]
        public string Reason { get; set; }
    }
}
