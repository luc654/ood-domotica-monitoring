using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class NotificationScreen : Screen
{
    protected override string Title => "View notifications";
    protected override List<string> Options => new List<string>
    {
        "View recent notifications",
        "Advanced view notifications",
        "Search for notifications",
        "Back to campus overview",
    };
    
    
    protected override void HandleOption(int selected)
    {
        switch (selected)
        {
            case 0:
                selectNotification();
                break;
            case 1:
                break;
            case 2:
                // TODO Add sennor
                break;
            case 3:
                running = false;
                return;
        }
    }


    private void selectNotification()
    {
        NotificationHelper notificationHelper = Program.GlobalContext.notificationHelper;

        // nothing to select yet, show a message instead of crashing on an empty list
        if (notificationHelper.getNotificationCount() == 0)
        {
            Program.GlobalContext.notification = "No notifications yet";
            return;
        }

        int selectedIndex = helper.handleTerminal(notificationHelper.getAllNotificationsAsString(), Title,
            "Select notification");
        Notification selectedNotification = notificationHelper.getNotificationByListID(selectedIndex);
        showFormattedString(selectedNotification.getPrintableData());

    }
}