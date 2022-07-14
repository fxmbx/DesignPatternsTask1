using BackendTraineesTask1.Data;
using BackendTraineesTask1.EmailService;
using BackendTraineesTask1.Models.Dto;
using BackendTraineesTask1.Response;
using BackendTraineesTask1.Services.SmsService;

namespace BackendTraineesTask1.Services.UserService
{
    public class UserService : IUserService
    {
        // private readonly ApplicationDataContext dbcontext;
        private readonly IEmailSender emailSender;
        private readonly ISmsService smsService;
        public UserService(IEmailSender emailSender, ISmsService smsService)
        {
            // this.dbcontext = dbcontext;
            this.emailSender = emailSender;
            this.smsService = smsService;
        }

        public async Task<ServiceResponse<string>> SendNotification(SendRequestDto req)
        {
            var response = new ServiceResponse<string>();
            try
            {
                if(req.NotificationType == NotificationType.Email){
                    var message = new MailRequest(req.ToEmail,req.EmailSubject,req.BodyContent, req.ToUserName);
                    await emailSender.SendMail(message);
                    response.Data = req.EmailSubject;
                    response.Message = String.Format("Email sucessfully sent to {0}",req.ToEmail);
                    response.Success = true;
                }else if(req.NotificationType == NotificationType.Sms){
                    var res = await smsService.Send(req.ToNumber, req.BodyContent);
                    response.Data = res.Status.ToString();
                    response.Message= res.Body;
                    response.Success = true;
                }
            }
            catch (System.Exception ex)
            {
                
               response.Message = ex.Message;
               response.Success = false;
            }
            return response;
        }

       
    }
}