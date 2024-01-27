using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Canvas inventory;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ViewInventory();
        }
    }

    void ViewInventory()
    {
        if (inventory.gameObject.activeSelf == false)
        {
            inventory.gameObject.SetActive(true);
        }
        else
        {
            inventory.gameObject.SetActive(false);
        }
    }

}
