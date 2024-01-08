using UnityEngine;
using System.Collections;



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

    [Header("Objects")]

    public GameController gameController;

    public Rigidbody2D rb2d;

    private GameObject gameControllerObj;
    private Animator anim;
    private bool isDashing;


    public void Start()
    {
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();

        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;


    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

        isDashingController();
    }

    public void isDashingController()
    {
        if (isDashing && canDash)
        {
            StartCoroutine(DelayDash());
            playerSpeed = forceDash;
            Invoke("resetSpeed", 0.1f);

        }
    }

    public IEnumerator DelayDash()
    {
        canDash = false;
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }

    public void resetSpeed()
    {
        playerSpeed = gameController.playerSettings.speed;
        isDashing = false;
        canDash = false;
    }
    public void FixedUpdate()
    {
        if (canWalk == true)
        {

            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.velocity = movement.normalized * playerSpeed;
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    public void UpdateData()
    {
        playerSpeed = gameController.playerSettings.speed;
        forceDash = gameController.playerSettings.forceDash;
        hp = gameController.playerSettings.hp;
        cooldownDash = gameController.playerSettings.cooldownDash;

    }
}