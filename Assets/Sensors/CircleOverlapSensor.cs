using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProximitySensor : MonoBehaviour
{
    public abstract string ID { get; }
    public abstract Collider2D[] Sense();
}

public class CircleOverlapSensor : ProximitySensor
{
    public string id;
    public float range = 1;
    public LayerMask layerMask = 0;

    public override string ID => id;

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
