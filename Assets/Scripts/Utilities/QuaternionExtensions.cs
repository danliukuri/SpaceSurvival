using UnityEngine;

namespace Utilities
{
    public static class QuaternionExtensions
    {
        public static bool IsRightRotated(Quaternion from, Quaternion to)
        {
            float fromY = from.eulerAngles.y;
            float toY = to.eulerAngles.y;
            float clockWise = 0f;
            float counterClockWise = 0f;

            if (fromY <= toY)
            {
                clockWise = toY - fromY;
                counterClockWise = fromY + (360 - toY);
            }
            else
            {
                clockWise = (360 - fromY) + toY;
                counterClockWise = fromY - toY;
            }
            return (clockWise <= counterClockWise);
        }
    }
}