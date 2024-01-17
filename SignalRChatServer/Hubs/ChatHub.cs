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
			await Clients.All.SendAsync("getClients", ClientData.AllClients);
		}

		public async Task SendMessageAsync(string message, string userName)
		{
			userName = userName.Trim();

			Client sender = ClientData.AllClients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

			if(userName == "All")
			{
				await Clients.Others.SendAsync("receiveMessage", message, sender.UserName);
			}
			else
			{
				Client client = ClientData.AllClients.FirstOrDefault(x => x.UserName == userName);
				await Clients.Client(client.ConnectionId).SendAsync("receiveMessage", message, sender.UserName);
			}
		}

		public async Task AddGroup(string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			GroupData.AllGroups.Add(new Group { GroupName = groupName });
			await Clients.All.SendAsync("getRooms", GroupData.AllGroups);
		}

		public async Task AddClientToGroup(string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		}
	}
}
