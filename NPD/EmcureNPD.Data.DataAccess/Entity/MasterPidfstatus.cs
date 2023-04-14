using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterPidfstatus
    {
        public MasterPidfstatus()
        {
            MasterNotifications = new HashSet<MasterNotification>();
            PidfLastStatuses = new HashSet<Pidf>();
            PidfManagementApprovalStatusHistories = new HashSet<PidfManagementApprovalStatusHistory>();
            PidfStatuses = new HashSet<Pidf>();
            PidfstatusHistories = new HashSet<PidfstatusHistory>();
        }

        public int PidfstatusId { get; set; }
        public string Pidfstatus { get; set; }
        public bool? IsActive { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string StatusColor { get; set; }
        public bool? IsDashboard { get; set; }
        public int? ModuleId { get; set; }

        public virtual ICollection<MasterNotification> MasterNotifications { get; set; }
        public virtual ICollection<Pidf> PidfLastStatuses { get; set; }
        public virtual ICollection<PidfManagementApprovalStatusHistory> PidfManagementApprovalStatusHistories { get; set; }
        public virtual ICollection<Pidf> PidfStatuses { get; set; }
        public virtual ICollection<PidfstatusHistory> PidfstatusHistories { get; set; }
    }
}
