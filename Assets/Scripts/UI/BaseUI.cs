using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    #region Fields
    [SerializeField] Slider baseHpSlider;
    [SerializeField] TextMeshProUGUI baseHpText;
    [SerializeField] GameObject playerBase;
    Base baseScript;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        baseScript = playerBase.GetComponent<Base>();
        UpdateHpSliderMaxValue();
    }
    public void UpdateHpSliderMaxValue()
    {
        baseHpSlider.maxValue = baseScript.Hp;
    }
    public void UpdateHpSlider()
    {
        baseHpSlider.value = baseScript.Hp;
    }
    public void UpdateHpText()
    {
        baseHpText.text = Mathf.Ceil(baseScript.Hp) + "/" + Mathf.Ceil(baseHpSlider.maxValue);
    }
    #endregion
}
