using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace School.IntegrationLayer
{
    public class EmailOTPIntegration
    {
        public void SendEmail(
            string smtpServer,
            int smtpPort,
            string smtpUsername,
            string smtpPassword,
            bool enableSsl,
            string fromEmail,
            string toEmail,
            string subject,
            string body,
            bool isBodyHtml = true,
            string[] attachments = null)
        {
            try
            {
                // Create the mail message
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isBodyHtml;

                    //mail.AlternateViews

                    // Add attachments if any
                    if (attachments != null)
                    {
                        foreach (var attachmentPath in attachments)
                        {
                            if (!string.IsNullOrEmpty(attachmentPath))
                            {
                                var attachment = new Attachment(attachmentPath, MediaTypeNames.Application.Octet);
                                mail.Attachments.Add(attachment);
                            }
                        }
                    }

                    // Configure the SMTP client
                    using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = enableSsl;

                        // Send the email
                        smtpClient.Send(mail);
                    }
                }

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                // You might want to rethrow or handle the exception differently
                throw;
            }
        }
    }

}
