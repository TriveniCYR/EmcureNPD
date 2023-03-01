using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        //[Required]
        //[Display(Name = "question")]
        //public string Question1 { get; set; }
       

        //[Required]
        //[Display(Name = "question")]
        //public string Question2 { get; set; }
       

        //[Required]
        //[Display(Name = "question")]
        //public string Question3 { get; set; }
       

        //[Required]
        //[Display(Name = "answer")]
        //public string Answer1 { get; set; }

        //[Required]
        //[Display(Name = "answer")]
        //public string Answer2 { get; set; }

        //[Required]
        //[Display(Name = "answer")]
        //public string Answer3 { get; set; }
    }
}
