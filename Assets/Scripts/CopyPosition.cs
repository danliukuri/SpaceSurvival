using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 defaultPosition;
    [SerializeField] bool useDefaultX;
    [SerializeField] bool useDefaultY;
    [SerializeField] bool useDefaultZ;

    void FixedUpdate()
    {
        transform.position = new Vector3(useDefaultX ? defaultPosition.x : target.position.x,
                                         useDefaultY ? defaultPosition.y : target.position.y,
                                         useDefaultZ ? defaultPosition.z : target.position.z);
    }
}
