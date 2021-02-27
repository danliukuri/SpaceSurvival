using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] ParticleSystem rightParticleSystem;
    [SerializeField] ParticleSystem leftParticleSystem;

    Quaternion previosRotation;
    // Start is called before the first frame update
    void Start()
    {
        previosRotation = transform.rotation;
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.rotation.eulerAngles.y - previosRotation.eulerAngles.y) > 10f)
        {
            if (QuaternionExtensions.IsRightRotated(transform.rotation, previosRotation))
                rightParticleSystem.Emit(1);
            else
                leftParticleSystem.Emit(1);

            previosRotation = transform.rotation;
        }
    }
}