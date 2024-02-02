using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }



    public void Restart()
    {
        gameController.Reset();
        SceneManager.LoadScene("TrueLobby");
    }

    public void Menu()
    {
        gameController.Reset();
        SceneManager.LoadScene("Menu");
    }
}
