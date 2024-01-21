using Emreraslan.Services.Abstract;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using System;

namespace Emreraslan.Services.Concrete
{
    public class MailService : IMailService
    {
        public void SendMail(string aliciMail, string konu, string title)
        {
            using (var mail = new MailMessage())
            using (var client = new SmtpClient())
            {
                mail.From = new MailAddress("");
                mail.To.Add(aliciMail);
                mail.Subject = title;
               
                mail.Body = konu;
                mail.IsBodyHtml = true;

                client.Host = "win4.wlsrv.com";
                client.Port = 587;
                client.EnableSsl = false;

                client.Credentials = new NetworkCredential("", "");

                client.Send(mail);


            }

        }
    }
}
