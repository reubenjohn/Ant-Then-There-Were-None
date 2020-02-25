using System.Linq;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField] public CircleOverlapSensor Nose { get; private set; }
    [SerializeField] public CircleOverlapSensor FrontTouch { get; private set; }

    public Collider2D[] LatestOdors { get; private set; }
    public Collider2D[] FrontContacts { get; private set; }
    public ObstacleDetector ObstacleDetector { get; private set; }

    private Animator animator;
    private Rigidbody2D rb;
    private RelativeJoint2D mandible;
    private Transform front;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mandible = GetComponent<RelativeJoint2D>();
        ObstacleDetector = GetComponent<ObstacleDetector>();
        front = transform.Find("Front");

        Transform sensors = transform.Find("Sensors");
        Nose = sensors.Find("Nose").GetComponent<CircleOverlapSensor>();
        FrontTouch = sensors.Find("Front Touch").GetComponent<CircleOverlapSensor>();
    }

    void Update()
    {
        LatestOdors = Nose.Sense();
        FrontContacts = FrontTouch.Sense();
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

    public void Grab(Rigidbody2D targetBody)
    {
        mandible.enabled = true;
        mandible.connectedBody = targetBody;
    }

    public void Release(Rigidbody2D targetBody)
    {
        mandible.enabled = false;
        mandible.connectedBody = null;
    }
}
