using EmcureNPD.Resource.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "EmailValid")]

        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Business Unit Required.")]
        //public int BusinessUnitId { get; set; }
        //public List<MasterBusinessUnitEntity> masterBusinessUnitEntities { get; set; }
    }
}