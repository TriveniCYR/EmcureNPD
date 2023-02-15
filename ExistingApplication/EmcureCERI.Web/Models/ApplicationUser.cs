using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsChangePassword { get; set; }

        public bool? IsEnabled { get; set; }

        public bool? IsRejected { get; set; }

        public string RejectionReason { get; set; }

        public bool? IsDeactivated { get; set; }

        public string DeactivationReason { get; set; }
    }
}
