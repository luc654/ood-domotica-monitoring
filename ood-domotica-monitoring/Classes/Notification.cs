namespace ood_domotica_monitoring.Classes;

public enum NotificationLevel
{
    Info,
    Warning,
    Critical
}
public class Notification
{
    public int id { get; }

    public DateTime datetime { get;}
    
    public string message { get; }
    public int componentId { get; }
    public NotificationLevel notificationLevel { get; }

    public Notification(DateTime time, NotificationLevel level, string message, int componentId, int id)
    {
        this.datetime = time;
        this.message = message;
        this.notificationLevel = level;
        this.componentId = componentId;
        this.id = id;
    }

    /// <summary>
    /// this function has to be called from inside a zone subclass (meaning any class that inherents terminalhelper from Zone.cs). Following this the return value (string) should be passed into showFormattedString().
    /// </summary>
    /// <returns></returns>
    public string getPrintableData()
    {
        string specifications = $"""
                                    Notification ID: {id}
                                    Level: {notificationLevel}
                                    Message: {message}
                                    -----------------
                                    Component ID:  {componentId}
                                 """;
        return specifications;
    }
    
    
    
}