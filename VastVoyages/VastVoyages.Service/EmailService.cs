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
        public void sendNotificationEmail(EmployeeDTO employee, PurchaseOrderDTO PO)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(employee.Email);
            mailMessage.From = new MailAddress("admin@VastVoyages.ca");
            mailMessage.Subject = "Purchase order final decision notification";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"<h2>Your purchase order is closed.</h2>" +
                               $"<p>Purchase Order Number: {PO.PONumber}</p>" +
                               $"<p>Submission Date: {PO.SubmissionDate}</p>" +
                               $"<p>Total cost: {PO.Total.ToString("C")}</p>" +
                               "<hr>";
            
            foreach(ItemDTO item in PO.items)
            {
                mailMessage.Body += $"<p>Item Name: {item.ItemName}</p>" +
                                    $"<p>Item Status: {item.ItemStatus}</p>";
            }

            mailMessage.Body += $"<p>You can view it from this link : </p><a href=\"google.ca\">Listing Link</a>";

            SmtpClient smtpClient = new SmtpClient("localhost"); // network host name in web.config
            smtpClient.Send(mailMessage);

            return;
        }

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
