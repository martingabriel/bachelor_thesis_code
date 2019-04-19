using System;
using System.Collections.Generic;

namespace StudentsNotifier.Models
{
    public class Message
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string MessageText { get; set; }
        public List<string> UserIds { get; set; }
        public string MessageFrom { get; set; }
        public bool SendMessageResult { get; set; }
    }
}
