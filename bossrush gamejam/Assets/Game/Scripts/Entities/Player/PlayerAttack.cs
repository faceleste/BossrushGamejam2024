using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackDamage;
    public float attackRange;
    public float attackRate;
    public float timeToAttack;
    public bool canBasicAttack;

    [Header("Special Attack")]
    public float timeSpecialAttack;
    public float rangeSpecialAttack;
    public bool isInRange;
    public LayerMask attackLayer;
    public bool isInSpecialAtk;
    public Transform aceleracaoPoint;
    public Vector2 target;
    public float cooldownSpecialAttack;
    public bool isAttackNoRange;

    [Header("Objects")]
    public GameObject attackPointBasicAttack;
    public GameObject attackPointSpecialAttack;
    public LineRenderer line;

    [Header("cooldownTwoAttacks")]
    public float cooldownAtks = 1;
    public float currentCooldownAtk = 1;
    public float specialAttackSpeed;

    private float currentAttack;

    void Start()
    {
        cooldownSpecialAttack = timeSpecialAttack;
    }

    void FixedUpdate()
    {
        CreateLineBetweenPlayerAndSpecial();
        AttackSpecial();

        if(Input.GetButton("Fire1"))
        {
            currentCooldownAtk -= Time.deltaTime;
            
            if(currentCooldownAtk <= 0.8 && isInSpecialAtk == false)
            {
                // ATAQUE ESPECIAL
                cooldownSpecialAttack = timeSpecialAttack;
                isAttackNoRange = false;
                isInSpecialAtk = true;
                canBasicAttack = false;   
                attackPointSpecialAttack.SetActive(true);
                RangeManagement();
            }
        }
        else
        {   
           
            if(attackPointSpecialAttack.transform.position == this.transform.position)
            {
                cooldownSpecialAttack = timeSpecialAttack;
                currentAttack = timeToAttack;
                attackPointSpecialAttack.SetActive(false);
                canBasicAttack = true; 
                isInSpecialAtk = false;
            } 
            else
            {
            }

            isInSpecialAtk = false;
            if(currentCooldownAtk < cooldownAtks && canBasicAttack == true) 
            {
                // ATAQUE NORMAL
                currentAttack -= Time.deltaTime;
                Attack();
                currentCooldownAtk = cooldownAtks;
            }
            else
            {
                currentCooldownAtk = cooldownAtks;
            }
        }
    }



    private void CalculeRotation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;    
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        attackPointSpecialAttack.transform.rotation = rotation;
    }

    private void AttackSpecial()
    {
        if(isInSpecialAtk)
        {
            
            cooldownSpecialAttack -= Time.deltaTime;
            isInRange = Physics2D.OverlapCircle(this.transform.position, rangeSpecialAttack, attackLayer);
            attackPointSpecialAttack.SetActive(true);
            float tempoDecorrido = 0f;      
            CalculeRotation();

            if(isInRange && isAttackNoRange == false)
            {
                
                if(cooldownSpecialAttack <= timeSpecialAttack && cooldownSpecialAttack >= 0)
                {
                    target = new Vector2(aceleracaoPoint.transform.position.x, aceleracaoPoint.transform.position.y);
                    attackPointSpecialAttack.transform.position = Vector3.Lerp(attackPointSpecialAttack.transform.position, target, specialAttackSpeed );
                }
                if(cooldownSpecialAttack < 0)
                {
                    attackPointSpecialAttack.transform.position = Vector3.Lerp(attackPointSpecialAttack.transform.position, this.transform.position, specialAttackSpeed/8);
                    if(attackPointSpecialAttack.transform.position == this.transform.position)
                    {
                        attackPointSpecialAttack.SetActive(false);
                        isInSpecialAtk = false;

                    }   
                }
            }
            else
            {   
                isAttackNoRange = true;
                if(cooldownSpecialAttack < -0.2f)
                {
                    isInRange = Physics2D.OverlapCircle(this.transform.position, rangeSpecialAttack, attackLayer); 
                    attackPointSpecialAttack.transform.position = Vector3.MoveTowards(attackPointSpecialAttack.transform.position, this.transform.position, specialAttackSpeed / 1.2f);
                    if(attackPointSpecialAttack.transform.position == this.transform.position)
                    {
                        
                        cooldownSpecialAttack = timeSpecialAttack;
                        attackPointSpecialAttack.SetActive(false);
                        canBasicAttack = true; 
                        isInSpecialAtk = false;
                    }   

                }
                
            }
        }
        else
        {
           attackPointSpecialAttack.transform.position = Vector3.MoveTowards(attackPointSpecialAttack.transform.position, this.transform.position, specialAttackSpeed  / 1.2f);
        }
        //currentCooldownAtk = cooldownAtks;
    }


    private void RangeManagement()
    {
        isInRange = Physics2D.OverlapCircle(this.transform.position, rangeSpecialAttack, attackLayer);
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        Gizmos.DrawSphere(transform.position, rangeSpecialAttack);
    }

    void Attack()
    {
        //BLOCO FUTURO DE ATAQUE PARA OS INIMIGOS QUANDO IMPLEMENTADOS 
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointBasicAttack.position, attackRange);
        // foreach (Collider2D enemy in hitEnemies)
        // {
        //     Debug.Log("Acertou: " + enemy.name);
        //     enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        // }

        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.1f);
        attackPointBasicAttack.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackPointBasicAttack.SetActive(false);
    }

    private void CreateLineBetweenPlayerAndSpecial()
    {

        line.SetPosition(0, transform.position);
        line.SetPosition(1, attackPointSpecialAttack.transform.position);

    }

    private IEnumerator DelaySpecialAttack()
    {
        /*isInSpecialAtk = true;
        attackPointSpecialAttack.SetActive(true);

        float tempoDecorrido = 0f;

        if (isInRange)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


            attackPointSpecialAttack.transform.rotation = rotation;

            while (tempoDecorrido < timeSpecialAttack)
            {
                attackPointSpecialAttack.transform.position = Vector2.Lerp(transform.position, mousePosition, tempoDecorrido / timeSpecialAttack);
                tempoDecorrido += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(timeSpecialAttack);

            //Inserir retorno da arma ao player. 

            while (tempoDecorrido < timeSpecialAttack)
            {
                attackPointSpecialAttack.transform.position = Vector2.Lerp(transform.position, mousePosition, tempoDecorrido / timeSpecialAttack);
                tempoDecorrido += Time.deltaTime;
                yield return null;
            }
            currentAttack = attackRate;

        }
        // else
        // {
        //     tempoDecorrido = 0f;
        //     //voltar para a posição do player. 
        //     while (tempoDecorrido < timeSpecialAttack)
        //     {
        //         attackPointSpecialAttack.transform.position = Vector2.Lerp(transform.position, attackPointSpecialAttack.transform.position, 2);
        //         tempoDecorrido += Time.deltaTime;
        //         yield return null;
        //     }

        // } */
        yield return new WaitForSeconds(0.1f);
    }


}