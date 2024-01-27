using UnityEngine;

public class StoreController : MonoBehaviour
{
    public Canvas store;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Testesteteteststs"); 
            ViewStore();
        }
    }

    void ViewStore()
    {
        if (store.gameObject.activeSelf == false)
        {
            store.gameObject.SetActive(true);
        }
        else
        {
            store.gameObject.SetActive(false);
        }
    }
}