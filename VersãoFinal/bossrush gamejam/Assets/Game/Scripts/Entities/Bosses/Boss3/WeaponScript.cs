using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject[] armas;
    public float delaySpawnArma;
    public float timeDestroy = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnArma());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnArma()
    {
        // Cria uma lista de índices
        List<int> indices = new List<int>();
        for (int i = 0; i < armas.Length; i++)
        {
            indices.Add(i);
        }

        // Embaralha a lista de índices
        for (int i = 0; i < indices.Count; i++)
        {
            int temp = indices[i];
            int randomIndex = Random.Range(i, indices.Count);
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // Ativa os objetos em uma ordem aleatória
        foreach (int index in indices)
        {
            yield return new WaitForSeconds(delaySpawnArma);
            armas[index].SetActive(true);
        }
        yield return new WaitForSeconds(1.4f);
        //Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.1f);
        //Time.timeScale = 1f;
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
