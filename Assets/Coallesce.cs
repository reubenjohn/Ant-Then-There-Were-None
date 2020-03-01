using UnityEngine;

public class Coallesce : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (collision.gameObject.TryGetComponent<Coallesce>(out Coallesce other)
                && (Rigidbody.mass > other.Rigidbody.mass) || (Rigidbody.mass == other.Rigidbody.mass && GetInstanceID() > other.GetInstanceID()))
            {
                float totalMass = Rigidbody.mass + other.Rigidbody.mass;
                Rigidbody.MovePosition((Rigidbody.position * Rigidbody.mass + other.Rigidbody.position * other.Rigidbody.mass) / totalMass);
                Rigidbody.mass = totalMass;
                Destroy(other.gameObject);
            }
        }
    }
}
