using Emreraslan.Core.Dtos;

namespace Emreraslan.Services.Abstract
{
    public interface IMailService
    {
        void SendMail(ContactUsDto dto);
    }
}
