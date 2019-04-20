using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsNotifier.Models;

namespace StudentsNotifier.Services
{
    public interface IDataStore
    {
        Task<bool> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(string id);
        Task<Item> GetItemAsync(string id);
        Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false);

        // Messages
        Task<bool> AddMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(string id);
        Task<Message> GetMessageAsync(string id);
        Task<IEnumerable<Message>> GetUserMessagesAsync(string id);
        Task<IEnumerable<Message>> GetAllMessagesAsync(bool forceRefresh = false);

        // User
        Task<User> AddUserAsync(User user);
        Task<User> GetUserAsync(string id);
    }
}
