using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StudentsNotifier.Models;
using Xamarin.Forms;

namespace StudentsNotifier.ViewModels
{
    public class TimetableViewModel : BaseViewModel
    {
        public ObservableCollection<RozvrhovaAkce> RozvrhoveAkce { get; set; }
        public Command LoadTimetableCommand { get; set; }

        public TimetableViewModel()
        {
            Title = "Timetable";
            RozvrhoveAkce = new ObservableCollection<RozvrhovaAkce>();
            LoadTimetableCommand = new Command(async () => await ExecuteLoadTimetableCommand());

        }

        async Task ExecuteLoadTimetableCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                RozvrhoveAkce.Clear();
                string loggedUser = await DataStore.GetLoggedUserID();
                var akce = await DataStore.GetUserRozvrhoveAkceAsync(loggedUser);
                foreach (var a in akce)
                {
                    RozvrhoveAkce.Add(a);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
