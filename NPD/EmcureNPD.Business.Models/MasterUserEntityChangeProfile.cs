using EmcureNPD.Resource.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterUserEntityChangeProfile
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "FullName", ResourceType = typeof(Master))]
        public string FullName { get; set; }        
       

        [Display(Name = "MobileNumber", ResourceType = typeof(Master))]
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile Number should be in 10 digit format")]
        public string MobileNumber { get; set; }
        public int MobileCountryId { get; set; }
        
        [Display(Name = "Address", ResourceType = typeof(Master))]
        public string Address { get; set; }
      
    }
}
