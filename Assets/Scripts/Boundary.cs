using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player"))
            Destroy(other.gameObject);
    }
}
