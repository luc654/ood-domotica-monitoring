using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;








public class ZoneScreen : Screen
{
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
                Sensor sensor = selectSensor();
                new SensorScreen(zone, sensor).Loop();
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
                running = false;
                return;
        }
    }


    private Sensor selectSensor()
    {
        int selectedSensorIndex = helper.handleTerminal(zone.getSensorNamesAndID(), "Zone inspection", "Select sensor to inspect");
        Sensor selectedSensor = zone.getSensors()[selectedSensorIndex];
        return selectedSensor;
    }   
    private void selectDevice()
    {
        // int selectedSensorIndex = helper.handleTerminal(building.getZoneNamesAndID(), "Building overview", "Select zone to inspect");
        throw new NotImplementedException();
    }   
}