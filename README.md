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

Простой пример:
	class Program
	{
		static void Main(string[] args)
		{
			TelegramNotifierBot bot = new TelegramNotifierBot("1364647734:AAFhug2kLz2A73MKteT3fH7-Wg9Z1qdmG1A", new SampleDatabase());

			Console.WriteLine("Write \"Exit\" for close.");

			int cnt = 0;

			while (true)
			{
				Thread.Sleep(10000);

				bot.SendNotification("DezmontDeXa", "hello!(" + cnt++ + ")");
			}
		}
	}
