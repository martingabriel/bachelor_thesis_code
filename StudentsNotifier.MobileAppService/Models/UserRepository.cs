using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace StudentsNotifier.MobileAppService.Models
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, User> users =
               new ConcurrentDictionary<string, User>();

        public UserRepository()
        {
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "User 1", Role = "ST"});
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "User 2", Role = "UC"});
            Add(new User { Id = Guid.NewGuid().ToString(), Name = "User 3", Role = "ST"});
        }

        public void Add(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            users[user.Id] = user;
        }

        public User Find(string id)
        {
            User user;
            users.TryGetValue(id, out user);

            return user;
        }

        public User Get(string id)
        {
            return users[id];
        }

        public IEnumerable<User> GetAll()
        {
            return users.Values;
        }

        public User Remove(string id)
        {
            User user;
            users.TryRemove(id, out user);

            return user;
        }

        public void Update(User user)
        {
            users[user.Id] = user;
        }

        public IEnumerable<RozvrhovaAkce> GetRozvrhoveAkce(string userId)
        {
            if (users[userId] != null)
            {
                string stagId = users[userId].StagID;

                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        string jsonString = wc.DownloadString("https://stag-ws.utb.cz/ws/services/rest2/rozvrhy/getRozvrhByStudent?outputFormat=JSON&osCislo=" + stagId);
                        var rozvrhoveAkce = RozvrhoveAkce.FromJson(jsonString);
                        users[userId].SetRozvrhoveAkce(rozvrhoveAkce.RozvrhovaAkce);
                        return users[userId].RozvrhoveAkce;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Json read failed..");
                        return null;
                    }
                }
            }
            else
                return null;
        }

        public IEnumerable<string> GetUserIDsByRozvrhoveAkce(string rozvrhovaAkceId)
        {
            var resultUser = users.Values.Where(usr => usr.ContainsRoakce(rozvrhovaAkceId));
            var result = (from u in resultUser select u.Id);

            return result;
        }
    }
}
