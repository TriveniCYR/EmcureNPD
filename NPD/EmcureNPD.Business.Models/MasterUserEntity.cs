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
    public class MasterUserEntity
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "FullName", ResourceType = typeof(Master))]
        public string FullName { get; set; }
        
        [Display(Name = "Active", ResourceType = typeof(Master))]
        public bool IsActive { get; set; }
        [Display(Name = "Management")]
        public bool IsManagement { get; set; }

        public int[] DepartmentId { get; set; }
               
        public int[] BusinessUnitId { get; set; }
               
        public int[] CountryId { get; set; }
        public int[] RegionId { get; set; }
        public string DepartmentIds { get; set; }
        public string BusinessUnitIds { get; set; }
        public string CountryIds { get; set; }
        public string RegionIds { get; set; }
        //-------Start------New Columns Added to User List------
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public string BusinessUnitName { get; set; }
        //-------End------New Columns Added to User List------

        public int RoleId { get; set; }

        [Display(Name = "EmailAddress", ResourceType = typeof(Master))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[Remote("CheckEmailAddressExists", "User", ErrorMessage = "Email Address already exists in database.")]
        public string EmailAddress { get; set; }

        [Display(Name = "MobileNumber", ResourceType = typeof(Master))]
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile Number should be in 10 digit format")]
        public string MobileNumber { get; set; }


        [Display(Name = "Password", ResourceType = typeof(Master))]
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password and Confirm Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassowrd { get; set; }


        [Display(Name = "Address", ResourceType = typeof(Master))]
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string Address { get; set; }
        public int LoggedUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        //public List<MasterUserEntity> Users { get; set; }
        //public List<SelectListItem> Departments { get; set; }
        //public List<SelectListItem> Roles { get; set; }
        //public MasterBusinessUnitEntity MasterBusinessUnitEntity { get; set; }
        //public MasterCountryEntity MasterCountryEntity { get; set; }

    }
}
