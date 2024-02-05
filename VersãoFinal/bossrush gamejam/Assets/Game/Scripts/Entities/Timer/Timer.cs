using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour
{
    private TMP_Text timeText;

    private GameController gameController;
    private float totalMinutes;

    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        timeText = GetComponent<TMP_Text>();

    }

    void Update()
    {

        totalMinutes = gameController.timeSettings.fullTime;
        float seconds = 60 - (gameController.timeSettings.currentTime % 60);
        float minutes = gameController.timeSettings.fullTime - (gameController.timeSettings.currentTime / 60);


        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if (minutes < 5)
        {
            timeText.color = Color.red;
        }

        if (minutes < 10 && minutes > 5)
        {
            timeText.color = Color.yellow;
        }

        if (minutes > 10)
        {
            timeText.color = Color.green;
        }

        if (gameController.timeSettings.currentTime/ 60  >= totalMinutes)
        {
            gameController.timeSettings.canCountTime = false;

            SceneManager.LoadScene("GameOverTemplate");
        }

    }
}
