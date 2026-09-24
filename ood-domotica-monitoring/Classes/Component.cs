namespace ood_domotica_monitoring.Classes;


// First define all enums.
public enum HardwareType
{
    Sensor,
    Device
}

public enum SensorType
{
    Movement,
    Energy,
    Temperature
}

public enum NotificationLevel
{
    Info,
    Warning,
    Critical
}

// Parent class. no touchy. For future reference, abstract functions are functions that must be implemented in child classes, for this case read is an abstract function.
public abstract class Component
{
    public int Id { get; }
    public HardwareType HardwareType { get; }
    public string Naam { get; }
    public int ZoneId { get; }

    protected Component(int id, HardwareType hardwareType, string naam, int zoneId)
    {
        Id = id;
        HardwareType = hardwareType;
        Naam = naam;
        ZoneId = zoneId;
    }

    public abstract int Read();
}

