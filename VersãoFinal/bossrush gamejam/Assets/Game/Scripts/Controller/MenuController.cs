using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject options;
    public AudioClip[] buttonSounds;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {

        PlayButtonSound(); 

        UnityEngine.SceneManagement.SceneManager.LoadScene("TrueLobby");
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }

    public void OpenOptions()
    {
        PlayButtonSound(); 
        options.SetActive(true);

    }


    private void PlayButtonSound()
    {
        audioSource.clip = buttonSounds[Random.Range(0, buttonSounds.Length)];
        audioSource.Play();
    }
}
