using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotsController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform player;
    [SerializeField] List<Transform> shotSpawnes;
    [SerializeField] float fireDelta = 0.5F;

    GameObject newProjectile;
    float nextFire = 0.5F;
    float myTime = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            shotSpawnes.ForEach(spw => newProjectile = Instantiate(projectile, spw.position, player.rotation));
            
            nextFire -= myTime;
            myTime = 0.0f;
        }
    }
}
