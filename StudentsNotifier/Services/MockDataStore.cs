using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsNotifier.Models;

namespace StudentsNotifier.Services
{
    public class MockDataStore : IDataStore
    {
        List<Item> items;
        List<Message> messages;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
            };

            messages = new List<Message>();
            var mockMessages = new List<Message>
            {
                new Message { Id = Guid.NewGuid().ToString(), MessageText = "Test message text ...", MessageFrom="Test user 1" },
                new Message { Id = Guid.NewGuid().ToString(), MessageText = "Test message text ...", MessageFrom="Test user 2" },
            };

            foreach (var item in mockItems)
                items.Add(item);

            foreach (var msg in mockMessages)
                messages.Add(msg);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
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
    }
}