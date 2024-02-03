using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStepSound : MonoBehaviour
{
    public Player player;
    public AudioClip[] SonsPedras;
    public AudioClip[] sonsGrama;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.canChangeStepsSound == true)
        {
            for(int i = 0; i< player.sonsPassos.Length; i++)
            {
                player.sonsPassos[i] = SonsPedras[i];
            }
            
        }
        else
        {
            for(int i = 0; i< player.sonsPassos.Length; i++)
            {
                player.sonsPassos[i] = sonsGrama[i];
            } 
        }
    }
}
