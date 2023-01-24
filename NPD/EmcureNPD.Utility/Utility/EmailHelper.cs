using EmcureNPD.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmcureNPD.Utility.Helpers
{
    public class EmailHelper
    {
        public void SendMail(string toList, string ccList, string subject, string body)
        {

            string msg = string.Empty;

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("dev.net.smtp@gmail.com");
            message.From = fromAddress;
            message.To.Add(toList);
            if (ccList != null && ccList != string.Empty)
                message.CC.Add(ccList);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;


            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = "mail.emcure.co.in";
                smtpClient.Credentials = new System.Net.NetworkCredential("smtp.gmail.com", "pass123!@#");
                smtpClient.Port = 587;//put smtp port here
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                string fileName = @"C:\Log\logger.txt";
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception e)
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes(e.ToString());
                        fs.Write(title, 0, title.Length);
                    }
                }
            }
        }

        //FOLLOWING CODE ADDED BY YOGESH B ON DATE 06/02/2020
        public async Task<bool> SendMail(SMTPDetailsVM objSMTPDetailsVM, EmailDetailsVM mailModel)
        {
            using (MailMessage mm = new MailMessage())
            {
                string DisplayName = String.IsNullOrEmpty(mailModel.DispalyName) ? "Emcure Project Management" : mailModel.DispalyName;
                mm.From = new MailAddress(objSMTPDetailsVM.FromMail, DisplayName);
                //mm.From = new MailAddress(objSMTPDetailsVM.FromMail, "Web");
                if (mailModel.ToMail.Contains(";"))
                {
                    foreach (var address in mailModel.ToMail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.To.Add(address);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mailModel.ToMail))
                    {
                        mm.To.Add(mailModel.ToMail);
                    }

                }

                mm.Subject = mailModel.Subject;
                mm.Body = mailModel.Body;
                mm.IsBodyHtml = true;

                if (mailModel.BCCMail != null && mailModel.BCCMail.Count > 0)
                {
                    foreach (string bccEmailId in mailModel.BCCMail)
                    {
                        if (!string.IsNullOrEmpty(bccEmailId))
                        {
                            mm.Bcc.Add(new MailAddress(bccEmailId));
                        }
                    }
                }

                if (mailModel.CCMail != null && mailModel.CCMail.Count > 0)
                {
                    foreach (string ccEmailId in mailModel.CCMail)
                    {
                        if (!string.IsNullOrEmpty(ccEmailId))
                        {
                            mm.CC.Add(new MailAddress(ccEmailId));
                        }
                    }
                }

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = objSMTPDetailsVM.HostName;
                    smtp.EnableSsl = objSMTPDetailsVM.IsEnableSSL;
                    NetworkCredential NetworkCred;
                    if (Convert.ToBoolean(objSMTPDetailsVM.IsWithoutPassword == true))
                    {
                        NetworkCred = new NetworkCredential();
                    }
                    else
                    {
                        NetworkCred = new NetworkCredential(objSMTPDetailsVM.FromMail, objSMTPDetailsVM.FromPassword);
                    }
                    smtp.UseDefaultCredentials = Convert.ToBoolean(objSMTPDetailsVM.IsDefaultCredentials);
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(objSMTPDetailsVM.PortNumber);
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    try
                    {
                        //smtp.Send(mm);
                        await smtp.SendMailAsync(mm);
                        LogData("Mail send successfully....");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        LogData(ex.ToString());
                        return false;
                    }
                }
            }


            //TEST EMAIL SETTINGS : DO NOT USE
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    mail.To.Add("ymbalapure@gmail.com");
            //    mail.From = new MailAddress("kriosyogeshb@gmail.com");
            //    mail.Subject = "Test";
            //    mail.Body = "Test project task updated mailbody content";
            //    mail.IsBodyHtml = true;
            //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new System.Net.NetworkCredential("kriosyogeshb@gmail.com", "kriosyogb@2346");
            //    smtp.Send(mail);
            //    return true;
            //}
            //catch(Exception ex)
            //{
            //    LogData(ex.ToString());
            //    return false;
            //}
        }

        public bool SendMail1(SMTPDetailsVM objSMTPDetailsVM, EmailDetailsVM mailModel, string wwwRootPath, bool isAttachment)
        {
            using (MailMessage mm = new MailMessage())
            {
                string DisplayName = String.IsNullOrEmpty(mailModel.DispalyName) ? "Emcure Project Management" : mailModel.DispalyName;
                mm.From = new MailAddress(objSMTPDetailsVM.FromMail, DisplayName);
                string tempEmail = "";
                //mm.From = new MailAddress(objSMTPDetailsVM.FromMail, "Web");
                if (mailModel.ToMail.Contains(";"))
                {
                    foreach (var address in mailModel.ToMail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.To.Add(address);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mailModel.ToMail))
                    {
                        mm.To.Add(mailModel.ToMail);
                        tempEmail = mailModel.ToMail;
                    }
                }

                mm.Subject = mailModel.Subject;
                mm.Body = mailModel.Body;
                mm.IsBodyHtml = true;

                if (mailModel.BCCMail != null && mailModel.BCCMail.Count > 0)
                {
                    foreach (string bccEmailId in mailModel.BCCMail)
                    {
                        if (!string.IsNullOrEmpty(bccEmailId))
                        {
                            mm.Bcc.Add(new MailAddress(bccEmailId));
                        }
                    }
                }

                if (mailModel.CCMail != null && mailModel.CCMail.Count > 0)
                {
                    foreach (string ccEmailId in mailModel.CCMail)
                    {
                        if (!string.IsNullOrEmpty(ccEmailId))
                        {
                            mm.CC.Add(new MailAddress(ccEmailId));
                        }
                    }
                }


                //Attachment
                if (isAttachment == true)
                {
                    string tempAttachFile = wwwRootPath + "/wwwroot/document/EmcureProject Documentation.pdf";
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(tempAttachFile);
                    mm.Attachments.Add(attachment);
                }


                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = objSMTPDetailsVM.HostName;
                    smtp.EnableSsl = objSMTPDetailsVM.IsEnableSSL;
                    NetworkCredential NetworkCred;
                    if (Convert.ToBoolean(objSMTPDetailsVM.IsWithoutPassword == true))
                    {
                        NetworkCred = new NetworkCredential();
                    }
                    else
                    {
                        NetworkCred = new NetworkCredential(objSMTPDetailsVM.FromMail, objSMTPDetailsVM.FromPassword);
                    }
                    smtp.UseDefaultCredentials = Convert.ToBoolean(objSMTPDetailsVM.IsDefaultCredentials);
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(objSMTPDetailsVM.PortNumber);
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    try
                    {
                        smtp.Send(mm);
                        LogData("Mail send successfully.... to : " + tempEmail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        LogData(ex.ToString());
                        return false;
                    }
                }
            }
        }

        private void LogData(string strData)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "//LogData.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    if (strData != null)
                    {
                        writer.WriteLine(strData);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}