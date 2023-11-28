using System;

namespace Template.Web.SignalR.Hubs.Events
{
    public class NewMessageEvent
    {
        public Guid IdGroup { get; set; }

        public Guid IdUser { get; set; }
        public Guid IdMessage { get; set; }
    }
}
