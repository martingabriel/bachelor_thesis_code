using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace StudentsNotifier.MobileAppService.Models
{
    public class LectionRatingRepository : ILectionRating
    {
        private static ConcurrentDictionary<string, LectionRating> lections =
               new ConcurrentDictionary<string, LectionRating>();

        private static ConcurrentDictionary<string, Vote> votes = 
            new ConcurrentDictionary<string, Vote>();

        public LectionRatingRepository()
        {
            List<Tuple<string, int>> tuples = new List<Tuple<string, int>>();
            tuples.Add(new Tuple<string, int>("test", 1));
            Add(new LectionRating { Id = "guid1", Votes = tuples });
        }

        public void Add(LectionRating item)
        {
            item.Id = Guid.NewGuid().ToString();
            lections[item.Id] = item;
        }

        public LectionRating Get(string id)
        {
            return lections[id];
        }

        public IEnumerable<LectionRating> GetAll()
        {
            return lections.Values;
        }

        public LectionRating Remove(string id)
        {
            LectionRating lection;
            lections.TryRemove(id, out lection);

            return lection;
        }

        public void Update(LectionRating item)
        {
            lections[item.Id] = item;
        }



        public void AddVote(Vote vote)
        {
            vote.Id = Guid.NewGuid().ToString();
            lections[vote.LectionRatingId].Votes.Add(new Tuple<string, int>(vote.Id, vote.UserVote));
            votes[vote.Id] = vote;
        }

        public Vote GetVote(string id)
        {
            return votes[id];
        }

        public Vote RemoveVote(string id)
        {
            Vote vote;
            votes.TryRemove(id, out vote);

            LectionRating lection = lections[vote.LectionRatingId];

            if (lection != null)
                lection.Votes.RemoveAll(v => v.Item1.Equals(vote.Id));

            return vote;
        }
    }
}
