using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ProximitySensor : MonoBehaviour
{
    public abstract string ID { get; }
    public abstract IEnumerable<Collider2D> Sense();
}

public class CircleOverlapSensor : ProximitySensor
{
    public string id;
    public float range = 1;
    public LayerMask layerMask = 0;
    public float angle = 180;

    public override string ID => id;

    public override IEnumerable<Collider2D> Sense()
    {
        return Physics2D.OverlapCircleAll(transform.position, range, layerMask)
            .Where(collider =>
            {
                float vectorAngle = Vector2.Angle(transform.right, collider.transform.position - transform.position);
                return vectorAngle < angle / 2;
            });
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireArc(transform.position, transform.forward, transform.right, -angle / 2, range);
        UnityEditor.Handles.DrawWireArc(transform.position, transform.forward, transform.right, angle / 2, range);
    }
}
