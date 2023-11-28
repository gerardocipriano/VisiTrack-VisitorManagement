using Microsoft.AspNetCore.SignalR;
using System;

namespace Template.Web.SignalR.Hubs
{
    public interface ITemplateClientEvent
    {
        public System.Threading.Tasks.Task NewMessage(Guid idUser, Guid idMessage);
    }

    [Microsoft.AspNetCore.Authorization.Authorize] // Makes the hub usable only by authenticated users
    public class TemplateHub : Hub<ITemplateClientEvent>
    {
        private readonly IPublishDomainEvents _publisher;

        public TemplateHub(IPublishDomainEvents publisher)
        {
            _publisher = publisher;
        }

        public async System.Threading.Tasks.Task JoinGroup(Guid idGroup)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idGroup.ToString());
        }
        public async System.Threading.Tasks.Task LeaveGroup(Guid idGroup)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idGroup.ToString());
        }
    }
}
