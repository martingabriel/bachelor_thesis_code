using System;
using System.Collections.Generic;

namespace StudentsNotifier.MobileAppService.Models
{
    public interface ILectionRating
    {
        void Add(LectionRating item);
        void Update(LectionRating item);
        LectionRating Remove(string id);
        LectionRating Get(string id);
        IEnumerable<LectionRating> GetAll();

        void AddVote(Vote vote);
        Vote GetVote(string id);
        Vote RemoveVote(string id);
    }
}
