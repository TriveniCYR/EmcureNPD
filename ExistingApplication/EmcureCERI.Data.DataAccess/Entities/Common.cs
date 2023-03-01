using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    class Common
    {
    }

    public class DRFTaskSubTaskOutput
    {
        public int TaskOrder { get; set; }
        public string TaskName { get; set; }
        public int SubTaskID { get; set; }
    }
    public class DRFTaskSubTaskOutputNew
    {
        public int TaskOrder { get; set; }
        public string TaskName { get; set; }
        public int SubTaskID { get; set; }
        public int? TaskDependency { get; set; }
        public int? TaskDuration { get; set; }
        public int? TaskOwnerID { get; set; }

    }

    public class DRFTaskSubTaskListOutput
    {
        public Int64 ProjectTaskMappingID { get; set; }
        public int ParentID { get; set; }
        public string TaskName { get; set; }
        public string EmpName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public decimal TotalPercentage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string TaskStatus { get; set; }
        public string Priority { get; set; }
    }

    public class UploadedFileModel
    {
        public int PIDFID { get; set; }
        public string SaveFilePath { get; set; }
        public string SaveFileName { get; set; }
    }

    public class PIDFStrengthList
    {
        public Int64 PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product strength
        public string ProductName { get; set; }//producttype
        public string PlantName { get; set; }
        public string FormulationName { get; set; }
        //public string Strength { get; set; } 
        public Int64 ParentID { get; set; }
        public string StatusID {get;set;}
        public string Status {get;set;}
        public string Action { get; set; }
    }

    public class PIDFParentStrengthList
    {
        public Int64 PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product strength
        public string ProductName { get; set; }//producttype
        public string PlantName { get; set; }
        public string FormulationName { get; set; }
        //public string Strength { get; set; } 
        //public Int64 ParentID { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public List<children> children { get; set; }

    }
    public class children
    {
        public Int64? PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product strength
        public string ProductName { get; set; }//producttype
        public string PlantName { get; set; }
        public string FormulationName { get; set; }
        //public string Strength { get; set; } 
        //public Int64 ParentID { get; set; }
        public string StatusID { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
    }

    public class PIDFCountrywiseList
    {
        public Int64 PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product continent
        public string CountryName { get; set; }
        public string PackSizeName { get; set; }
        public string PackingName { get; set; }
        public string PidfStrength { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        public Int64 ParentID { get; set; }
        public string Action { get; set; }

    }

    public class PIDFParentCountrywiseList
    {
        public Int64? PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product continent
        public string CountryName { get; set; }
        public string PackSizeName { get; set; }
        public string PackingName { get; set; }
        public string PidfStrength { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        //public Int64 ParentID { get; set; }
        public string Action { get; set; }
        public List<countryChildren> children { get; set; }

    }

    public class countryChildren
    {
        public Int64? PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } //product continent
        public string CountryName { get; set; }
        public string PackSizeName { get; set; }
        public string PackingName { get; set; }
        public string PidfStrength { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        //public Int64 ParentID { get; set; }
        public string Action { get; set; }
    }

    public class ApprovedPIDFParentList
    {
        public Int64? PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } 
        public string CountryName { get; set; }
        public string PackSizeName { get; set; }
        public string PackingName { get; set; }
        public string PidfStrength { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        public string ProjectStatus { get; set; }
        public string Action { get; set; }
        public List<AprovedPidfCountryList> children { get; set; }

    }

    public class AprovedPidfCountryList
    {
        public Int64? PIDFID { get; set; }
        public string PIDFNo { get; set; }
        public string ProjectorProductName { get; set; } 
        public string CountryName { get; set; }
        public string PackSizeName { get; set; }
        public string PackingName { get; set; }
        public string PidfStrength { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        public string ProjectStatus { get; set; }
        public string Action { get; set; }
    }


    public class ApprovedDRFList
    {
        public int InitializationID { get; set; }
        public string DRFNo { get; set; }
        public string ProjectName { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public string OrderFrequency { get; set; }
        public string CountryName { get; set; }
        public string StrengthName { get; set; }
        public string PackSizeName { get; set; }
        public string PackStyleName { get; set; }
        public string FormName { get; set; }
        public string PlantName { get; set; }
        public string IncotermsName { get; set; }
        public string MAHolder { get; set; }
        public string NameDossierSend { get; set; }
        public string ProjectManager { get; set; }
        public string Status { get; set; }
        public string DossierTemplate { get; set; }
        public string Final_Approved_Date { get; set; }
    }



    public class ParentNationalCheckList
    {
        public int TransactionID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public List<ChildrenNationalCheckList> children { get; set; }

    }

    public class ChildrenNationalCheckList
    {
        public int TransactionID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }

    }
    public class FinalApprovalDetails
    {
        public string FinalApprovalName { get; set; }
        public int? FinalCreatedBy { get; set; }
        public DateTime? FinalCreatedDate { get; set; }

    }
    public class DropdownDetails
    {
        public int DId { get; set; }
        public string DName { get; set; }
    }
    public class ProductMasterDropdownData
    {
        public string GenericName { get; set; }
    }
}
