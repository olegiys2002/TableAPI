﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OnlineAssistant : Hub
    {
        public  async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Receive Message", message);
        }
       
    }
}
