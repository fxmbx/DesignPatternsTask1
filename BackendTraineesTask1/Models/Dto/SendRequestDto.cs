namespace BackendTraineesTask1.Models.Dto
{
    public enum NotificationType {
        Email,
        Sms
    }
    public class SendRequestDto
    {

        public string ToEmail { get; set; } 
        // public string ToNumber { get; set; }
        public string EmailSubject { get; set; }
        public string BodyContent { get; set; }
        public string ToUserName { get; set; }
        public NotificationType NotificationType { get; set; } = NotificationType.Email;
    }
}