namespace ood_domotica_monitoring.Classes.ComponentSubclass;

public abstract class Device : Component
{
    public int CurrentValue { get; protected set; }
    public bool IsActive { get; protected set; }

    protected Device(int id, string naam, int zoneId)
        : base(id, HardwareType.Device, naam, zoneId)
    {
        IsActive = false;
    }

    public void enable()
    {
        IsActive = true;
    }

    // guards writes so a disabled device can't silently change state
    public void setValue(int newValue)
    {
        if (!IsActive)
        {
            Program.GlobalContext.notification = $"{Naam} is not active, value not set";
            return;
        }
        CurrentValue = newValue;
        OnValueSet(newValue);
    }

    protected virtual void OnValueSet(int newValue) { }

    public override int Read() => CurrentValue;
}

public class SmartLighting : Device
{
    public int BrightnessPercentage { get; private set; }

    public SmartLighting(int id, string naam, int zoneId) : base(id, naam, zoneId) { }

    public void setLightLevel(int percentage)
    {
        // clamp to a valid percentage range
        percentage = Math.Clamp(percentage, 0, 100);
        BrightnessPercentage = percentage;
        setValue(percentage);
    }
}

public class HVACSystem : Device
{
    public int TargetHVACvalue { get; private set; }

    public HVACSystem(int id, string naam, int zoneId) : base(id, naam, zoneId) { }

    // kept the diagram's original name (Dutch: "stel ... in" = "set ...")
    public void StelStandIn(int stand)
    {
        TargetHVACvalue = stand;
        setValue(stand);
    }
}

public class Thermostate : Device
{
    public int TargetTemperature { get; private set; }

    public Thermostate(int id, string naam, int zoneId) : base(id, naam, zoneId) { }

    public void setTargetValue(int target)
    {
        TargetTemperature = target;
        setValue(target);
    }
}