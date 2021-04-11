using System;
using TMPro;
using UnityEngine;
using Utilities.Timers;

public class GameTimeController : MonoBehaviour
{
    #region Fields
    [SerializeField] TextMeshProUGUI gameplayElapsedTime;
    [SerializeField] TextMeshProUGUI gameoverElapsedTime;
    [SerializeField] TextMeshProUGUI gameoverNewTheBestSurvivalTime;
    [SerializeField] TextMeshProUGUI theBestSurvivalTime;

    public Timer Timer { get; private set; } = new Timer();
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        gameoverNewTheBestSurvivalTime.transform.parent.gameObject.SetActive(false);
        gameoverElapsedTime.transform.parent.gameObject.SetActive(false);
        if(PlayerPrefs.HasKey("TheBestSurvivalTime"))
        {
            theBestSurvivalTime.text = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("TheBestSurvivalTime")).ToString("mm':'ss");
            theBestSurvivalTime.transform.parent.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Timer.Update();
        if (Timer.Running)
            gameplayElapsedTime.text = TimeSpan.FromSeconds(Timer.ElapsedSeconds).ToString("mm':'ss");
    }
    public void RunTimer()
    {
        gameplayElapsedTime.gameObject.SetActive(true);
        Timer.Run();
    }
    public void StopTimer()
    {
        gameplayElapsedTime.gameObject.SetActive(false);        
        if(IsNewTheBestSurvivalTime())
        {
            SaveTheBestSurvivalTime();
            DisplayElapsedTime(gameoverNewTheBestSurvivalTime);
        }
        else
            DisplayElapsedTime(gameoverElapsedTime);
        Timer.StopAndReset();
    }
    bool IsNewTheBestSurvivalTime() => PlayerPrefs.GetFloat("TheBestSurvivalTime") < (int)Timer.ElapsedSeconds;
    void SaveTheBestSurvivalTime() => PlayerPrefs.SetFloat("TheBestSurvivalTime", Timer.ElapsedSeconds);
    void DisplayElapsedTime(TextMeshProUGUI textMeshPro)
    {
        textMeshPro.text = gameplayElapsedTime.text;
        textMeshPro.transform.parent.gameObject.SetActive(true);
    }
    #endregion
}