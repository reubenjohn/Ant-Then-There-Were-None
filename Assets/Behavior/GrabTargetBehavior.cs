using UnityEditorExtensions;
using UnityEngine;

public class GrabTargetBehavior : StateMachineBehaviour
{
    [Layer] public int targetLayer;
    [Tag] public string targetTag;
    public float attractionStrength = 100f;
    public string targetGrabbedParameterName = null;
    public bool showAttractionForce = false;

    private Ant ant;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ant = animator.GetComponent<Ant>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in ant.FrontContacts)
        {
            if (targetLayer == collider.gameObject.layer && targetTag == collider.tag)
            {
                ant.Grab(collider.GetComponent<Rigidbody2D>());
                if (targetGrabbedParameterName != null)
                    animator.SetBool(targetGrabbedParameterName, true);
                return;
            }
        }
        foreach (var collider in ant.LatestOdors)
        {
            if (targetLayer == collider.gameObject.layer && targetTag == collider.tag)
                ant.AddAttraction(collider.GetComponent<Rigidbody2D>(), attractionStrength, showAttractionForce);
        }
    }
}
