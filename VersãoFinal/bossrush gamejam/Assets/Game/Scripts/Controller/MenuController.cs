using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject options ; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("TrueLobby");
    }
    public void QuitGame(){
        Application.Quit();
    }

    public void OpenOptions(){
        options.SetActive(true);
        
    }
}
