using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    Rigidbody rgdbody;
    // Start is called before the first frame update
    void Start()
    {
        rgdbody = GetComponent<Rigidbody>();
        rgdbody.velocity = new Vector3(Random.value, 0f, Random.value) * Random.Range(movementSpeed, movementSpeed + 0.5f);
        rgdbody.angularVelocity = new Vector3(Random.value, Random.value, Random.value) * Random.Range(rotationSpeed, rotationSpeed + 0.5f);
    }

    private void Update()
    {
        if (transform.position.y != 0)
        {
            rgdbody.velocity = new Vector3(rgdbody.velocity.x, 0f, rgdbody.velocity.z);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
           
    }
}
