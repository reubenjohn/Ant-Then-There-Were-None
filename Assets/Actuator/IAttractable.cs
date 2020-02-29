using UnityEngine;

public interface IAttractable
{
    void AddAttraction(Rigidbody2D attractor, float strength, bool showForce = false);
}