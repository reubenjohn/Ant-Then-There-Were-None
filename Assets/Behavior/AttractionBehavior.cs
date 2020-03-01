using System.Collections.Generic;
using UnityEditorExtensions;
using UnityEngine;

public class AttractionBehavior : ProximityBehavior
{
    public float strength = 10f;
    public bool showAttractionForce = false;

    protected IAttractable attractable;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attractable = animator.GetComponent<IAttractable>();
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    override protected void OnSense(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, IEnumerable<Collider2D> colliders)
    {
        colliders.ForEach(collider => 
            attractable.AddAttraction(collider, strength, showAttractionForce));
        base.OnSense(animator, stateInfo, layerIndex, colliders);
    }
}
