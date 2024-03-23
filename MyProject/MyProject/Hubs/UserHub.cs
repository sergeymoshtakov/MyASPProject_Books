using Microsoft.AspNetCore.SignalR;

namespace MyProject.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int ConnectionCount { get; set; } = 0;

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            //надіслати клієнту оновлену інформацію
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }

        // перевизначення методів підключення та відключення
        public override async Task OnConnectedAsync()
        {
            ConnectionCount++;
            await Clients.All.SendAsync("updateConnectionCount", ConnectionCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionCount--;
            await Clients.All.SendAsync("updateConnectionCount", ConnectionCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
