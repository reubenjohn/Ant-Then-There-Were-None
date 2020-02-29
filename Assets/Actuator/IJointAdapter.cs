using UnityEngine;

public interface IJointAdapter
{
    string[] DiscoverJointIds();
    Joint2D GetJoint2D(string id = null);
}