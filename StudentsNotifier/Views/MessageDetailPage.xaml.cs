using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StudentsNotifier.Models;
using StudentsNotifier.ViewModels;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageDetailPage : ContentPage
    {
        MessageDetailViewModel viewModel;

        public MessageDetailPage(MessageDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public MessageDetailPage()
        {
            InitializeComponent();

            var msg = new Message
            {
                MessageText = "Message text...",
                DateTime = DateTime.Now,
                MessageFrom = "123abc"
            };

            viewModel = new MessageDetailViewModel(msg);
            BindingContext = viewModel;
        }
    }
}
