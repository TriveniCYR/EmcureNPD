using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmcureNPD.Business.Models
{
    public class MasterTestTypeEntity
    {
            public int TestTypeId { get; set; }
            [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
            [Display(Name = "TestTypeName", ResourceType = typeof(Master))]
            [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
            public string TestTypeName { get; set; }
            [Display(Name = "Active", ResourceType = typeof(Master))]
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
    }
}
