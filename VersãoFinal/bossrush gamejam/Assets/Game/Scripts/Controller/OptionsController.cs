using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    private GameController gameController;
    public Canvas optionCanvas;

    [Header("Abas")]

    public GameObject resolutionAba;
    public GameObject volumeAba;
    public GameObject gameplayAba;

    [Header("Audio")]
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this); //This is a singleton
    }
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameObject.Find("GameController").GetComponent<GameController>() != null)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
    }
    public void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void changeQuality(float quality)
    {
        QualitySettings.SetQualityLevel((int)quality);
    }
    public void changeFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }

    public void changeResolution(int resolutionIndex)
    {
        if (resolutionIndex == 1)
        { //1920x1080 

            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            gameController.optionSettings.option.SetActive(false);
        }

        else if (resolutionIndex == 2)
        { //1280x720
            Screen.SetResolution(1280, 720, Screen.fullScreen);
            gameController.optionSettings.option.SetActive(false);
        }
        else if (resolutionIndex == 3)
        { //800x600
            Screen.SetResolution(800, 600, Screen.fullScreen);
            gameController.optionSettings.option.SetActive(false);

        }

    }

    public void changeConfirmation(bool isConfirmationViewed)
    {
        gameController.optionSettings.isConfirmationViewed = isConfirmationViewed;
    }

    public void changeMutedGame(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : 1;
    }

    public void OpenResolutionConfigs()
    {
        CloseAbas();
        resolutionAba.SetActive(true);

    }

    public void OpenVolumeConfigs()
    {
        CloseAbas();
        volumeAba.SetActive(true);
    }

    public void OpenGameplayConfigs()
    {
        CloseAbas();
        gameplayAba.SetActive(true);
    }

    private void CloseAbas()
    {
        audioSource.clip = clickSound;
        audioSource.Play();

        resolutionAba.SetActive(false);
        volumeAba.SetActive(false);
        gameplayAba.SetActive(false);
    }

    public void CloseMenu()
    {

        gameController.optionSettings.option.SetActive(false);
        Time.timeScale = 1;


    }

    public void SetPopUpSkill(bool toggle)
    {
        gameController.optionSettings.canViewConfirmation = toggle;
    }

    public void SetCheatMode(bool toggle)
    {
        gameController.playerSettings.isCheatMode = toggle;
    }

    public void ResetRun()
    {
        gameController.Reset();
        gameController.playerSettings.isFirstTime = false;
        SceneManager.LoadScene("TrueLobby");

    }
}
