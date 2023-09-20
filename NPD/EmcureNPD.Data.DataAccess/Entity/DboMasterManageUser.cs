using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class DboMasterManageUser
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
    }
}
