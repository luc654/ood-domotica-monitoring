namespace ood_domotica_monitoring.Classes;

public class Campus
{
    private int id { get; }
    private string name { get; }
    private List<Building> buildingList { get; }

    public Campus(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.buildingList = new List<Building>();
    }

    public int Id => id;
    public string Name => name;

    public void addBuilding(Building newBuilding)
    {
        buildingList.Add(newBuilding);
    }

    public List<Building> getBuildings()
    {
        return buildingList;
    }

    public Building getBuildingByID(int id)
    {
        Building selectedBuilding = buildingList.Find(e => e.Id == id);
        if (selectedBuilding == null)
        {
            // default to the first building if no building is found 
            Program.GlobalContext.notification = "Building not found";
            selectedBuilding = buildingList.First();
        }
        return selectedBuilding;
    }

    public List<string> getBuildingNamesAndID()
    {
        List<string> returnList = new List<string>();
        foreach (var building in this.buildingList)
        {
            returnList.Add($"{building.Id.ToString()} {building.Name}");
        }

        return returnList;
    }
}