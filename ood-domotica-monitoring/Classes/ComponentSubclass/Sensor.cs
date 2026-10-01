using ood_domotica_monitoring.Classes;

public abstract class Sensor : Component

// A sensor is an abstract class because it must be replaced by one of its three subcomponents, since each sensor has a value and benchmark stuff this gets stored in the parent class
{
    public int LastValue { get; protected set; }
    public bool HasBenchmark { get; protected set; }
    public int BenchmarkMin { get; protected set; }
    public int BenchmarkMax { get; protected set; }

    public SensorType SensorType { get; }

    // This constructor also sets important data in the Component class, everything after base( goes to said class
    protected Sensor(int id, string naam, int zoneId, SensorType sensorType) : base(id, HardwareType.Sensor, naam, zoneId)
    {
        SensorType = sensorType;
        HasBenchmark = false;
    }

    public void SetBenchmark(int min, int max)
    {
        BenchmarkMin = min;
        BenchmarkMax = max;
        HasBenchmark = true;
    }

    // IMPORTANT. classifications of notification levels. 
    
    // Margin of n*100%, if a reading is within n*100% of its benchmark (e.g. upperlimit = 100 and reading = 95) a notification gets made  
    protected const double InfoMargin = 0.10;
    
    // Same as above, but for if a reading is outside of its benchmark by n*100% (e.g. upperlimit = 100 and reading = 130) a critical gets made. 
    protected const double CriticalMargin = 0.25;
    // anything between infomargin and criticalmargin becomes an warning

    public override int Read()
    {
        LastValue = ReadValue();

        NotificationLevel? level = getNotificationLevel(LastValue);
        if (level != null)
        {
            setNotification(DateTime.Now, level.Value, createNotificationMessage(LastValue, level.Value), Id);
        }

        return LastValue;
    }


    protected virtual NotificationLevel? getNotificationLevel(int value)
    {
        if (!HasBenchmark)
        {
            return null;
        }

        // Range must be atleast 1 
        double range = Math.Max(1, BenchmarkMax - BenchmarkMin);

        // not gonna lie this is vibecoded because my chain of if statements was an eyesore. 
        if (value < BenchmarkMin || value > BenchmarkMax)
        {
            int distanceOutside = value < BenchmarkMin ? BenchmarkMin - value : value - BenchmarkMax;
            return distanceOutside > range * CriticalMargin ? NotificationLevel.Critical : NotificationLevel.Warning;
        }

        int distanceToLimit = Math.Min(value - BenchmarkMin, BenchmarkMax - value);
        return distanceToLimit <= range * InfoMargin ? NotificationLevel.Info : null;
    }

    private string createNotificationMessage(int value, NotificationLevel level)
    {
        string benchmark = $"[{BenchmarkMin}-{BenchmarkMax}]";
        // i love my switchcases short handed and long handed
        // the switch case is dead, long live the switch case
        // perhaps i should post this on my linkedin
        return level switch
        {
            NotificationLevel.Info => $"{Naam} reading {value} is close to the limit of {benchmark}",
            NotificationLevel.Warning => $"{Naam} reading {value} is outside benchmark {benchmark}",
            _ => $"{Naam} reading {value} is far outside benchmark {benchmark}",
        };
    }

    // abstract because each subcomponent MUST replace it with its own function and logic
    protected abstract int ReadValue();

    public void setNotification(DateTime time, NotificationLevel notification, string message, int componentId)
    {
        Program.GlobalContext.notificationHelper.addNotification(time, notification, message, componentId);
    }
    
    
    
    
    
    
    
    /// <summary>
    /// All sensors types and logic
    /// </summary>
    public class MovementSensor : Sensor
    {
        public int MovementDetected { get; private set; }

        public MovementSensor(int id, string naam, int zoneId) : base(id, naam, zoneId, SensorType.Movement) { }

        protected override int ReadValue()
        {
            MovementDetected = new Random().Next(0, 2); 
            return MovementDetected;
        }
    }
    
    public class EnergySensor : Sensor
    {
        public int MaxUsage { get; private set; }

        public EnergySensor(int id, string naam, int zoneId, int maxUsage)
            : base(id, naam, zoneId, SensorType.Energy)
        {
            MaxUsage = maxUsage;
            SetBenchmark(0, maxUsage);
        }

        protected override int ReadValue()
        {
            
                return new Random().Next(0, MaxUsage + 200); 
        }

        // Using little energy is never a problem, so only the upper limit counts for energy sensors. we at brutus manufactory care for energy
        protected override NotificationLevel? getNotificationLevel(int value)
        {
            if (value < BenchmarkMax * (1 - InfoMargin))
            {
                return null;
            }

            return base.getNotificationLevel(value);
        }
    }
    
    public class TemperatureReader : Sensor
    {
        public double MinValue { get; }
        public double MaxValue { get; }

        public TemperatureReader(int id, string naam, int zoneId, double minValue, double maxValue)
            : base(id, naam, zoneId, SensorType.Temperature)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            SetBenchmark((int)minValue, (int)maxValue);
        }

        protected override int ReadValue()
        {
            double reading = MinValue + new Random().NextDouble() * (MaxValue - MinValue);
            return (int)reading;
        }
    }
}