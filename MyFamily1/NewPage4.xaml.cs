using Microsoft.Maui.Controls;
using System;

namespace Myfamily1
{
    public partial class NewPage4 : ContentPage
    {
        public NewPage4()
        {
            InitializeComponent();
        }

        private async void ProfileButton_Clicked(object sender, EventArgs e)
        {
            // Переход на страницу профиля
            await Navigation.PushAsync(new NewPage4());
        }

        private async void ChatButton_Clicked(object sender, EventArgs e)
        {
            // Переход на страницу чата
            await Navigation.PushAsync(new NewPage5());
        }

        private async void CalendarButton_Clicked(object sender, EventArgs e)
        {
            // Переход на страницу календаря
            await Navigation.PushAsync(new NewPage6());
        }
    }
}
