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

Простой пример, использующий временное хранилище пользователей(SampleDatabase) и отправляющий уведомление указанному пользователю каждые 10 сек:

```c#
class Program
{
	static void Main(string[] args)
	{
		TelegramNotifierBot bot = new TelegramNotifierBot("YOUR-BOT-API-TOKEN", new SampleDatabase());

		int cnt = 0;

		while (true)
		{
			Thread.Sleep(10000);

			bot.SendNotification("SOME-USERNAME-WITHOUT-@", "hello!(" + cnt++ + ")");
		}
	}
}
```
