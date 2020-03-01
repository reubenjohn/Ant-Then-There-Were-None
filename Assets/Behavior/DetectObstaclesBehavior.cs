using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DetectObstaclesBehavior : StateMachineBehaviour
{
    public string incomingObstacleAngleParameter;
    protected ObstacleDetector obstacleDetector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        obstacleDetector = animator.GetComponent<ObstacleDetector>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var detection = obstacleDetector.Detect()
            .OrderByDescending(obstaclePositionSpeed => obstaclePositionSpeed.incomingSpeed)
            .FirstOrDefault();
        OnDetection(animator, stateInfo, layerIndex, detection);
    }

    public virtual void OnDetection(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, ObstacleDetector.ObstacleDetection detection)
    {
        animator.SetFloat(incomingObstacleAngleParameter, detection != null ? Vector2.SignedAngle(animator.transform.right, detection.relativePosition) : 180);
    }
}
