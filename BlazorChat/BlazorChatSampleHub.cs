using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class BlazorChatSampleHub : Hub
{
    public static readonly string HubUrl = "/chat";

    public async Task Broadcast(string username, string message)
    {
        await Clients.All.SendAsync("Broadcast", username, message);
    }

    public async Task Private(string from, string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("Private", from, message);
    }
}