using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Myfamily1
{
    public partial class NewPage5 : ContentPage
    {
        private readonly HttpClient _client;
        private ObservableCollection<string> _messages;
        private ClientWebSocket _webSocket;

        public NewPage5()
        {
            InitializeComponent();
            _client = new HttpClient();
            _messages = new ObservableCollection<string>();
            messageListView.ItemsSource = _messages; // Используем ListView для отображения сообщений

            // Инициализация WebSocket при загрузке страницы
            InitializeWebSocket();
        }

        private async void InitializeWebSocket()
        {
            _webSocket = new ClientWebSocket();
            await _webSocket.ConnectAsync(new Uri("ws://localhost:3001"), CancellationToken.None);

            // Запуск потока для получения сообщений от сервера
            Task.Run(async () =>
            {
                byte[] buffer = new byte[1024];
                while (_webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // Добавление полученного сообщения в чат
                        _messages.Add($"Сын: {message}");
                    });
                }
            });
        }

        private async void SendMessage_Clicked(object sender, EventArgs e)          
        {
            try
            {
                var message = messageEntry.Text;

                if (string.IsNullOrWhiteSpace(message))
                    return;

                // Отправка сообщения на сервер через WebSocket
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                // Добавление отправленного сообщения в чат
                Device.BeginInvokeOnMainThread(() =>
                {
                    _messages.Add($"Вы: {message}");
                });

                // Очистка поля ввода
                messageEntry.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
            }
        }
    }
}