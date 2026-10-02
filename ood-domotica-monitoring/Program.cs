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
			"Start scenario",
			"Play music (windows only)"
		};
		terminalHelper helper = new terminalHelper();
		while (running)
		{
			
		switch (helper.handleTerminal(options, "Mini casus", "Use arrow keys or 1 - 9 to choose an option, hit enter to select!"))
		{
			case 0:
			{
				DataLoader.loadData();
				Program.GlobalContext.notification = "Data loaded";
				break;
			}
			case 1:
			{
				try
				{
					int amount = DataLoader.seedComponents();
					Program.GlobalContext.notification = $"{amount} components loaded";
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
			case 3:
			{
				MusicPlayer musicPlayer = new MusicPlayer();
				musicPlayer.playSounds();
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