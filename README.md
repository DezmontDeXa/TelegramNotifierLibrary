# TelegramNotifierLibrary

Простая библиотека для реализации уведомлений через телеграм бота

Основной класс: TelegramNotifierBot. Конструктор принимает 3 аргумента:
1. строка с API токеном бота
2. реализация интерфейса IDatabase
3. (Необязательный) сообщение для нового клиента. По-умолчанию: "You have subscribed to notifications."

TelegramNotifierBot реализует IDisposable. 
При создании экземляра начинает получать сообщения. 
Если сообщение приходит от нового пользователя, запоминает пользователя и его chatId.

Для отправки уведомления используется метод SendNotification. 

Интерфейс IDatabase объявляет 3 метода: IsExistUsername, AddUsername, GetChatId.


