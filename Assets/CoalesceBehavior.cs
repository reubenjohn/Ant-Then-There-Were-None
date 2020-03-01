using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalesceBehavior : StateMachineBehaviour
{
    public string jointId;
    public string gripParameterName = null;
    public string massParameterName = "Mass";

    private Rigidbody2D rb;
    private Joint2D gripper;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        gripper = animator.GetComponent<IJointAdapter>().GetJoint2D(jointId);
        
        rb.mass += gripper.connectedBody.mass;
        Destroy(gripper.connectedBody.gameObject);
        gripper.enabled = false;
        if (!string.IsNullOrEmpty(massParameterName))
            animator.SetFloat(massParameterName, rb.mass);
        if (!string.IsNullOrEmpty(gripParameterName))
            animator.SetBool(gripParameterName, gripper.enabled);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
