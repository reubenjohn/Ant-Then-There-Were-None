using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProximitySensor : MonoBehaviour
{
    public abstract Collider2D[] Sense();
}

public class CircleOverlapSensor : ProximitySensor
{
    [SerializeField] private float range = 1;
    [SerializeField] private LayerMask layerMask = 0;

    public override Collider2D[] Sense()
    {
        return Physics2D.OverlapCircleAll(transform.position, range, layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, range);
    }
}
