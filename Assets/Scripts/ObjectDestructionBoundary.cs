using UnityEngine;

public class ObjectDestructionBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            Destroy(other.gameObject);
    }
}
