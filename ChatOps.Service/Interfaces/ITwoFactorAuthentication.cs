using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Interfaces
{
    public interface ITwoFactorAuthentication
    {
        string GenerateRandomCode(int codeLength = 6);
    }
}
