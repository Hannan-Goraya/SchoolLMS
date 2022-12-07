using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SchoolLMS.Web.Models.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLMS.Web.Infrastruture.Email
{

    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHost;

        public EmailServices(IConfiguration config, IWebHostEnvironment webHost)
        {

            _config = config;
            _webHost = webHost;
        }








        public void SendEmail(string email, string token, string name)
        {




            string Link = token;



            var emailCredential = _config.GetSection("EmailConfiguration");

            var data = emailCredential.Get<EmailCreadiantial>();


            var path = _webHost.ContentRootPath + Path.DirectorySeparatorChar.ToString()
                            + "Email"
                            + Path.DirectorySeparatorChar.ToString()
                            + "OtpMailTemplate.html";


            string mailHtmlbody = "";



            using (StreamReader steamReader = File.OpenText(path))
            {
                mailHtmlbody = steamReader.ReadToEnd();
            }


            string mailBody = string.Format(mailHtmlbody, name, Link);












            MailMessage message =
                new MailMessage(new MailAddress(data.UserName, "BitLayer"), new MailAddress(email));
            message.Subject = "otp";
            message.Body = mailBody;
            message.IsBodyHtml = true;



            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            credentials.UserName = data.UserName;
            credentials.Password = data.Password;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credentials;


            smtp.Send(message);


        }

    }
}
