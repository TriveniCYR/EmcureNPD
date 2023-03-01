using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace EmcureCERI.ConsoleApp.Helper
{
    public class EmailHelper
    {

        public void SendMail(string toList, string ccList, string subject, string body)
        {

            string msg = string.Empty;

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("admin@emcure.com");
            message.From = fromAddress;
            message.To.Add(toList);
            if (ccList != null && ccList != string.Empty)
                message.CC.Add(ccList);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;


            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = "array10.theemaillaundry.net";
                smtpClient.Port = 25;//put smtp port here
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


        //public void SendMail(string toList, string ccList, string subject, string body)
        //{
        //    string msg = string.Empty;

        //    MailMessage message = new MailMessage();
        //    MailAddress fromAddress = new MailAddress("iosrelocations123@gmail.com");
        //    message.From = fromAddress;
        //    message.To.Add(toList);
        //    if (ccList != null && ccList != string.Empty)
        //        message.CC.Add(ccList);
        //    message.Subject = subject;
        //    message.Body = body;
        //    message.IsBodyHtml = true;


        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Host = "smtp.gmail.com";
        //    smtpClient.Port = 587;//put smtp port here
        //    smtpClient.EnableSsl = true;
        //    smtpClient.UseDefaultCredentials = false;
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.Credentials = new System.Net.NetworkCredential("iosrelocations123@gmail.com", "Dahlia@123");
        //    // The userState can be any object that allows your callback  
        //    // method to identify this send operation. 
        //    // For this example, the userToken is a string constant. 
        //    string userState = "test message1";
        //    smtpClient.SendAsync(message, userState);

        //}
    }    
}