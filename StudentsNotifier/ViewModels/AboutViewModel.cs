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

        string id = string.Empty;
        public string LoggedUserId
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        string name = string.Empty;
        public string LoggedUserName
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string signText = string.Empty;
        public string SignButtonText
        {
            get { return signText; }
            set { SetProperty(ref signText, value); }
        }

        public AboutViewModel(User usr = null)
        {
            Title = "Settings";
            LoggedUser = usr;
            LoggedUserName = LoggedUser.Name;
            LoggedUserId = LoggedUser.Id;
            SignButtonText = "Login";

            GetUserData = new Command(async () => await ExecuteLoadUserDataCommand());
        }

        async Task ExecuteLoadUserDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // mock debug data
                LoggedUser.NotificationToken = DataStore.GetLoggedUserNotificationToken();
                User result = await DataStore.AddUserAsync(LoggedUser);
                LoggedUser = result;
                LoggedUserName = "Satoshi Nakamoto";
                LoggedUserId = LoggedUser.Id;
                SignButtonText = "✔️";

                // debug
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