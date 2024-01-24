using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueArmaSegue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startAnim;
    public GameObject endAnim;
    public GameObject momentAnim;
    public float speed = 3f;
    public Transform playerPosition;
    public Sprite[] sprites;
    public SpriteRenderer sr;
    public bool canMove = false;
    public float timeToMove = 2f;
    void Start()
    {
        
        sr = this.GetComponent<SpriteRenderer>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(DelayMove());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calcula a direção para o player
        Vector3 directionToPlayer = playerPosition.position - transform.position;

        // Calcula o ângulo em radianos
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Cria uma rotação que olha para a direção do player
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Aplica a rotação ao objeto (this)
        transform.rotation = rotation;
        Vector3 direction = (playerPosition.transform.position - this.transform.position).normalized;
        Vector3 offset = direction * -1; // Isso inverte a direção
        Vector3 lastPlayerPosition = playerPosition.transform.position - offset * 15;

        if(canMove)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, lastPlayerPosition, speed * Time.deltaTime);
        }
    }

    IEnumerator DelayMove()
    {
        GameObject anim = Instantiate(startAnim, this.transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(anim);
        int r = Random.Range(0, sprites.Length);
        sr.sprite = sprites[r];
        yield return new WaitForSeconds(timeToMove);
        canMove = true;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Arma")) 
        { 
            canMove = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            momentAnim = Instantiate(endAnim, this.transform.position, transform.rotation);
            StartCoroutine(DestroyObj());

        } 
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(momentAnim);
        Destroy(gameObject);

    }
}
