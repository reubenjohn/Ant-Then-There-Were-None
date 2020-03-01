using UnityEngine;

public interface IAttractable
{
    void AddAttraction(Collider2D attractor, float strength, bool showForce = false);
}