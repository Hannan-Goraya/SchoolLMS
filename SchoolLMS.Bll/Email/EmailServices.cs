

using SchoolLMS.Domain.Models;
using System.Net.Mail;

namespace SchoolLMS.Bll.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly IDapperRepository _dapper;

        public EmailServices (IDapperRepository dapper)
        {

            _dapper = dapper;
        }



        private List<EmailCreadentiol> GetEmailCread()
        {
            DynamicParameters parameters = new DynamicParameters();
            return  _dapper.ReturnList<EmailCreadentiol>("GetEmailCread", parameters).ToList()  ;
            
            
        }


        





        public void SendEmail(string email, string token, string name)
        {




            var emailcread = GetEmailCread().ToArray();






            string Link = token;






            var emailTemp = emailcread[0].EmailTemp.ToString();

          

            string mailBody = string.Format(emailTemp, name ,Link );
         
            

        
            



          

      

                MailMessage message =
                    new MailMessage(new MailAddress(emailcread[0].UserName, "BitLayer"), new MailAddress(email));
                message.Subject = "otp";
                message.Body = mailBody;
                message.IsBodyHtml = true;



                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                credentials.UserName = emailcread[0].UserName;
                credentials.Password = emailcread[0].Password;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credentials;


                smtp.Send(message);
        

        }




    }
}


