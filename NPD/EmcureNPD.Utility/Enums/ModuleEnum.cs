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
    }
    public enum SubModulePermissionEnum
    {
        RnD = 1,
        Clinical = 2,
        Analytical = 3,
        APIRnD = 4,
        APIIPD = 12,
        APICharter = 13,
        APICharterSummary = 14
    }

    public enum PermissionEnum
    {
        Any = 0,
        View = 1,
        Add = 2,
        Edit = 3,
        Delete = 4,
        Approve = 5
    }
}
