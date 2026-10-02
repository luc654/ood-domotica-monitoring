using ood_domotica_monitoring.Classes;
using ood_domotica_monitoring.Classes.ComponentSubclass;

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
                Device device = selectDevice();
                new DeviceScreen(zone, device).Loop();
                break;
            case 2:
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
    private Device selectDevice()
    {
        int selectedDeviceIndex = helper.handleTerminal(zone.getDeviceNamesAndID(), "Zone inspection", "Select device to inspect");
        Device selectedSensor = zone.getDevice()[selectedDeviceIndex];
        return selectedSensor;
    }   
}