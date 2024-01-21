namespace Emreraslan.Services.Abstract
{
    public interface IMailService
    {
        void SendMail(string aliciMail, string konu, string title);
    }
}
