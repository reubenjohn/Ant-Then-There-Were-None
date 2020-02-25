using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AvoidObstacles : StateMachineBehaviour
{
    public float strength = 1f;
    public bool showForces = false;

    private ObstacleDetector obstacleDetector;
    private AvoidanceStrategy avoidanceMode;

    public enum AvoidanceStrategy
    {
        CONTINUE_CURRENT_ROTATION, LEAST_ROTATION
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        obstacleDetector = animator.GetComponent<ObstacleDetector>();
        avoidanceMode = obstacleDetector.Sense().Count() == 1 ? AvoidanceStrategy.LEAST_ROTATION : AvoidanceStrategy.CONTINUE_CURRENT_ROTATION;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (avoidanceMode)
        {
            case AvoidanceStrategy.LEAST_ROTATION:
                var detections = obstacleDetector.Sense();
                if (detections.Count() == 1)
                {
                    AvoidWithLeastRotation(animator, detections.First());
                }
                else
                {
                    avoidanceMode = AvoidanceStrategy.CONTINUE_CURRENT_ROTATION;
                    AvoidWithCurrentRotation();
                }
                break;
            case AvoidanceStrategy.CONTINUE_CURRENT_ROTATION:
                AvoidWithCurrentRotation();
                break;
        }
    }
    public void AvoidWithCurrentRotation()
    {
        obstacleDetector.rb.AddTorque(strength * Mathf.Sign(obstacleDetector.rb.angularVelocity));
    }
    public void AvoidWithLeastRotation(Animator animator, ObstacleDetector.ObstacleDetection detection)
    {
        float angleToObstacle = Vector2.SignedAngle(animator.transform.right, detection.relativePosition);
        obstacleDetector.rb.AddTorque(strength * -Mathf.Sign(angleToObstacle));

        float incomingDistance = detection.relativePosition.magnitude;
        float requiredDeceleration = detection.incomingSpeed * Mathf.Max(detection.incomingSpeed, 0) / 2 / incomingDistance;
        Vector2 force = obstacleDetector.rb.mass * requiredDeceleration * -detection.relativePosition.normalized;
        obstacleDetector.rb.AddForce(force);
        if (showForces)
            Debug.DrawLine(obstacleDetector.forcePoint.position, (Vector2)obstacleDetector.forcePoint.position + force, Color.magenta);
    }
}
