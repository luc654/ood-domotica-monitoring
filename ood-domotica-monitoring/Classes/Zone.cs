using ood_domotica_monitoring.Classes.ComponentSubclass;

namespace ood_domotica_monitoring.Classes;

public class Zone
{
    private int id { get; }
    private string name { get; }

    private List<Component> componentList { get; }

    public Zone(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.componentList = new List<Component>();
    }

    public int Id => id;
    public string Name => name;

    public void addComponent(Component newComponent)
    {
        componentList.Add(newComponent);
    }

    public List<Component> getComponents()
    {
        return componentList;
    }

    // in Zone.cs
    public List<Sensor> getSensors() => componentList.OfType<Sensor>().ToList();
    public List<Device> getDevice() => componentList.OfType<Device>().ToList();

    public List<string> getSensorNamesAndID()
    {
        List<string> returnList = new List<string>();
        foreach (var sensor in getSensors())
        {
            returnList.Add($"{sensor.Id} {sensor.Naam}");
        }
        return returnList;
    }
    
    public List<string> getDeviceNamesAndID()
    {
        List<string> returnList = new List<string>();
        foreach (var device in getDevice())
        {
            returnList.Add($"{device.Id} {device.Naam}");
        }
        return returnList;
    }

}