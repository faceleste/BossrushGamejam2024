using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicOrder : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer playerSr;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerSr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > this.transform.position.y)
        {
            sr.sortingOrder = playerSr.sortingOrder + 1;
        }
        if(player.transform.position.y < this.transform.position.y)
        {
            sr.sortingOrder = playerSr.sortingOrder - 1;
        }
    }
}
