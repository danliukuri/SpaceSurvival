using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    #region Fields
    [SerializeField] Slider baseHpSlider;
    [SerializeField] TextMeshProUGUI baseHpText;
    [SerializeField] GameObject barrelConsumptionText;
    [SerializeField] Base baseScript;
    GameObject gameObj;
    float consumptionOfTheBarrelTextAnimationLength;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        UpdateHpSliderMaxValue();
        consumptionOfTheBarrelTextAnimationLength = barrelConsumptionText.GetComponent<Animator>().runtimeAnimatorController.animationClips.Select(c => c.length).Sum();
    }
    public void UpdateHpSliderMaxValue()
    {
        baseHpSlider.maxValue = baseScript.Hp;
    }
    public void UpdateHpSlider()
    {
        baseHpSlider.value = baseScript.Hp;
    }
    public void InstantiateBarrelConsumptionText()
    {
        gameObj = Instantiate(barrelConsumptionText, transform);
        Destroy(gameObj, consumptionOfTheBarrelTextAnimationLength);
    }
    public void UpdateHpText()
    {
        baseHpText.text = Mathf.Ceil(baseScript.Hp) + "/" + Mathf.Ceil(baseHpSlider.maxValue);
    }
    #endregion
}
