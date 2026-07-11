using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Myfamily1
{
    public partial class NewPage1 : ContentPage
    {
        private readonly HttpClient _client;

        public NewPage1()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private async void Cont(object sender, EventArgs e)
        {
            try
            {
                var email = post.Text;
                var password = pas.Text;

                // Логирование введенных данных
                System.Diagnostics.Debug.WriteLine($"Sending request to server: Email - {email}, Password - {password}");

                // Формирование строки с данными для отправки на сервер
                var data = $"email={email}&password={password}";
                var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

                // Отправка POST запроса на сервер
                var response = await _client.PostAsync("http://localhost:3001/login", content);

                // Чтение содержимого ответа
                var responseContent = await response.Content.ReadAsStringAsync();

                // Проверка на успешный вход
                if (responseContent.Contains("success\":true")) // Проверка на успешный вход на основе содержимого ответа
                {
                    // Переход на следующую страницу
                    await Navigation.PushAsync(new NewPage4());
                }
                else
                {
                    // Отображение сообщения об ошибке
                    error.TextColor = Color.FromHex("#ff0000");
                    error.Text = "Ошибка входа";
                }
            }
            catch (Exception ex)
            {
                // Отображение сообщения об ошибке
                error.TextColor = Color.FromHex("#ff0000");
                error.Text = "Произошла ошибка: " + ex.Message;
            }
        }
    }
}
