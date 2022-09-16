using Microsoft.AspNetCore.SignalR;


namespace Core.Services
{
    public class OnlineAssistant : Hub
    {
        public  async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Receive Message", message, Context.User.Identity.Name);
        }
    }
}
