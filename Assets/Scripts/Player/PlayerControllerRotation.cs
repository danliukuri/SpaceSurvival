using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRotation : MonoBehaviour
{
    [SerializeField] float speed;
    Camera mainCamera;
    Vector3 targetPoint;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeRotationTarget();
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
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
