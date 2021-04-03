using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Sides4
{
    #region Properties
    public float Top { get => top; set => top = value; }
    public float Down { get => down; set => down = value; }
    public float Left { get => left; set => left = value; }
    public float Right { get => right; set => right = value; }
    #endregion

    #region Fields
    [SerializeField] float top;
    [SerializeField] float down;
    [SerializeField] float left;
    [SerializeField] float right;
    #endregion

    #region Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Sides4 operator +(Sides4 sides4, float value)
    {
        return new Sides4()
        {
            Top = sides4.Top + value,
            Down = sides4.Down + value,
            Left = sides4.Left + value,
            Right = sides4.Right + value
        };
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Sides4 operator +(float value, Sides4 sides4)
    {
        return new Sides4()
        {
            Top = sides4.Top + value,
            Down = sides4.Down + value,
            Left = sides4.Left + value,
            Right = sides4.Right + value
        };
    }
    #endregion
}