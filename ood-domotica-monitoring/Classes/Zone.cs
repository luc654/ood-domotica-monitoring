namespace ood_domotica_monitoring.Classes;

public class Zone
{
    private int id { get; }
    private string name { get; }

    public Zone(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public int Id => id;
    public string Name => name;

}