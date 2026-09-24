using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class BuildingScreen
{
    private static terminalHelper helper = new terminalHelper();
    private static bool running = true;
    private static Building building = null;
    
    public static void loop(int buildingId)
    {
        building = Program.GlobalContext.campus.getBuildingByID(buildingId);
        
        List<string> options = new List<string>
        {
            "Select Zone",
            "Calculate average of all zones",
            "Add Zone",
            "Back to Campus",
        };
        

        while (running)
        {
            switch (helper.handleTerminal(options, $"{building.Name} overview", "Select option to continue"))
            {
                case 0:
                {
                    int selectedZoneID = selectZone();
                    ZoneScreen.loop(selectedZoneID, building);
                    break;
                }
                case 1:
                {
                    CampusScreen.loop();
                    break;
                }
                case 2:
                {
                    throw new NotImplementedException();
                }
                case 3:
                {
                    return;
                }
            }
        }
    }


    private static int selectZone()
    {
        int selectedZoneIndex = helper.handleTerminal(building.getZoneNamesAndID(), "Building overview", "Select zone to inspect");
        return selectedZoneIndex;

    }
}