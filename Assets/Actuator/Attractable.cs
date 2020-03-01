
using UnityEngine;

public class Attractable : MonoBehaviour, IAttractable
{
    public Rigidbody2D attractableRigidBody;
    public Transform attractionTransform;

    public void AddAttraction(Rigidbody2D attractor, float strength, bool showForce = false)
    {
        float magnitude = attractor.mass;
        Vector2 force = strength * magnitude * (attractor.position - (Vector2)attractionTransform.position).normalized;
        attractableRigidBody.AddForceAtPosition(force, attractionTransform.position);
        if (showForce)
            Debug.DrawLine(attractionTransform.position, (Vector2)attractionTransform.position + force, Color.blue);
    }
}