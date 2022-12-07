namespace SchoolLMS.Web.Infrastruture.Email
{
    public interface IEmailServices
    {
        public void SendEmail(string email, string token, string name);
    }
}