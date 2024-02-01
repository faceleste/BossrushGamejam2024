using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptBackLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTo(float f)
    {
        StartCoroutine(Back(f));
    }

    IEnumerator Back(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene("TrueLobby");
    }
}
