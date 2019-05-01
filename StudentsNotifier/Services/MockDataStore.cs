using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsNotifier.Models;

namespace StudentsNotifier.Services
{
    public class MockDataStore : IDataStore
    {
        List<Message> messages;

        public MockDataStore()
        {

            messages = new List<Message>();
            var mockMessages = new List<Message>
            {
                new Message { Id = Guid.NewGuid().ToString(), MessageText = "Test message text ...", MessageFrom="Test user 1" },
                new Message { Id = Guid.NewGuid().ToString(), MessageText = "Test message text ...", MessageFrom="Test user 2" },
            };

            foreach (var msg in mockMessages)
                messages.Add(msg);
        }

        public async Task<bool> AddMessageAsync(Message message)
        {
            messages.Add(message);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteMessageAsync(string id)
        {
            var oldMessage = messages.Where((Message arg) => arg.Id == id).FirstOrDefault();
            messages.Remove(oldMessage);

            return await Task.FromResult(true);
        }

        public async Task<Message> GetMessageAsync(string id)
        {
            return await Task.FromResult(messages.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Message>> GetUserMessagesAsync(string id)
        {
            return await Task.FromResult(messages.Where(msg => msg.UserIds.Contains(id)));
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(messages);
        }

        public Task<User> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RozvrhoveAkce> GetUserRozvrhoveAkceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLoggedUserID()
        {
            throw new NotImplementedException();
        }

        Task<List<RozvrhovaAkce>> IDataStore.GetUserRozvrhoveAkceAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserIdsByRozvrhovaAkceAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}