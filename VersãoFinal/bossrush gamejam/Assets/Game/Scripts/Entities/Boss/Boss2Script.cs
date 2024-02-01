using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Script : MonoBehaviour
{
    public float TimeDestroyAtkLaser = 6f;
    public float TimeDestroyAtkLaserGira = 4f;
    public float TimeDestroyAtkLaserPlayer = 1f;
    public bool canAtkAgainLaser = true;
    public bool canAtkAgainLaserGira = true;
    public bool canAtkAgainLaserPlayer = true;

    public Transform playerPosition;
    public GameObject laserPrefab; // Arraste o prefab do seu laser aqui
    public float laserSpeed = 20f; // Velocidade do laser
    
    public float ang01, ang02, ang03;
    public float currentAng1, currentAng02, currentAng03;
    //public float rotationSpeed = 20f;
    public bool isLaserActivated = false;

    public bool atk01;
    public bool atk02;
    public bool atk03;

    public float speed = 1.0f; // velocidade de movimento
    public float width = 1.0f; // largura do movimento
    public float height = 1.0f; // altura do movimento
    private float time;
  
    public float cooldownChangeAtks = 8f;
    public float cooldownLaserDesativado = 1.5f;
    public int numPiscar;
    public float angle;

    public float timeLaserPlayer = 1;
    public float cooldownLaserPlayer;

    public int typeLaser = 0;
    public float cooldownChangeTypeLaser;
    public float timeChangeTypeLaser = 1;

    public Vector3 lastPlayerPosition;
    public float speedMove = 3f;
    public int numLasersPlayer;

    public Transform armaMaca;
    public Animator animCam;
    public SpriteRenderer sr;
    public GameObject[] sangue;
    public GameObject marcaSangue;
    public bool isFliped;
    public Color defaultColor;
    public float delayChangeMomentAtk = 12f;

    public GameObject olhoLeft;
    public Transform positionOlhoLeft;
    public Transform positionOlhoLeft2;
    public GameObject olhoRight;
    public Transform positionOlhoRight;
    public Transform positionOlhoRight2;
    public bool isOlhoLeftSpawned = false;
    public bool isOlhoRightSpawned = false;

    IEnumerator Rotine()
    {
        

        yield return new WaitForSeconds(6f);
        atk01 = true;
        yield return new WaitForSeconds(delayChangeMomentAtk);
        atk01 = false;
        
        if(isOlhoLeftSpawned == false)
        {
            GameObject olholeft = Instantiate(olhoLeft, positionOlhoLeft.transform.position, Quaternion.Euler(0, 0, 15));
            GameObject olholeft2 = Instantiate(olhoLeft, positionOlhoLeft2.transform.position, Quaternion.Euler(0, 0, 15));
            isOlhoLeftSpawned = true;
            olholeft.GetComponent<EyeScript>().left = true;
            olholeft2.GetComponent<EyeScript>().left = true;
            olholeft.GetComponent<EyeScript>().atk01 = true;
            olholeft2.GetComponent<EyeScript>().atk03 = true;
        }
        if(isOlhoRightSpawned == false)
        {
            GameObject olhoright = Instantiate(olhoRight, positionOlhoRight.transform.position, Quaternion.Euler(0, 0, -15));
            GameObject olhoright2 = Instantiate(olhoRight, positionOlhoRight2.transform.position, Quaternion.Euler(0, 0, -15));
            isOlhoRightSpawned = true;
            olhoright.GetComponent<EyeScript>().right = true;
            olhoright2.GetComponent<EyeScript>().right = true;
            olhoright.GetComponent<EyeScript>().atk01 = true;
            olhoright2.GetComponent<EyeScript>().atk03 = true;
        }
        
        atk02 = true;
        yield return new WaitForSeconds(delayChangeMomentAtk);
        atk02 = false;
        yield return new WaitForSeconds(3f);

        atk03 = true;
        yield return new WaitForSeconds(delayChangeMomentAtk);
        atk03 = false;
        StartCoroutine(Rotine());
    }

    IEnumerator RotineAtkNormal()
    {
        canAtkAgainLaser = false;
        //isLaserActivated = true;
        yield return new WaitForSeconds(TimeDestroyAtkLaser);
        //isLaserActivated = false;
        yield return new WaitForSeconds(cooldownLaserDesativado);
        canAtkAgainLaser = true;
        yield return new WaitForSeconds(1);
        //StartCoroutine(RotineAtkNormal());

    }

    IEnumerator RotineAtkGira()
    {
        canAtkAgainLaserGira = false;
        //isLaserActivated = true;
        yield return new WaitForSeconds(TimeDestroyAtkLaserGira);
        //isLaserActivated = false;
        yield return new WaitForSeconds(cooldownLaserDesativado);
        canAtkAgainLaserGira = true;
        //StartCoroutine(RotineAtkGira());

    }

    IEnumerator RotineAtkPlayer()
    {
        canAtkAgainLaserPlayer = false;
        Vector3 direction = (playerPosition.transform.position - this.transform.position).normalized;
        Vector3 offset = direction * -1; // Isso inverte a direção
        lastPlayerPosition = playerPosition.transform.position - offset * 8;

        //isLaserActivated = true;
        yield return new WaitForSeconds(TimeDestroyAtkLaserPlayer);
        if(atk03)
        {
            for(int i = 0; i < numLasersPlayer; i++)
            {
                FireLaserPlayer();
                yield return new WaitForSeconds(0.3f);
            }
            
            //isLaserActivated = false;
            yield return new WaitForSeconds(cooldownLaserDesativado);
            canAtkAgainLaserPlayer = true;
        }
        
        //StartCoroutine(RotineAtkPlayer());

    }

    void FireLaser(float angleA)
    {
        // Crie uma nova instância do laser na posição e rotação do objeto atual
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, angleA));
        laser.GetComponent<Laser>().bossScript = this;
        laser.GetComponent<Laser>().timeDestroy = TimeDestroyAtkLaser;
        laser.transform.parent = gameObject.transform;
        // Calcule a direção do laser com base no ângulo desejado
        Vector3 direction = Quaternion.Euler(0, 0, angleA) * transform.up;
        // Adicione uma força ao laser na direção calculada
        laser.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);
    }

    void FireLaserPlayer()
    {
        // Calcule a direção do laser com base na posição do jogador
        Vector3 direction = (playerPosition.transform.position - transform.position).normalized;

        // Calcule o ângulo para o jogador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Crie uma nova instância do laser na posição do objeto atual e com a rotação para o jogador
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject laser = Instantiate(laserPrefab, transform.position, rotation);
        laser.transform.parent = gameObject.transform;

        laser.GetComponent<Laser>().bossScript = this;
        laser.GetComponent<Laser>().timeDestroy = TimeDestroyAtkLaserPlayer;
        // Adicione uma força ao laser na direção calculada
        laser.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);
    }

    void FireLaserGiratorio(float angleA)
    {
        // Crie uma nova instância do laser na posição e rotação do objeto atual
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, angleA));
        //laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, angleA));
        laser.GetComponent<Laser>().bossScript = this;
        laser.GetComponent<Laser>().canGirar = true;
        laser.GetComponent<Laser>().angle = angleA;
        laser.GetComponent<Laser>().timeDestroy = TimeDestroyAtkLaserGira;
        laser.transform.parent = gameObject.transform;

        // Calcule a direção do laser com base no ângulo desejado
        Vector3 direction = Quaternion.Euler(0, 0, angleA) * transform.up;
        // Adicione uma força ao laser na direção calculada
        laser.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);
    }
    

    void Start()
    {
        defaultColor = sr.color;
        cooldownLaserPlayer = timeLaserPlayer;
        StartCoroutine(Rotine());
    }

    void FixedUpdate()
    {
        
        cooldownChangeTypeLaser -= Time.deltaTime;
        if(cooldownChangeTypeLaser >= timeChangeTypeLaser/2)
        {
            typeLaser = 1;
        }
        else if(cooldownChangeTypeLaser < timeChangeTypeLaser/2 && cooldownChangeTypeLaser > 0)
        {
            typeLaser = 2;
        }
        if(cooldownChangeTypeLaser <= 0)
        {
            typeLaser = 3;
        }

        if (isLaserActivated)
        {
            if(atk01)
            {   
                Move();
                if(canAtkAgainLaser)
                {
                    typeLaser = 1;
                    cooldownChangeTypeLaser = timeChangeTypeLaser;
                    FireLaser(ang01);
                    FireLaser(ang02);
                    FireLaser(ang03);
                    FireLaser(ang01 + (ang02/2));
                    FireLaser(ang02 + (ang02/2));
                    FireLaser(ang03 + (ang02/2));
                    StartCoroutine(RotineAtkNormal());
                }

            } 
            else
            {
                time = 0;
            } 
            if(atk02)
            {
                if(canAtkAgainLaserGira)
                {
                    typeLaser = 1;
                    cooldownChangeTypeLaser = timeChangeTypeLaser;
                    FireLaserGiratorio(ang01);
                    FireLaserGiratorio(ang02);
                    FireLaserGiratorio(ang03);
                    StartCoroutine(RotineAtkGira());
                }

            }

            if(atk03)
            {
                typeLaser = 1;
                cooldownChangeTypeLaser = timeChangeTypeLaser;
                
                if(canAtkAgainLaserPlayer)
                {
                    
                    
                    if(cooldownLaserPlayer >= 0)
                    {
                        StartCoroutine(RotineAtkPlayer());
                        cooldownLaserPlayer -= Time.deltaTime;
                        
                    }
                    else
                    {
                        atk03 = false;
                        cooldownLaserPlayer = timeLaserPlayer;
                        atk03 = true;
                    }
                    //FireLaserPlayer();
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, lastPlayerPosition, speedMove/3);
                }

                
            }
        }


    }
    void Update()
    {
        
        if(playerPosition.transform.position.x < this.transform.position.x)
        {
            sr.flipX = true;
            //Quaternion target = Quaternion.Euler(0, -180, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = true;
        }
        if(playerPosition.transform.position.x > this.transform.position.x)
        {
            sr.flipX = false;
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            isFliped = false;
        }

        /*if (isLaserActivated)
        {
            if(atk01)
            {
                lr1.enabled = true;
                lr2.enabled = true;
                lr3.enabled = true;
                lr4.enabled = true;
                lr5.enabled = true;
                lr6.enabled = true;
                ShootLaser(lr1, ang01);
                ShootLaser(lr4, ang01 + (ang02/2));
                ShootLaser(lr2, ang02);
                ShootLaser(lr5, ang02 + (ang02/2));
                ShootLaser(lr3, ang03);
                ShootLaser(lr6, ang03 + (ang02/2));

               
            }
            else
            {
                
            lr6.enabled = false;
            lr5.enabled = false;
            lr4.enabled = false;
            }
            if(atk02)
            {
                currentAng1 += rotationSpeed * Time.deltaTime;
                currentAng02 += rotationSpeed * Time.deltaTime;
                currentAng03 += rotationSpeed * Time.deltaTime;
                lr1.enabled = true;
                lr2.enabled = true;
                lr3.enabled = true;
                ShootLaser(lr1, ang01 + currentAng1);
                ShootLaser(lr2, ang02 + currentAng02);
                ShootLaser(lr3, ang03 + currentAng03);
                
                
            }
        }
        else
        {
            lr6.enabled = false;
            lr5.enabled = false;
            lr4.enabled = false;
            lr1.enabled = false;
            lr2.enabled = false;
            lr3.enabled = false;
        }
        */
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
            this.transform.position = this.transform.position + direcao * distancia;
        } 
    } 

    IEnumerator SwitchColor()
    {
        
        int rand = Random.Range(0, sangue.Length);
        //sangue[rand].SetActive(true);
        if(isFliped == true)
        {   
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y+0.6f), Quaternion.Euler(0, 180, 0)).transform.parent = gameObject.transform;
        }
        else
        {
            Instantiate(sangue[rand], new Vector2(this.transform.position.x, this.transform.position.y+0.6f), Quaternion.Euler(0, 0, 0)).transform.parent = gameObject.transform;
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
        Instantiate(marcaSangue, new Vector2(this.transform.position.x, this.transform.position.y+0.3f), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(1f);
        //Destroy(t);
        //sangue[rand].SetActive(false);
    }


    public void Move()
    {
            time += Time.deltaTime * speed;

        float x = width * Mathf.Cos(time) + transform.position.x;
        float y = height * Mathf.Sin(time) + transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void ShootLaser(LineRenderer lr, float angle)
    {
        lr.SetPosition(0, transform.position);
        Vector3 direction = Quaternion.Euler(0, 0, angle) * transform.up;
        lr.SetPosition(1, transform.position + direction * 1000);
    }
}
