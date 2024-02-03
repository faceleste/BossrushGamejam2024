using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Stats")]
    public int hp;
    public int shields;
    public bool canWalk;
    public float playerSpeed;

    public bool canTakeDmg = true;

    [Header("Dash")]
    public float forceDash;
    public float cooldownDash;
    public bool canDash = true;
    public GameObject spriteDashPrefab;
    public ScriptSpriteDash scriptSpriteDash;
    public Sprite spriteDash;
    public bool isFliped;

    [Header("Objects")]
    public GameController gameController;
    public Rigidbody2D rb2d;
    private GameObject gameControllerObj;
    public Animator playerAnim;
    public Animator animCam;
    public float moveHorizontal;
    public float moveVertical;
    public Vector2 preMove;
    private bool isDashing;
    public SpriteRenderer sr;
    public float colorj = 0.15f;
    public GameObject animDie;
    public Transform localTpMorte;
    public bool isDied = false;
    public bool isWalking;
    public bool isInCutscene;
    public DoorScript door;
    public Cam camera;

    public Image[] heartObj;
    public Image[] shieldObj;
    public Sprite heart;
    public Sprite nullHeart;
    public Sprite shield;
    public Sprite nullShield;
    public Color defaultColor;

    public bool canRecoverShield = true;
    public PlayerAttack playerAttack;

    public int numDashs;

    public AudioSource aSPassos;
    public AudioClip[] sonsPassos;
    public bool canChangeStepsSound = false;
    public bool canSomPasso = true;

    public AudioSource aSDash;
    public AudioClip[] sonsDash;
    public bool canSoundsCutscene = false;
    public void Start()
    {
        defaultColor = sr.color;
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        VerificaImgVida();
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Cam>();
        Time.timeScale = 1f;
        animDie.SetActive(false);
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
        numDashs = gameController.playerSettings.qtdDash;
        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;
        bool isFirstTime = gameController.playerSettings.isFirstTime;
        shields = gameController.playerSettings.numShields;
        if(isInCutscene)
        {
            canWalk = false;
            if(!isFirstTime)
            {
                canSoundsCutscene = true;
                StartCoroutine(Cutscene());
            }
            //StartCoroutine(Cutscene());
        }
    }

    public void Update()
    {

        if(isInCutscene)
        {
            
        }
        if(canSoundsCutscene == true)
        {
            StartCoroutine(RandomizePasso());
        }
        if(gameController.playerSettings.canRecoverShield == true)
        {
            if(canRecoverShield)
            {
                StartCoroutine(RecoverShield());
            }
        }
        VerificaImgVida();
        if(!gameController.playerSettings.canMove){ 
            playerSpeed = 0 ; 
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

        
        
    }

    IEnumerator RecoverShield()
    {
        canRecoverShield = false;
        yield return new WaitForSeconds(120);
        if(shields != 1)
        {
            shields = 1;
        }
        canRecoverShield = true;
    }
    public void FCutscene()
    {
        canSoundsCutscene = true;
        StartCoroutine(Cutscene());
    }
    
    public IEnumerator Cutscene()
    {
        playerAttack.canAttackAgain = false;
        canWalk = false;
        yield return new WaitForSeconds(1.5f);
        rb2d.velocity = new Vector2(0, 2);
        playerAnim.SetTrigger("Cutscene");
        yield return new WaitForSeconds(3);
        rb2d.velocity = new Vector2(0, 0);
        canSoundsCutscene = false;
        camera.player = door.positionCamera;
        yield return new WaitForSeconds(0.5f);
        door.Active();
        yield return new WaitForSeconds(2.5f);
        camera.player = camera.newPlayer;
        yield return new WaitForSeconds(0.5f);
        canWalk = true;
        isInCutscene = false;
        playerAttack.canAttackAgain = true;
    }

    public void isDashingController()
    {
        
        if (isDashing && canDash && canWalk && numDashs != 0)
        {
            
            playerAnim.SetBool("isDashing", true);

            preMove = new Vector2(moveHorizontal, moveVertical);
            canWalk = false;
            // Inicie com a velocidade máxima
            playerSpeed = forceDash;
            
            rb2d.velocity = preMove.normalized * playerSpeed;
            StartCoroutine(SpawnSpriteDash());
            StartCoroutine(DelayDash2());
            numDashs --;
            
    
        }
        else
        {
            isDashing = false;
        }
    }
    
    IEnumerator DelayDash2()
    {
        RandomizeDash();
        float elapsedTime = 0;
        float dashTime = 0.40f; // Ajuste para a duração desejada do dash

        while (elapsedTime < dashTime)
        {
            // Diminua a velocidade ao longo do tempo
            playerSpeed = Mathf.Lerp(forceDash, 0, elapsedTime / dashTime);
            rb2d.velocity = preMove.normalized * playerSpeed;

            elapsedTime += Time.deltaTime;
            if(elapsedTime >= dashTime - 0.1f)
            {
                playerAnim.SetBool("isDashing", false);
            }
            yield return null;
            
        }
         playerAnim.SetBool("isDashing", false);
        // No final do dash, pare completamente
        //yield return new WaitForSeconds(0.1f)
        if(isDied == false)
        {
            Invoke("resetSpeed", 0.01f);
        }

        canDash = false;
        

        
        if(numDashs == 0)
        {
            yield return new WaitForSeconds(cooldownDash);
            numDashs = gameController.playerSettings.qtdDash;
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            canDash = true;
            yield return new WaitForSeconds(0.3f);
            numDashs = 0;
           
            
        }
        
        //if(numDashs != 1)
        //{
           
        //}
        

        if(isDied == false)
        {
            canDash = true;
            if(numDashs == 0)
            {
                yield return new WaitForSeconds(cooldownDash);
                numDashs = gameController.playerSettings.qtdDash;
            }
            //playerAnim.SetBool("isMoving", false);
        }
    }

    public void RandomizeDash()
    {
        int r = Random.Range(0, sonsDash.Length);
        aSDash.clip = sonsDash[r];
        aSDash.Play();
    }
    public IEnumerator SpawnSpriteDash()
    {
        

        yield return new WaitForSeconds(0.04f);
        for(int i = 0; i<=4; i++)
        {
            yield return new WaitForSeconds(0.04f);
            scriptSpriteDash = Instantiate(spriteDashPrefab, this.transform.position, transform.rotation).GetComponent<ScriptSpriteDash>();
            spriteDash = sr.sprite;
            scriptSpriteDash.mainSprite = spriteDash;
            if(isFliped)
            {
                scriptSpriteDash.sr.flipX = true; 
            }
            scriptSpriteDash.newColor = new Color(0f, 0.8f, 1.0f, 0);
            scriptSpriteDash.sr.color = new Color(0f, 0.8f, 1.0f, colorj);
            colorj += 0.10f;
            
        }
        colorj = 0.15f;
        yield return new WaitForSeconds(0.1f);
        canWalk = true;
    }
    public IEnumerator DelayDash()
    {
        //yield return new WaitForSeconds(0.1f)
        StartCoroutine(SpawnSpriteDash());
        canDash = false;
        
        yield return new WaitForSeconds(0.04f);
        playerAnim.SetBool("isDashing", false);
        yield return new WaitForSeconds(cooldownDash);
        if(isDied == false)
        {
            canDash = true;
            //playerAnim.SetBool("isMoving", false);
        }
        
        
    }

    public void resetSpeed()
    {
        playerSpeed = gameController.playerSettings.speed;
        isDashing = false;
        canDash = false;
    }
    public void FixedUpdate()
    {
        isDashingController();

        if (canWalk == true)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.velocity = movement.normalized * playerSpeed;

            if(moveHorizontal > 0)
            {
                sr.flipX = true;
                isFliped = true;
            }
            if(moveHorizontal < 0)
            {
                sr.flipX = false;
                isFliped = false;
            }

            if(moveHorizontal != 0 || moveVertical != 0)
            {
                playerAnim.SetFloat("Horizontal", moveHorizontal);
                playerAnim.SetFloat("Vertical", moveVertical);
                StartCoroutine(RandomizePasso());
                if(!isWalking)
                {
                    
                    isWalking = true;
                    playerAnim.SetBool("isMoving", isWalking);
                }
                
            }
            else
            {
                if(isWalking)
                {
                    isWalking = false;
                    playerAnim.SetBool("isMoving", isWalking);
                    rb2d.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            //rb2d.velocity = new Vector2(0, 0);
        }
    }

    public IEnumerator RandomizePasso()
    {
        if(canSomPasso == true)
        {
            canSomPasso = false;
            int r = Random.Range(0, sonsPassos.Length);
            aSDash.clip = sonsPassos[r];
            aSDash.Play();
            yield return new WaitForSeconds(0.3f);
            canSomPasso = true;
        }
    }

    public void UpdateData()
    {
        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;
        shields = gameController.playerSettings.numShields;
        numDashs = gameController.playerSettings.qtdDash;
        VerificaImgVida();
        

    }

    public void VerificaImgVida()
    {
        if(hp == 1)
        {
            heartObj[0].sprite = heart;
            heartObj[1].sprite = nullHeart;
        }
        if(hp == 2)
        {
            heartObj[0].sprite = heart;
            heartObj[1].sprite = heart;
        }

        if(shields == 0)
        {
            shieldObj[0].sprite = nullShield;
        }
        if(shields == 1)
        {
            shieldObj[0].sprite = shield;
        }
    }
    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Fogo")) 
        { 
            if(canTakeDmg)
            {
                Debug.Log("Acertado");
                if(shields <= 0)
                {
                    hp --;
                    heartObj[hp].sprite = nullHeart;
                    StartCoroutine(Damage(shields));
                }
                else
                {
                    shields --;
                    shieldObj[0].sprite = nullShield;
                    StartCoroutine(Damage(shields));
                }

                if(hp <= 0)
                {
                    heartObj[0].sprite = nullHeart;
                    StartCoroutine(PlayerDied());
            }
            }
        } 
    } 
    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Fogo")) 
        { 
            if(canTakeDmg)
            {
                Debug.Log("Acertado");
                //SceneManager.LoadScene("Boss01");
                if(shields <= 0)
                {
                    hp --;
                    heartObj[hp].sprite = nullHeart;
                    StartCoroutine(Damage(shields));
                }
                else
                {
                    shields --;
                    shieldObj[0].sprite = nullShield;
                    StartCoroutine(Damage(shields));
                }

                if(hp <= 0)
                {
                    heartObj[0].sprite = nullHeart;
                    StartCoroutine(PlayerDied());
                }
            }
        } 

        if (collision.gameObject.CompareTag("Door")) 
        { 
            Debug.Log("Acertado");
            canWalk = false;
            rb2d.velocity = new Vector2(0, 0);
            //SceneManager.LoadScene("Boss01");
            StartCoroutine(ChangeScene());
        } 

        if(collision.gameObject.CompareTag("Pedra")) 
        { 
            canChangeStepsSound = true;
        } 
        else if(collision.gameObject.CompareTag("Grama"))
        {
            canChangeStepsSound = false;
        }
   
    } 

    IEnumerator Damage(int dano)
    {
        canTakeDmg = false;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
        for(int i = 0; i < 8; i++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.4f);
            yield return new WaitForSeconds(0.1f);
            sr.color = defaultColor;
            yield return new WaitForSeconds(0.1f);
        }
        canTakeDmg = true;
    }
    IEnumerator ChangeScene()
    {
        playerAnim.SetBool("isMoving", false);
        //animCam.SetTrigger("Pmorrendo");
        yield return new WaitForSeconds(1f);
        //sr.sortingOrder = 2050;
        animDie.SetActive(true);
        
        yield return new WaitForSeconds(2.5f);
        if(gameController.playerSettings.numEstagiosConcluidos == 0)
        {
            SceneManager.LoadScene("Boss01");
        }
        if(gameController.playerSettings.numEstagiosConcluidos == 1)
        {
            SceneManager.LoadScene("Boss03");
        }
        if(gameController.playerSettings.numEstagiosConcluidos == 2)
        {
            SceneManager.LoadScene("Boss04");
        }
        if(gameController.playerSettings.numEstagiosConcluidos == 3)
        {
            SceneManager.LoadScene("Boss02");
        }
        
    }
    IEnumerator PlayerDied()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        canWalk = false;
        rb2d.velocity = new Vector2(0, 0);
        isDied = true;
        sr.sortingOrder = 2050;
        animDie.SetActive(true);
        playerAnim.SetTrigger("Morrendo");
        yield return new WaitForSeconds(0.5f);
        animCam.SetTrigger("Pmorrendo");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("TrueLobby");
        //this.transform.position = localTpMorte.transform.position;
        
    }

}