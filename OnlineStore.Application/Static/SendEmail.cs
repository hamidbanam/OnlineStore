using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Static
{
    public class SendEmail
    {
        public static void Send(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress("eshop.co.1402@gmail.com", "فروشگاه منطقه آزاد انزلی");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("eshop.co.1402@gmail.com", "pblz hiwa lduj zgmj");

            SmtpServer.Send(mail);

        }
    }
}
