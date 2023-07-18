using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class TblSessionManager
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public DateTime? TokenIssuedAt { get; set; }
        public DateTime? VallidTo { get; set; }
        public string UserToken { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
