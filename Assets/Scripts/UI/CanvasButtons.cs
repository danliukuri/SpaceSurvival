using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameplayMenu;
    [SerializeField] GameObject gameoverMenu;
    #endregion

    #region Methods
    public void Play()
    {
        Camera.main.GetComponent<CameraController>().MoveToDefaultPosition();
        mainMenu.SetActive(false);
    }
    public void Quit()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("https://yuriy-danyliuk.itch.io/");
#endif
    }

    public void StartGameplay()
    {
        Game.Started = true;
        gameplayMenu.SetActive(true);
    }
    public void FinishGameplay()
    {
        Time.timeScale = 0f;
        Game.Started = false;
        gameoverMenu.SetActive(true);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
