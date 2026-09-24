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

    // this is short code for read only variable, expression-bodied member 
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
    public List<string> getZoneNamesAndID()
    {
        List<string> returnList = new List<string>();
        foreach (var zone in this.zoneList)
        {
            returnList.Add($"{zone.Id.ToString()} {zone.Name}");
        }

        return returnList;
    }
}