namespace ood_domotica_monitoring.Classes;
public class NotificationHelper
{
    private List<Notification> _notifications { get; } = new List<Notification>() { };
    private int currId = 0;

    public List<string> getAllNotificationsAsString()
    {

        return notificationListToStringList(_notifications);
    }

    // id is the position in getAllNotificationsAsString(), which is newest first, so flip it bakc using - 1 - id
    public Notification getNotificationByListID(int id)
    {
        return _notifications[_notifications.Count - 1 - id];
    }

    public int getNotificationCount()
    {
        return _notifications.Count;
    }

    public void addNotification(DateTime time, NotificationLevel notification, string message, int componentId)
    {
        currId++;
        Notification newNotification = new Notification(time, notification, message, componentId, currId);
        _notifications.Add(newNotification);
    }
    
    public List<string> getNotificationLevelAsStringList()
    {
        List<string> values = Enum.GetNames(typeof(NotificationLevel)).ToList();
        return values;
    }

    public List<string> getNotificationsByLevel(NotificationLevel level)
    {
        List<Notification> notificationsWithSpecifiedLevel = _notifications.Where(o => o.notificationLevel == level).ToList();
        
        return notificationListToStringList(notificationsWithSpecifiedLevel);
    }

    public Notification getNotificationById(int id)
    {
        return _notifications.First(o => o.id == id);
    }

    public Notification getNotificationByString(string rawValue)
    {
        string[] tokens = rawValue.Split(new[] { " |" }, StringSplitOptions.None);
        int notificationId =  Int32.Parse(tokens[0]);
        return getNotificationById(notificationId);

    }

    private List<string> notificationListToStringList(List<Notification> notificationsList)
    {
        List<string> returnList = new List<string>();
        
        for (int i = notificationsList.Count - 1; i >= 0; i--)
        {
            Notification notification = notificationsList[i];
            returnList.Add($"{notification.id} | {notification.datetime.DayOfWeek} {notification.datetime.Hour}:{notification.datetime.Minute}:{notification.datetime.Second} | {notification.notificationLevel} | {notification.message}");
        }

        return returnList;
    }

    public List<string> getNotificationListStringByQuery(string query)
    {
        List<string> returnlist = new List<string>();

        foreach (Notification notification in _notifications)
        {
            if (notification.message.ToUpper().Contains(query.ToUpper()))
            {
                returnlist.Add($"{notification.id} | {notification.datetime.DayOfWeek} {notification.datetime.Hour}:{notification.datetime.Minute}:{notification.datetime.Second} | {notification.notificationLevel} | {notification.message}");

            }
        }

        Console.ReadLine();
        return returnlist;
    }
}