using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StudentsNotifier.Models;
using Xamarin.Forms;
using System.Linq;

namespace StudentsNotifier.ViewModels
{
    public class TimetableDetailViewModel : BaseViewModel
    {
        public RozvrhovaAkce Akce { get; set; }
        public ObservableCollection<User> Students { get; set; }
        public string SelectedMessage { get; set; }

        public Command LoadStudentsCommand { get; set; }
        public Command SendMessageCommand { get; set; }
        public Command SendRatingRequestCommand { get; set; }

        public TimetableDetailViewModel(RozvrhovaAkce akce = null)
        {
            Title = akce?.Nazev;
            Akce = akce;

            Students = new ObservableCollection<User>();

            LoadStudentsCommand = new Command(async () => await ExecuteLoadStudentsCommand());
            SendMessageCommand = new Command(async () => await ExecuteSendMessageCommand());
            SendRatingRequestCommand = new Command(async () => await ExecuteSendRatingRequestCommand());

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

        public async Task ExecuteSendMessageCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var studentIDs = from student in Students
                                 where student.NotificationToken != null
                                 select student.NotificationToken;

                Message msg = new Message();
                msg.MessageFrom = await DataStore.GetLoggedUserName();
                msg.UserIds = new List<string>();
                msg.UserIds.AddRange(studentIDs);
                msg.DateTime = DateTime.Now;
                msg.MessageText = SelectedMessage;

                bool result = await DataStore.AddMessageAsync(msg);
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

        public async Task ExecuteSendRatingRequestCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                LectionRating newRating = new LectionRating();
                newRating.LectionID = Akce.RoakIdno.ToString();
                newRating.LectionName = Akce.Nazev;

                await DataStore.AddLectionRating(newRating);
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
