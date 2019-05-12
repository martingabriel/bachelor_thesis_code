using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
        IEnumerable<Message> messages;
        IEnumerable<LectionRating> ratings;
        string LoggedUserID;
        string LoggedUserName;
        string LoggedUserNotificationToken;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            messages = new List<Message>();
        }

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

        #endregion

        #region User

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
                return null;

            var serializedItem = JsonConvert.SerializeObject(user);

            var response = await client.PostAsync($"api/User", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            string responseJson = await response.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<User>(responseJson);
            LoggedUserID = responseUser.Id;
            LoggedUserName = responseUser.Name;
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

        public async Task<List<RozvrhovaAkce>> GetUserRozvrhoveAkceAsync(string id)
        {
            if (id != null)
            {
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        string address = client.BaseAddress + $"api/User/RozvrhoveAkce/{id}";
                        string jsonString = wc.DownloadString(address);
                        var rozvrhoveAkce = RozvrhoveAkce.FromJson(jsonString);
                        return await Task.Run(() => rozvrhoveAkce);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Json read failed..\n" + ex.ToString());
                        return null;
                    }
                }
            }

            return null;
        }

        public async Task<string> GetLoggedUserID()
        {
            return LoggedUserID;
        }

        public async Task<string> GetLoggedUserName()
        {
            return LoggedUserName;
        }

        public async Task<IEnumerable<string>> GetUserIdsByRozvrhovaAkceAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/User/UserIDsByRozvrhovaAkce/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<string>>(json));
            }

            return null;
        }

        public void SetLoggedUserNotificationToken(string token)
        {
            Debug.WriteLine("[SET]" + token);
            this.LoggedUserNotificationToken = token;
        }

        public string GetLoggedUserNotificationToken()
        {
            Debug.WriteLine("[GET]" + LoggedUserNotificationToken);
            return this.LoggedUserNotificationToken;
        }

        #endregion

        #region Lection Rating

        public async Task<LectionRating> AddLectionRating(LectionRating rating)
        {
            if (rating == null)
                return null;

            var serializedItem = JsonConvert.SerializeObject(rating);

            var response = await client.PostAsync($"api/LectionRating", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            string responseJson = await response.Content.ReadAsStringAsync();
            var responseRating = JsonConvert.DeserializeObject<LectionRating>(responseJson);
            return responseRating;
        }

        public async Task<Vote> GetVote(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/LectionRating/Vote/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Vote>(json));
            }

            return null;
        }

        public async Task<Vote> AddVoteAsync(Vote vote)
        {
            if (vote == null)
                return null;

            var serializedItem = JsonConvert.SerializeObject(vote);

            var response = await client.PostAsync($"api/LectionRating/Vote", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            string responseJson = await response.Content.ReadAsStringAsync();
            var responseVote = JsonConvert.DeserializeObject<Vote>(responseJson);
            return responseVote;
        }

        public async Task<VoteRequest> SendVoteRequest()
        {
            var json = await client.GetStringAsync($"api/LectionRating/SendVoteRequest");
            VoteRequest request = await Task.Run(() => JsonConvert.DeserializeObject<VoteRequest>(json));

            return request;
        }

        public async Task<IEnumerable<LectionRating>> GetAllLecitonRatings()
        {
            var json = await client.GetStringAsync($"api/LectionRating");
            ratings = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<LectionRating>>(json));

            return ratings;
        }

        public async Task<LectionRating> GetLectionRating(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/LectionRating/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<LectionRating>(json));
            }

            return null;
        }

        #endregion
    }
}