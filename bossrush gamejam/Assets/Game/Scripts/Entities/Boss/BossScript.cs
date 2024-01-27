using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public Image barraVida;
    public bool meleeMoment;
    public bool cruzMoment;
    public bool circleMoment;

    [Header("Ataque melee")]
    public float speed; 
    public float meleeDamage;
    public bool isInMeleeAtk;
    public float timeToAtkAgain = 0.5f;
    private float cooldownToAtkAgain;
    public bool isInRangeMelee;
    public GameObject steps;
    public bool canSpawnPegadas = true; 
    public Vector3 lastPlayerPosition;
    public GameObject spriteDashPrefab;
    public ScriptSpriteDash scriptSpriteDash;
    public Sprite[] spriteBossBranco;
    public bool isFliped;
    public float colorj = 1f;

    [Header("Ataque cruz")]
    public bool canSpawnCruz;
    public bool canMoveToCruz;
    public GameObject cruzPrefab;
    public float cooldownSpawnCruz;
    public int numCruzes;
    public bool cruzSpawned;

    [Header("Ataque Circulo Fogo")]
    public bool canSpawnCircle;
    public GameObject[] circlePrefab;
    public float cooldownSpawnCircle;
    public int numCircles;

    public GameObject meleeObjAtk;
    public float rangeToMeleeAtk;
    public LayerMask playerLayer;
    public Transform playerPosition;
    public PlayerAttack player;
    public SpriteRenderer sr;
    private Color defaultColor;
    public Animator anim;
    public bool canSpawnSprite = true;
    public Animator animCam;
    public GameObject[] sangue;
    public bool canAttackPlayer = true;
    public Player playerMove;
    public GameObject marcaSangue;
    public float cooldownChangeAtk = 12f;
    public Transform armaMaca;

    public Transform centerBoss;
    public float vida;
    public float currentVida;
    public GameObject splashSangue;

    // Start is called before the first frame update
    void Start()
    {
        currentVida = vida;   
        cooldownToAtkAgain = timeToAtkAgain;
        defaultColor = sr.color;
        StartCoroutine(Rotine());
    }

    public void StartRoutineBoss()
    {
        StartCoroutine(Rotine());
    }

    // Update is called once per frame
    void Update()
    {
        isInRangeMelee = Physics2D.OverlapCircle(this.transform.position, rangeToMeleeAtk, playerLayer);
        if(playerPosition.transform.position.x < this.transform.position.x)
        {
            sr.flipX = true;
            //Quaternion target = Quaternion.Euler(0, -180, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = true;
        }
        if(playerPosition.transform.position.x > this.transform.position.x)
        {
            sr.flipX = false;
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = false;
        }
        if(playerMove.isDied == true)
        {
            canAttackPlayer = false;
        }
    }

   
    void FixedUpdate()
    {
        if(canAttackPlayer)
        {
            if(meleeMoment)
            {
                animCam.SetBool("canMoveNext", true);
                if(isInMeleeAtk)
                {
                    //if(isInRangeMelee)
                    //{
                        StartCoroutine(MeleeAtk());
                    //}
            
                }
                else
                {
                    
                    this.transform.position = Vector3.MoveTowards(this.transform.position, lastPlayerPosition, speed/3);
                    if(this.transform.position == lastPlayerPosition)
                    {
                        anim.SetBool("Atacando", false);
                    }
                    else
                    {
                        anim.SetBool("Atacando", true);
                        if(canSpawnSprite == true)
                        {
                            StartCoroutine(SpawnSpriteBoss());
                        }
                        if(canSpawnPegadas)
                        {
                            StartCoroutine(ShowSteps());       
                        }
                    }
                    
                    

                }

                if(cooldownToAtkAgain < timeToAtkAgain)
                {
                    cooldownToAtkAgain += Time.deltaTime;
                }
            }
            else
            {
                anim.SetBool("Atacando", false);
                animCam.SetBool("canMoveNext", false);
            }

            if(cruzMoment)
            {
                if(canMoveToCruz == true)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosition.transform.position, speed/4);
                    if(canSpawnSprite)
                    {
                        StartCoroutine(SpawnSpriteBoss());
                    }
                    if(canSpawnPegadas)
                    {
                        //StartCoroutine(ShowSteps());       
                    }
                }

                if(this.transform.position == playerPosition.transform.position && cruzSpawned == false)
                {
                    anim.SetTrigger("Fincando");
                    Instantiate(cruzPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
                    StartCoroutine(DelayToShakeCam());
                    cruzSpawned = true;
                    canMoveToCruz = false;
                }
                
                for(int i = 0; i<= numCruzes; i++)
                {
                    if(canSpawnCruz)
                    {
                        StartCoroutine(CruzAtk());
                    }
                }
            }

            if(circleMoment)
            {
                
                for(int i = 0; i<= numCircles; i++)
                {
                    if(canSpawnCircle)
                    {
                        StartCoroutine(CircleAtk());
                    }
                }
            }
        }

    }

    public IEnumerator ShowSteps()
    {
        canSpawnPegadas = false;
        yield return new WaitForSeconds(0.02f);
        Instantiate(steps, new Vector2(this.transform.position.x, this.transform.position.y+0.4f), Quaternion.Euler(0, 0, 0));
        canSpawnPegadas = true;
    }
    public IEnumerator SpawnSpriteBoss()
    {
 
        canSpawnSprite = false;
        yield return new WaitForSeconds(0.1f);
        int r = Random.Range(0, spriteBossBranco.Length);
        scriptSpriteDash = Instantiate(spriteDashPrefab, new Vector2(this.transform.position.x, this.transform.position.y+2), transform.rotation).GetComponent<ScriptSpriteDash>();
        scriptSpriteDash.mainSprite = spriteBossBranco[r];
        scriptSpriteDash.speedSumir = 0.2f;
        if(isFliped)
        {
            scriptSpriteDash.sr.flipX = true; 
        }
        scriptSpriteDash.newColor = new Color(1f, 1f, 1f, 0);
        scriptSpriteDash.sr.color = new Color(1f, 1f, 1f, colorj);
        
        /*if(colorj >= 0.8f)
        {
            colorj -= 0.10f;
        }
        else
        {
            colorj += 0.10f;
        }*/
        canSpawnSprite = true;
    }
    public IEnumerator Rotine()
    {
        cruzMoment = false;
        circleMoment = false;
        yield return new WaitForSeconds(cooldownChangeAtk/3);
        meleeMoment = true;
        yield return new WaitForSeconds(cooldownChangeAtk);
        meleeMoment = false;
        cruzMoment = true;
        circleMoment = false;
        yield return new WaitForSeconds(cooldownChangeAtk);
        meleeMoment = false;
        cruzMoment = false;
        circleMoment = true;
        yield return new WaitForSeconds(cooldownChangeAtk);

        StartCoroutine(Rotine());
    }
    public IEnumerator CruzAtk()
    {

        if(cruzMoment == true)
        {
            canSpawnCruz = false;
            yield return new WaitForSeconds(cooldownSpawnCruz/1.5f);
            if(cruzMoment)
            {
                canMoveToCruz = true;
                yield return new WaitForSeconds(cooldownSpawnCruz/2);
                animCam.SetBool("canMoveNext", true); 
                yield return new WaitForSeconds(cooldownSpawnCruz/2);
                canMoveToCruz = false;    
                     
            }
        }
        
        canSpawnCruz = true;  
        cruzSpawned = false;
    }

    public IEnumerator CircleAtk()
    {

        canSpawnCircle = false;
        yield return new WaitForSeconds(cooldownSpawnCircle);
        int r = Random.Range(0, circlePrefab.Length);
        Instantiate(circlePrefab[r], playerPosition.position, Quaternion.Euler(0, 0, 0));
        canSpawnCircle = true;
        

    }
    public IEnumerator MeleeAtk()
    {
        Vector3 direction = (playerPosition.transform.position - this.transform.position).normalized;
        Vector3 offset = direction * -1; // Isso inverte a direção
        lastPlayerPosition = playerPosition.transform.position - offset * 5;
        isInMeleeAtk = false;
        //meleeObjAtk.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        meleeObjAtk.SetActive(false);
        yield return new WaitForSeconds(cooldownToAtkAgain);
        isInMeleeAtk = true;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Arma")) 
        { 
            StartCoroutine(DelayTakeDmg());
            
            if(currentVida <= 0)
            {
                anim.SetTrigger("Morrendo");
                //Time.timeScale = 1f;
                StartCoroutine(TimeToDie());
            }

            animCam.SetTrigger("Shake");
            Debug.Log("Acertado");
            StartCoroutine(SwitchColor());
            Instantiate(splashSangue, centerBoss.transform.position, transform.rotation);
            Vector3 direcao = armaMaca.transform.position - this.transform.position;
            direcao = direcao.normalized * -1; // Normaliza a direção e inverte
            float distancia = 0.5f; // Define a distância que você quer mover para trás
            this.transform.position = this.transform.position + direcao * distancia;
        } 
    } 

    IEnumerator TimeToDie()
    {
        playerMove.canWalk = false;
        Time.timeScale = 0.4f;
        playerMove.playerAnim.SetBool("isMoving", false);
        playerMove.animDie.SetActive(true);
        playerMove.rb2d.velocity = new Vector2(0, 0);
        playerMove.sr.sortingOrder = 2050;
        sr.sortingOrder = playerMove.sr.sortingOrder;
        
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Destroy(this);
    }
    IEnumerator DelayTakeDmg()
    {
        for(float i = currentVida; i > currentVida - player.dano;  i-=0.6f)
        {
            barraVida.fillAmount = i / vida;
            yield return new WaitForSeconds(0.000005f);
        }
        currentVida -= player.dano;
    }

    IEnumerator SwitchColor()
    {
        
        int rand = Random.Range(0, sangue.Length);
        //sangue[rand].SetActive(true);
        if(isFliped == true)
        {   
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y+1.8f), Quaternion.Euler(0, 180, 0)).transform.parent = gameObject.transform;
        }
        else
        {
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y+1.8f), Quaternion.Euler(0, 0, 0)).transform.parent = gameObject.transform;
        }

        Time.timeScale = 0.02f;
        for(int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.001f);
        }
        Time.timeScale = 1;
        for(int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.2f);
            sr.color = defaultColor;
            yield return new WaitForSeconds(0.2f);
        }
        Instantiate(marcaSangue, new Vector2(this.transform.position.x, this.transform.position.y+0.4f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        //Destroy(t);
        //sangue[rand].SetActive(false);
    }

    IEnumerator DelayToShakeCam()
    {
        
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.002f);
        Time.timeScale = 1;
        animCam.SetTrigger("Shake");
    
    }

}

