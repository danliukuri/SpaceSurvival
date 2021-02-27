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
    Vector3 spawnerPosition;
    float nextFire = 0.5f;
    float myTime = 0.0f;

    private void Start()
    {
        spawnerPosition = new Vector3();
    }
    void Update()
    {
        myTime += Time.deltaTime;

        if (Input.GetMouseButton(0) && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            
            shotSpawnes.ForEach(spw =>
            {
                spawnerPosition.Set(spw.position.x, 0f, spw.position.z);
                newProjectile = Instantiate(projectile, spawnerPosition, Quaternion.Euler(0f, player.rotation.eulerAngles.y, 0f));
            });
            
            nextFire -= myTime;
            myTime = 0.0f;
        }
    }
}
