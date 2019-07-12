using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;
using System.Collections.Generic;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableDetailPage : ContentPage
    {
        TimetableDetailViewModel viewModel;

        public TimetableDetailPage(TimetableDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public TimetableDetailPage()
        {
            InitializeComponent();

            var akce = new RozvrhovaAkce
            {
                Nazev = "TestAction"
            };

            viewModel = new TimetableDetailViewModel(akce);
            BindingContext = viewModel;
        }


        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            await viewModel.ExecuteLoadStudentsCommand();
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Picker picker = sender as Picker;

            viewModel.SelectedMessage = picker.SelectedItem.ToString();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            viewModel.RemoveStudent(mi.CommandParameter as User);
        }
    }
}
