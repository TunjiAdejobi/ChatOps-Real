using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Services.Interfaces
{
    public interface IPasswordService
    {
        string CreateHash(string password, string salt);

        string CreateSalt();

        string GetCustomSalt(string salt);

        string EncryptString(string text, string salt);

        string DecryptString(string cipherText, string salt);                
    }
}
