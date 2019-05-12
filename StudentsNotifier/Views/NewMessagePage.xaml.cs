using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StudentsNotifier.Models;

namespace StudentsNotifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMessagePage : ContentPage
    {
        public Message Msg { get; set; }

        public NewMessagePage()
        {
            InitializeComponent();

            Msg = new Message
            {
                MessageFrom = "Od: Test",
                MessageText = "Text zprávy",
                DateTime = DateTime.Now
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddMessage", Msg);
            await Navigation.PopModalAsync();
        }
    }
}
