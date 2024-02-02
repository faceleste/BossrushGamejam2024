using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaScript : MonoBehaviour
{
    public PlayerAttack playerAtk;
    public Rigidbody2D rb2d;
    public float thrust = 2f;
    public Vector3 direction;

    public AudioSource audio;
    public AudioClip[] sonsHits;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
    /*if(cooldownSpecialAttack <= timeSpecialAttack && cooldownSpecialAttack >= 0)
    {
        direction = (playerAtk.target - new Vector2 (transform.position.x, transform.position.y)).normalized;
    }
    if(cooldownSpecialAttack < 0)
    {
        direction = (playerAtk.target - new Vector2 (transform.position.x, transform.position.y)).normalized;
    }*/
    direction = (playerAtk.target - new Vector2 (transform.position.x, transform.position.y)).normalized;
       
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        audio.clip = sonsHits[Random.Range(0, sonsHits.Length)];
        audio.Play(); 
        Debug.Log("colidiu");
        playerAtk.canArmaReturn = false;
        // Calcula a força de recuo
        if(playerAtk.cooldownSpecialAttack <= playerAtk.timeSpecialAttack && playerAtk.cooldownSpecialAttack >= 0)
        {
            playerAtk.cooldownSpecialAttack = 0;
            Vector2 recoilForce = direction * thrust * -1;
            rb2d.AddForce(recoilForce);
            StartCoroutine(DelayToArmaBack());
        }

        // Adiciona a força de recuo ao objeto
       
    } 

    public IEnumerator DelayToArmaBack()
    {
        yield return new WaitForSeconds(0.2f);
        rb2d.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.15f);
        playerAtk.canArmaReturn = true;

    }
}
