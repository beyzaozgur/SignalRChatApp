using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Data;
using SignalRChatServer.Models;

namespace SignalRChatServer.Hubs
{
	public class ChatHub : Hub
	{
		public async Task GetUserName(string userName)
		{
			var isExisted = ClientData.AllClients.Any(x => x.UserName == userName);

			if(isExisted)
			{
				await Clients.Caller.SendAsync("alreadyExitsAlert", userName);
			}
			else
			{
				Client client = new Client
				{
					ConnectionId = Context.ConnectionId,
					UserName = userName
				};

				var isValid = true;

				ClientData.AllClients.Add(client);
				await Clients.Others.SendAsync("userJoined", userName);
				await Clients.Caller.SendAsync("callerJoined", isValid);
				await Clients.All.SendAsync("getClients", ClientData.AllClients);
			}
		}

		public async Task SendMessageAsync(string message, string userName)
		{
			if(!string.IsNullOrEmpty(userName))
			{
				userName = userName.Trim();

				Client sender = ClientData.AllClients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

				if (userName == "All")
				{
					await Clients.Others.SendAsync("receiveMessage", message, sender.UserName);
				}
				else
				{
					Client client = ClientData.AllClients.FirstOrDefault(x => x.UserName == userName);
					await Clients.Client(client.ConnectionId).SendAsync("receiveMessage", message, sender.UserName);
				}
			}
		}

		public async Task SendMessageToGroupAsync(string message, string groupName)
		{
			Group group = GroupData.AllGroups.FirstOrDefault(x => x.GroupName == groupName);

			var isMember = group.Clients.Any(x => x.ConnectionId == Context.ConnectionId);

			if (!string.IsNullOrEmpty(groupName) && isMember)
			{
				groupName = groupName.Trim();

				await Clients.Group(groupName).SendAsync("receiveMessage", message, ClientData.AllClients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId).UserName);
			}
			else
			{
				await Clients.Caller.SendAsync("notMemberMessage", "You are not a member of this group. Please join first.");

			}
		}

		public async Task AddGroup(string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

			Group group = new Group { GroupName = groupName };
			group.Clients.Add(ClientData.AllClients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId));

			GroupData.AllGroups.Add(group);
			await Clients.All.SendAsync("getRooms", GroupData.AllGroups);
		}

		public async Task AddClientToGroup(string groupName)
		{
			Client client = ClientData.AllClients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

			Group group = GroupData.AllGroups.FirstOrDefault(x => x.GroupName == groupName);

			var isMember = group.Clients.Any(x => x.ConnectionId == Context.ConnectionId);

			if (!isMember)
			{
				group.Clients.Add(client);
				await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			}
		}

		public async Task GetClientsInGroup(string groupName)
		{
			Group group = GroupData.AllGroups.FirstOrDefault(x => x.GroupName == groupName);

			if (groupName == "All")
			{
				await Clients.All.SendAsync("getClients", ClientData.AllClients);
			}
			else
			{
				await Clients.Caller.SendAsync("getClients", group.Clients);
			}
		}
	}
}
