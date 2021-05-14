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

        public void SendReviewReminders(Email email)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(email.mailTo);
            mailMessage.From = new MailAddress(email.mailFrom);
            mailMessage.Subject = email.subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = email.body;

            if (email.cc != null &&  (email.cc).Count() > 0)
            {
                //string[] ccString = (email.cc).Split(',');

                foreach (string cc in email.cc.Select(r => String.Join(";", r.Split(',', ';'))).ToList())
                {
                    mailMessage.CC.Add(cc);
                }
            }

            //mailMessage.CC.Add("Charles@VastVoyages.ca");

            SmtpClient smtpClient = new SmtpClient("localhost");
            smtpClient.Send(mailMessage);

            return;
        }
    }
}
