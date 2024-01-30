using UnityEngine;

public class StoreController : MonoBehaviour
{
    public Canvas store;
    public bool isNextDemon;
    public LayerMask layerPlayer;
    public Transform positionDemon;
    public float range = 1f;

    public GameObject buttonE;

    void Update()
    {
        isNextDemon = Physics2D.OverlapCircle(positionDemon.transform.position, range, layerPlayer);
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
        }
        else
        {
            store.gameObject.SetActive(false);
        }
       

    }

    void ViewStoreExit()
    {
        store.gameObject.SetActive(false);
    }
}