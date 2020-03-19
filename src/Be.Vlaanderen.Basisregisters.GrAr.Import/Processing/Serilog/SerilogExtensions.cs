using System.Net;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace Serilog
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration SendGridSmtp(this LoggerSinkConfiguration sinkConfiguration, string apiKey, string subject, string fromEmail, string toEmail, LogEventLevel restrictedToMinimumLevel, string smtpServer = "smtp.sendgrid.net", int port = 465, bool enableSsl = true)
        {
            return
                sinkConfiguration
                    .Email
                    (
                        new EmailConnectionInfo
                        {
                            MailServer = smtpServer,
                            NetworkCredentials = new NetworkCredential(userName: "apikey", password: apiKey),
                            EmailSubject = subject,
                            FromEmail = fromEmail,
                            ToEmail = toEmail,
                            EnableSsl = enableSsl,
                            Port = port
                        },
                        restrictedToMinimumLevel: restrictedToMinimumLevel
                    );
        }
    }
}
