using Resources;
using UI;
using UnityEngine;

public class Base : MonoBehaviour
{
    #region Properties
    public float Hp { get; protected set; }
    public float PreviousHpValue { get; protected set; }
    #endregion

    #region Fields
    [SerializeField] AnimationCurve destructionCurve;
    [SerializeField] float destructionRate;
    [SerializeField] int maxWeight;
    [SerializeField] BaseUI baseUI;
    [SerializeField] CanvasButtons canvasButtons;
    [SerializeField] GameTimeController gameTimeController;

    StockOfResources stockOfResources;
    #endregion

    #region Methods
    void Update()
    {
        Hp -= destructionCurve.Evaluate(gameTimeController.Timer.ElapsedSeconds / 2000f) + destructionRate;
        if (Hp <= 0f)
            canvasButtons.FinishGameplay();
        else if (PreviousHpValue - Hp >= 1f)
        {
            PreviousHpValue = Mathf.Ceil(Hp);
            baseUI.InstantiateBarrelConsumptionText();
        }
        baseUI.UpdateHpSlider();
        baseUI.UpdateHpText();
    }
    void Awake()
    {
        stockOfResources = new StockOfResources(maxWeight);
        PreviousHpValue = Hp = maxWeight;
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
        PreviousHpValue = Mathf.Ceil(Hp);
    }
    #endregion
}