using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class Pointer : MonoBehaviour
    {
        #region Fields
        [SerializeField] Camera uiCamera;
        [SerializeField] Vector3 targetPosition;
        [Header("Rect transforms")]
        [SerializeField] RectTransform rectTransformPosition;
        [SerializeField] RectTransform rectTransformRotation;
        [Header("Poiter area settings")]
        [SerializeField] float borderSize;
        [SerializeField] Sides4 paddings;
        [Header("Animation names")]
        [SerializeField] string creationAnimationName;
        [SerializeField] string destructionAnimationName;

        PositionAndAngle positionAndAngle;
        Camera mainCamera;
        Sides4 screenBounds;

        Animator animator;
        WaitForSeconds waitForAnimationEnd;
        int creationAnimationNameHash;
        int destructionAnimationNameHash;

        bool isVisible;
        #endregion

        #region Methods
        void Awake()
        {
            mainCamera = Camera.main;
            screenBounds = paddings + borderSize;
            positionAndAngle = new PositionAndAngle(screenBounds);

            animator = GetComponent<Animator>();
            float animationLength = animator.runtimeAnimatorController.animationClips[0].length;
            waitForAnimationEnd = new WaitForSeconds(animationLength);

            creationAnimationNameHash = Animator.StringToHash(creationAnimationName);
            destructionAnimationNameHash = Animator.StringToHash(destructionAnimationName);
        }
        void Update()
        {
            Vector3 targetPositionScreenPoint = GetTargetPositionScreenPoint();
            if (IsTargetVisible(targetPositionScreenPoint))
            {
                if (isVisible && !animator.enabled)
                {
                    PlayAnimation(destructionAnimationNameHash);
                    isVisible = false;
                }
                if (animator.enabled)
                    RotateAndMove(targetPositionScreenPoint, true);
            }
            else if (!isVisible && !animator.enabled)
            {
                PlayAnimation(creationAnimationNameHash);
                isVisible = true;
            }
            if (isVisible)
                RotateAndMove(targetPositionScreenPoint);
        }
        public void PlayAnimation(int animationNameHash)
        {
            animator.enabled = true;
            animator.Play(animationNameHash);
            StartCoroutine(DeactivateAnimator());
        }
        IEnumerator DeactivateAnimator()
        {
            yield return waitForAnimationEnd;
            animator.enabled = false;
        }

        bool IsTargetVisible(in Vector3 screenPosition)
        {
            return screenPosition.x > screenBounds.Left && screenPosition.x < Screen.width - screenBounds.Right &&
                   screenPosition.y > screenBounds.Down && screenPosition.y < Screen.height - screenBounds.Top;
        }
        Vector3 GetTargetPositionScreenPoint() => mainCamera.WorldToScreenPoint(targetPosition);
        void RotateAndMove(Vector3 targetPositionScreenPoint, bool isTargetVisible = false)
        {
            float angle;

            if (isTargetVisible)
                (_, angle) = positionAndAngle.Get(targetPositionScreenPoint);
            else
                (targetPositionScreenPoint, angle) = positionAndAngle.Get(targetPositionScreenPoint);

            rectTransformRotation.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            rectTransformPosition.position = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            rectTransformPosition.localPosition = (Vector2)rectTransformPosition.localPosition;
        }
        #endregion

        #region Classes
        class PositionAndAngle
        {
            Vector3 screenCentre = new Vector2(Screen.width, Screen.height) / 2;
            Sides4 screenBounds = new Sides4();
            public PositionAndAngle(Sides4 screenBounds)
            {
                this.screenBounds.Top = screenCentre.y - screenBounds.Top;
                this.screenBounds.Down = screenCentre.y - screenBounds.Down;
                this.screenBounds.Left = screenCentre.x - screenBounds.Left;
                this.screenBounds.Right = screenCentre.x - screenBounds.Right;
            }
            public (Vector3, float) Get(Vector3 screenPosition)
            {
                // Our screenPosition's origin is screen's bottom-left corner.
                // But we have to get the arrow's screenPosition and rotation with respect to screenCentre.
                screenPosition -= screenCentre;

                // When the targets are behind the camera their projections on the screen (WorldToScreenPoint) are inverted,
                // so just invert them.
                if (screenPosition.z < 0)
                    screenPosition *= -1;

                // Angle between the x-axis (bottom of screen) and a vector starting at zero(bottom-left corner of screen) and terminating at screenPosition.
                float angle = Mathf.Atan2(screenPosition.y, screenPosition.x);
                // Slope of the line starting from zero and terminating at screenPosition.
                float slope = Mathf.Tan(angle);

                // Two point's line's form is (y2 - y1) = m (x2 - x1) + c, 
                // starting point (x1, y1) is screen botton-left (0, 0),
                // ending point (x2, y2) is one of the screenBounds,
                // m is the slope
                // c is y intercept which will be 0, as line is passing through origin.
                // Final equation will be y = mx.
                if (screenPosition.x > 0)
                {
                    // Keep the x screen position to the maximum x bounds and
                    // find the y screen position using y = mx.
                    screenPosition = new Vector3(screenBounds.Right, screenBounds.Right * slope, 0);
                }
                else
                {
                    screenPosition = new Vector3(-screenBounds.Left, -screenBounds.Left * slope, 0);
                }
                // Incase the y ScreenPosition exceeds the y screenBounds 
                if (screenPosition.y > screenBounds.Top)
                {
                    // Keep the y screen position to the maximum y bounds and
                    // find the x screen position using x = y/m.
                    screenPosition = new Vector3(screenBounds.Top / slope, screenBounds.Top, 0);
                }
                else if (screenPosition.y < -screenBounds.Down)
                {
                    screenPosition = new Vector3(-screenBounds.Down / slope, -screenBounds.Down, 0);
                }
                // Bring the ScreenPosition back to its original reference.
                screenPosition += screenCentre;

                return (screenPosition, angle);
            }
        }
        #endregion
    }
}