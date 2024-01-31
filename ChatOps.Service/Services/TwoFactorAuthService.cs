using ChatOps.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Services
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        private readonly ITwoFactorAuthentication _twoFactorAuthentication;
        private readonly IVerificationCodeRepository _codeRepository;
        private readonly ITwilioSmsService _smsService;

        public TwoFactorAuthService(
            ITwoFactorAuthentication twoFactorAuthentication,
            IVerificationCodeRepository codeRepository,
            ITwilioSmsService smsService)
        {
            _twoFactorAuthentication = twoFactorAuthentication;
            _codeRepository = codeRepository;
            _smsService = smsService;
        }

        public void SendVerificationCode(string userId, string phoneNumber)
        {
            // Generate a random verification code
            string verificationCode = _twoFactorAuthentication.GenerateRandomCode();

            // Save the code in the repository
            _codeRepository.SaveVerificationCode(userId, verificationCode);

            // Send the code via SMS using Twilio
            string message = $"Your verification code is: {verificationCode}";
            _smsService.SendSms(phoneNumber, message);
        }

        public bool VerifyCode(string userId, string enteredCode)
        {
            // Retrieve the saved verification code from the repository
            string savedCode = _codeRepository.GetVerificationCode(userId);

            // Check if the entered code matches the saved code
            return savedCode != null && savedCode.Equals(enteredCode, StringComparison.Ordinal);
        }
    }

}
