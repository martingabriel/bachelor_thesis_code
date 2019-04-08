using System;
using System.Collections.Generic;

namespace StudentsNotifier.MobileAppService.Models
{
    public interface IUserRepository
    {
        void Add(User item);
        void Update(User item);
        User Remove(string id);
        User Get(string id);
        IEnumerable<User> GetAll();
    }
}
