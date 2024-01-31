using ChatOps.Service.Interfaces;
using ChatOps.Services.Hubs;
using ChatOps.Services.Interfaces;
using ChatOps.Services.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IHubContext<HubService> _hubContext;        

        public UserService(IHubContext<HubService> hubContext)            
        {
            _hubContext = hubContext;          
        }

        public async Task PushUser(Model.Models.Chats user)
        {
            await _hubContext.Clients.All.SendAsync("ReceivedUser", user);
        }

        public async Task PushMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceivedMessage", message);
        }        
    }
}
