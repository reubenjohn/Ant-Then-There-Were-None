using UnityEngine;

public class MassScaler : MonoBehaviour
{
    public float density = 100f;

    public Rigidbody2D Rigidbody { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale = transform.localScale = Vector3.one * Mathf.Sqrt(Rigidbody.mass / density);
    }
}
