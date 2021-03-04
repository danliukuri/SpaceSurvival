using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Rigidbody rgdbody;

    /// <summary> Random value from [-1.1; -0.1] or [0.1; 1.1] </summary>
    float MyRandomValue() => (Random.value + 0.1f) * Random.Range(-1, 2);

    // Start is called before the first frame update
    void Start()
    {
        rgdbody = GetComponent<Rigidbody>();
        rgdbody.velocity = new Vector3(MyRandomValue(), 0f, MyRandomValue()) * movementSpeed;
        rgdbody.angularVelocity = new Vector3(MyRandomValue(), MyRandomValue(), MyRandomValue()) * rotationSpeed;
    }

    private void Update()
    {
        if (transform.position.y != 0)
        {
            rgdbody.velocity = new Vector3(rgdbody.velocity.x, 0f, rgdbody.velocity.z);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}