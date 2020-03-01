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

    override protected bool OnTargetFound(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Collider2D collider)
    {
        base.OnTargetFound(animator, stateInfo, layerIndex, collider);
        attractable.AddAttraction(collider.attachedRigidbody, strength, showAttractionForce);
        return false;
    }
}
