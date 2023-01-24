using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Utility.Models
{
    public class RolePermissionModel
    {
        public int RoleModuleId { get; set; }
        public int RoleId { get; set; }
        public int MainModuleId { get; set; }
        public int SubModuleId { get; set; }
        public string MainModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string ControlName { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approve { get; set; }
    }

}
