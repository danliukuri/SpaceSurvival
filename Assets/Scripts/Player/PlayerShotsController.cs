using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Timers;

namespace Player
{
    public class PlayerShotsController : MonoBehaviour
    {
        #region Fields
        [Header("Parameters")]
        [SerializeField] GameObject projectile;
        [SerializeField] Transform player;
        [SerializeField] List<Transform> shotSpawnes;
        [SerializeField] float fireDelta = 0.5f;

        GameObject newProjectile;
        Player playerScript;
        TimerWithDuration timer = new TimerWithDuration();
        Vector3 spawnerPosition = new Vector3();
        #endregion

        #region Methods
        void Start()
        {
            playerScript = player.GetComponent<Player>();
            timer.Run(fireDelta);
        }
        void Update()
        {
            timer.Update();

            if (Input.GetMouseButton(0) && timer.Finished && playerScript.IsActive)
            {
                shotSpawnes.ForEach(spw =>
                {
                    spawnerPosition.Set(spw.position.x, 0f, spw.position.z);
                    newProjectile = Instantiate(projectile, spawnerPosition, Quaternion.Euler(0f, player.rotation.eulerAngles.y, 0f));
                });
                timer.StopAndReset();
                timer.Run(fireDelta);
            }
        }
        #endregion
    }
}