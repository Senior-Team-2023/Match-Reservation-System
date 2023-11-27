using Microsoft.AspNetCore.SignalR;

namespace MarkReservationSystem.Hubs
{
    public class ReservationHub : Hub
    {
        public async Task SendMessage(string action, int seatPos)
        {
            Console.WriteLine("SendMessage invoked from client");
            await Clients.All.SendAsync("ReceiveMessage", action, seatPos);
        }
    }
}