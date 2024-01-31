using ChatOps.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Services
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly Dictionary<string, string> _verificationCodeStorage = new Dictionary<string, string>();

        public void SaveVerificationCode(string userId, string verificationCode)
        {
            _verificationCodeStorage[userId] = verificationCode;
        }

        public string GetVerificationCode(string userId)
        {
            return _verificationCodeStorage.TryGetValue(userId, out var code) ? code : null;
        }

        public void RemoveVerificationCode(string userId)
        {
            _verificationCodeStorage.Remove(userId);
        }
    }
}
