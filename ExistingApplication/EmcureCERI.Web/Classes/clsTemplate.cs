using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Classes
{
    public class clsTemplate
    {

        private readonly IConfiguration _config;
        [Obsolete]
        private IHostingEnvironment _env; 
        //private IHttpContextAccessor _contextAccessor; 

        [Obsolete]
       // public clsTemplate(IConfiguration config, IHostingEnvironment env, IHttpContextAccessor contextAccessor)
        public clsTemplate(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
           // _contextAccessor = contextAccessor;
        }

        [Obsolete]
        public string CreateCommonMailBody(string strEmailBodyText, string DynamicURL, int CompanyID)
        {
            string body = string.Empty;
            string TemplatePath = string.Empty;

           // var context = _contextAccessor.HttpContext; 
           // int CompanyID = Convert.ToInt32(context.Session.GetString("CurrentUserCompanyID"));
            if (CompanyID == 2) 
            {
               TemplatePath = _env.ContentRootPath + _config.GetSection("MailTemplate:CommonNotificationgennova").Value;
            } 
            else 
            {  
                TemplatePath = _env.ContentRootPath + _config.GetSection("MailTemplate:CommonNotification").Value;
            }
            //string ApplicationUrl = _config.GetSection("ApplicationURL:UrlLink").Value;
            string ApplicationUrl = DynamicURL;
            //using streamreader for reading my htmltemplate   
            using (StreamReader reader = new StreamReader(TemplatePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{messagetext}", strEmailBodyText);
            body = body.Replace("{ApplicationURL}", ApplicationUrl);
            return body;
        }
    }
}
