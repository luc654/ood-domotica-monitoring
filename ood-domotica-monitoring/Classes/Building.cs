namespace ood_domotica_monitoring.Classes;

public class Building
{
    private int id { get; }
    private string name { get; }
    private List<Zone> zoneList { get; }

    public Building(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.zoneList = new List<Zone>();
    }

    public int Id => id;
    public string Name => name;

    public void addZone(Zone newZone)
    {
        zoneList.Add(newZone);
    }

    public List<Zone> getZones()
    {
        return zoneList;
    }

    public Zone getZoneByID(int id)
    {
        Zone selectedZone = zoneList.Find(e => e.Id == id);
        if (selectedZone == null)
        {
            // default to the first zone if no zone is found
            Program.GlobalContext.notification = "Zone not found";
            selectedZone = zoneList.First();
        }
        return selectedZone;
    }
}