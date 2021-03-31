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
    [SerializeField] GameObject foundBarrelText;

    GameObject gameObj;
    float noFreeSpaceTextAnimationLength;
    float foundBarrelTextAnimationLength;
    #endregion

    #region Methods
    private void Awake()
    {
        noFreeSpaceTextAnimationLength = GetAnimationLength(noFreeSpaceText);
        foundBarrelTextAnimationLength = GetAnimationLength(foundBarrelText);
    }
    public void UpdateResourceUnloadingSlider(float maxValue, float value)
    {
        if(maxValue != resourceUnloadingSlider.maxValue)
            resourceUnloadingSlider.maxValue = maxValue;
        resourceUnloadingSlider.value = value;
    }
    public void NoFreeSpace(Vector3 position)
    {
        InstantiateObjWithAnimation(noFreeSpaceTextAnimationLength, noFreeSpaceText, new Vector3(position.x, position.y + 1f, position.z));
    }
    public void FoundBarrel(Vector3 position)
    {
        InstantiateObjWithAnimation(foundBarrelTextAnimationLength, foundBarrelText, new Vector3(position.x, position.y + 1f, position.z));
    }
    void InstantiateObjWithAnimation(float animationLength, GameObject obj, Vector3? position = null, Quaternion? rotation = null)
    {
        gameObj = Instantiate(obj, position ?? obj.transform.position, rotation ?? obj.transform.rotation);
        Destroy(gameObj, animationLength);
    }
    float GetAnimationLength(GameObject gameObj) => gameObj.GetComponent<Animator>().runtimeAnimatorController.animationClips.Select(c => c.length).Sum();
    #endregion
}
