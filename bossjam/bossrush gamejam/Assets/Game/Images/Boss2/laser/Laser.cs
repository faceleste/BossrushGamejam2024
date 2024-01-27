using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool canFollowBossScript = true;
    public GameObject laserPequeno;
    public GameObject laserMedio;
    public GameObject laserGrande;
    public Boss2Script bossScript;
    public bool canGirar;
    public float currentAng;
    public float angle;
    public float rotationSpeed = 20f;
    public float timeDestroy = 10f;
    public float originalTime;
    public bool canSumir = true;

    // Start is called before the first frame update
    void Start()
    {
        laserGrande.SetActive(false);
        laserMedio.SetActive(false);
        laserPequeno.SetActive(false);
        StartCoroutine(DestroyOBJ());
        originalTime = timeDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        timeDestroy -= Time.deltaTime;
        if(canSumir)
        {
            if(timeDestroy >= (originalTime / 1.25f))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(false);
                laserPequeno.SetActive(true);
            }
            if(timeDestroy <= (originalTime / 1.25f) && timeDestroy >= (originalTime / 1.5f))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(true);
                laserPequeno.SetActive(false);
            }
            if(timeDestroy < (originalTime / 1.5f) && timeDestroy > (originalTime/4))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(true);
                laserMedio.SetActive(false);
                laserPequeno.SetActive(false);
            }
            
            if(timeDestroy <= (originalTime / 4) && timeDestroy > (originalTime/5))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(true);
                laserPequeno.SetActive(false);
            }
            if(timeDestroy <= (originalTime/5))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(false);
                laserPequeno.SetActive(true);      
            }

        }
        else
        {
            if(timeDestroy >= (originalTime - 0.25f))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(false);
                laserPequeno.SetActive(true);
            }
            if(timeDestroy <= (originalTime - 0.25f) && timeDestroy >= (originalTime - 0.5f))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(false);
                laserMedio.SetActive(true);
                laserPequeno.SetActive(false);
            }
            if(timeDestroy < (originalTime - 0.5f) && timeDestroy > (originalTime / 4))
            {
                canFollowBossScript = false;
                laserGrande.SetActive(true);
                laserMedio.SetActive(false);
                laserPequeno.SetActive(false);
            }
        }
        if(canFollowBossScript == true)
        {
            /*if(bossScript != null)
            {
                if(bossScript.typeLaser == 1)
                {
                    laserGrande.SetActive(false);
                    laserMedio.SetActive(false);
                    laserPequeno.SetActive(true);
                }
                if(bossScript.typeLaser == 2)
                {
                    laserGrande.SetActive(false);
                    laserMedio.SetActive(true);
                    laserPequeno.SetActive(false);
                }
                if(bossScript.typeLaser == 3)
                {
                    laserGrande.SetActive(true);
                    laserMedio.SetActive(false);
                    laserPequeno.SetActive(false);
                }
            }*/
        }
        
    }

    void FixedUpdate()
    {
        if(canGirar)
        {
            Giro(angle);
        }
    }

    public void Giro(float angleA)
    {
        currentAng += rotationSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0, 0, angleA + currentAng);
    }

    IEnumerator DestroyOBJ()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
