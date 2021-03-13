using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    #region Fields
    [SerializeField] int maxWeight;
    StockOfResources stockOfResources;
    #endregion

    #region Methods
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
    }
    public StockOfResources GetStockOfResources() => stockOfResources;
    #endregion
}