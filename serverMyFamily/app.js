const express = require('express');
const bodyParser = require('body-parser');
const http = require('http');
const WebSocket = require('ws');

const app = express();
const port = 3001;
const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

// Разрешаем приложению использовать json и urlencoded данные
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// Обработка POST запроса на /login
app.post('/login', (req, res) => {
    // Удаление проверки данных
    // Проверяем наличие введенных данных
    // if (email === 'example@gmail.com' && password === 'password') {
    // Если пользователь найден, отправляем успешный ответ
    res.status(200).json({ success: true });
    // } else {
    // Если пользователь не найден, отправляем ошибку
    // res.status(401).json({ success: false, message: 'Invalid email or password' });
    // }
    console.log(req.body);
});

// Обработка POST запроса на /sendMessage
app.post('/sendMessage', (req, res) => {
    // Получаем сообщение из тела запроса
    const { message } = req.body;

    // Отправляем сообщение всем подключенным клиентам через WebSocket
    wss.clients.forEach(function each(client) {
        if (client.readyState === WebSocket.OPEN) {
            client.send(message);
        }
    });

    // Отправляем успешный ответ
    res.status(200).json({ success: true });
    console.log(req.body);
});

// Запускаем сервер
server.listen(port, () => {
    console.log(`Server is listening at http://localhost:${port}`);
});

// Обработка подключения нового клиента через WebSocket
wss.on('connection', (ws) => {
    console.log('a user connected');

    // Обработка получения сообщения от клиента и его отправка всем подключенным клиентам через WebSocket
    ws.on('message', (message) => {
        console.log('message: ' + message);
        wss.clients.forEach(function each(client) {
            if (client !== ws && client.readyState === WebSocket.OPEN) {
                client.send(message);
            }
        });
    });

    // Обработка отключения клиента
    ws.on('close', () => {
        console.log('user disconnected');
    });
});
