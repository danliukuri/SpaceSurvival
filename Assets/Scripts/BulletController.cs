using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rgdbody;
    // Start is called before the first frame update
    void Start()
    {
        rgdbody = GetComponent<Rigidbody>();
        rgdbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
