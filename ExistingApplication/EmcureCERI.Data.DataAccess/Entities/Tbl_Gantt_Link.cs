using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Gantt_Link
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public Int64 DRFID { get; set; }
        //[MaxLength(1)]
        public string Type { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
    }
}
