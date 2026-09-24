using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class BuildingScreen : Screen
{
    private readonly Building building;

    public BuildingScreen(int buildingId)
    {
        building = Program.GlobalContext.campus.getBuildingByID(buildingId);
    }

    protected override string Title => $"{building.Name} overview";

    protected override List<string> Options => new List<string>
    {
        "Select Zone",
        "Calculate average of all zones",
        "Add Zone",
        "Back to Campus",
    };

    protected override void HandleOption(int selected)
    {
        switch (selected)
        {
            case 0:
                int zoneId = SelectZone();
                new ZoneScreen(zoneId, building).Loop();
                break;
            case 1:
                break;
            case 2:
                throw new NotImplementedException();
            case 3:
                running = false;
                break;
        }
    }

    private int SelectZone()
        => helper.handleTerminal(building.getZoneNamesAndID(), "Building overview", "Select zone to inspect");

}