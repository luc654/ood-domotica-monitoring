using ood_domotica_monitoring;
using ood_domotica_monitoring.Classes;

public class Program
{
	
	public static void Main()
	{
		bool running = true;
		
		List<string> options = new List<string>
		{
			"Laadt data in",
			"Seed componenten",
			"Begin scernario"
		};
		terminalHelper helper = new terminalHelper();
		while (running)
		{
			
		switch (helper.handleTerminal(options, "Mini casus", "Selecteer een optie om de Casus te beginnen"))
		{
			case 0:
			{
				DataLoader.loadData();
				Program.GlobalContext.notification = "Data ingeladen";
				break;
			}
			case 1:
			{
				// seedComponents throws when there are no zones yet, show that as a notification instead of crashing
				try
				{
					DataLoader.seedComponents();
					Program.GlobalContext.notification = "Componenten toegevoegd";
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