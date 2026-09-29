using System.Runtime.InteropServices.Swift;
using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class SensorScreen : Screen
{
    private readonly Sensor selectedSensor;
    private readonly Zone zone;
    protected override string Title => $"{zone.Name} -> {selectedSensor.Naam} overview";


    protected override List<string> Options => new List<string>()
    {
        "Read Current Value",
        "View Notifications made by this sensor",
        "View specifications of this sensor",
        $"Return to {zone.Name} overview"
    };

    public SensorScreen(Zone zone, Sensor selectedSensor)
    {
        this.zone = zone;
        this.selectedSensor = selectedSensor;
    }

    protected override void HandleOption(int selected)
    {
        switch (selected)
        {
            case 0:
                getCurrentValue();
                break;
            case 1:
                throw new NotImplementedException();
                break;
            case 2:
                viewSpecifications();
                break;
            case 3:                
                running = false;
                return;
        }
    }

    private void getCurrentValue()
    {
        int value = selectedSensor.Read();
        Program.GlobalContext.notification = $"Huidige waarde is: {value}";
    }

    private void viewSpecifications()
    {
        string specifications = $"""
                                    Naam: {selectedSensor.Naam}
                                    Laatste waarde: {selectedSensor.LastValue}
                                    Min - Max waardes: {selectedSensor.BenchmarkMin} - {selectedSensor.BenchmarkMax}
                                    Sensor soort: {selectedSensor.SensorType}
                                    ID: {selectedSensor.Id}
                                    Zone ID:  {selectedSensor.ZoneId}
                                 """;
        showFormattedString(specifications);

    }
}