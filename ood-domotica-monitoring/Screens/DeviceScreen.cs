using System.Runtime.InteropServices.Swift;
using ood_domotica_monitoring.Classes;
using ood_domotica_monitoring.Classes.ComponentSubclass;

namespace ood_domotica_monitoring;

public class DeviceScreen : Screen
{
    private readonly Device selectedDevice;
    private readonly Zone zone;
    protected override string Title => $"{zone.Name} -> {selectedDevice.Naam} overview";


    protected override List<string> Options => new List<string>()
    {
        "Read Current Value",
        "View specifications of this device",
        $"Return to {zone.Name} overview"
    };

    public DeviceScreen(Zone zone, Device selectedDevice)
    {
        this.zone = zone;
        this.selectedDevice = selectedDevice;
    }

    protected override void HandleOption(int selected)
    {
        switch (selected)
        {
            case 0:
                getCurrentValue();
                break;
            case 1:
                viewSpecifications();
                break;
            case 2:                
                running = false;
                return;
        }
    }

    private void getCurrentValue()
    {
        int value = selectedDevice.Read();
        Program.GlobalContext.notification = $"Current Value is: {value}";
    }

    private void viewSpecifications()
    {
        string specifications = $"""
                                    Name: {selectedDevice.Naam}
                                    Device type: {selectedDevice.HardwareType}
                                    ID: {selectedDevice.Id}
                                    Zone ID:  {selectedDevice.ZoneId}
                                 """;
        showFormattedString(specifications);
        

    }
    
}