using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class ZoneScreen
{
    private static terminalHelper helper = new terminalHelper();
    private static bool running = true;
    private static Zone zone = null;
    
    public static void loop(int zoneId, Building building)
    {
        zone = building.getZoneByID(zoneId);
        
        List<string> options = new List<string>
        {
            "Select Sensor",
            "Select Device",
            "Add new Sensor",
            "Add new Device",
            $"Back to {building.Name}",
        };
        

        while (running)
        {
            switch (helper.handleTerminal(options, $"{zone.Name} overview", "Select option to continue"))
            {
                case 0:
                {
                    // selectZone();
                    throw new NotImplementedException();
                    break;
                }
                case 1:
                {
                    CampusScreen.loop();
                    break;
                }
                case 2:
                {
                    throw new NotImplementedException();
                }
                case 3:
                {
                    throw new NotImplementedException();
                }
                case 4:
                {
                    return;
                }
            }
        }
    }


    private static void selectSensor()
    {
        // int selectedSensorIndex = helper.handleTerminal(building.getZoneNamesAndID(), "Building overview", "Select zone to inspect");
        throw new NotImplementedException();
    }   
    private static void selectDevice()
    {
        // int selectedSensorIndex = helper.handleTerminal(building.getZoneNamesAndID(), "Building overview", "Select zone to inspect");
        throw new NotImplementedException();
    }   
}