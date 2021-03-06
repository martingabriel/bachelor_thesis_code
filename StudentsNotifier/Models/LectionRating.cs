﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsNotifier.Models
{
    public class LectionRating
    {
        public string Id { get; set; }
        public string LectionID { get; set; }
        public string LectionName { get; set; }
        public string VoteCode { get; set; }
        public List<Tuple<string, int>> Votes { get; set; }
        public int DurationInSec { get; set; }

        public double AvgVote { get { return Votes.Count > 0 ? Votes.Average(x => x.Item2) : 0; } }
        public int MaxVote { get { return Votes.Count > 0 ? Votes.Max(x => x.Item2) : 0; } }
        public int MinVote { get { return Votes.Count > 0 ? Votes.Min(x => x.Item2) : 0; } }
    }

    public class Vote
    {
        public string id { get; set; }
        public string lectionRatingId { get; set; }
        public int userVote { get; set; }
    }

    public class VoteRequest
    {
        public string LectionRatingId { get; set; }
        public List<string> UserToRequestIds { get; set; }
        public bool SendRequestResult { get; set; }
    }
}
