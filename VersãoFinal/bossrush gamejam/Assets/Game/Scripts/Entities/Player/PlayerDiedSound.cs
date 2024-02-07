using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedSound : MonoBehaviour
{
    public Player player;
    public AudioSource audioSource;
    public bool canPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDied == true && canPlay == true)
        {
            StartCoroutine(CanPlay());
        }
    }

    IEnumerator CanPlay()
    {
        canPlay = false;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
    }
}
