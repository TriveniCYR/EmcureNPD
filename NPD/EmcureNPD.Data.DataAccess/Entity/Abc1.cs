using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class Abc1
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public string ForgotPasswordToken { get; set; }
        public DateTime? ForgotPasswordDateTime { get; set; }
        public bool? IsManagement { get; set; }
        public bool? Apiuser { get; set; }
        public bool? FormulationGl { get; set; }
        public bool? AnalyticalGl { get; set; }
        public string DesignationName { get; set; }
    }
}
