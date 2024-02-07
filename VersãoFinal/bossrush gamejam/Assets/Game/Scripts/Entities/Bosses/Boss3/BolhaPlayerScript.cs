using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolhaPlayerScript : MonoBehaviour
{
    public GameObject bolha;
    public Player player;

    public bool canSpawn = true;
    public GameObject[] currentBolha;
    public int numBolhas = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isWalking == true && canSpawn == true)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        canSpawn = false;
        GameObject aCurrentBolha = Instantiate(bolha, this.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        DestroyOBJ(aCurrentBolha);
        canSpawn = true;
    }

    IEnumerator DestroyOBJ(GameObject g)
    {
        yield return new WaitForSeconds(3f);
        Destroy(g);
    }
}
