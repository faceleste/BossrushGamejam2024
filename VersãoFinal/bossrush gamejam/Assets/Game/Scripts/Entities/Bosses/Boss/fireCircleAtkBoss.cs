using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireCircleAtkBoss : MonoBehaviour
{
    public GameObject cFogo;
    public GameObject aviso;

    void Start()
    {
        cFogo.SetActive(false);
        aviso.SetActive(true);
        StartCoroutine(ChangeAviso());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ChangeAviso()
    {
        cFogo.SetActive(false);
        yield return new WaitForSeconds(1f);
        aviso.SetActive(false);
        cFogo.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
