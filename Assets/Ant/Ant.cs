using System.Linq;
using UnityEngine;
using System;

public class Ant : MonoBehaviour, IProximitySensorAdapter, IJointAdapter
{
    public Joint2D Gripper { get; private set; }

    private ProximitySensor[] proximitySensors;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Gripper = GetComponent<RelativeJoint2D>();

        Transform sensorsTransform = transform.Find("Sensors");
        proximitySensors = sensorsTransform.GetComponentsInChildren<ProximitySensor>();
    }

    void Update()
    {
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
