using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model;
using System.Net.Mail;

namespace VastVoyages.Service
{
    public class EmailService
    {
        /// <summary>
        /// Send email to employees
        /// </summary>
        /// <param name="email"></param>
        public void SendNotificationEmail(Email email)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(email.mailTo);
            mailMessage.From = new MailAddress(email.mailFrom);
            mailMessage.Subject = email.subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = email.body;

            SmtpClient smtpClient = new SmtpClient("localhost"); // network host name in web.config
            smtpClient.Send(mailMessage);

            return;
        }

    }
}
