using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruzAtkBoss01 : MonoBehaviour
{
    public GameObject[] cruz;
    public GameObject aviso;
    public int[] numPegos;
    public int k;
    List<int> indicesSorteados = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < cruz.Length; i++)
        {
            cruz[i].GetComponent<Renderer>().sortingOrder = -(int)(cruz[i].transform.position.y * 5);
            cruz[i].GetComponent<CircleCollider2D>().enabled = false;
            //cruz[i].SetActive(false);
        }
        aviso.SetActive(true);
        
        StartCoroutine(ChangeAviso());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ChangeAviso()
    {
        //cruz.SetActive(false);
        yield return new WaitForSeconds(1f);
        aviso.SetActive(false);
        for(int i = 0; i < cruz.Length; i++)
        {
            do
            {
                k = Random.Range(0, cruz.Length);
            } while (indicesSorteados.Contains(k));

            indicesSorteados.Add(k);
            cruz[k].SetActive(true);
            yield return new WaitForSeconds(0.006f);
            if(i > cruz.Length / 2)
            {
                for(int k = 0; k < cruz.Length; k++)
                {
                    cruz[k].GetComponent<CircleCollider2D>().enabled = true;
                }
            }
        }
        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);
    }

   IEnumerator ActiveObj()
   {
    yield return new WaitForSeconds(0.4f);
    for(int i = 0; i < cruz.Length; i++)
        {
            cruz[i].GetComponent<CircleCollider2D>().enabled = true;
        }
   }
}
