using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    #region Fields
    [SerializeField] TextMeshProUGUI gameplayEleapsedTimeTextObj;
    [SerializeField] TextMeshProUGUI gameoverEleapsedTimeTextObj;

    Timer timer = new Timer();
    #endregion

    #region Methods
    public void RunTimer()
    {
        gameplayEleapsedTimeTextObj.gameObject.SetActive(true);
        timer.Run();
    }
    public void StopTimer()
    {
        gameplayEleapsedTimeTextObj.gameObject.SetActive(false);
        timer.Stop();
        gameoverEleapsedTimeTextObj.text = "You survived " + gameplayEleapsedTimeTextObj.text + ", a good result !";
    }
    // Update is called once per frame
    void Update()
    {
        timer.Update();
        if(timer.Running)
            gameplayEleapsedTimeTextObj.text = System.TimeSpan.FromSeconds(timer.ElapsedSeconds).ToString("mm':'ss");
    }
    #endregion
}