using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties
    public bool IsActive { get; set; } = true;
    #endregion

    #region Fields
    [SerializeField] int maxWeight;
    StockOfResources StockOfResources;
    #endregion

    #region Methods
    void Start()
    {
        StockOfResources = new StockOfResources(maxWeight);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Barrel"))
        {
            if (StockOfResources.HasFreeSpace)   
            {
                StockOfResources.Add(new Barrel());
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("No free space in a ship");
            }
        }
    }

    #endregion
}
