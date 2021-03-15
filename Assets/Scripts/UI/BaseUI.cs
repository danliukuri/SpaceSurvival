using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    #region Fields
    [SerializeField] Slider baseHpSlider;
    [SerializeField] GameObject playerBase;
    Base baseScript;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        baseScript = playerBase.GetComponent<Base>();
        baseHpSlider.maxValue = baseScript.GetStockOfResources().MaxWeight;
        UpdateHpSlider();
    }

    public void UpdateHpSlider()
    {
        baseHpSlider.value = baseScript.Hp;
    }
    #endregion
}
