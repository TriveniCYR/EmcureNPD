using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.DRFViewModels
{
    public class DRFViewModel
    {
        public int? Id { get; set; }

        public string ProductId { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API Name")]
        public string APIName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API Vendor")]
        public string APIVendor { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API Volume")] /*required for batches supplied for 3 years*/
        public string APIVolume { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strength")]
        public string Strength { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Registration Date ")]/*/ Tentative Registration Date*/
        public string RegistrationDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Re-Registration Date")]/*/ Tentative Re-Registration date*/
        public string ReRegistrationDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Register Site / Plant")]
        public string RegisterPlant { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Therapeutic Category")]
        public int TherapeuticCategoryID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Manufactured")] /*Plant at which is*/
        public int ProductManufactureID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Formulation")]
        public int FormulationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Dossier Template")]
        public int DossierTemplateID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Drug Category")]
        public int DrugCategoryID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Continent")]
        public int ContinentID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Submission Checklist")]
        public string SubmissionChecklist { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "WHO-PQ (World Health Organization Filing)")]
        public bool WHOPQ { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? PIDFID { get; set; }

        public string RegistrationFeesArray { get; set; }
       
    }
    public class RegistrationFeesArray
    {
        public int ID { get; set; }
        public int Value { get; set; }
    }

    public class DRFContinentCountry
    {
        public int ContinentID { get; set; }
    }

    public class PIDFByCountryInDRF
    {
        public int CountryID { get; set; }
    }

    public class PIDFList
    {
        public int ID { get; set; }
        public string PIDFID { get; set; }
        public string NewProjectName { get; set; }
        public int ContinentID { get; set; }
        public string Continent { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
    }

    public class GetDRFAttachedPIDF
    {
        public int DRFID { get; set; }
        public int CountryID { get; set; }
    }

    public class GetDRFTaskSubTask
    {
        public int DRFID { get; set; }
    }

    public class DRFTaskSubTaskDetailsForEditDelete
    { 
        public int ProjectMappingID { get; set; }
    }

   

    public class TaskOwner
    {
        public int? EmpID { get; set; }
        public string EmpName { get; set; }
    }

    public class Priority
    {
        public int? PriorityID { get; set; }
        public string PriorityName { get; set; }
    }

    public class ProjectStatus
    {
        public int? ProjectStatusID { get; set; }
        public string Status { get; set; }
    }


    public class MainTaskList
    {
        public int? MainTaskID { get; set; }
        public string MainTaskName { get; set; }
    }

    public class DRFTaskSubTaskEditModel
    {

        public Int64 DRFEditProjectTaskMappingID { get; set; }

        public int DRFEditParentID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Name")]
        public string DRFEditTaskName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Owner")]
        public int DRFEditEmpID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Start Date")]
        public DateTime DRFEditStartDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "End Date")]
        public DateTime DRFEditEndDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Duration")]
        public int DRFEditTaskDuration { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Total Percentage")]
        [Range(0, 100)]
        public decimal DRFEditTotalPercentage { get; set; }

        public DateTime DRFEditModifiedDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Status")]
        public int DRFEditTaskStatusID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Priority")]
        public int DRFEditPriorityID { get; set; }

        public List<TaskOwner> TaskOwner { get; set; }
        public List<Priority> Priority { get; set; }
        public List<ProjectStatus> ProjectStatus { get; set; }

    }


    public class DRFTaskAddModel
    {

        // public Int64 DRFEditProjectTaskMappingID { get; set; }

        //public int DRFEditParentID { get; set; }
        public int DRFAddTaskDRFID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Name")]
        public string DRFAddTaskName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Owner")]
        public int DRFAddTaskEmpID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Start Date")]
        public DateTime DRFAddTaskStartDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "End Date")]
        public DateTime DRFAddTaskEndDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Duration")]
        public int DRFAddTaskTaskDuration { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Total Percentage")]
        [Range(0, 100)]
        public decimal DRFAddTaskTotalPercentage { get; set; }

        //public DateTime DRFEditModifiedDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Status")]
        public int DRFAddTaskTaskStatusID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Priority")]
        public int DRFAddTaskPriorityID { get; set; }
        public List<TaskOwner> TaskOwner { get; set; }
        public List<Priority> Priority { get; set; }
        public List<ProjectStatus> ProjectStatus { get; set; }

    }

    public class DRFSubTaskAddModel
    {

        // public Int64 DRFEditProjectTaskMappingID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Main Task")]
        public int DRFAddSubTaskTaskID { get; set; }

       // public int DRFAddSubTaskParentID { get; set; }

        public int DRFAddSubTaskDRFID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Sub Task")]
        public string DRFAddSubTaskName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Owner")]
        public int DRFAddSubTaskEmpID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Start Date")]
        public DateTime DRFAddSubTaskStartDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "End Date")]
        public DateTime DRFAddSubTaskEndDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Task Duration")]
        public int DRFAddSubTaskTaskDuration { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Total Percentage")]
        [Range(0, 100)]
        public decimal DRFAddSubTaskTotalPercentage { get; set; }

        //public DateTime DRFEditModifiedDate { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Status")]
        public int DRFAddSubTaskTaskStatusID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Priority")]
        public int DRFAddSubTaskTaskPriorityID { get; set; }
        public List<TaskOwner> TaskOwner { get; set; }
        public List<Priority> Priority { get; set; }
        public List<ProjectStatus> ProjectStatus { get; set; }
        public List<MainTaskList> MainTaskList { get; set; }

    }


    public class MilestoneModel
    {
        public DRFTaskAddModel DRFTaskAddModel { get; set; }

        public DRFTaskSubTaskEditModel DRFTaskSubTaskEditModel { get; set; }

        public DRFSubTaskAddModel DRFSubTaskAddModel { get; set; }

    }
}
