using System.Linq;
using UnityEngine;

public class QueenAnt : MonoBehaviour, IProximitySensorAdapter, IJointAdapter
{
    public Joint2D Gripper { get; private set; }

    private ProximitySensor[] proximitySensors;

    void Start()
    {
        proximitySensors = transform.Find("Sensors").GetComponentsInChildren<ProximitySensor>();
        Gripper = GetComponent<RelativeJoint2D>();
    }

    public ProximitySensor[] DiscoverSensors()
    {
        return proximitySensors;
    }

    public ProximitySensor GetSensor(string id = null)
    {
        return proximitySensors.FirstOrDefault(sensor => sensor.ID == id);
    }

    public string[] DiscoverJointIds()
    {
        return new string[] { "Gripper" };
    }

    public Joint2D GetJoint2D(string id = null)
    {
        if (id == "Gripper")
            return Gripper;
        else
            return null;
    }
}
