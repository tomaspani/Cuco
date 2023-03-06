using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public float timerMax = 60;

    public Text timeText;

    void Start()
    {
        
        timeRemaining = timerMax;
    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                
            }
            else
            {
                
                timeRemaining = 0;
                timerIsRunning = false;
                
                new WaitForSeconds(3f);
                SceneManager.LoadScene("DefeatScreen");
            }
        }


    }

    public void StartTimer ()
    {
        
        timerIsRunning = true;
    }

    public void StopTimer ()
    {
      
        timerIsRunning = false;
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
