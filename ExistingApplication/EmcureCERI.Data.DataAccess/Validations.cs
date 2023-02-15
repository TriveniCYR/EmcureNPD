using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmcureCERI.Data.DataAccess.Entities
{

    [ModelMetadataType(typeof(BaselineDataMasterMetaData))]
    public partial class BaselineDataMaster { }

    [ModelMetadataType(typeof(PatientDetailsMetaData))]
    public partial class PatientDetails { }

    [ModelMetadataType(typeof(Questionnaire1MetaData))]
    public partial class Questionnaire1 { }

    [ModelMetadataType(typeof(Questionnaire2MetaData))]
    public partial class Questionnaire2 { }

    [ModelMetadataType(typeof(Questionnaire3MetaData))]
    public partial class Questionnaire3 { }

    [ModelMetadataType(typeof(Questionnaire4MetaData))]
    public partial class Questionnaire4 { }

    public class PatientDetailsMetaData
    {
        [Display(Name = "Informed Consent Form is verified and approved.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please verify and approve Informed Consent Form to register a patient.")]
        public bool IsConsentFcheckByAdmin { get; set; }

        public bool IsConsentFcheckByAdminNew { get; set; }

    }

    public class BaselineDataMasterMetaData
    {
        [Display(Name = "Baseline Data Form is verified and approved.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please verify and approve Baseline Data Form to register a patient.")]
        public bool IsConfirmedByAdmin { get; set; }

        public bool IsConfirmedByAdminNew { get; set; }

    }

    public class Questionnaire1MetaData
    {
        [Display(Name = "Informed Consent and Eligibility")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Icg { get; set; }

        [Required(ErrorMessage = "Required Date Participant")]
        public DateTime? DateParticipant { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? SoluInfusion { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Informedconsent { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Cmv { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Other Indication, please specify")]
        public string OtherIndication { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Hypersensitivity { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Probenecide { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? RenalInsufficiency { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Concomitant { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? DirectIntraocular { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? InductionTreatment { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? MaintenanceTreatment { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? PediatricPopulation { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? ElderlyPopulation { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? PregnantLactating { get; set; }

        [Required]
        [Display(Name = "Date of Assessment")]
        public DateTime? DateAssessment { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Icgb { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Cglrfp { get; set; }



    }

    public class Questionnaire2MetaData
    {
        [Display(Name = "Demographic Data")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "The Date of Birth field is required.")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Sex { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? MedicalHistory { get; set; }


        [Required]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Mh1anySignificant { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? Mh1startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? Mh1stopDate { get; set; }

        public bool? Mh1ongoing { get; set; }


       // [Required]
       // [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Mh2anySignificant { get; set; }

      //  [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? Mh2startDate { get; set; }

        //[Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? Mh2stopDate { get; set; }

        public bool? Mh2ongoing { get; set; }

      //  [Required]
       // [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Mh3anySignificant { get; set; }

       // [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? Mh3startDate { get; set; }

       // [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? Mh3stopDate { get; set; }

        public bool? Mh3ongoing { get; set; }

       // [Required]
       // [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Mh4anySignificant { get; set; }

       // [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? Mh4startDate { get; set; }

        //[Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? Mh4stopDate { get; set; }

        public bool? Mh4ongoing { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Medications { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M1startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M1stopDate { get; set; }

        public bool? M1ongoing { get; set; }



        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2medication { get; set; }


        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M2startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M2stopDate { get; set; }

        public bool? M2ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M3startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M3stopDate { get; set; }

        public bool? M3ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M4startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M4stopDate { get; set; }

        public bool? M4ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M5startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M5stopDate { get; set; }

        public bool? M5ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M6startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M6stopDate { get; set; }

        public bool? M6ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M7startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M7stopDate { get; set; }

        public bool? M7ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M8startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M8stopDate { get; set; }

        public bool? M8ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M9startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M9stopDate { get; set; }

        public bool? M9ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M10startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M10stopDate { get; set; }

        public bool? M10ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M11medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M11indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M11dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M11frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M11route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M11startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M11stopDate { get; set; }

        public bool? M11ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M12medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M12indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M12dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M12frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M12route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M12startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M12stopDate { get; set; }

        public bool? M12ongoing { get; set; }


        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string HaematologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string HaematologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BiochemistryA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BiochemistryC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UreaA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UreaC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string CreatinineA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string CreatinineC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string PhosphateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string PhosphateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UricAcidA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UricAcidC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BicarbonateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BicarbonateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UrineAnalysisA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string UrineAnalysisC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SerologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SerologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string OthersSpecify { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string OthersSpecifyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string OthersSpecifyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Comments { get; set; }

    }

    public class Questionnaire3MetaData
    {
        [Display(Name = "Cidofovir Treatment Initiation Details")]
        public string Heading { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Indication { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Dose { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Frequency { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Route { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Remarks { get; set; }
    }

    public class Questionnaire4MetaData
    {
        [Display(Name = "Prescriber’s Sign Off")]
        public string Heading { get; set; }

        [Required]
        public string Statement { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box")]
        public bool ConfirmBdf { get; set; }

        [Required]
        [Display(Name = "Date of Acceptance")]
        public DateTime? Doa { get; set; }
    }


    [ModelMetadataType(typeof(FollowUpFormMasterMetaData))]
    public partial class FollowUpFormMaster { }

    [ModelMetadataType(typeof(Fufquestionnaire1MetaData))]
    public partial class Fufquestionnaire1 { }

    [ModelMetadataType(typeof(Fufquestionnaire2MetaData))]
    public partial class Fufquestionnaire2 { }

    [ModelMetadataType(typeof(Fufquestionnaire3MetaData))]
    public partial class Fufquestionnaire3 { }

    [ModelMetadataType(typeof(Fufquestionnaire4MetaData))]
    public partial class Fufquestionnaire4 { }

    [ModelMetadataType(typeof(Fufquestionnaire5MetaData))]
    public partial class Fufquestionnaire5 { }

    [ModelMetadataType(typeof(Fufquestionnaire6MetaData))]
    public partial class Fufquestionnaire6 { }

    [ModelMetadataType(typeof(Fufquestionnaire7MetaData))]
    public partial class Fufquestionnaire7 { }

    public class FollowUpFormMasterMetaData { }

    public class Fufquestionnaire1MetaData
    {

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Pexperienced { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm1 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate1 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate1 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId1 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId1 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid1 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId1 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId1 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm2 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate2 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate2 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId2 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId2 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid2 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId2 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId2 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm3 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate3 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate3 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId3 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId3 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid3 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId3 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId3 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm4 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate4 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate4 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId4 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId4 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid4 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId4 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId4 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm5 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate5 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate5 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId5 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId5 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid5 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId5 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId5 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string EventTerm6 { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? StartDate6 { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? StopDate6 { get; set; }

        [Required]
        [Display(Name = "SAE")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string SaeId6 { get; set; }

        [Required]
        [Display(Name = "Con-comitant Medication given")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string ComMedId6 { get; set; }

        [Required]
        [Display(Name = "Study Drug Action")]
        public int? StudyDaid6 { get; set; }

        [Required]
        [Display(Name = "Outcome")]
        public int? OutcomeId6 { get; set; }

        [Required]
        [Display(Name = "Relationship to Study Drug")]
        public int? RelaStudyId6 { get; set; }

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? CidofovirInjection { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Specify { get; set; }

        [StringLength(1000, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Aedetails { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string PrescriberName { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? Date { get; set; }
    }

    public class Fufquestionnaire2MetaData
    {

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BhaematologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BhaematologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BbiochemistryA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BbiochemistryC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BureaA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BureaC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BcreatinineA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BcreatinineC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BphosphateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BphosphateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BuricAcidA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BuricAcidC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BbicarbonateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BbicarbonateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BurineAnalysisA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BurineAnalysisC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BserologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BserologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BothersSpecify { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BothersSpecifyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string BothersSpecifyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Bcomments { get; set; }

        [Required(ErrorMessage = "The Date of Assessment field is required.")]
        public DateTime? Bdoa { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FhaematologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FhaematologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FbiochemistryA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FbiochemistryC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FureaA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FureaC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FcreatinineA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FcreatinineC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FphosphateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FphosphateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FuricAcidA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FuricAcidC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FbicarbonateA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FbicarbonateC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FurineAnalysisA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FurineAnalysisC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FserologyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FserologyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FothersSpecify { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FothersSpecifyA { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FothersSpecifyC { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Fcomments { get; set; }

        [Required(ErrorMessage = "The Date of Assessment field is required.")]
        public DateTime? Fdoa { get; set; }
    }

    public class Fufquestionnaire3MetaData
    {

        public bool? TreatmentD { get; set; }
        public bool? Underlying { get; set; }
        public bool? TreatmentR { get; set; }
        public bool? Dop { get; set; }
        public bool? LostFollowUp { get; set; }
        public bool? Therapy { get; set; }
        public bool? Withdrawal { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string WithdrawalReason { get; set; }


        public bool? DropOut { get; set; }
        public bool? OtherOutcomes { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string OtherOutcomesSpecify { get; set; }

        [Required(ErrorMessage = "The Date of Assessment field is required.")]
        public DateTime? Doa { get; set; }
    }

    public class Fufquestionnaire4MetaData
    {

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Indication { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Dose { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Route { get; set; }

        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "The Remarks field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Remarks { get; set; }

        [Display(Name = "Total Dose Administered")]
        [Required(ErrorMessage = "The Total Dose Administered field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string TotalDose { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? DotstartDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? DotstopDate { get; set; }

        [Display(Name = "Total duration of treatment")]
        [Required(ErrorMessage = "The Total duration of treatment field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string TotalTreatment { get; set; }

        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "The Remarks field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Tremarks { get; set; }
    }

    public class Fufquestionnaire5MetaData
    {

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? MedicalHistory { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C1condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C1startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C1stopDate { get; set; }

        public bool? C1ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C2condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C2startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C2stopDate { get; set; }

        public bool? C2ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C3condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C3startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C3stopDate { get; set; }


        public bool? C3ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C4condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C4startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C4stopDate { get; set; }

        public bool? C4ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C5condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C5startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C5stopDate { get; set; }

        public bool? C5ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C6condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C6startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C6stopDate { get; set; }

        public bool? C6ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C7condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C7startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C7stopDate { get; set; }

        public bool? C7ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C8condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C8startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C8stopDate { get; set; }

        public bool? C8ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C9condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C9startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C9stopDate { get; set; }

        public bool? C9ongoing { get; set; }

        [Required(ErrorMessage = "The Medical Condition field is required.")]
        [StringLength(500, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string C10condition { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? C10startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? C10stopDate { get; set; }

        public bool? C10ongoing { get; set; }


        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Fcomments { get; set; }

        [Required(ErrorMessage = "The Date of Assessment field is required.")]
        public DateTime? Doa { get; set; }

    }

    public class Fufquestionnaire6MetaData
    {

        [Required(ErrorMessage = "You gotta tick the box!")]
        public bool? Medications { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M1route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M1startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M1stopDate { get; set; }

        public bool? M1ongoing { get; set; }



        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M2route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M2startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M2stopDate { get; set; }

        public bool? M2ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M3route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M3startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M3stopDate { get; set; }

        public bool? M3ongoing { get; set; }

        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M4route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M4startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M4stopDate { get; set; }

        public bool? M4ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M5route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M5startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M5stopDate { get; set; }

        public bool? M5ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M6route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M6startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M6stopDate { get; set; }

        public bool? M6ongoing { get; set; }



        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M7route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M7startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M7stopDate { get; set; }

        public bool? M7ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M8route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M8startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M8stopDate { get; set; }

        public bool? M8ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M9route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M9startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M9stopDate { get; set; }

        public bool? M9ongoing { get; set; }


        [Display(Name = "Medication")]
        [Required(ErrorMessage = "The Medication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10medication { get; set; }

        [Display(Name = "Indication")]
        [Required(ErrorMessage = "The Indication field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10indication { get; set; }

        [Display(Name = "Reason for use")]
        [Required(ErrorMessage = "The Reason for use field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10reason { get; set; }

        [Display(Name = "Dosage and units")]
        [Required(ErrorMessage = "The Dosage and units field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10dosageUnits { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10frequency { get; set; }

        [Display(Name = "Route")]
        [Required(ErrorMessage = "The Route field is required.")]
        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string M10route { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        public DateTime? M10startDate { get; set; }

        [Required(ErrorMessage = "The Stop Date field is required.")]
        public DateTime? M10stopDate { get; set; }

        public bool? M10ongoing { get; set; }

        [StringLength(250, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Comments { get; set; }

        [Required(ErrorMessage = "The Date of Assessment field is required.")]
        public DateTime? Doa { get; set; }

    }

    public class Fufquestionnaire7MetaData
    {
        [Required]
        public string Statement { get; set; }

        [Required]
        [Display(Name = "Prescriber Name")]
        public string PrescriberName { get; set; }

        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box")]
        public bool ConfirmFuf { get; set; }

        [Required]
        [Display(Name = "Date of Acceptance")]
        public DateTime? Doa { get; set; }
    }

}