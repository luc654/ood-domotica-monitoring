using System.Runtime.CompilerServices;

namespace ood_domotica_monitoring;

public class CampusScreen
{
    private static bool running = true;
	private static terminalHelper helper = new terminalHelper();

    public static void loop()
    {
        List<string> options = new List<string>
        {
            "Select Building",
            "Calculate average of all buildings",
            "Add building",
            "Close",
        };
        

        while (running)
        {
            switch (helper.handleTerminal(options, "Campus overview", "Select option to continue"))
            {
                case 0:
                {
                    int indexBuilding = selectBuildings();
                    BuildingScreen.loop(indexBuilding);
                    break;
                }
                case 1:
                {
                    CampusScreen.loop();
                    break;
                }
            }
        }
    }


    private static int selectBuildings()
    {
        // I dont know how to explain, but one side is 0 based index while the other is 1 based index? just add 1 to the returnvaluu 
        int selectedBuildingIndex = helper.handleTerminal(Program.GlobalContext.campus.getBuildingNamesAndID(), "Campus overview", "Select building to inspect");
        return selectedBuildingIndex + 1;
    }
}