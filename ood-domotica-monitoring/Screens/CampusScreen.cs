using System.Runtime.CompilerServices;
using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class CampusScreen
{
    private static bool running = true;
	private static terminalHelper helper = new terminalHelper();

    // TODO switch to native screen element instead of this clusterfuck
    public static void loop()
    {

        // Check if data has been loaded
        if (Program.GlobalContext.campus.getBuildings().Count == 0)
        {
            Program.GlobalContext.notification = "Load data first silly, i didnt have time to make a whole CRUD system";
            return;
        }
        
        List<string> options = new List<string>
        {
            "Select Building",
            "Calculate average of all buildings",
            "Add building",
            "Read ALL devices",
            "View notifications",
            "Close",
        };
        

        while (running)
        {
            switch (helper.handleTerminal(options, "Campus overview", "Select option to continue"))
            {
                case 0:
                {
                    int indexBuilding = selectBuildings();
                    new BuildingScreen(indexBuilding).Loop();
                    break;
                }
                case 1:
                {
                    CampusScreen.loop();
                    break;
                }
                case 2:
                    // Add new building ahh
                    throw new NotImplementedException();
                case 3:
                    readAllDevices();
                    break;
                case 4:
                    new NotificationScreen().Loop();
                    break;
                case 5:
                    running = false;
                    return;
            }
        }
    }


    private static int selectBuildings()
    {
        // I dont know how to explain, but one side is 0 based index while the other is 1 based index? just add 1 to the returnvaluu 
        int selectedBuildingIndex = helper.handleTerminal(Program.GlobalContext.campus.getBuildingNamesAndID(), "Campus overview", "Select building to inspect");
        return selectedBuildingIndex + 1;
    }

    private static void readAllDevices()
    {

        int notificationAmountBefore = Program.GlobalContext.notificationHelper.getNotificationCount();
        foreach (var buildings in Program.GlobalContext.campus.getBuildings())
        {
            foreach (var zone in buildings.getZones())
            {
                zone.readAllComponents();
            }
        }

        int notificationAmountAfter = Program.GlobalContext.notificationHelper.getNotificationCount();
        int newNotificationAmount = notificationAmountAfter - notificationAmountBefore;
        
        Program.GlobalContext.notification = $"{newNotificationAmount.ToString()} notifications added!";
    }
}