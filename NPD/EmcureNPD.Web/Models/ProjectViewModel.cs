using System.Collections.Generic;

namespace EmcureNPD.Web.Models
{
    public class ProjectViewModel
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProjectsView
    {
        public List<Projects> table { get; set; }
        public List<StrengthView> table1 { get; set; }
        public List<ManagerView> table2 { get; set; }
    }

    public class Projects
    {
        public string projectName { get; set; }
    }

    public class StrengthView
    {
        public string strength { get; set; }
    }

    public class ManagerView
    {
        public string managerName { get; set; }
    }


}
