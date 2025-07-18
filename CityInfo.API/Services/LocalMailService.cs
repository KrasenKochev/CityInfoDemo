namespace CityInfo.API.Services
{
	public class LocalMailService
	{
		private string _mailTo = "dummyTo@dummyTo.com";
		private string _mailFrom = "dummyFrom@dummyFrom.com";

		public void Send (string subject, string message)
		{
			//Send mail - ToString output to console window
			Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}.");
			Console.WriteLine($"Subject: {subject}");
			Console.WriteLine($"Message: {message}");
		}






	}
}
