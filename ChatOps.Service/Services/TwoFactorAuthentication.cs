using ChatOps.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Services
{
    public class TwoFactorAuthentication : ITwoFactorAuthentication
    {
        private readonly Random _random = new Random();

        public string GenerateRandomCode(int codeLength = 6)
        {
            return new string(Enumerable.Range(0, codeLength)
                .Select(_ => (char)('0' + _random.Next(10)))
                .ToArray());
        }
    }
}
