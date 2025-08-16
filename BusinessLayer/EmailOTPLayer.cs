using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using School.IntegrationLayer;
//using School.DataLayer;
using System.Security.Cryptography;
using School.Models;
using School.Helper;
using School.DataLayer;

namespace School.BusinessLayer
{
    public class EmailOTPLayer
    {
        public void SendEmail(
        string toEmail,
        string subject,
        string body)
        {
            try
            {
                string SMTPServer = ConfigurationHelper.GetValue("SMTPHost");
                int SMTPPort = 587;
                string SenderEmail = ConfigurationHelper.GetValue("SenderEmail");
                string SenderPassword = ConfigurationHelper.GetValue("SenderPassword");
                EmailOTPIntegration EmailOTP = new();
                EmailOTP.SendEmail(SMTPServer, SMTPPort, SenderEmail, SenderPassword, true, SenderEmail, toEmail, subject, body, false);

                Console.WriteLine("Email sent successfully!" + SenderEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email Account : " + ex.Message);
                
            }
        }

        public string Generate6DigitOTP()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                
                byte[] randomNumber = new byte[4];
                
                rng.GetBytes(randomNumber);

                
                int otpValue = Math.Abs(BitConverter.ToInt32(randomNumber, 0));

                
                int sixDigitOTP = otpValue % 1000000;

                
                return sixDigitOTP.ToString("D6");
            }
        }

      

    }

}
