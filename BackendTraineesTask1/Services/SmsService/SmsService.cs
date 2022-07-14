using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BackendTraineesTask1.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly TwilioSettings twilio;

        public SmsService(IOptions<TwilioSettings> twilio)
        {
            this.twilio = twilio.Value;
        }
        public async Task<MessageResource> Send(string mobileNumber, string body)
        {
            TwilioClient.Init(twilio.AccountSID, twilio.AuthToken);

            var res = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(twilio.TwilioPhoneNumber),
                    to: mobileNumber
                );        

            return res;
        }
    }
}