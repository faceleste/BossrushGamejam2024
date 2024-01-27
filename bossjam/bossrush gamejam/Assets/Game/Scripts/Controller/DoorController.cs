using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public BoxCollider2D boss1;
    public BoxCollider2D boss2;
    public BoxCollider2D boss3;
    public BoxCollider2D boss4;
    public Player player;


    void Start()
    {

    }

    void Update()
    {
        RedirectToBoss(boss1, "Boss01");
        RedirectToBoss(boss2, "Boss02");
        RedirectToBoss(boss3, "Boss03");
        RedirectToBoss(boss4, "Boss04");

    }

    void RedirectToBoss(BoxCollider2D boss, string scene)
    {
        if (PlayerColisaoSpriteBoss())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

        }
    }

    public bool PlayerColisaoSpriteBoss()
    {

        if (boss1.IsTouching(player.GetComponent<BoxCollider2D>()) || boss2.IsTouching(player.GetComponent<BoxCollider2D>()) || boss3.IsTouching(player.GetComponent<BoxCollider2D>()) || boss4.IsTouching(player.GetComponent<BoxCollider2D>()))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
