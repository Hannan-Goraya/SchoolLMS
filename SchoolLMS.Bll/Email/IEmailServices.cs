namespace SchoolLMS.Bll.Email
{
    public interface IEmailServices
    {
        void SendEmail(string email, string token, string name);
    }
}