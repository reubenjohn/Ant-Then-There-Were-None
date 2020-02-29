using System.Linq;

public interface IProximitySensorAdapter
{
    ProximitySensor[] DiscoverSensors();
    ProximitySensor GetSensor(string id = null);
}