using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    #region Fields
    [SerializeField] KeyCode activePointerKey;
    [SerializeField] Pointer pointer;
    #endregion

    #region Methods
    void Update()
    {
        if (Game.Started && Input.GetKeyDown(activePointerKey))
            pointer.Active = !pointer.Active;
    }
    #endregion
}
