﻿using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterPackagingTypeEntity
    {
        public int PackagingTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "PackagingTypeName", ResourceType = typeof(Master))]
		[RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot contain only spaces.")]
		public string PackagingTypeName { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
