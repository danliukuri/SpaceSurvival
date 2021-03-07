using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Properties
    public bool IsActive { get; set; }
    #endregion

    #region Fields
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
    float yDefaultPosition;
    #endregion

    #region Methods
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        rgdbody = GetComponent<Rigidbody>();

        yDefaultPosition = transform.position.y;
        IsActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rgdbody.isKinematic = true;
        rgdbody.isKinematic = false;
    }

    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Dot(transform.position - mainCamera.transform.position, mainCamera.transform.forward);
        targetRotation = GetTargetYRotation(mousePosition);

        if (Input.GetMouseButton(1) && IsActive)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            mousePosition.y = yDefaultPosition;
            transform.position = Vector3.Lerp(transform.position, mousePosition, movementSpeed * Time.deltaTime);

            targetRotation = GetTargetXZRotation();
            previosRotation = transform.rotation;
        }
        if (IsActive)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ChangeRotationTarget(Vector3 mousePosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
            targetPoint = ray.GetPoint(hitdist);
    }

    Quaternion GetTargetYRotation(Vector3 mousePosition)
    {
        ChangeRotationTarget(mousePosition);
        return Quaternion.LookRotation(targetPoint - transform.position);
    }
    Quaternion GetTargetXZRotation()
    {
        xRotationController = (mousePosition - transform.position).magnitude / 10f;
        zRotationController = ((Mathf.Abs(transform.rotation.y - previosRotation.y) * 100f > 1f) ? 1f :
                                Mathf.Abs(transform.rotation.y - previosRotation.y) * 100f);

        return Quaternion.Euler(maxTilt * xRotationController, targetRotation.eulerAngles.y,
                (QuaternionExtensions.IsRightRotated(transform.rotation, previosRotation) ? maxTilt : -maxTilt) * zRotationController);
    }
    #endregion
}