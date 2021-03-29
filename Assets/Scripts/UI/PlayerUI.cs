using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerUI : MonoBehaviour
{
    #region Properties
    public bool IsResourceUnloadingSliderActive { get => resourceUnloadingSlider.IsActive();
        set => resourceUnloadingSlider.gameObject.SetActive(value); }
    #endregion

    #region Fields
    [SerializeField] Slider resourceUnloadingSlider;
    [SerializeField] GameObject noFreeSpaceText;

    GameObject gameObj;
    float animationLength;
    #endregion

    #region Methods
    private void Awake()
    {
        animationLength = noFreeSpaceText.GetComponent<Animator>().runtimeAnimatorController.animationClips.Select(c => c.length).Sum();
    }
    public void UpdateResourceUnloadingSlider(float maxValue, float value)
    {
        if(maxValue != resourceUnloadingSlider.maxValue)
            resourceUnloadingSlider.maxValue = maxValue;
        resourceUnloadingSlider.value = value;
    }
    public void NoFreeSpace(Vector3 position)
    {
        gameObj = Instantiate(noFreeSpaceText, new Vector3(position.x, position.y + 1f, position.z), noFreeSpaceText.transform.rotation);
        Destroy(gameObj, animationLength);
    }
    #endregion
}
