using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    Camera mainCamera;
    Rigidbody rgdbody;
    Vector3 mousePosition;
    Vector3 targetPoint;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        rgdbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rgdbody.isKinematic = true;
        rgdbody.isKinematic = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.y = 0f;
            transform.position = Vector3.Lerp(transform.position, mousePosition, movementSpeed * Time.deltaTime);
        }
        ChangeRotationTarget();
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ChangeRotationTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
            targetPoint = ray.GetPoint(hitdist);
    }
}