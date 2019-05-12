using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StudentsNotifier.Models;
using StudentsNotifier.Views;
using Xamarin.Forms;

namespace StudentsNotifier.ViewModels
{
    public class LectionRatingViewModel : BaseViewModel
    {
        public ObservableCollection<LectionRating> Ratings { get; set; }
        public ObservableCollection<Vote> Votes { get; set; }
        public Command LoadLectionRatingsCommand { get; set; }

        public LectionRatingViewModel()
        {
            Title = "Hodnocení výuky";
            Ratings = new ObservableCollection<LectionRating>();
            Votes = new ObservableCollection<Vote>();
            LoadLectionRatingsCommand = new Command(async () => await ExecuteLoadLectionRatingsCommand());

            MessagingCenter.Subscribe<NewVotePage, Vote>(this, "AddVote", async (obj, vote) =>
            {
                var newVote = vote as Vote;
                Votes.Add(newVote);
                await DataStore.AddVoteAsync(newVote);
            });
        }

        async Task ExecuteLoadLectionRatingsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Ratings.Clear();
                //var messages = await DataStore.GetAllMessagesAsync(true);
                var result = await DataStore.GetAllLecitonRatings();
                foreach (var rating in result)
                {
                    Ratings.Add(rating);
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
