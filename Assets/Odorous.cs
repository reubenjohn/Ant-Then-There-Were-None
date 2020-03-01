using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odorous : MonoBehaviour, IConnectable
{
    private int layerBackup;

    public void OnGripUpdate(Rigidbody2D attachedRigidbody)
    {
        if (attachedRigidbody != null)
        {
            layerBackup = gameObject.layer;
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = layerBackup;
        }
    }
}
