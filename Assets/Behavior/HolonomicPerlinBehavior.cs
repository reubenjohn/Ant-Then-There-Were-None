using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolonomicPerlinBehavior : StateMachineBehaviour
{
    public float forwardScale = .2f;
    public float backwardScale = .1f;
    public float angularScale = .1f;

    private Rigidbody2D rb;

    private float linearSeed;
    private float angularSeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        linearSeed = Random.Range(-1e6f, 0);
        angularSeed = Random.Range(2e6f, 3e6f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 force = (Vector2)animator.transform.right * ((forwardScale + backwardScale) * Mathf.PerlinNoise(Time.time + linearSeed, Time.time + linearSeed) - backwardScale);
        rb.velocity += force;
        Debug.DrawLine(rb.position, rb.position + force, Color.red);
        rb.AddTorque(angularScale * (2 * Mathf.PerlinNoise(Time.time + angularSeed, Time.time + angularSeed) - 1));
    }
}
