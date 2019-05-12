using System;
using System.Collections.Generic;
using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;
using Entry = Microcharts.Entry;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LectionRatingDetailPage : ContentPage
    {
        LectionRatingDetailViewModel viewModel;

        List<Entry> Entries { get; set; }


        public LectionRatingDetailPage(LectionRatingDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            InitEntries();
        }

        public LectionRatingDetailPage()
        {
            InitializeComponent();

            var rating = new LectionRating
            {
                LectionName = "Test rating",
                VoteCode = "asdf"
            };

            viewModel = new LectionRatingDetailViewModel(rating);
            BindingContext = viewModel;
            InitEntries();
        }

        private void InitEntries()
        {
            Entries = new List<Entry>();
            LoadChart();
            VoteChart.Chart = new DonutChart { Entries = Entries, LabelTextSize=20 };
        }

        private async void AddVote_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewVotePage(viewModel.rating.Id)));
        }

        private void LoadChart()
        {
            int[] rating = { 0, 0, 0, 0, 0 };
            Dictionary<int, int> ratings = new Dictionary<int, int>();

            if (viewModel.rating.Votes.Count > 0)
            {

                foreach (Tuple<string, int> vote in viewModel.rating.Votes)
                {
                    if (vote.Item2 - 1 >= 0)
                    { 
                        rating[vote.Item2 - 1]++;

                        if (!ratings.ContainsKey(vote.Item2))
                            ratings.Add(vote.Item2, (rating[vote.Item2 - 1]));
                        else
                            ratings[vote.Item2] = (rating[vote.Item2 - 1]);
                    }
                }

                foreach (KeyValuePair<int, int> e in ratings)
                {
                    if (e.Value > 0)
                    {
                        Entry entry = new Entry(e.Value);
                        entry.Label = "Hodnocení " + e.Key.ToString();


                        switch (e.Key)
                        {
                            case 1:
                                entry.Color = SKColor.Parse("#66FF33");
                                break;
                            case 2:
                                entry.Color = SKColor.Parse("#006600");
                                break;
                            case 3:
                                entry.Color = SKColor.Parse("#0066CC");
                                break;
                            case 4:
                                entry.Color = SKColor.Parse("#FF9933");
                                break;
                            case 5:
                                entry.Color = SKColor.Parse("#FF0000");
                                break;
                            default:
                                break;
                        }

                        Entries.Add(entry);
                    }
                }

                /*foreach (int i in rating)
                {
                    if (i > 0)
                    {
                        Entry entry = new Entry(i);
                        entry.Label = i.ToString();

                        switch (i)
                        {
                            case 1:
                                entry.Color = SKColor.Parse("#66FF33");
                                break;
                            case 2:
                                entry.Color = SKColor.Parse("#006600");
                                break;
                            case 3:
                                entry.Color = SKColor.Parse("#0066CC");
                                break;
                            case 4:
                                entry.Color = SKColor.Parse("#FF9933");
                                break;
                            case 5:
                                entry.Color = SKColor.Parse("#FF0000");
                                break;
                            default:
                                break;
                        }

                        Entries.Add(entry);
                    }
                }*/
            }
        }
    }
}
