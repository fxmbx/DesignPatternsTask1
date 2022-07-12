namespace BackendTraineesTask1.EmailService
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }

        public MailRequest(string to, string subject, string body, string userName)
        {
            To =to;
            Subject = subject;
            Body = body;
            UserName = userName;
        }
    }
}