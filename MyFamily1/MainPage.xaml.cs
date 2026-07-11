using Microsoft.Maui.Controls;

namespace Myfamily1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Обработчик события для кнопки "Вход"
        private async void OnLogInClicked(object sender, EventArgs e)
        {
            // Переходим на страницу входа
            await Navigation.PushAsync(new NewPage1());
        }

        // Обработчик события для кнопки "Создать семью"
        private async void OnCreateFamilyClicked(object sender, EventArgs e)
        {
            // Переходим на страницу создания семьи
            await Navigation.PushAsync(new NewPage2());
        }

        // Обработчик события для кнопки "Войти в семью"
        private async void OnJoinFamilyClicked(object sender, EventArgs e)
        {
            // Переходим на страницу входа в семью
            await Navigation.PushAsync(new NewPage3());
        }
    }
}
