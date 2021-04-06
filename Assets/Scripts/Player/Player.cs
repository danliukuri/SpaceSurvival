using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    #region Properties
    public bool IsActive { get; set; }
    public bool AreResourcesBeingUnloaded { get; protected set; } = true;
    public float DefaultYPosition => defaultYPosition; 
    #endregion

    #region Fields
    [SerializeField] KeyCode keyToUnloadResources;
    [SerializeField] Base baseScript;
    [SerializeField] int maxWeight;
    [SerializeField] float defaultYPosition;
    [SerializeField] PlayerUI playerUI;

    bool isPossibleToSpawn = true;
    const float spawnRate = 1f;
    StockOfResources stockOfResources;
    TimerWithDuration timer = new TimerWithDuration();
    #endregion

    #region Methods
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Barrel"))
        {
            if (stockOfResources.HasFreeSpace)
            {
                playerUI.FoundBarrel(collision.transform.position);
                AreResourcesBeingUnloaded = false;
                stockOfResources.Add(new Barrel());
                Destroy(collision.gameObject);
            }
            else if(isPossibleToSpawn)
            {
                isPossibleToSpawn = false;
                StartCoroutine(PossibleToSpawn());
                playerUI.NoFreeSpace(collision.transform.position);
            }    
        }
    }
    IEnumerator PossibleToSpawn()
    { 
        yield return new WaitForSeconds(spawnRate);
        isPossibleToSpawn = true;
    }
    public void UnloadResources()
    {
        if (timer.Finished)
        {
            playerUI.UnloadResource(stockOfResources.Resources.Last());
            baseScript.AddResource(stockOfResources.Resources.Last());
            stockOfResources.Remove(stockOfResources.Resources.Last());
            timer.StopAndReset();
        }
        else if (Input.GetKey(keyToUnloadResources))
        {
            if (timer.Running)
            {
                timer.Update();
                playerUI.UpdateResourceUnloadingSlider(timer.Duration, timer.ElapsedSeconds);
            }
            else if (stockOfResources.Resources.Count == 0)
            { 
                playerUI.IsResourceUnloadingSliderActive = false;
                AreResourcesBeingUnloaded = true;
            }
            else
            {
                timer.Run(stockOfResources.Resources.Last().Weight);
                playerUI.IsResourceUnloadingSliderActive = true;
            }  
        }
        else if (Input.GetKeyUp(keyToUnloadResources))
        {
            timer.StopAndReset();
            playerUI.IsResourceUnloadingSliderActive = false;
            playerUI.UpdateResourceUnloadingSlider(timer.Duration, timer.ElapsedSeconds);
        }    
            
    }
    #endregion
}
