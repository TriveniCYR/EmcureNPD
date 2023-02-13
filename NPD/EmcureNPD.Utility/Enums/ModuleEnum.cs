using System.ComponentModel;

namespace EmcureNPD.Utility.Enums
{
    public enum ModuleEnum
    {
        UserManagement = 1,
        RoleManagement = 2,
        Dashboard = 3,
        MasterManagement = 4,
        PIDF = 5,
        PBF = 6,
        IPD = 7,
        Finance = 8,
        Medical = 9,
        APIListManagement = 10,
        Formulation = 11,
        ManagementHOD = 12    
    }

    public enum ModulePermissionEnum
    {
        [Description("User Management")]
        UserManagement = 1,
        [Description("Role Management")]
        RoleManagement = 2,
        Dashboard = 3,
        [Description("Master Management")]
        MasterManagement = 4,
        PIDF = 5,
        PBF = 6,
        IPD = 7,
        Finance = 8,
        Medical = 9,


        [Description("API List Management")]
        APIListManagement = 13,
        [Description("Audit logs")]
        Auditlogs = 14,
        Commercial = 15,
        ManagementHOD = 16,
        Project = 17
        //Formulation = 11,
        //Rnd= 12,
        //Clinical= 13,
        //Analytical = 14,
        //[Description("API RnD")]
        //APIRnd = 15,
        //[Description("Dossier Management")]
        //Dossier = 17,
        //[Description("API IPD")]
        //APIIpd = 15,
        //[Description("API Charter")]
        //APICharter = 15,
        //[Description("PBF RnD")]
        //PBFRnd = 15,
    }
}
