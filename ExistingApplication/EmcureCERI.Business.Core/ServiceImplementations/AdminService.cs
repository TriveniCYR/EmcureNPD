namespace EmcureCERI.Business.Core
{
    using EmcureCERI.Business.Contract;
    using EmcureCERI.Business.Core.ServiceImplementations;
    using EmcureCERI.Business.Models.DataModel;
    using EmcureCERI.Data.DataAccess.Entities;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;

    public class AdminService : IAdminService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _configuration;
        private readonly IPatientService _patient;

        #region Default Construtor

        public AdminService(IConfiguration configuration, IPatientService patient)
        {
            _db = new EmcureCERIDBContext();
            _configuration = configuration;
            _patient = patient;
        }

        #endregion

        public AdminObject GetAdmin()
        {
            AdminObject model = new AdminObject();
            int adminId = Convert.ToInt32(_configuration["AdminId"].ToString());
            var adminObj = _db.AspNetUsers.Where(o => o.UserId == adminId).FirstOrDefault();
            var adminDetail = _db.PrescriberDetails.Where(o => o.AspNetUserId == adminObj.UserId).FirstOrDefault();
            model.Email = adminObj.Email;
            model.FullName = adminDetail.FirstName + " " + adminDetail.LastName;
            return model;
        }

        public PatientPrescriverObject GetPatientPrescriberByPatientId(int Id)
        {
            PatientPrescriverObject model = new PatientPrescriverObject();
            var patient = _patient.GetPatient(Id);
            if (patient != null) {
                model.PatientFullName = patient.FirstName + " " + patient.LastName;
                var prescriber = _db.AspNetUsers.Where(o => o.UserId == patient.AspNetUserId).FirstOrDefault();
                var presciberObj = _db.PrescriberDetails.Where(o => o.AspNetUserId == patient.AspNetUserId).FirstOrDefault();
                model.PrescriberEmail = prescriber.Email;
                model.PrescriberFullName = presciberObj.FirstName + " " + presciberObj.LastName;
            }
            return model;
        }

        public PrescriverObject GetPrescriber(int Id) {
            PrescriverObject model = new PrescriverObject();
            var prescriberDetail = _db.PrescriberDetails.Where(o => o.AspNetUserId == Id).FirstOrDefault();
            model.FullName = prescriberDetail.FirstName + " " + prescriberDetail.LastName;
            return model;
        }
    }
}
