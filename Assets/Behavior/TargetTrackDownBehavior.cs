using UnityEditorExtensions;
using UnityEngine;

public class TargetTrackDownBehavior : StateMachineBehaviour
{
    [Tag] public string targetTag;
    [Layer] public int targetLayer;
    [Tag] public string targetMarkerTag;
    [Layer] public int targetMarkerLayer;
    public float strength = 10f;
    public string targetFoundParameterName = null;
    public bool showAttractionForce = false;
    private Ant ant;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ant = animator.GetComponent<Ant>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in ant.LatestOdors)
        {
            if (targetFoundParameterName != null && targetTag == collider.tag && targetLayer == collider.gameObject.layer)
                animator.SetBool(targetFoundParameterName, true);
            else if (targetMarkerTag == collider.tag && targetMarkerLayer == collider.gameObject.layer)
                ant.AddAttraction(collider.attachedRigidbody, strength, showAttractionForce);
        }
    }
}
