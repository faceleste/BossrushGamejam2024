using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        float seconds = 59 - gameController.timeSettings.currentTime % 60;
        float minutes = totalMinutes - gameController.timeSettings.currentTime / 60;
        float miliseconds = (gameController.timeSettings.currentTime * 100) % 100;

        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
    }
}
