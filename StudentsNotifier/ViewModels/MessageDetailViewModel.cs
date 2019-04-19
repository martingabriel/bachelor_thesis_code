using System;
using StudentsNotifier.Models;

namespace StudentsNotifier.ViewModels
{
    public class MessageDetailViewModel : BaseViewModel
    {
        public Message Msg { get; set; }
        public MessageDetailViewModel(Message msg = null)
        {
            Title = msg?.MessageText;
            Msg = msg;
        }
    }
}
