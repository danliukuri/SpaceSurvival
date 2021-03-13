using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    #region Properties
    public bool IsActive { get; set; } = true;
    public bool AreResourcesBeingUnloaded { get; protected set; }
    #endregion

    #region Fields
    [SerializeField] KeyCode keyToUnloadResources;
    [SerializeField] GameObject playerBase;
    [SerializeField] int maxWeight;

    StockOfResources baseStockOfResources;
    StockOfResources stockOfResources;

    
    Timer timer = new Timer();
    #endregion

    #region Methods
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
    }
    void Start()
    {
        baseStockOfResources = playerBase.GetComponent<Base>().GetStockOfResources();
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
            Debug.Log("isUnloaded");
            baseStockOfResources.Add(stockOfResources.Resources.Last());
            stockOfResources.Remove(stockOfResources.Resources.Last());
            timer.Reset();
        }
        else if (Input.GetKey(keyToUnloadResources))
        {
            if (timer.Running)
                timer.Update();
            else if (stockOfResources.Resources.Count == 0)
                AreResourcesBeingUnloaded = true;
            else
                timer.Run(stockOfResources.Resources.Last().Weight);
        }
        else if (Input.GetKeyUp(keyToUnloadResources))
            timer.Reset();
    }
    #endregion
}
