using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.PrescriberViewModels
{
    public class AddFollowUpFormVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Follow Up Form Name")]
        public string FollowUpFormName { get; set; }
    }
}
