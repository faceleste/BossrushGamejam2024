using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedoAtkScript : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Vector2 velocidade;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyObj());
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = velocidade;
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
