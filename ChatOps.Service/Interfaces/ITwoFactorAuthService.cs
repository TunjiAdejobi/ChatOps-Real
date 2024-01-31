using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Interfaces
{
    public interface ITwoFactorAuthService
    {
        void SendVerificationCode(string userId, string phoneNumber);
        bool VerifyCode(string userId, string enteredCode);
    }
}
