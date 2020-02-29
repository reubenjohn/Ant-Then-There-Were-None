using System.Linq;
using UnityEngine;
using System;

public class Ant : MonoBehaviour, IProximitySensorAdapter, IAttractable, IJointAdapter
{
    private ProximitySensor[] proximitySensors;

    public ObstacleDetector ObstacleDetector { get; private set; }
    public Joint2D Gripper { get; private set; }

    private Animator animator;
    private Rigidbody2D rb;
    private Transform front;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Gripper = GetComponent<RelativeJoint2D>();
        ObstacleDetector = GetComponent<ObstacleDetector>();
        front = transform.Find("Front");

        Transform sensorsTransform = transform.Find("Sensors");
        proximitySensors = sensorsTransform.GetComponentsInChildren<ProximitySensor>();
    }

    void Update()
    {
        // TODO Move to avoid obtacles behavior
        var detection = ObstacleDetector.Sense()
            .OrderByDescending(obstaclePositionSpeed => obstaclePositionSpeed.incomingSpeed)
            .FirstOrDefault();
        animator.SetFloat("Incoming Obstacle Angle", detection != null ? Vector2.Angle(transform.right, detection.relativePosition) : 180);
    }

    public void AddAttraction(Rigidbody2D attractor, float strength, bool showForce = false)
    {
        float magnitude = attractor.mass;
        Vector2 force = strength * magnitude * (attractor.position - (Vector2)front.position).normalized;
        rb.AddForceAtPosition(force, front.position);
        if (showForce)
            Debug.DrawLine(front.position, (Vector2)front.position + force, Color.blue);
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
