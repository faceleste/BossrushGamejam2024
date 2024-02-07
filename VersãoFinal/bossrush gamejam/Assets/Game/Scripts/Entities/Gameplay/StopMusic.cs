using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject diedCircle;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(diedCircle.activeSelf == true)
        {
            audioSource.Stop();
        }
    }
}
