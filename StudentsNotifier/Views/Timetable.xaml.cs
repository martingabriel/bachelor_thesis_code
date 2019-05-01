using System;
using System.Collections.Generic;

using StudentsNotifier.Models;
using StudentsNotifier.Views;
using StudentsNotifier.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Timetable : ContentPage
    {
        TimetableViewModel viewModel;

        public Timetable()
        {
            InitializeComponent();

            BindingContext = viewModel = new TimetableViewModel();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var akce = args.SelectedItem as RozvrhovaAkce;
            if (akce == null)
                return;

            // detail akce
            await Navigation.PushAsync(new TimetableDetailPage(new TimetableDetailViewModel(akce)));

            TimetableListView.SelectedItem = null;
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadTimetableCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.RozvrhoveAkce.Count == 0)
                viewModel.LoadTimetableCommand.Execute(null);
        }
    }
}
