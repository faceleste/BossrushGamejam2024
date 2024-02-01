using UnityEngine;
using System.Collections;
public class StoreController : MonoBehaviour
{
    public Canvas store;
    public bool isNextDemon;
    public LayerMask layerPlayer;
    public Transform positionDemon;
    public float range = 1f;
    public bool canExit = false;

    public GameObject buttonE;

    public GameController gameController;
    public Player playerScript;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if(gameController.playerSettings.isFirstTime == true)
        {
            store.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        isNextDemon = Physics2D.OverlapCircle(new Vector2(positionDemon.transform.position.x, positionDemon.transform.position.y+0.5f), range, layerPlayer);

        if(isNextDemon)
        {
            buttonE.SetActive(true);
            //buttonE.GetComponent<SpriteRenderer>().sortingOrder = 5000;
            if(Input.GetKeyDown(KeyCode.E))
            {
                ViewStoreEnter();
            }
        }
        else
        {
            buttonE.SetActive(false);

            if(Input.GetKeyDown(KeyCode.E))
            {
                ViewStoreExit();
            }
        }



    }

    void ViewStoreEnter()
    {
        if(store.gameObject.activeSelf == false)
        {
            store.gameObject.SetActive(true);
            StartCoroutine(DelayToExit());
        }
        else
        {
            if(canExit)
            {
                if(gameController.playerSettings.isFirstTime == true)
                {
                    playerScript.FCutscene();
                    gameController.playerSettings.isFirstTime = false;
                }
                store.gameObject.SetActive(false);
                canExit = false;
            }
            
        }
       

    }

    void ViewStoreExit()
    {
        if(canExit)
        {
            
            store.gameObject.SetActive(false);
            canExit = false;
            if(gameController.playerSettings.isFirstTime == true)
            {
                playerScript.FCutscene();
                gameController.playerSettings.isFirstTime = false;
            }
        }
        
    }
    
    IEnumerator DelayToExit()
    {
        yield return new WaitForSeconds(0.8f);
        canExit = true;
    }
}