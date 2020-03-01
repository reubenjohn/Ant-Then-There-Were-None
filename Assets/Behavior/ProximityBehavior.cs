using System;
using System.Collections.Generic;
using System.Linq;
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
        OnSense(animator, stateInfo, layerIndex,
            proximitySensor.Sense()
                .Where(collider => collider.tag == targetTag && collider.gameObject.layer == targetLayer)
        );
    }

    virtual protected void OnSense(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, IEnumerable<Collider2D> colliders)
    {
        if (!string.IsNullOrEmpty(proximityParameterName))
            animator.SetFloat(
                proximityParameterName,
                colliders
                    .Select(collider => (collider.transform.position - animator.transform.position).magnitude)
                    .DefaultIfEmpty(float.MaxValue)
                    .Min()
            );
    }
}
