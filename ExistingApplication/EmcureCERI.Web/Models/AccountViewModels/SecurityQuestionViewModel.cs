using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class SecurityQuestionViewModel
    {
        
        public string Email { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }

        [Required]
        [Display(Name = "answer")]
        public string Answer1 { get; set; }

        [Required]
        [Display(Name = "answer")]
        public string Answer2 { get; set; }

        [Required]
        [Display(Name = "answer")]
        public string Answer3 { get; set; }

    }
}
