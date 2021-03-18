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
    [SerializeField] GameObject playerBase;
    [SerializeField] int maxWeight;
    [SerializeField] float defaultYPosition;
    [SerializeField] GameObject UIScripts;

    PlayerUI playerUI;
    Base baseScript;
    StockOfResources stockOfResources;

    Timer timer = new Timer();
    #endregion

    #region Methods
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
        baseScript = playerBase.GetComponent<Base>();
        playerUI = UIScripts.GetComponent<PlayerUI>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Barrel"))
        {
            if (stockOfResources.HasFreeSpace)   
            {
                AreResourcesBeingUnloaded = false;
                stockOfResources.Add(new Barrel());
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("No free space in a ship");
            }
        }
    }
    public void UnloadResources()
    {
        if (timer.Finished)
        {
            baseScript.AddResource(stockOfResources.Resources.Last());
            stockOfResources.Remove(stockOfResources.Resources.Last());
            timer.Reset();
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
            timer.Reset();
            playerUI.IsResourceUnloadingSliderActive = false;
            playerUI.UpdateResourceUnloadingSlider(timer.Duration, timer.ElapsedSeconds);
        }    
            
    }
    #endregion
}
