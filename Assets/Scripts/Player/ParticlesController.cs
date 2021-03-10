using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    #region Fields
    [Header("Particle systems")]
    [SerializeField] ParticleSystem rightParticleSystem;
    [SerializeField] ParticleSystem leftParticleSystem;
    [SerializeField] float angleWhenEmitSideParticles;
    [SerializeField] ParticleSystem backRightParticleSystem;
    [SerializeField] ParticleSystem backLeftParticleSystem;
    [SerializeField] float distanseWhenEmitBackParticles;

    ParticleSystem.EmissionModule emission;
    Quaternion previosRotation;
    Vector3 previosPosition;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        previosRotation = transform.rotation;
        previosPosition = transform.position;
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.rotation.eulerAngles.y - previosRotation.eulerAngles.y) >= angleWhenEmitSideParticles)
        {
            if (QuaternionExtensions.IsRightRotated(transform.rotation, previosRotation))
                rightParticleSystem.Emit(1);
            else
                leftParticleSystem.Emit(1);

            previosRotation = transform.rotation;
        }
        if (Vector3.Distance(transform.position, previosPosition) >= distanseWhenEmitBackParticles)
        {
            backRightParticleSystem.Emit(1);
            backLeftParticleSystem.Emit(1);

            previosPosition = transform.position;
        }
    }
    #endregion
}