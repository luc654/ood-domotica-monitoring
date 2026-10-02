using System.Runtime.CompilerServices;
using System.Text;
using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class CampusScreen
{
    private static bool running = true;
	private static terminalHelper helper = new terminalHelper();

    // TODO switch to native screen element instead of this clusterfuck
    // Note, i might not have time to do this because its a confusing mess but just know that all the other screens do use the OOP screen class so just take inspiration from that
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
            "Read ALL devices",
            "View notifications",
            "Close",
        };
        

        while (running)
        {
            switch (helper.handleTerminal(options, "Campus overview", "Select option to continue"))
            {
                case 0:
                    int indexBuilding = selectBuildings();
                    new BuildingScreen(indexBuilding).Loop();
                    break;
                case 1:
                    calculateAverage();
                    break;
                
                case 2:
                    readAllDevices();
                    break;
                case 3:
                    new NotificationScreen().Loop();
                    break;
                case 4:
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

    private static void calculateAverage()
    {
        
        int specificCalculateIndex = helper.handleTerminal(Enum.GetNames(typeof(SensorType)).ToList(), "Campus overview", "Select specific sensor type to inspect across all buildings");
        SensorType selectedSensor = (SensorType)specificCalculateIndex;

        StringBuilder sb = new StringBuilder();
        int total = 0;
        foreach (var building in Program.GlobalContext.campus.getBuildings())
        {
            int buildingTotal = 0;
            int index = 0;
            
            sb.AppendLine($"Building: {building.Name}");
            foreach (var zone in building.getZones())
            {
                int zonevalue = zone.returnSpecificSensortypesValue(selectedSensor);
                sb.AppendLine($"    Zone {zone.Name}: {zonevalue}");
                total += zonevalue;
                buildingTotal += zonevalue;
                index++;
            }
            sb.AppendLine($"Total: {buildingTotal} | Average: {(int)Math.Ceiling((double)buildingTotal / index)}");
            sb.AppendLine("");
            
        }
        sb.AppendLine($"Total value of {selectedSensor} across all buildings: {total} | Average across all buildings: {(int)Math.Ceiling((double)total / Program.GlobalContext.campus.getBuildings().Count)}");
        List<string> returnButton = new List<string>() { "terug" };

        helper.handleTerminal(returnButton, $"{selectedSensor} usage across all buildings.", sb.ToString());

        
    }
}