using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsNotifier.MobileAppService.Models
{
    public class LectionRating
    {
        public string Id { get; set; }
        public string LectionID { get; set; }
        public string LectionName { get; set; }
        public string VoteCode { get; set; }
        public List<Tuple<string, int>> Votes { get; set; }
        public int DurationInSec { get; set; }

        public double AvgVote { get { return Votes.Average(x => x.Item2); } }
        public int MaxVote { get { return Votes.Max(x => x.Item2); } }
        public int MinVote { get { return Votes.Min(x => x.Item2); } }
    }

    public class Vote
    {
        public string Id { get; set; }
        public string LectionRatingId { get; set; }
        public int UserVote { get; set; }
    }

    public class VoteRequest
    {
        public string LectionRatingId { get; set; }
        public List<string> UserToRequestIds { get; set; }
        public bool SendRequestResult { get; set; }
    }
}
