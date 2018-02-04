using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Messaging.SMTP
{
    /// <summary>
    /// SMTP Server configuration parameters.
    /// </summary>
    public sealed class SMTPServerSettings
    {
        /// <summary>
        /// SMTP Port. EG: 25/587
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Sender email to be used as the person who dispatched the email.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// SMTP host address.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP Email User.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// SMTP Email Password.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// If true means the server should works with SSL protocol.
        /// </summary>
        public bool SSLEnabled { get; set; }
    }
}
