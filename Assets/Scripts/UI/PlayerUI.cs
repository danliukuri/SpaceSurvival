using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Properties
    public bool IsResourceUnloadingSliderActive { get => resourceUnloadingSlider.IsActive();
        set => resourceUnloadingSlider.gameObject.SetActive(value); }
    #endregion

    #region Fields
    [SerializeField] Slider resourceUnloadingSlider;
    #endregion

    #region Methods
    public void UpdateResourceUnloadingSlider(float maxValue, float value)
    {
        if(maxValue != resourceUnloadingSlider.maxValue)
            resourceUnloadingSlider.maxValue = maxValue;
        resourceUnloadingSlider.value = value;
    }
    #endregion
}
