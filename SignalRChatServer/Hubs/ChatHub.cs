using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Data;
using SignalRChatServer.Models;

namespace SignalRChatServer.Hubs
{
	public class ChatHub: Hub
	{
		public async Task GetUserName(string userName)
		{
			Client client = new Client
			{
				ConnectionId = Context.ConnectionId,
				UserName = userName
			};

			ClientData.AllClients.Add(client);
			await Clients.Others.SendAsync("userJoined", userName);
		}
	}
}
