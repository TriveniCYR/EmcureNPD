using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class TblWishList
    {
        public long WishListId { get; set; }
        public int? WishListTypeId { get; set; }
        public int? GeographyId { get; set; }
        public int? CountryId { get; set; }
        public string MoleculeName { get; set; }
        public string Strength { get; set; }
        public string IsInhouseOrInLicensed { get; set; }
        public DateTime? DateOfFiling { get; set; }
        public DateTime? DateOfApproval { get; set; }
        public string NameofVendor { get; set; }
        public string VendorEvaluationRemark { get; set; }
        public string ReferenceDrugProduct { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
    }
}
