using ChatOps.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ChatOps.Services.Services
{
    public class TwilioSmsService : ITwilioSmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhoneNumber;

        public TwilioSmsService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _twilioPhoneNumber = configuration["Twilio:PhoneNumber"];
        }

        public void SendSms(string phoneNumber, string message)
        {
            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);

            // Send SMS via Twilio
            var twilioMessage = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            // Handle the response if needed
            if (twilioMessage.Status != MessageResource.StatusEnum.Sent)
            {
                // Handle the error
            }
        }
    }
}
