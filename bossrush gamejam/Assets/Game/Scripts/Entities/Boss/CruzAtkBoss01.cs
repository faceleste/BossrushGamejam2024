using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruzAtkBoss01 : MonoBehaviour
{
    public GameObject cruz;
    public GameObject aviso;
    // Start is called before the first frame update
    void Start()
    {
        cruz.SetActive(false);
        aviso.SetActive(true);
        StartCoroutine(ChangeAviso());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ChangeAviso()
    {
        cruz.SetActive(false);
        yield return new WaitForSeconds(1f);
        aviso.SetActive(false);
        cruz.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

   
}
