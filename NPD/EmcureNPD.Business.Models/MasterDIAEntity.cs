﻿using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterDIAEntity
    {
        public int DIAId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "DIAName", ResourceType = typeof(Master))]
        public string DIAName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
