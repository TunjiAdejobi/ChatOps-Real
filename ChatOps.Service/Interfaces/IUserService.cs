using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Service.Interfaces
{
    public interface IUserService
    {
        Task PushUser(Model.Models.Chats user);
        Task PushMessage(string message);
                
        //void SaveVerificationCode(object id, string verificationCode);
    }
}
