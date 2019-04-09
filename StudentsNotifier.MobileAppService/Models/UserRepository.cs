﻿using System;
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
    }
}
