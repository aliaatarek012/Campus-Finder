using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Settings;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class MailingService : IMailingService
    {
            private readonly MailSettings _mailSettings;

            public MailingService(IOptions<MailSettings> mailSettings)
            {
                _mailSettings = mailSettings.Value;
            }

            public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments, string mailfrom = null)
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(mailTo))
                    throw new ArgumentException("Recipient email address is required.");
                if (string.IsNullOrWhiteSpace(subject))
                    throw new ArgumentException("Email subject is required.");
                if (string.IsNullOrWhiteSpace(body))
                    throw new ArgumentException("Email body is required.");

                var email = new MimeMessage();
                var fromAddress = mailfrom ?? _mailSettings.Email;

                // Set sender and from addresses
                email.Sender = MailboxAddress.Parse(_mailSettings.Email);
                email.From.Add(MailboxAddress.Parse(fromAddress));
                email.To.Add(MailboxAddress.Parse(mailTo));
                email.Subject = subject;

                var builder = new BodyBuilder();

                // Add attachments
                if (attachments != null)
                {
                    foreach (var file in attachments)
                    {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    
                }
                    }
                }

                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();

                try
                {
                    using var smtp = new MailKit.Net.Smtp.SmtpClient();
                    var secureSocketOptions = Enum.Parse<SecureSocketOptions>(_mailSettings.SecureSocketOptions);
                    await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, secureSocketOptions);
                    smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    throw new ApplicationException("Failed to send email. Please try again later.", ex);
                }
            }

            public string GenerateCode()
            {
                var randomNumber = new byte[4];
                using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                var code = BitConverter.ToUInt32(randomNumber, 0) % 900000 + 100000;
                return code.ToString("D6");
            }
        
    }
}
