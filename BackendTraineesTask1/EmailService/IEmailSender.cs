namespace BackendTraineesTask1.EmailService
{
    public interface IEmailSender
    {
           Task SendMail(MailRequest message);
    }
}