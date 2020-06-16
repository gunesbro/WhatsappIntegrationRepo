using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsappIntegration.WebUI.Hubs
{
    public class WhatsappHub : Hub
    {
        public async Task SendMessage(int chatId,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatId, message);
        }
    }
}
