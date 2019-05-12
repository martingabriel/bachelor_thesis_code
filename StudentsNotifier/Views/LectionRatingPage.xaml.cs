using System;
using System.Collections.Generic;
using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LectionRatingPage : ContentPage
    {
        LectionRatingViewModel viewModel;

        public LectionRatingPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LectionRatingViewModel();
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var rating = e.SelectedItem as LectionRating;
            if (rating == null)
                return;

            await Navigation.PushAsync(new LectionRatingDetailPage(new LectionRatingDetailViewModel(rating)));

            LectionRatingListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Ratings.Count == 0)
                viewModel.LoadLectionRatingsCommand.Execute(null);
        }
    }
}
