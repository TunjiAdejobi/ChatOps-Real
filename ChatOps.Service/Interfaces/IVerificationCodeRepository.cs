using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Interfaces
{
    public interface IVerificationCodeRepository
    {
        void SaveVerificationCode(string userId, string verificationCode);
        string GetVerificationCode(string userId);
        void RemoveVerificationCode(string userId);
    }
}
