using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentsNotifier.Models;

namespace StudentsNotifier.Services
{
    public class AzureDataStore : IDataStore
    {
        HttpClient client;
        IEnumerable<Item> items;
        IEnumerable<Message> messages;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Item>();
            messages = new List<Message>();
        }

        #region Item

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        #endregion

        #region Message

        public async Task<IEnumerable<Message>> GetAllMessagesAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/Message");
                messages = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Message>>(json));
            }

            return messages;
        }

        public async Task<IEnumerable<Message>> GetUserMessagesAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/Message/UserMessages/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Message>>(json));
            }

            return null;
        }

        public async Task<Message> GetMessageAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/Message/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Message>(json));
            }

            return null;
        }

        public async Task<bool> AddMessageAsync(Message msg)
        {
            if (msg == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(msg);

            var response = await client.PostAsync($"api/Message", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMessageAsync(Message msg)
        {
            if (msg == null || msg.Id == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(msg);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/Message/{msg.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMessageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/Message/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
                return null;

            var serializedItem = JsonConvert.SerializeObject(user);

            var response = await client.PostAsync($"api/User", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            string responseJson = await response.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<User>(responseJson);
            return responseUser;
        }

        public async Task<User> GetUserAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/User/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
            }

            return null;
        }

        #endregion
    }
}