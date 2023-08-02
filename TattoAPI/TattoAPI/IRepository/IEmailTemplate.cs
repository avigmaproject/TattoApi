using TattoAPI.Models.Avigma;

namespace TattoAPI.IRepository
{
    public interface IEmailTemplate
    {
        public List<dynamic> SendRequestPassword(UserLogin userLogin);
        public List<dynamic> VerifiedRegistration(EmailDTO model, int OTP);
        public List<dynamic> NewBusinessRegister(EmailDTO model, string Buss_Name, string Buss_Number, string Buss_Address);
        public List<dynamic> BusinessActive(EmailDTO model, string Buss_Name, string Buss_Number, string Buss_Address);
        public String GetEmailMessageText(string changePasswordLink, string Name, string Email, int Type);
        public List<dynamic> NewUserRegister(EmailDTO model, string mess, int Type);
    }
}
