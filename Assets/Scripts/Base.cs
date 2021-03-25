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
    [SerializeField] BaseUI baseUI;
    [SerializeField] CanvasButtons canvasButtons;

    StockOfResources stockOfResources;
    #endregion

    #region Methods
    private void Update()
    {
        if(Game.Started)
        {
            Hp -= destructionCurve.Evaluate(Time.time / 1000f) + destructionRate;
            baseUI.UpdateHpSlider();
            baseUI.UpdateHpText();
            if (Hp <= 0f)
                canvasButtons.FinishGameplay();
        }
    }
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
        Hp = maxWeight;
    }
    public StockOfResources GetStockOfResources() => stockOfResources;

    public void AddResource(Resource resource)
    {
        stockOfResources.Add(resource);
        Hp += resource.Weight;
        if (Hp > maxWeight)
        {
            maxWeight = (int)Mathf.Ceil(Hp);
            baseUI.UpdateHpSliderMaxValue();
        } 
        baseUI.UpdateHpSlider();
        baseUI.UpdateHpText();
    }
    #endregion
}