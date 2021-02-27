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
            if (IsRightRotated(transform.rotation, previosRotation))
                rightParticleSystem.Emit(1);
            else
                leftParticleSystem.Emit(1);

            previosRotation = transform.rotation;
        }
    }
    
    bool IsRightRotated(Quaternion from, Quaternion to)
    {
        float fromY = from.eulerAngles.y;
        float toY = to.eulerAngles.y;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromY <= toY)
        {
            clockWise = toY - fromY;
            counterClockWise = fromY + (360 - toY);
        }
        else
        {
            clockWise = (360 - fromY) + toY;
            counterClockWise = fromY - toY;
        }
        return (clockWise <= counterClockWise);
    }
}
