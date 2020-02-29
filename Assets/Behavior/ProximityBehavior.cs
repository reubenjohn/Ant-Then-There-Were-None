using UnityEditorExtensions;
using UnityEngine;

public class ProximityBehavior : StateMachineBehaviour
{
    public string proximitySensorId;
    [Tag] public string targetTag;
    [Layer] public int targetLayer;
    public string proximityParameterName = null;

    protected ProximitySensor proximitySensor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        proximitySensor = animator.GetComponent<IProximitySensorAdapter>().GetSensor(proximitySensorId);
        OnStateUpdate(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in proximitySensor.Sense())
        {
            if (collider.tag == targetTag && collider.gameObject.layer == targetLayer)
            {
                OnTargetFound(animator, stateInfo, layerIndex, collider);
                return;
            }
        }
        if (!string.IsNullOrEmpty(proximityParameterName))
            animator.SetFloat(proximityParameterName, float.MaxValue);
    }

    protected virtual void OnTargetFound(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Collider2D collider)
    {
        if (!string.IsNullOrEmpty(proximityParameterName))
            animator.SetFloat(proximityParameterName, (collider.transform.position - animator.transform.position).magnitude);
    }
}
