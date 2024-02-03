using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBocaScript : MonoBehaviour
{
    public GameObject aviso;
    public GameObject boca;
    public float DelayStart;
    public float timeSumirAviso = 0.6f;
    public float timeDestroyObj = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAtk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAtk()
    {
        yield return new WaitForSeconds(DelayStart);
        aviso.SetActive(true);
        yield return new WaitForSeconds(timeSumirAviso);
        aviso.SetActive(false);
        boca.SetActive(true);
        yield return new WaitForSeconds(timeDestroyObj);
        Destroy(gameObject);

    }
}
