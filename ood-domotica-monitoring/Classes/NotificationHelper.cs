namespace ood_domotica_monitoring.Classes;
public class NotificationHelper
{
    private List<Notification> _notifications { get; } = new List<Notification>() { };
    private int currId = 0;

    public List<string> getAllNotificationsAsString()
    {
        List<string> returnList = new List<string>();
        // newest first, so walk backwards through the list (down to and including index 0)
        for (int i = _notifications.Count - 1; i >= 0; i--)
        {
            Notification notification = _notifications[i];
            returnList.Add($"{notification.datetime.DayOfWeek} {notification.datetime.Hour}:{notification.datetime.Minute}:{notification.datetime.Second} | {notification.notificationLevel} | {notification.message}");
        }

        return returnList;
    }

    // id is the position in getAllNotificationsAsString(), which is newest first, so flip it back
    public Notification getNotificationByListID(int id)
    {
        return _notifications[_notifications.Count - 1 - id];
    }

    public int getNotificationCount()
    {
        Console.WriteLine(_notifications.Count);
        return _notifications.Count;
    }

    public void addNotification(DateTime time, NotificationLevel notification, string message, int componentId)
    {
        currId++;
        Console.WriteLine("New notification added:");
        // since notifications dont use user generated values, we dont need to validate them.
        Notification newNotification = new Notification(time, notification, message, componentId, currId);
        _notifications.Add(newNotification);
    }
}