using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;


public class Player : MonoBehaviour
{

    [Header("Stats")]
    public float hp;
    public bool canWalk;
    public float playerSpeed;

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
    public void Start()
    {
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Cam>();
        animDie.SetActive(false);
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();

        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;
        bool isFirstTime = gameController.playerSettings.isFirstTime;
        if(isInCutscene)
        {
            canWalk = false;
            if(!isFirstTime)
            {
                StartCoroutine(Cutscene());
            }
            //StartCoroutine(Cutscene());
        }
    }

    public void Update()
    {

        if(!gameController.playerSettings.canMove){ 
            playerSpeed = 0 ; 
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

        
        
    }
    public void FCutscene()
    {
        StartCoroutine(Cutscene());
    }
    
    public IEnumerator Cutscene()
    {
        canWalk = false;
        yield return new WaitForSeconds(1.5f);
        rb2d.velocity = new Vector2(0, 2);
        playerAnim.SetTrigger("Cutscene");
        yield return new WaitForSeconds(3);
        rb2d.velocity = new Vector2(0, 0);
        camera.player = door.positionCamera;
        yield return new WaitForSeconds(0.5f);
        door.Active();
        yield return new WaitForSeconds(2.5f);
        camera.player = camera.newPlayer;
        yield return new WaitForSeconds(0.5f);
        canWalk = true;
    }

    public void isDashingController()
    {
        
        if (isDashing && canDash && canWalk)
        {
            
            playerAnim.SetBool("isDashing", true);

            preMove = new Vector2(moveHorizontal, moveVertical);
            canWalk = false;
            // Inicie com a velocidade máxima
            playerSpeed = forceDash;
            
            rb2d.velocity = preMove.normalized * playerSpeed;
            StartCoroutine(SpawnSpriteDash());
            StartCoroutine(DelayDash2());
            
    
        }
        else
        {
            isDashing = false;
        }
    }
    
    IEnumerator DelayDash2()
    {
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
        

        

        yield return new WaitForSeconds(cooldownDash);
        if(isDied == false)
        {
            canDash = true;
            //playerAnim.SetBool("isMoving", false);
        }
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

    public void UpdateData()
    {
        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;

    }
    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Fogo")) 
        { 
            Debug.Log("Acertado");
            canWalk = false;
            rb2d.velocity = new Vector2(0, 0);
            //SceneManager.LoadScene("Boss01");
            StartCoroutine(PlayerDied());
        } 
    } 
    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Fogo")) 
        { 
            Debug.Log("Acertado");
            canWalk = false;
            rb2d.velocity = new Vector2(0, 0);
            //SceneManager.LoadScene("Boss01");
            StartCoroutine(PlayerDied());
        } 

        if (collision.gameObject.CompareTag("Door")) 
        { 
            Debug.Log("Acertado");
            canWalk = false;
            rb2d.velocity = new Vector2(0, 0);
            //SceneManager.LoadScene("Boss01");
            StartCoroutine(ChangeScene());
        } 
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
            //SceneManager.LoadScene("Boss02");
        }
        if(gameController.playerSettings.numEstagiosConcluidos == 4)
        {
            //SceneManager.LoadScene("Boss04");
        }
        
    }
    IEnumerator PlayerDied()
    {
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