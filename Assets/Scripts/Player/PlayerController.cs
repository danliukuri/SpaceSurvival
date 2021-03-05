using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsActive { get; set; }

    [Header("Parameters")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxTilt;

    Camera mainCamera;
    Rigidbody rgdbody;
    Quaternion previosRotation;
    Quaternion targetRotation;
    Vector3 mousePosition;
    Vector3 targetPoint;
    float xRotationController;
    float zRotationController;

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
    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Dot(transform.position - mainCamera.transform.position, mainCamera.transform.forward);
        ChangeRotationTarget();
        targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        if (Input.GetMouseButton(1) && IsActive)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            mousePosition.y = transform.position.y;
            MoveTo(mousePosition);

            xRotationController = (mousePosition - transform.position).magnitude / 10f;
            zRotationController = ((Mathf.Abs(transform.rotation.y - previosRotation.y) * 100f > 1f) ? 1f :
                                    Mathf.Abs(transform.rotation.y - previosRotation.y) * 100f);

            targetRotation = Quaternion.Euler(maxTilt * xRotationController, targetRotation.eulerAngles.y,
                (QuaternionExtensions.IsRightRotated(transform.rotation, previosRotation) ? maxTilt : -maxTilt) * zRotationController);

            previosRotation = transform.rotation;
        }
        RotateTo(targetRotation);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
    public void RotateTo(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ChangeRotationTarget()
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
            targetPoint = ray.GetPoint(hitdist);
    }
}