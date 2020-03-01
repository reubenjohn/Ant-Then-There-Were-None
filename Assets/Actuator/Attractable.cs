
using UnityEngine;

public class Attractable : MonoBehaviour, IAttractable
{
    public Rigidbody2D attractableRigidBody;
    public Transform attractionTransform;

    public void AddAttraction(Collider2D attractorCollider, float strength, bool showForce = false)
    {
        float magnitude = attractorCollider.attachedRigidbody.mass;
        Vector2 force = strength * magnitude * ((Vector2)attractorCollider.transform.position - (Vector2)attractionTransform.position).normalized;
        attractableRigidBody.AddForceAtPosition(force, attractionTransform.position);
        if (showForce)
            Debug.DrawLine(attractionTransform.position, (Vector2)attractionTransform.position + force, Color.blue);
    }
}