using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class DRFDetails
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string APIName { get; set; }
        public string APIVendor { get; set; }
        public string APIVolume { get; set; }
        public string Strength { get; set; }
        public string ModuleName { get; set; }

        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public Nullable<System.DateTime> ReRegistrationDate { get; set; }
        public string RegisterPlant { get; set; }
        public Nullable<int> TherapeuticCategoryID { get; set; }
        public Nullable<int> ProductManufactureID { get; set; }
        public Nullable<int> FormulationID { get; set; }
        public Nullable<int> DossierTemplateID { get; set; }
        public Nullable<int> DrugCategoryID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string SubmissionChecklist { get; set; }
        public Nullable<bool> WHOPQ { get; set; }
        public Nullable<int> ProjectManagerID { get; set; }

        public Nullable<int> ProjectStatusID { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> LastProjectStatusID { get; set; }

    }
}
