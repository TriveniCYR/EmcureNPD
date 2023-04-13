using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterUserResetPasswordEntity
    {
        public int UserId { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Master))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "A password must have Minimum 8, " +
            "Maximum 16 characters,at least one number,at least one upper case,at least one lower case," +
            "at least one special character e.g. @$!%*?&")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password and Confirm Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassowrd { get; set; }

        public string ForgotPasswordToken { get; set; }
    }
}
