using System;
using System.Collections.Generic;
using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;
using Xamarin.Forms;

namespace StudentsNotifier.Views
{
    public partial class NewVotePage : ContentPage
    {
        public Vote vote { get; set; }

        public NewVotePage(string ratingID)
        {
            InitializeComponent();

            vote = new Vote
            {
                id = "id",
                lectionRatingId = ratingID,
                userVote = 0
            };

            BindingContext = this;
        }

        void Handle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;

            vote.userVote = picker.SelectedIndex + 1;
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddVote", vote);
            await Navigation.PopModalAsync();
        }
    }
}
