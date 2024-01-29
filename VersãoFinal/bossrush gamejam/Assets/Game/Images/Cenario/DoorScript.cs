using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform positionCamera;
    public SpriteRenderer srp1, srp2, srp3, srp4;
    public GameObject p1, p2, p3, p4;

    public int numEstagiosConcluidos = 0;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        numEstagiosConcluidos = gameController.playerSettings.numEstagiosConcluidos;

        if(numEstagiosConcluidos == 1)
        {
            p1.GetComponent<Animator>().enabled = false;
            p1.SetActive(true);
            srp1.color = new Color(0.4f, 0.4f, 0.4f, 1);
        
        }
        if(numEstagiosConcluidos == 2)
        {
            p1.GetComponent<Animator>().enabled = false;
            p2.GetComponent<Animator>().enabled = false;
            p1.SetActive(true);
            p2.SetActive(true);
            srp1.color = new Color(0.4f, 0.4f, 0.4f, 1);
            srp2.color = new Color(0.4f, 0.4f, 0.4f, 1);
        }
        if(numEstagiosConcluidos == 3)
        {
            p1.GetComponent<Animator>().enabled = false;
            p2.GetComponent<Animator>().enabled = false;
            p3.GetComponent<Animator>().enabled = false;
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            srp1.color = new Color(0.4f, 0.4f, 0.4f, 1);
            srp2.color = new Color(0.4f, 0.4f, 0.4f, 1);
            srp3.color = new Color(0.4f, 0.4f, 0.4f, 1);
        }
        if(numEstagiosConcluidos == 4)
        {
            p1.GetComponent<Animator>().enabled = false;
            p2.GetComponent<Animator>().enabled = false;
            p3.GetComponent<Animator>().enabled = false;       
            p4.GetComponent<Animator>().enabled = false;
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            srp1.color = new Color(0.4f, 0.4f, 0.4f, 1);
            srp2.color = new Color(0.4f, 0.4f, 0.4f, 1);
            srp3.color = new Color(0.4f, 0.4f, 0.4f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Active()
    {
        if(numEstagiosConcluidos == 0)
        {
            p1.SetActive(true);
        }
        if(numEstagiosConcluidos == 1)
        {
            p2.SetActive(true);
        }
        if(numEstagiosConcluidos == 2)
        {
            p3.SetActive(true);
        }
        if(numEstagiosConcluidos == 3)
        {
            p4.SetActive(true);
        }
    }

    public void Activep1()
    {
        p1.SetActive(true);
    }
    public void Activep2()
    {
        p2.SetActive(true);
    }
    public void Activep3()
    {
        p3.SetActive(true);
    }
    public void Activep4()
    {
        p4.SetActive(true);
    }
}
