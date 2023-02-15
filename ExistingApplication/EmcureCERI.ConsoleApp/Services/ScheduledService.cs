using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Data.Repository;
using EmcureCERI.ConsoleApp.Helper;
using EmcureCERI.ConsoleApp.Models;
using System;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract;
using Microsoft.Extensions.DependencyInjection;
using EmcureCERI.Business.Core;
using EmcureCERI.Business.Models.DataModel;

namespace EmcureCERI.ConsoleApp.Services
{
    public class ScheduledService : IScheduledService
    {
        public Task SendEmails()
        {
            Console.WriteLine("SendEmails" + DateTime.Now.ToString());
            // Execute your scheduled task here
            using (EmcureCERIDBContext db = new EmcureCERIDBContext())
            {
                var questionnaires = db.Questionnaire2;
                foreach (var item in questionnaires)
                {
                    if (item.Dob.HasValue)
                    {
                        DateTime dob = Convert.ToDateTime(item.Dob.Value);
                        AgeVM text = CalculateYourAge(dob);
                        int age = CalculateAge(dob);
                        bool checker = item.IsAdult.HasValue ? item.IsAdult.Value : false;

                        if (text.Years >= 18 && !checker)
                        {
                            var baselines = db.BaselineDataMaster;
                            foreach (var baseline in baselines)
                            {
                                if (baseline.Quest2 == item.Id)
                                {
                                    int patientId = baseline.PatientId.HasValue ? baseline.PatientId.Value : 0;
                                    if (patientId != 0)
                                    {
                                        AdminObject adminObj = GetAdmin();
                                        PatientPrescriverObject obj = GetPatientPrescriberByPatientId(patientId);
                                        EmailHelper email = new EmailHelper();

                                        var ContactTo = "PVUK@tillomed.co.uk";

                                        //Reject Patient ICF Form
                                        using (EmcureCERIDBContext _db = new EmcureCERIDBContext())
                                        {
                                            PatientDetails model = _db.PatientDetails.Find(patientId);
                                            model.IsConsentFcheckByAdmin = false;
                                            model.IsConsentFcheckByHcp = false;
                                            model.IsStatus = false;
                                            model.RejectionReason = "Because patient age turn to 18 so requried his own sign of ICF from";
                                            _db.PatientDetails.Update(model);
                                            _db.SaveChanges();
                                        }
                                        

                                        //Below Mail send to admin and prescriber when patient turn 18 year old, 
                                        //send every day mail whenever presciber upload a ICF document with patient sign 
                                        //Send Mail to Admin
                                        string emailbodyA = GetMessageBodyForAdmin(adminObj.FullName, obj.PrescriberFullName, obj.PatientFullName, ContactTo, text.TurnIn18);
                                        email.SendMail(adminObj.Email, "", "The Patient " + obj.PatientFullName + " Turn 18 Over Course Of Study", emailbodyA);
                                        //Send Mail to Prescriber
                                        string emailbodyP = GetMessageBodyForPrescriber(adminObj.FullName, obj.PrescriberFullName, obj.PatientFullName, ContactTo, text.TurnIn18);
                                        email.SendMail(obj.PrescriberEmail, "", "The Patient " + obj.PatientFullName + " Turn 18 Over Course Of Study", emailbodyP);

                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public AgeVM CalculateYourAge(DateTime Dob)
        {

            AgeVM ageVM = new AgeVM();

            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            ageVM.TurnIn18 = Dob.AddYears(18).ToShortDateString();
            ageVM.Years = Years;
            ageVM.Months = Months;
            ageVM.Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            ageVM.Hours = Now.Subtract(PastYearDate).Hours;
            ageVM.Minutes = Now.Subtract(PastYearDate).Minutes;
            ageVM.Seconds = Now.Subtract(PastYearDate).Seconds;
            return ageVM;
        }




        public AdminObject GetAdmin()
        {
            using (EmcureCERIDBContext _db = new EmcureCERIDBContext())
            {
                AdminObject model = new AdminObject();
                int adminId = 1; //Admin Id
                foreach (var adminObj in _db.AspNetUsers)
                {
                    if (adminObj.UserId == adminId)
                    {
                        foreach (var adminDetail in _db.PrescriberDetails)
                        {
                            if (adminDetail.AspNetUserId == adminObj.UserId)
                            {
                                model.Email = adminObj.Email;
                                model.FullName = adminDetail.FirstName + " " + adminDetail.LastName;
                            }
                        }
                    }
                }
                return model;
            }
        }

        public PatientPrescriverObject GetPatientPrescriberByPatientId(int Id)
        {
            using (EmcureCERIDBContext _db = new EmcureCERIDBContext())
            {
                PatientPrescriverObject model = new PatientPrescriverObject();
                var patient = _db.PatientDetails.Find(Id);
                if (patient != null)
                {
                    model.PatientFullName = patient.FirstName + " " + patient.LastName;
                    foreach (var prescriber in _db.AspNetUsers)
                    {
                        if (prescriber.UserId == patient.AspNetUserId)
                        {
                            foreach (var presciberObj in _db.PrescriberDetails)
                            {
                                if (presciberObj.AspNetUserId == patient.AspNetUserId)
                                {
                                    model.PrescriberEmail = prescriber.Email;
                                    model.PrescriberFullName = presciberObj.FirstName + " " + presciberObj.LastName;
                                }
                            }
                        }
                    }
                }
                return model;
            }
        }

        public string GetMessageBodyForAdmin(string adminName, string prescriberName, string patientName, string ContactTo, string TurnIn18)
        {
            string response = "<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'><div style='margin:0px auto 0;max-width:640px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'><div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody></table></div><div style='margin:0px auto;max-width:640px;background:#ffffff;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'><div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'><table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'><tbody><tr><td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'><div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'><h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + adminName + ",</h2><p>Patient (" + patientName + "), for whom consent to participate in the study was given by their Guardian, will be turning 18 on " + TurnIn18 + ". We kindly request, you to re-validate " + patientName + "'s patient information sheet and informed consent form(ICF) by themselves to continue to participate in the study. We kindly request, therefore, that following their birthday, please follow up with " + prescriberName + ", requesting him to get a sign on new patient information sheet and informed consent form(ICF) for the patient to continue to participate in the study.</p><p>Please notify Dr. " + prescriberName + " that, he/she will not be able to enter any further follow-up data for " + patientName + " from " + TurnIn18 + ", until the new ICF has been uploaded.</p><p>If you have any queries or concerns, please kindly contact us at <a href='mailto:" + ContactTo + "' target='_top'>" + ContactTo + "</a> or on 01480 402400.</p><p>Many Thanks</p><br><p style='margin-bottom:5px;'>Best Regards, </p><p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p></div></td></tr></tbody></table></div></td></tr></tbody></table></div></div></div></body></table></div></td></tr></tbody></table></div></div></div></body>";
            return response;
        }

        public string GetMessageBodyForPrescriber(string adminName, string prescriberName, string patientName, string ContactTo, string TurnIn18)
        {
            string response = "<body style='background: #ebebeb;'> <div style='max-width:550px;margin:35px auto 0; padding:15px;'> <div style='width:100%;box-shadow:0px 1px 5px rgba(44,80,102,0.5);border-radius:4px;overflow:hidden'><div style='margin:0px auto 0;max-width:640px;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%; background-color: #059a9d;'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:35px 15px'><div style='cursor:auto;color:white;font-family:sans-serif;font-size:32px;font-weight:600;line-height:30px;text-align:center;'>Welcome to <span style='font-family:times'>Tillomed Laboratories</span></div></td></tr></tbody></table></div><div style='margin:0px auto;max-width:640px;background:#ffffff;'><table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#ffffff;' align='center' border='0'><tbody><tr><td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:15px 25px 20px;'><div aria-labelledby='mj-column-per-100' class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;max-width: 450px;'><table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'><tbody><tr><td style='word-break:break-word;font-size:0px;padding:0px 0px 10px;' align='left'><div style='cursor:auto;color:#737F8D;font-family:sans-serif;font-size:16px;line-height:24px;text-align:left;'><h2 style='font-family: sans-serif;font-weight: 500;font-size: 20px;color: #4F545C;letter-spacing: 0.27px;'>Dear " + prescriberName + ",</h2><p>Patient (" + patientName + "), for whom consent to participate in the study was given by their Guardian, will be turning 18 on " + TurnIn18 + ". We kindly request, therefore, that following their birthday, you ask the patient to sign a new patient information sheet and informed consent form(ICF) themselves to continue to participate in the study.We kindly request, therefore, that following their birthday, you ask the patient to sign a new patient information sheet and informed consent form(ICF) themselves to continue to participate in the study.</p><p>Please note that you will not be able to enter any further follow-up data for this patient from " + TurnIn18 + " until the new ICF has been uploaded.</p><p>If you have any queries or concerns, please kindly contact us at <a href='mailto:" + ContactTo + "' target='_top'>" + ContactTo + "</a> or on 01480 402400.</p><p>Many Thanks</p><br><p style='margin-bottom:5px;'>Best Regards, </p><p style='margin-top:5px; margin-bottom:0;'>Team @ Tillomed Laboratoriess.</p></div></td></tr></tbody></table></div></td></tr></tbody></table></div></div></div></body></table></div></td></tr></tbody></table></div></div></div></body>";
            return response;
        }
        
        
    }
}
