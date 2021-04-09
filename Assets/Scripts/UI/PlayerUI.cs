using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Resources;
using Resources.Barrel;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        #region Properties
        public bool IsResourceUnloadingSliderActive
        {
            get => resourceUnloadingSlider.IsActive();
            set => resourceUnloadingSlider.gameObject.SetActive(value);
        }
        #endregion

        #region Fields
        [SerializeField] Slider resourceUnloadingSlider;
        [SerializeField] GameObject noFreeSpaceText;
        [SerializeField] GameObject foundBarrelText;
        [SerializeField] GameObject foundBarrelTextUI;
        [SerializeField] Transform canvas;

        GameObject gameObj;
        float noFreeSpaceTextAnimationLength;
        float foundBarrelTextAnimationLength;
        float foundBarrelTextUIAnimationLength;
        #endregion

        #region Methods
        private void Awake()
        {
            noFreeSpaceTextAnimationLength = GetAnimationLength(noFreeSpaceText);
            foundBarrelTextAnimationLength = GetAnimationLength(foundBarrelText);
            foundBarrelTextUIAnimationLength = GetAnimationLength(foundBarrelTextUI);
        }
        public void UpdateResourceUnloadingSlider(float maxValue, float value)
        {
            if (maxValue != resourceUnloadingSlider.maxValue)
                resourceUnloadingSlider.maxValue = maxValue;
            resourceUnloadingSlider.value = value;
        }
        public void NoFreeSpace(Vector3 position)
        {
            InstantiateObjWithAnimation(noFreeSpaceTextAnimationLength, noFreeSpaceText, new Vector3(position.x, position.y + 1f, position.z));
        }
        public void UnloadResource(in Resource resource)
        {
            if (resource is Barrel)
            {
                gameObj = Instantiate(foundBarrelTextUI, canvas);
                Destroy(gameObj, foundBarrelTextUIAnimationLength);
            }
        }
        public void FoundBarrel(Vector3 position)
        {
            InstantiateObjWithAnimation(foundBarrelTextAnimationLength, foundBarrelText, new Vector3(position.x, position.y + 1f, position.z));
        }
        void InstantiateObjWithAnimation(float animationLength, in GameObject obj, Vector3? position = null, Quaternion? rotation = null)
        {
            gameObj = Instantiate(obj, position ?? obj.transform.position, rotation ?? obj.transform.rotation);
            Destroy(gameObj, animationLength);
        }
        float GetAnimationLength(in GameObject gameObj) => gameObj.GetComponent<Animator>().runtimeAnimatorController.animationClips.Select(c => c.length).Sum();
        #endregion
    }
}