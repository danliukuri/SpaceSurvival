using UnityEngine;
using System.Collections.Generic;

public class GameHandler : Tayx.Graphy.Utils.G_Singleton<GameHandler>
{
    #region Properties
    public static bool GamePlayStarted { get; set; }
    #endregion

    #region Fields
    [Tooltip("Scripts that need to be activated at the beginning of the game")]
    [SerializeField] List<MonoBehaviour> scirpsToActive;

    GameTimeController gameTimeController;
    #endregion

    #region Methods
    void Awake()
    {
        gameTimeController = GetComponent<GameTimeController>();
        scirpsToActive.ForEach(s => s.enabled = false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartGameplay()
    {
        GamePlayStarted = true;
        gameTimeController.RunTimer();
        scirpsToActive.ForEach(s => s.enabled = true);
    }
    public void FinishGameplay()
    {
        Time.timeScale = 0f;
        GamePlayStarted = false;
        scirpsToActive.ForEach(s => s.enabled = false);
        gameTimeController.StopTimer();
    }
    #endregion
}