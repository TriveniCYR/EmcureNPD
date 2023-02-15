using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_FinanceDetails
    {
        public int Id { get; set; }
        public bool Overallbusinesscase { get; set; }
        public decimal Exworks { get; set; }
        public decimal GCminimum { get; set; }
        public Nullable<int> Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InitializationID { get; set; }
        public decimal? ExworksYearTwo { get; set; }
        public decimal? GCminimumYearTwo { get; set; }
        public decimal? ExworksYearThree { get; set; }
        public decimal? GCminimumYearThree { get; set; }

        public decimal? Expenses { get; set; }
        public decimal? FilingCost { get; set; }
        public decimal? TotalContribution { get; set; }
        public decimal? TotalPercentage { get; set; }
        public decimal? NetContribution { get; set; } 
        public decimal? NetPercentage { get; set; }

        public decimal? Freight { get; set; }
        public decimal? FreightYearTwo { get; set; }
        public decimal? FreightYearThree { get; set; }

        public decimal? LitigationCost { get; set; }
        public decimal? FreightCost { get; set; }
        public decimal? RegistrationCost { get; set; }
        public decimal? ConsultantCost { get; set; }
        public decimal? LegalizationCost { get; set; }
        public decimal? TranslationCost { get; set; }
        public decimal? OtherCost { get; set; }
        public decimal? BECost { get; set; }
        public decimal? BioCost { get; set; }
        public decimal? CTCost { get; set; }
    }
}
