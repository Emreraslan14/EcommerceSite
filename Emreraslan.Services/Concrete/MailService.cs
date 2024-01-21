using Emreraslan.Core.Dtos;
using Emreraslan.Services.Abstract;
using System.Net;
using System.Net.Mail;

namespace Emreraslan.Services.Concrete
{
    public class MailService : IMailService
    {
        public void SendMail(ContactUsDto dto)
        {
            try
            {
                using (var mail = new MailMessage())
                using (var client = new SmtpClient())
                {
                    mail.From = new MailAddress("andShop@emreraslan.com");
                    mail.To.Add("eraslan1494@outlook.com");
                    mail.Subject = dto.Subject;

                    mail.Body = "Mail : " + dto.Email + "<br/> Phone : " + dto.Phone + "<br/> Name : " + dto.Name + "<br/> Message : " + dto.Message;
                    mail.IsBodyHtml = true;

                    client.Host = "89.252.183.170";
                    client.Port = 587;
                    client.EnableSsl = false;

                    client.Credentials = new NetworkCredential("andShop@emreraslan.com", "d9x&kW180");

                    client.Send(mail);


                }
            }
            catch(Exception e)
            {

            }

        }
    }
}
