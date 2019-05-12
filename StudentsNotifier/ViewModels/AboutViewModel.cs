using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentsNotifier.Models;
using Xamarin.Forms;

namespace StudentsNotifier.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public User LoggedUser { get; set; }
        public ICommand GetUserData { get; }

        string title = string.Empty;
        public string LoggedUserId
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public AboutViewModel(User usr = null)
        {
            Title = "Nastavení";
            LoggedUser = usr;
            LoggedUserId = LoggedUser.Id;

            GetUserData = new Command(async () => await ExecuteLoadUserDataCommand());
        }

        async Task ExecuteLoadUserDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                LoggedUser.NotificationToken = DataStore.GetLoggedUserNotificationToken();
                User result = await DataStore.AddUserAsync(LoggedUser);
                LoggedUser = result;
                LoggedUserId = LoggedUser.Id;
                Debug.WriteLine("User [" + LoggedUser.StagID  + "] ID: " + LoggedUser.Id);
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