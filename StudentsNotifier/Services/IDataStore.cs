using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsNotifier.Models;

namespace StudentsNotifier.Services
{
    public interface IDataStore
    {
        // Messages
        Task<bool> AddMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(string id);
        Task<Message> GetMessageAsync(string id);
        Task<IEnumerable<Message>> GetUserMessagesAsync(string id);
        Task<IEnumerable<Message>> GetAllMessagesAsync(bool forceRefresh = false);

        // User
        Task<User> AddUserAsync(User user);
        Task<User> GetUserAsync(string id);
        Task<List<RozvrhovaAkce>> GetUserRozvrhoveAkceAsync(string id);
        Task<IEnumerable<string>> GetUserIdsByRozvrhovaAkceAsync(string id);

        Task<string> GetLoggedUserID();
    }
}
