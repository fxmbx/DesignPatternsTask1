using Twilio.Rest.Api.V2010.Account;

namespace BackendTraineesTask1.Services.SmsService
{
    public interface ISmsService
    {
         Task<MessageResource> Send(string mobileNumber, string body);
    }
}