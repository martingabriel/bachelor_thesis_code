using System;
using System.Collections.Generic;

namespace StudentsNotifier.MobileAppService.Models
{
    public interface IMessageRepository
    {
        void Add(Message msg);
        Message Remove(string id);
        Message Get(string id);
        IEnumerable<Message> GetUserMessages(string id);
        IEnumerable<Message> GetAll();
    }
}
