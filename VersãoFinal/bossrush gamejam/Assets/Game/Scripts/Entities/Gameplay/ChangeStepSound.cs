using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStepSound : MonoBehaviour
{
    public Player player;
    public AudioClip[] SonsPedras;
    public AudioClip[] sonsGrama;
    public AudioClip[] bobSponja;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.playerSettings.isSpongeBob)
        {
            player.sonsPassos = new AudioClip[1];
            for (int i = 0; i < player.sonsPassos.Length; i++)
            {
                player.sonsPassos[i] = bobSponja[i];
            }

        }
        else if (player.canChangeStepsSound == true)
        {
            for (int i = 0; i < player.sonsPassos.Length; i++)
            {
                player.sonsPassos[i] = SonsPedras[i];
            }

        }
        else
        {
            for (int i = 0; i < player.sonsPassos.Length; i++)
            {
                player.sonsPassos[i] = sonsGrama[i];
            }
        }
    }
}
