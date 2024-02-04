using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public PlayerAttack pAttack;
    public Boss2Script boss;
    public Transform playerPosition;
    public GameObject laserPrefab; // Arraste o prefab do seu laser aqui
    public float laserSpeed = 20f;  
    public bool canAtkAgainLaserPlayer;
    public bool canAtkAgainLaser;
    public Vector3 lastPlayerPosition;
    public float TimeDestroyAtkLaserPlayer;
    public float TimeDestroyAtkLaserNormal;
    public int numLasersPlayer;
    public float cooldownLaserDesativado;
    public bool atk03;
    public bool atk01;
    public float angleAtk01;
    public float anglePlayer = 40f;
    public Transform spawnLaserPosition;
    public bool isFliped;
    public SpriteRenderer sr;
    public Transform armaMaca;
    public Color defaultColor;
    public GameObject[] sangue;
    public GameObject marcaSangue;
    public Animator animCam;
    public bool left;
    public bool right;
    
    public GameObject actualLaser;
    public float vida = 20f;
    //public int typeAttack;
    // Start is called before the first frame update
    void Start()
    {
        armaMaca = GameObject.FindGameObjectWithTag("Arma").GetComponent<Transform>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultColor = sr.color;
        pAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss2Script>();
        animCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    void Update()
    {
        if(playerPosition.transform.position.x < this.transform.position.x)
        {
            //sr.flipX = true;
            //Quaternion target = Quaternion.Euler(0, -180, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = true;
        }
        if(playerPosition.transform.position.x > this.transform.position.x)
        {
            //sr.flipX = false;
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(atk03)
        {
            //typeLaser = 1;
            //cooldownChangeTypeLaser = timeChangeTypeLaser;
                
            if(canAtkAgainLaserPlayer)
            {         
                StartCoroutine(RotineAtkPlayer());
            }

                
        }

        if(atk01)
        {
            if(canAtkAgainLaser)
            {
                FireLaser(angleAtk01);
            }
            
        }
    }

    void FireLaser(float angleA)
    {
        // Crie uma nova instância do laser na posição e rotação do objeto atual
        
        GameObject laser = Instantiate(laserPrefab, spawnLaserPosition.transform.position, Quaternion.Euler(0, 0, angleA));
        actualLaser = laser;
        laser.GetComponent<Laser>().timeDestroy = TimeDestroyAtkLaserNormal + 1;
        laser.GetComponent<Laser>().canSumir = false;
        laser.transform.parent = gameObject.transform;
        // Calcule a direção do laser com base no ângulo desejado
        Vector3 direction = Quaternion.Euler(0, 0, angleA) * transform.up;
        // Adicione uma força ao laser na direção calculada
        laser.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);
        StartCoroutine(DelayLaserSumir());
    }
    IEnumerator DelayLaserSumir()
    {
        canAtkAgainLaser = false;
        yield return new WaitForSeconds(TimeDestroyAtkLaserNormal);
        canAtkAgainLaser = true;
    }
    IEnumerator RotineAtkPlayer()
    {
        canAtkAgainLaserPlayer = false;
        Vector3 direction = (playerPosition.transform.position - this.transform.position).normalized;
        Vector3 offset = direction * -1; // Isso inverte a direção
        lastPlayerPosition = playerPosition.transform.position - offset * 8;

        //isLaserActivated = true;
        yield return new WaitForSeconds(TimeDestroyAtkLaserPlayer);
        for(int i = 0; i < numLasersPlayer; i++)
        {
            FireLaserPlayer();
            yield return new WaitForSeconds(0.3f);
        }
        
        //isLaserActivated = false;
        yield return new WaitForSeconds(cooldownLaserDesativado);
        canAtkAgainLaserPlayer = true;
        
        //StartCoroutine(RotineAtkPlayer());

    }

    void FireLaserPlayer()
    {
        // Calcule a direção do laser com base na posição do jogador
        Vector3 direction = (playerPosition.transform.position - transform.position).normalized;

        // Calcule o ângulo para o jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Crie uma nova instância do laser na posição do objeto atual e com a rotação para o jogador
        this.transform.rotation = Quaternion.Euler(0, 0, angle + anglePlayer);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject laser = Instantiate(laserPrefab, spawnLaserPosition.transform.position, rotation);
        laser.transform.parent = gameObject.transform;

        //laser.GetComponent<Laser>().bossScript = this;
        laser.GetComponent<Laser>().timeDestroy = TimeDestroyAtkLaserPlayer;
        // Adicione uma força ao laser na direção calculada
        laser.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Arma")) 
        { 
            animCam.SetTrigger("Shake");
            Debug.Log("Acertado");
            StartCoroutine(SwitchColor());
            Vector3 direcao = armaMaca.transform.position - this.transform.position;
            direcao = direcao.normalized * -1; // Normaliza a direção e inverte
            float distancia = 0.5f; // Define a distância que você quer mover para trás
            //this.transform.position = this.transform.position + direcao * distancia;
        } 
    } 

    IEnumerator SwitchColor()
    {
        
        int rand = Random.Range(0, sangue.Length);
        //sangue[rand].SetActive(true);
        if(isFliped == true)
        {   
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y-0.4f), Quaternion.Euler(0, 180, 0)).transform.parent = gameObject.transform;
        }
        else
        {
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y-0.4f), Quaternion.Euler(0, 0, 0)).transform.parent = gameObject.transform;
        }

        Time.timeScale = 0.02f;
        for(int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.001f);
        }
        Time.timeScale = 1;
        for(int i = 0; i < 1; i++)
        {
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            yield return new WaitForSeconds(0.2f);
            sr.color = defaultColor;
            yield return new WaitForSeconds(0.2f);
        }
        Instantiate(marcaSangue, new Vector2(this.transform.position.x, this.transform.position.y-0.2f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        vida -= pAttack.dano;
        
        if(vida <= 0)
        {
            if(left)
            {
                boss.isOlhoLeftSpawned = false;
            }
            if(right)
            {
                boss.isOlhoRightSpawned = false;
            }
            Time.timeScale = 1f;
            Destroy(actualLaser);
            Destroy(gameObject);
        }


        //Destroy(t);
        //sangue[rand].SetActive(false);
    }

}
