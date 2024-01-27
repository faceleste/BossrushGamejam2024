using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoledWeaponScript : MonoBehaviour
{
    public GameObject bolha;
    public GameObject arma;
    public SpriteRenderer sr;
    public float timeActive = 0.4f;
    public float timeActived = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        timeActive = 1f;
        //sr = this.transform.Find("sprite").GetComponent<SpriteRenderer>();
        //arma = this.transform.Find("sprite").GetComponent<GameObject>();
        arma.SetActive(false);
        StartCoroutine(Active());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Active()
    {
        GameObject bo = Instantiate(bolha, this.transform.position, transform.rotation);
        yield return new WaitForSeconds(timeActive);
        Destroy(bo);
        arma.SetActive(true);
        sr.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1);
        sr.color = new Color(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
