using System;
using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;
        //public User LoggedUser { get; set; }

        public AboutPage(AboutViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public AboutPage()
        {
            InitializeComponent();

            var LoggedUser = new User
            {
                Name = "Petr Novák",
                StagID = "A15655",
                Id = "..."
            };

            viewModel = new AboutViewModel(LoggedUser);
            BindingContext = viewModel;
        }
    }
}