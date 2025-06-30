using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class NotificationEmailService : INotificationEmailService
    {
        private readonly SmtpClient _smtpClient;

        public NotificationEmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public void SendLowStockerAlert(string emailAddress, string productName)
        {
            var mailMessage = new MailMessage("noreply@stockapp.com", emailAddress)
            {
                Subject = "Low Stock Alert",
                Body = $"The product {productName} is low on stock."
            };
            _smtpClient.Send(mailMessage);
        }
    }
}
