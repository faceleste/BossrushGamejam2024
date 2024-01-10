using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool meleeMoment;
    public bool cruzMoment;
    public bool circleMoment;

    [Header("Ataque melee")]
    public float speed; 
    public float meleeDamage;
    public bool isInMeleeAtk;
    public float timeToAtkAgain = 0.5f;
    private float cooldownToAtkAgain;
    public bool isInRangeMelee;

    [Header("Ataque cruz")]
    public bool canSpawnCruz;
    public GameObject cruzPrefab;
    public float cooldownSpawnCruz;
    public int numCruzes;

    public GameObject meleeObjAtk;
    public float rangeToMeleeAtk;
    public LayerMask playerLayer;
    public Transform playerPosition;
    public PlayerAttack player;
    public SpriteRenderer sr;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        cooldownToAtkAgain = timeToAtkAgain;
        defaultColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        isInRangeMelee = Physics2D.OverlapCircle(this.transform.position, rangeToMeleeAtk, playerLayer);
    }

    void FixedUpdate()
    {
        if(meleeMoment)
        {
            
            if(isInMeleeAtk)
            {
                if(isInRangeMelee)
                {
                    StartCoroutine(MeleeAtk());
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosition.transform.position, speed/10);
                }
            }
            else
            {
                
            }

            if(cooldownToAtkAgain < timeToAtkAgain)
            {
                cooldownToAtkAgain += Time.deltaTime;
            }
        }

        if(cruzMoment)
        {

            for(int i = 0; i<= numCruzes; i++)
            {
                if(canSpawnCruz)
                {
                    StartCoroutine(CruzAtk());
                }
            }
        }
    }

    public IEnumerator CruzAtk()
    {

        canSpawnCruz = false;
        yield return new WaitForSeconds(cooldownSpawnCruz);
        Instantiate(cruzPrefab, playerPosition.position, transform.rotation);
        canSpawnCruz = true;
        

    }
    public IEnumerator MeleeAtk()
    {

        isInMeleeAtk = false;
        meleeObjAtk.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        meleeObjAtk.SetActive(false);
        yield return new WaitForSeconds(cooldownToAtkAgain);
        isInMeleeAtk = true;
    }

    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Arma")) 
        { 
            Debug.Log("Acertado");
            StartCoroutine(SwitchColor());
        } 
    } 

    IEnumerator SwitchColor()
    {
        
        for(int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.1f);
            sr.color = defaultColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

}

