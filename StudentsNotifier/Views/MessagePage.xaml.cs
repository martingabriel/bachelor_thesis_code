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
    public partial class MessagePage : ContentPage
    {
        MessageViewModel viewModel;

        public MessagePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MessageViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var msg = args.SelectedItem as Message;
            if (msg == null)
                return;

            await Navigation.PushAsync(new MessageDetailPage(new MessageDetailViewModel(msg)));

            // Manually deselect item.
            MessagesListView.SelectedItem = null;
        }

        async void AddMessage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewMessagePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Messages.Count == 0)
                viewModel.LoadMessagesCommand.Execute(null);
        }
    }
}
