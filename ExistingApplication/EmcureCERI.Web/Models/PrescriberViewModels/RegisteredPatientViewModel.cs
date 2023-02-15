using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmcureCERI.Web.Models
{
    public class RegisteredPatientViewModel
    {
        public string Prescriber { get; set; }
        public IEnumerable<SelectListItem> PrescriberList { get; set; }
    }
}
