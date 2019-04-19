using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace StudentsNotifier.MobileAppService.Models
{
    public class MessageRepository : IMessageRepository
    {
        private static ConcurrentDictionary<string, Message> messages = new ConcurrentDictionary<string, Message>();

        public MessageRepository()
        {

        }

        public void Add(Message msg)
        {
            msg.Id = Guid.NewGuid().ToString();
            messages[msg.Id] = msg;
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
