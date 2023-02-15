using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class TBL_Initialization_List
    {

       
            
            public int InitializationID { get; set; }
            public string DRFNo { get; set; }
            public Nullable<int> CountryID { get; set; }
            public string GenericName { get; set; }

        
            public Int64 SrNo { get; set; }
            public string Pharmaceutical_Form { get; set; }
            public string Strength_Name { get; set; }
            public string Country { get; set; }
           
             public string Partner { get; set; }

                  
     
        
            public string Status { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public Nullable<int> Createdby { get; set; }
            public string CreatedDate { get; set; }
            public Nullable<int> Modifiedby { get; set; }
            public DateTime ModifiedDate { get; set; }
            public string CountryName { get; set; }
          

            public string FormName { get; set; }
            public string StrengthName { get; set; }
      

       
           

        //sonali add here Report timeline properties------------
        public string DRFCreatedDate { get; set; }
        public string CMNNameApprovedDate { get; set; }
        public string CM_Approved_DayHours { get; set; }
        public string LMApprovedDate { get; set; }
        public string LM_Approved_DayHours { get; set; }
        public string HODApprovedDate { get; set; }
        public string HOD_Approved_DayHours { get; set; }
        public string IP_Approved { get; set; }
        public string After1stApproved_IPDept_DayHourse { get; set; }
        public string Manufacturing_Approved { get; set; }
        public string After1stApproved_ManufacturingDept_DayHourse { get; set; }
        public string SCM_Approved { get; set; }
        public string After1stApproved_SCMDept_DayHourse { get; set; }
        public string RA_Approved { get; set; }
        public string After1stApproved_RADept_DayHourse { get; set; }
        public string Medical_Approved { get; set; }
        public string After1stApproved_MedicalDept_DayHourse { get; set; }
        public string Region { get; set; }
        public string Finance_Approved { get; set; }
        public string After1stApproved_FinanceialDept_DayHourse { get; set; }


        public string ManufacturingSite { get; set; }
        public string ManufacturingSiteName { get; set; }

        public string Batchsize { get; set; }
        public string Dimensions { get; set; }
        public decimal PackorShipper { get; set; }
        public decimal GrossWeight { get; set; }
     
        public int Leadtime { get; set; }
    }

    public class TBL_Initialization_ListForManF
    {



        public int InitializationID { get; set; }
        public string Drfno { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string GenericName { get; set; }


        public Int64 SrNo { get; set; }
        public string Pharmaceutical_Form { get; set; }
        public string Strength_Name { get; set; }
        public string Country { get; set; }
        public string InHouse_or_third_Party { get; set; }

        public string Project_Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Createdby { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
     

        //public string FormName { get; set; }
     
        public string ManufacturingSite { get; set; }
        public string ManufacturingSiteName { get; set; }
        public string Api_Name { get; set; }
        public string Apisite_Name { get; set; }

        public string Batchsize { get; set; }
        public string Dimensions_Name { get; set; }
        public string Mos { get; set; }
        public decimal PackorShipper { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal MHeight { get; set; }

        public decimal MWidth { get; set; }

        public decimal MLength { get; set; }
        public decimal Fcastunit { get; set; }
        public decimal TotalShipper { get; set; }

        public decimal Total_vol_wt { get; set; }
        public decimal TotalGr_Wt { get; set; }
        public decimal TotalCBM { get; set; }
        public int Leadtime { get; set; }
    }
}
