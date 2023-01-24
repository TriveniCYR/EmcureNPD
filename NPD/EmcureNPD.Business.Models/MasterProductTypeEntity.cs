﻿using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterProductTypeEntity
    {
        public int ProductTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "ProductTypeName", ResourceType = typeof(Master))]
        public string ProductTypeName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
