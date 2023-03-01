using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class ProjectsModel
    {
        public List<ProjectNameModel> lsProjectName { get; set; }
         public List<ProjectStrength> lsProjectStrength { get; set; }
        public List<Manager> lsManager { get; set; }
    }
    public class ProjectNameModel
    {
        public string ProjectName { get; set; }
    }
    public class ProjectStrength
    {
        public string Strength { get; set; }
    }
    public class Manager
    {
        public string ManagerName { get; set; }
    }
}
