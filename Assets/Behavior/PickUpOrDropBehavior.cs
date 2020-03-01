using System.Collections.Generic;
using UnityEditorExtensions;
using UnityEngine;

public class PickUpOrDropBehavior : StateMachineBehaviour
{
    public string jointId;
    public Mode mode = Mode.PICK_UP;
    public string touchProximitySensorId;
    [Layer] public int targetLayer;
    [Tag] public string targetTag;
    public string gripParameterName = null;

    private ProximitySensor proximitySensor;
    private Joint2D gripper;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        proximitySensor = animator.GetComponent<IProximitySensorAdapter>().GetSensor(touchProximitySensorId);
        gripper = animator.GetComponent<IJointAdapter>().GetJoint2D(jointId);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in proximitySensor.Sense())
        {
            if (collider.gameObject.layer == targetLayer && collider.tag == targetTag)
            {
                if (mode == Mode.PICK_UP)
                {
                    gripper.enabled = true;
                    gripper.connectedBody = collider.attachedRigidbody;
                }
                else
                {
                    gripper.enabled = false;
                    gripper.connectedBody = null;
                }
                break;
            }
        }
        if (!string.IsNullOrEmpty(gripParameterName))
            animator.SetBool(gripParameterName, gripper.enabled);
    }

    public enum Mode
    {
        PICK_UP, DROP
    }
}
