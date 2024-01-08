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

    [Header("Objects")]
    public GameObject attackPointBasicAttack;
    public GameObject attackPointSpecialAttack;
    public LineRenderer line;

    private float currentAttack;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && canBasicAttack)
        {
            currentAttack -= Time.deltaTime;
            Attack();
        }
        if (Input.GetButton("Fire2"))
        {
            canBasicAttack = false;
            CreateLineBetweenPlayerAndSpecial();
            RangeManagement();
            StartCoroutine(DelaySpecialAttack());
        }
        else
        {
            canBasicAttack = true;
            ReturnSpecialAttack();
        }

    }

    private void ReturnSpecialAttack()
    {
        CreateLineBetweenPlayerAndSpecial();
        attackPointSpecialAttack.transform.position = Vector2.Lerp(attackPointSpecialAttack.transform.position, transform.position, 0.03f);
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
        yield return new WaitForSeconds(0.4f);
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

        // }
    }


}