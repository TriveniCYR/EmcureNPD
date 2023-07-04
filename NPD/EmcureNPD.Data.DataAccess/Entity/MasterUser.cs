﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUser
    {
        public MasterUser()
        {
            MasterNotificationUsers = new HashSet<MasterNotificationUser>();
            MasterUserBusinessUnitMappings = new HashSet<MasterUserBusinessUnitMapping>();
            MasterUserCountryMappings = new HashSet<MasterUserCountryMapping>();
            MasterUserDepartmentMappings = new HashSet<MasterUserDepartmentMapping>();
            MasterUserRegionMappings = new HashSet<MasterUserRegionMapping>();
            PidfPbfGeneralAnalyticalGls = new HashSet<PidfPbfGeneral>();
            PidfPbfGeneralFormulationGls = new HashSet<PidfPbfGeneral>();
            ProjectTasks = new HashSet<ProjectTask>();
            TblSessionManagers = new HashSet<TblSessionManager>();
            UserSessionLogMasters = new HashSet<UserSessionLogMaster>();
        }

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
        public int? MobileCountryId { get; set; }
        public bool? ApigroupLeader { get; set; }

        public virtual MasterRole Role { get; set; }
        public virtual ICollection<MasterNotificationUser> MasterNotificationUsers { get; set; }
        public virtual ICollection<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual ICollection<MasterUserCountryMapping> MasterUserCountryMappings { get; set; }
        public virtual ICollection<MasterUserDepartmentMapping> MasterUserDepartmentMappings { get; set; }
        public virtual ICollection<MasterUserRegionMapping> MasterUserRegionMappings { get; set; }
        public virtual ICollection<PidfPbfGeneral> PidfPbfGeneralAnalyticalGls { get; set; }
        public virtual ICollection<PidfPbfGeneral> PidfPbfGeneralFormulationGls { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<TblSessionManager> TblSessionManagers { get; set; }
        public virtual ICollection<UserSessionLogMaster> UserSessionLogMasters { get; set; }
    }
}
