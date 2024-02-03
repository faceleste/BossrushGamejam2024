using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss4Script : MonoBehaviour
{
    public Image barraVida;
    public float vida;
    public float currentVida;
    public float speed = 0f;
    public Color defaultColor;
    public PlayerAttack pAttack;
    public Animator animCam;
    public SpriteRenderer sr;
    public Animator anim;
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

    public GameObject[] sangue;
    public GameObject marcaSangue;

    public bool isFliped;
    public GameObject splashSangue;
    public Transform centerBoss;
    public Player player;
    public ScriptBackLobby lobby;
    public float f;
    public GameController gameController;
    [Header("Condições")]

    public bool isFire;
    public bool isBledding;

    private int bloodBossCount = 0;
    public int stacksBlood = 1;

    public GameObject sangueSangramento;
    public GameObject fogoSkill;
    // Start is called before the first frame update
    IEnumerator Rotine()
    {
        atk01 = false;
        yield return new WaitForSeconds(timeChangeAtks / 1.8f);
        atk02 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk02 = false;
        yield return new WaitForSeconds(timeChangeAtks / 4);
        atk01 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk01 = false;
        yield return new WaitForSeconds(timeChangeAtks / 4);
        atk03 = true;
        yield return new WaitForSeconds(timeChangeAtks);
        atk03 = false;

        StartCoroutine(Rotine());
    }
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currentVida = vida;
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        armaMaca = GameObject.FindGameObjectWithTag("Arma").GetComponent<Transform>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultColor = sr.color;
        pAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        animCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(atk01 == true)
        {
         StartCoroutine(Rotine());
        }
    }

    void Update()
    {
        if (currentVida <= 0)
        {
            anim.SetTrigger("Morrendo");
            StartCoroutine(TimeToDie());
        }

        if (playerPosition.transform.position.x < this.transform.position.x)
        {
            //sr.flipX = true;
            //Quaternion target = Quaternion.Euler(0, -180, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = true;
        }
        if (playerPosition.transform.position.x > this.transform.position.x)
        {
            //sr.flipX = false;
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = false;
        }

        BossBleed();
        if (isFire)
        {
            f = 0.30196078f;
        }
    }

    void BossBleed()
    {
        if (isBledding)
        {
            InvokeRepeating("BloodBoss", 0f, 1f);
            isBledding = false;

        }
    }
    void BloodBoss()
    {

        currentVida -= gameController.playerSettings.damageBlood * stacksBlood;
        barraVida.fillAmount = currentVida / vida;
        Debug.Log("vida: " + currentVida + "stacks: " + stacksBlood);
        Instantiate(sangueSangramento, new Vector2(this.transform.position.x, this.transform.position.y+2.8f), transform.rotation);
        //colocar efeito de sangramento here
        //animação de
        //Instantiate(fogoSkill, new Vector2(this.transform.position.x, this.transform.position.y+3.2f), transform.rotation);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (atk01)
        {
            if (canSpawnAtkArma)
            {
                StartCoroutine(AtkSpawnArma());
            }
        }
        if (atk02)
        {
            if (canSpawnAtkArma2)
            {
                StartCoroutine(AtkSpawnArmaCirculo());
            }
        }
        if (atk03)
        {
            if (canSpawnAtkArmaSegue)
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
        //anim.SetTrigger("Atacking");
        canSpawnAtkArma2 = false;
        Instantiate(spawnArma2, playerPosition.transform.position, transform.rotation);
        yield return new WaitForSeconds(delaySpawnArma2Again);
        canSpawnAtkArma2 = true;
    }
    IEnumerator AtkSpawnArmaSegue()
    {
        canSpawnAtkArmaSegue = false;
        Instantiate(spawnArma3, playerPosition.transform.position, transform.rotation);
        yield return new WaitForSeconds(delaySpawnArma3Again);
        canSpawnAtkArmaSegue = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arma"))
        {

            // 1 - vida
            // x   currentvida
            StartCoroutine(DelayTakeDmg());

            if (currentVida <= 0)
            {
                anim.SetTrigger("Morrendo");
                //Time.timeScale = 1f;
                StartCoroutine(TimeToDie());
            }

            Instantiate(splashSangue, centerBoss.transform.position, transform.rotation);
            animCam.SetTrigger("Shake");
            Debug.Log("Acertado");
            StartCoroutine(SwitchColor());
            Vector3 direcao = armaMaca.transform.position - this.transform.position;
            direcao = direcao.normalized * -1; // Normaliza a direção e inverte
            float distancia = 0.05f; // Define a distância que você quer mover para trás
            this.transform.position = this.transform.position + direcao * distancia;

            if (gameController.playerSettings.canAttackBlood)
            {
                if (!isBledding)
                {
                    isBledding = true;
                }

                stacksBlood++;

                if (stacksBlood >= 10)
                {
                    stacksBlood = 10;
                }
            }
        }
    }
    IEnumerator TimeToDie()
    {
        player.canWalk = false;
        Time.timeScale = 0.4f;
        player.playerAnim.SetBool("isMoving", false);
        player.animDie.SetActive(true);
        player.rb2d.velocity = new Vector2(0, 0);
        player.sr.sortingOrder = 2050;
        sr.sortingOrder = player.sr.sortingOrder;

        yield return new WaitForSeconds(1f);
        gameController.playerSettings.numEstagiosConcluidos++;
        Time.timeScale = 1f;
        lobby.BackTo(3f);
        Destroy(this);
    }
    IEnumerator DelayTakeDmg()
    {
        for (float i = currentVida; i > currentVida - pAttack.dano; i -= 0.6f)
        {
            barraVida.fillAmount = i / vida;
            yield return new WaitForSeconds(0.000005f);
        }
        currentVida -= pAttack.dano;
    }
    IEnumerator SwitchColor()
    {

        int rand = Random.Range(0, sangue.Length);
        //sangue[rand].SetActive(true);
        if (isFliped == true)
        {
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y + 2.6f), Quaternion.Euler(0, 180, 0)).transform.parent = gameObject.transform;
        }
        else
        {
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y + 2.6f), Quaternion.Euler(0, 0, 0)).transform.parent = gameObject.transform;
        }

        Time.timeScale = 0.02f;
        for (int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.001f);
        }
        Time.timeScale = 1;
        for (int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.2f);
            sr.color = defaultColor;
            yield return new WaitForSeconds(0.2f);
        }
        Instantiate(marcaSangue, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        //Destroy(t);
        //sangue[rand].SetActive(false);
    }

}
