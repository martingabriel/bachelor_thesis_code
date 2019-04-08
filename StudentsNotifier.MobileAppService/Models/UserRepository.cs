using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Net;

namespace StudentsNotifier.MobileAppService.Models
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, User> users =
               new ConcurrentDictionary<string, User>();

        public UserRepository()
        {
            Add(new User { Id = Guid.NewGuid().ToString(), MainName = "User 1", Surname = "Test 1" });
            Add(new User { Id = Guid.NewGuid().ToString(), MainName = "User 2", Surname = "Test 2" });
            Add(new User { Id = Guid.NewGuid().ToString(), MainName = "User 3", Surname = "Test 3" });
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
    }
}
