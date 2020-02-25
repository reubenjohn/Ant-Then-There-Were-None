using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleDetector : MonoBehaviour
{
    public Rigidbody2D rb;
    public ProximitySensor proximitySensor = null;
    public Transform forcePoint = null;

    public IEnumerable<ObstacleDetection> Sense()
    {
        return proximitySensor.Sense()
               .Select(obstacle =>
               {
                   Vector2 relativePosition = obstacle.ClosestPoint(forcePoint.position) - (Vector2)forcePoint.position;
                   Vector2 relativeVelocity = obstacle.attachedRigidbody.velocity - rb.GetPointVelocity(forcePoint.position);
                   float incomingSpeed = Vector2.Dot(-relativePosition.normalized, relativeVelocity);
                   return new ObstacleDetection(obstacle, relativePosition, incomingSpeed);
               });
    }

    public class ObstacleDetection
    {
        public Collider2D collider;
        public Vector2 relativePosition;
        public float incomingSpeed;

        public ObstacleDetection(Collider2D collider, Vector2 relativePosition, float incomingSpeed)
        {
            this.collider = collider;
            this.relativePosition = relativePosition;
            this.incomingSpeed = incomingSpeed;
        }
    }
}
