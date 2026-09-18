using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class DataLoader
{
    public static void loadData()
    {
        Campus campus = Program.GlobalContext.campus;

        Building mercury = new Building(1, "Mercury");
        mercury.addZone(new Zone(1, "Red"));
        mercury.addZone(new Zone(2, "Orange"));
        mercury.addZone(new Zone(3, "Yellow"));

        Building venus = new Building(2, "Venus");
        venus.addZone(new Zone(4, "Green"));
        venus.addZone(new Zone(5, "Blue"));
        venus.addZone(new Zone(6, "Indigo"));

        Building jupiter = new Building(3, "Jupiter");
        jupiter.addZone(new Zone(7, "Violet"));
        jupiter.addZone(new Zone(8, "Cyan"));
        jupiter.addZone(new Zone(9, "Magenta"));

        campus.addBuilding(mercury);
        campus.addBuilding(venus);
        campus.addBuilding(jupiter);
    }
}