using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectable
{
    void OnGripUpdate(Rigidbody2D attachedRigidbody);
}
