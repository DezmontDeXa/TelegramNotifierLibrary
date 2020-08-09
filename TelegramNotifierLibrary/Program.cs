using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramNotifierLibrary
{
	//class Program
	//{
	//	static void Main(string[] args)
	//	{
	//		TelegramNotifierBot bot = new TelegramNotifierBot("1364647734:AAFhug2kLz2A73MKteT3fH7-Wg9Z1qdmG1A", new SampleDatabase());

	//		Console.WriteLine("Write \"Exit\" for close.");

	//		int cnt = 0;

	//		while (true)
	//		{
	//			Thread.Sleep(10000);

	//			bot.SendNotification("DezmontDeXa", "hello!(" + cnt++ + ")");
	//		}
	//	}
	//}

	public class TelegramNotifierBot : IDisposable
	{
		CancellationTokenSource cts = new CancellationTokenSource();
		TelegramBotClient client;
		IUsernameProvider db;
		string messageForNewUser;
		public TelegramNotifierBot(string token, IUsernameProvider database, string messageForNewUser = "You have subscribed to notifications.")
		{
			ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

			this.messageForNewUser = messageForNewUser;

			db = database;

			client = new TelegramBotClient(token);

			client.OnMessage += Client_OnMessage;

			client.StartReceiving(new[] { UpdateType.Message }, cts.Token);
		}
		public void Dispose()
		{
			cts.Cancel();
		}
		private void Client_OnMessage(object sender, MessageEventArgs e)
		{
			var username = e.Message.From.Username;

			var chatId = e.Message.Chat.Id;

			if (db.IsExistUsername(username))
			{
				return;
			}
			else
			{
				db.AddUsername(username, chatId);

				client.SendTextMessageAsync(chatId, messageForNewUser).Wait();
			}
		}
		public void SendNotification(string username, string message)
		{
			long chatId = db.GetChatId(username);

			client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(chatId), message).Wait();
		}
	}

	/// <summary>
	/// Interface for work with usernames and chatIds
	/// </summary>
	public interface IUsernameProvider
	{
		/// <summary>
		/// Check for username exist
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		bool IsExistUsername(string username);

		/// <summary>
		/// Add new username and him chatId
		/// </summary>
		/// <param name="username"></param>
		/// <param name="chatId"></param>
		void AddUsername(string username, long chatId);

		/// <summary>
		/// Get chatId by username
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		long GetChatId(string username);
	}

	/// <summary>
	/// Simple implementation for IDatabase with Dictionary[username, chatId]
	/// </summary>
	public class SampleUsernameProvider : IUsernameProvider
	{
		Dictionary<string, long> db = new Dictionary<string, long>();

		public void AddUsername(string username, long chatId)
		{
			db.Add(username, chatId);
		}

		public long GetChatId(string username)
		{
			return db[username];
		}

		public bool IsExistUsername(string username)
		{
			return db.ContainsKey(username);
		}
	}


}
