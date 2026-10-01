using ood_domotica_monitoring;
using ood_domotica_monitoring.Classes;

public class Program
{
	
	public static void Main()
	{
		bool running = true;
		
		List<string> options = new List<string>
		{
			"Load data",
			"Fill components",
			"Start scenario"
		};
		terminalHelper helper = new terminalHelper();
		while (running)
		{
			
		switch (helper.handleTerminal(options, "Mini casus", "Use arrow keys or 1 - 9 to choose an option, hit enter to select!"))
		{
			case 0:
			{
				DataLoader.loadData();
				Program.GlobalContext.notification = "Data loaded, 1337 engaged";
				break;
			}
			case 1:
			{
				try
				{
					DataLoader.seedComponents();
					Program.GlobalContext.notification = "Components hashed (decrypting SHA512)";
				}
				catch (InvalidOperationException e)
				{
					Program.GlobalContext.notification = e.Message;
				}
				break;
			}
			case 2:
			{
				CampusScreen.loop();
				break;
			}
		}
		}
	}
	
	
	/// <summary>
	/// Global variables holder.
	/// </summary>
	public static class GlobalContext
	{

		public static string notification {get; set;} = "";
		public static Campus campus { get; set; } = new Campus(1, "Main Campus");
		public static NotificationHelper notificationHelper { get; } = new NotificationHelper();
	}

}