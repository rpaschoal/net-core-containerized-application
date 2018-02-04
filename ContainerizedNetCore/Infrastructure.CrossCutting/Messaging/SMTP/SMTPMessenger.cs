using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Messaging.SMTP
{
    /// <summary>
    /// Utility to dispatch emails.
    /// </summary>
    public static class SMTPMessenger
    {
        /// <summary>
        /// Dispatchs an email message. Exceptions are raised to be treated and logged on consumers.
        /// </summary>
        /// <param name="settings">SMTP Server Settings. Hold basic configuration like Host, Port, etc.</param>
        /// <param name="to">E-mail address of the recipient.</param>
        /// <param name="subject">E-mail's subject title.</param>
        /// <param name="message">The E-mail's body message.</param>
        /// <param name="isBodyHTML">Defines if HTML content is present on E-mail's body.</param>
        public static void SendEmail(SMTPServerSettings settings, string to, string subject, string message, bool isBodyHTML = true)
        {
            List<MailAddress> toList = new List<MailAddress>() { new MailAddress(to) };
            SendEmail(settings, toList, subject, message, isBodyHTML);
        }

        /// <summary>
        /// Dispatchs an email message. Exceptions are raised to be treated and logged on consumers.
        /// </summary>
        /// <param name="settings">SMTP Server Settings. Hold basic configuration like Host, Port, etc.</param>
        /// <param name="to">List of MailAddresses indicating E-mail address and display names of the recipients.</param>
        /// <param name="subject">E-mail's subject title.</param>
        /// <param name="message">The E-mail's body message.</param>
        /// <param name="isBodyHTML">Defines if HTML content is present on E-mail's body.</param>
        public static void SendEmail(SMTPServerSettings settings, List<MailAddress> to, string subject, string message, bool isBodyHTML = true)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(settings.Host);

            mail.From = new System.Net.Mail.MailAddress(settings.SenderEmail);
            to.ForEach(x => mail.To.Add(x));
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = isBodyHTML;

            SmtpServer.EnableSsl = settings.SSLEnabled;
            SmtpServer.Port = settings.Port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(settings.User, settings.Password);

            SmtpServer.Send(mail);
        }
    }
}
