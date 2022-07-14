using System.Net.Mail;
using MimeKit;
using  Microsoft.Extensions.Options;
using MimeKit.Cryptography;
using MailKit.Security;

namespace BackendTraineesTask1.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfiguration;
        public EmailSender(IOptions<EmailConfiguration> _emailconf)
        {
            emailConfiguration= _emailconf.Value;
        }

        public async Task SendMail(MailRequest mailRequest)
        {
            var emailMessage = CreateEmailMessage(mailRequest);
             await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MailRequest mailRequest)
        {
            string FilePath = "./EmailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", mailRequest.UserName).Replace("[title]", mailRequest.Subject).Replace("[body]", mailRequest.Body);


            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(emailConfiguration.DisplayName, emailConfiguration.Mail));
            // mailMessage.To.Add(mess.To);
            mailMessage.To.Add(new MailboxAddress(null,mailRequest.To));
            mailMessage.Subject = mailRequest.Subject;
           
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = MailText;
            mailMessage.Body = bodyBuilder.ToMessageBody();

           
            return mailMessage;
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    // client.CheckCertificateRevocation = false;

                    await client.ConnectAsync("smtp.aol.com", 465,useSsl:true);
                   
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                   
                    await client.AuthenticateAsync("ftmicroservice@aol.com", "8yjs3FM#&aguren");

                    await client.SendAsync(emailMessage);
                }catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    throw;
                }finally{
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            // "   at MailKit.Net.Smtp.SmtpStream.ReadAheadAsync(Boolean doAsync, CancellationToken cancellationToken)\n   at MailKit.Net.Smtp.SmtpStream.ReadResponseAsync(Boolean doAsync, CancellationToken cancellationToken)\n   at MailKit.Net.Smtp.SmtpClient.SendCommandAsync(String command, Boolean doAsync, CancellationToken cancellationToken)\n   at MailKit.Net.Smtp.SmtpClient.AuthenticateAsync(Encoding encoding, ICredentials credentials, Boolean doAsync, CancellationToken cancellationToken)\n   at BackendTraineesTask1.EmailService.EmailSender.SendAsync(MimeMessage emailMessage) in /Users/aurora/Funbi_Node_Tut/SAEDTrainee/BackendTraineesTask1/BackendTraineesTask1/Services/EmailService/EmailSender.cs:line 60"
        }
    }
}