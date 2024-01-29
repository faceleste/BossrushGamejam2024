using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{   
    public Vector3 positionInstantiad;
    public Transform armaMaca;

    public GameObject marcaSangue;

    public Vector3 direcao;
    public float distancia = 7f;
    public float speed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        distancia = Random.Range(distancia-3, distancia+1);
        positionInstantiad = this.transform.position;
        armaMaca = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        direcao = armaMaca.transform.position - this.transform.position;
        direcao = direcao.normalized * -1; // Normaliza a direção e inverte
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, positionInstantiad + direcao * distancia, speed);
        if(this.transform.position == positionInstantiad + direcao * distancia)
        {
            Instantiate(marcaSangue, this.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
