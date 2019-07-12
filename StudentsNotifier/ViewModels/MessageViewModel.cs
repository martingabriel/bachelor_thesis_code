using System;
using Xamarin.Forms;

using StudentsNotifier.Models;
using StudentsNotifier.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StudentsNotifier.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        public Command LoadMessagesCommand { get; set; }

        public MessageViewModel()
        {
            Title = "Messages";
            Messages = new ObservableCollection<Message>();
            LoadMessagesCommand = new Command(async () => await ExecuteLoadMessagesCommand());

            MessagingCenter.Subscribe<NewMessagePage, Message>(this, "AddMessage", async (obj, msg) =>
            {
                var newMessage = msg as Message;
                Messages.Add(newMessage);
                await DataStore.AddMessageAsync(newMessage);
            });
        }

        async Task ExecuteLoadMessagesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Messages.Clear();
                //var messages = await DataStore.GetAllMessagesAsync(true);
                var messages = await DataStore.GetUserMessagesAsync(DataStore.GetLoggedUserNotificationToken());
                foreach (var msg in messages)
                {
                    Messages.Add(msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
