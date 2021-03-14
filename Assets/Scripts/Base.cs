using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    #region Properties
    public float Hp { get; protected set; }
    #endregion

    #region Fields
    [SerializeField] AnimationCurve destructionCurve;
    [SerializeField] float destructionRate;
    [SerializeField] int maxWeight;
    [SerializeField] GameObject UIScripts;

    StockOfResources stockOfResources;
    BaseUI baseUI;
    #endregion

    #region Methods
    private void Update()
    {
        Hp -= destructionCurve.Evaluate(Time.time / 1000f) + destructionRate;
        baseUI.UpdateHpSlider();
    }
    void Awake()
    {
        baseUI = UIScripts.GetComponent<BaseUI>();
        stockOfResources = new StockOfResources(maxWeight);
        Hp = maxWeight;
    }
    public StockOfResources GetStockOfResources() => stockOfResources;

    public void AddResource(Resource resource)
    {
        stockOfResources.Add(resource);
        Hp += resource.Weight;
        baseUI.UpdateHpSlider();
    }
    #endregion
}