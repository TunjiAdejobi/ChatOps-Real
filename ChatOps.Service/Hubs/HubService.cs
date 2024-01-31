using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChatOps.Services.Hubs
{
    public class HubService : Hub
    {
        public HubService()
        {

        }

        public void BroadcastUsers( Model.Models.Chats user)
        {
            Clients.All.SendAsync("ReceivedUser", user);
        }

        public void BroadcastMessage(string message)
        {
            Clients.All.SendAsync("ReceivedMessage", message);
        }
    }
}
