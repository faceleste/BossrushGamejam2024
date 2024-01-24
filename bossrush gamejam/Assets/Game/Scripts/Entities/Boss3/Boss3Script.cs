using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Script : MonoBehaviour
{
    public float speed = 0f; 
    public Color defaultColor;
    public PlayerAttack pAttack;
    public Animator animCam;
    public SpriteRenderer sr;
    public bool atk01;
    public bool atk02;
    public bool atk03;

    public GameObject spawnArma;
    public GameObject spawnArma2;
    public GameObject spawnArma3;
    public Transform playerPosition;
    public bool canSpawnAtkArma;
    public bool canSpawnAtkArma2;
    public bool canSpawnAtkArmaSegue;
    public Transform[] armasPosition;
    public float delaySpawnArmaAgain;
    public float delaySpawnArma2Again;
    public float delaySpawnArma3Again;

    public Transform armaMaca;
    public int numArmasSpawnadas;

    public float timeChangeAtks = 10;
    
    // Start is called before the first frame update
    IEnumerator Rotine()
    {
        atk01 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk01 = false;
        atk02 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk02 = false;
        atk03 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk03 = false;
        StartCoroutine(Rotine());
    }
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        armaMaca = GameObject.FindGameObjectWithTag("Arma").GetComponent<Transform>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultColor = sr.color;
        pAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        animCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        StartCoroutine(Rotine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(atk01)
        {
            if(canSpawnAtkArma)
            {
                StartCoroutine(AtkSpawnArma());
            }
        }
        if(atk02)
        {
            if(canSpawnAtkArma2)
            {
                StartCoroutine(AtkSpawnArmaCirculo());
            }
        }
        if(atk03)
        {
            if(canSpawnAtkArmaSegue)
            {
                StartCoroutine(AtkSpawnArmaSegue());
            }
        }
    }

    IEnumerator AtkSpawnArma()
    {
        canSpawnAtkArma = false;
        Instantiate(spawnArma, playerPosition.transform.position, transform.rotation);
        yield return new WaitForSeconds(delaySpawnArmaAgain);
        canSpawnAtkArma = true;
    }

    IEnumerator AtkSpawnArmaCirculo()
    {
        canSpawnAtkArma2 = false;
        Instantiate(spawnArma2, playerPosition.transform.position, transform.rotation);
        yield return new WaitForSeconds(delaySpawnArma2Again);
        canSpawnAtkArma2 = true;
    }
    IEnumerator AtkSpawnArmaSegue()
    {
        canSpawnAtkArmaSegue = false;
        int r = Random.Range(0, armasPosition.Length);
        Instantiate(spawnArma3, armasPosition[r].transform.position, transform.rotation);
        yield return new WaitForSeconds(delaySpawnArma3Again);
        canSpawnAtkArmaSegue = true;
    }
}
