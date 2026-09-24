using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;








public class ZoneScreen : Screen
{
    private static terminalHelper helper = new terminalHelper();
    private static bool running = true;
    private readonly Building building;
    private readonly Zone zone;

    public ZoneScreen(int zoneId, Building building)
    {
        this.building = building;
        this.zone = building.getZoneByID(zoneId);
    }

    protected override string Title => $"{zone.Name} overview";
    
    protected override List<string> Options => new List<string>
    {
        "Select Sensor",
        "Select Device",
        "Add new Sensor",
        "Add new Device",
        $"Back to {building.Name}",
    };

    protected override void HandleOption(int selected)
    {
        switch (selected)
        {
            case 0:
                // TODO select sensor
                break;
            case 1:
                // TODO Select device
                break;
            case 2:
                // TODO Add sennor
                break;
            case 3:
                // TODO Add device
                break;
            case 4:
                return;
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