using ood_domotica_monitoring.Classes;

public abstract class Sensor : Component
{
    public int LastValue { get; protected set; }
    public bool HasBenchmark { get; protected set; }
    public int BenchmarkMin { get; protected set; }
    public int BenchmarkMax { get; protected set; }
    
    public SensorType SensorType { get; }

    protected Sensor(int id, string naam, int zoneId, SensorType sensorType)
        : base(id, HardwareType.Sensor, naam, zoneId)
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

    // Public Read() wraps the sensor-specific reading logic and
    // automatically raises a notification if the value falls outside
    // the configured benchmark range.
    public override int Read()
    {
        LastValue = ReadValue();

        if (HasBenchmark && (LastValue < BenchmarkMin || LastValue > BenchmarkMax))
        {
            setNotification(
                DateTime.Now,
                NotificationLevel.Warning,
                $"{Naam} reading {LastValue} is outside benchmark [{BenchmarkMin}-{BenchmarkMax}]",
                Id);
        }

        return LastValue;
    }

    // Each concrete sensor implements its own actual read logic here.
    protected abstract int ReadValue();

    public void setNotification(DateTime time, NotificationLevel notification, string message, int componentId)
    {
        throw new NotImplementedException();
    }
    
    
    
    
    
    
    
    /// <summary>
    /// All sensors, ye i know that this summary is for movement sensor dont look so deep into it
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
            
            return new Random().Next(0, MaxUsage + 20); // can exceed max on purpose to trigger benchmark warning
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