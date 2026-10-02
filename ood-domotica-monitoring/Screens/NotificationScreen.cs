using ood_domotica_monitoring.Classes;

namespace ood_domotica_monitoring;

public class NotificationScreen : Screen
{
    protected override string Title => "View notifications";
    protected override string Description => $"{Program.GlobalContext.notificationHelper.getNotificationCount()} notifications detected.";

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
                advancedViewNotification();
                break;
            case 2:
                searchForNotifications();
                break;
            case 3:
                running = false;
                return;
        }
    }


    private void selectNotification()
    {
        NotificationHelper notificationHelper = Program.GlobalContext.notificationHelper;

        // nothing to select yet
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


    private void advancedViewNotification()
    {
        NotificationHelper notificationHelper = Program.GlobalContext.notificationHelper;

        // nothing to select yet
        if (notificationHelper.getNotificationCount() == 0)
        {
            Program.GlobalContext.notification = "No notifications yet";
            return;
        }

        // Prompt user for specific notification level
        int selectedNotificationStateIndex = helper.handleTerminal(
            notificationHelper.getNotificationLevelAsStringList(), "Advanced notification viewer",
            "Select a notification level to view");
        NotificationLevel selectedNotificationState = (NotificationLevel) selectedNotificationStateIndex;

        List<string> notifications = notificationHelper.getNotificationsByLevel(selectedNotificationState);
        
        
        // Check if notifications exist
        if (notifications.Count == 0)
        {
            Program.GlobalContext.notification = "No notifications yet";
            return;
        }
        
        int selectedIndex = helper.handleTerminal(notifications, "",
            "Select notification");
        Notification selectedNotification = notificationHelper.getNotificationByString(notifications[selectedIndex]);
        showFormattedString(selectedNotification.getPrintableData());

    }

    private void searchForNotifications()
    {
        NotificationHelper notificationHelper = Program.GlobalContext.notificationHelper;
        string query = helper.handleQuestion("Type query for filtering notifications");
        List<string> notifications = notificationHelper.getNotificationListStringByQuery(query);
        
        // Check if notifications exist
        if (notifications.Count == 0)
        {
            Program.GlobalContext.notification = $"No notifications with query '{query}'. Try something else";
            return;
        }
        int selectedIndex = helper.handleTerminal(notifications, "",
            "Select notification");
        Notification selectedNotification = notificationHelper.getNotificationByString(notifications[selectedIndex]);
        showFormattedString(selectedNotification.getPrintableData());
    }
}