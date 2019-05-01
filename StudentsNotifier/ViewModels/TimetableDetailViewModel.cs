using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StudentsNotifier.Models;
using Xamarin.Forms;

namespace StudentsNotifier.ViewModels
{
    public class TimetableDetailViewModel : BaseViewModel
    {
        public RozvrhovaAkce Akce { get; set; }
        public ObservableCollection<User> Students { get; set; }

        public Command LoadStudentsCommand { get; set; }

        public TimetableDetailViewModel(RozvrhovaAkce akce = null)
        {
            Title = akce?.Nazev;
            Akce = akce;

            Students = new ObservableCollection<User>();

            LoadStudentsCommand = new Command(async () => await ExecuteLoadStudentsCommand());

            ExecuteLoadStudentsCommand();
        }

        public async Task ExecuteLoadStudentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Students.Clear();
                var students = await DataStore.GetUserIdsByRozvrhovaAkceAsync(Akce.RoakIdno.ToString());
                foreach (var student in students)
                {
                    var studentData = await DataStore.GetUserAsync(student);

                    if (studentData != null)
                        Students.Add(studentData);
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

        public void RemoveStudent(User u)
        {
            Students.Remove(u);
        }
    }
}
