using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using StudentsNotifier.MobileAppService.NotificationHubs;
using StudentsNotifier.MobileAppService.Controllers;

namespace StudentsNotifier.MobileAppService.Models
{
    public class MessageRepository : IMessageRepository
    {
        private static ConcurrentDictionary<string, Message> messages = new ConcurrentDictionary<string, Message>();

        private NotificationHubProxy notifications;

        public MessageRepository()
        {
            notifications = new NotificationHubProxy(null);
        }

        public void Add(Message msg)
        {
            msg.Id = Guid.NewGuid().ToString();
            messages[msg.Id] = msg;

            SendPushMessage(msg);
        }

        private async void SendPushMessage(Message msg)
        {

            // send message
            string toast = "{\"aps\":{\"alert\":{\"title\" : \"Received message:\", \"subtitle\" : \"" + msg.MessageFrom + "\",\"body\": \"" + msg.MessageText + "\"}}}";

            foreach (string userHandle in msg.UserIds)
            {
                await notifications.SendNotification(new Notification()
                {
                    Content = toast,
                    Platform = MobilePlatform.apns,
                    Handle = userHandle
                });
            }
        }

        public Message Get(string id)
        {
            return messages[id];
        }

        public IEnumerable<Message> GetUserMessages(string id)
        {
            var result = messages.Values.Where(msg => msg.UserIds.Contains(id));
            return result;
        }

        public IEnumerable<Message> GetAll()
        {
            return messages.Values;
        }

        public Message Remove(string id)
        {
            Message msg;
            messages.TryRemove(id, out msg);

            return msg;
        }
    }
}
