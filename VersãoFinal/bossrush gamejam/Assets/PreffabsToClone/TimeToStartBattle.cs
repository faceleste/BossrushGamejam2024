using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToStartBattle : MonoBehaviour
{
    public Cam camera;
    public Player player;
    public PlayerAttack playerAttack;

    public Transform bossTranform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        StartCoroutine(StartB());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartB()
    {
        player.enabled = false;
        playerAttack.enabled = false;
        yield return new WaitForSeconds(3f);
        camera.player = bossTranform;
        yield return new WaitForSeconds(3.5f);
        camera.player = camera.newPlayer;
        yield return new WaitForSeconds(0.6f);
        player.enabled = true;
        playerAttack.enabled = true;
    }
}
