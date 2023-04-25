﻿using EmcureNPD.Resource.Resources;
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
    public class MasterPackSizeEntity
    {
        public int PackSizeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "PackSizeName", ResourceType = typeof(Master))]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
        public string PackSizeName { get; set; }
        public int PackSize { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
