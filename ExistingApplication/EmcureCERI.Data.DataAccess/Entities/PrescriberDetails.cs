using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class PrescriberDetails
    {
        public int Id { get; set; }
        public int? AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentID { get; set; }
        public int? GeographyId { get; set; }
        public int? CountryId { get; set; }
        public bool IsDoctorPharmacist { get; set; }
        public int? SpecializationId { get; set; }
        public string OtherSpecialization { get; set; }
        public string GmcgpHcnumber { get; set; }
        public string HospitalAddress { get; set; }
        public string ContactAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string UniqueId { get; set; }
        public int? CompanyID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
