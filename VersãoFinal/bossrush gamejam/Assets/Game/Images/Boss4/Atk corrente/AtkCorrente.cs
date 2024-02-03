using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCorrente : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 lastPlayerPosition;
    public float speed;
    public float anglePlayer = 0;
    public bool canFollow = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStop());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canFollow)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector3 direction = (playerPosition.transform.position - this.transform.position).normalized;
            Vector3 offset = direction * -1; // Isso inverte a direção
            lastPlayerPosition = playerPosition.transform.position - offset * 40;
        }

        transform.position = Vector3.MoveTowards(this.transform.position, lastPlayerPosition, speed);

        Vector3 directionAngle = (playerPosition.transform.position - transform.position).normalized;

        // Calcule o ângulo para o jogador
        float angle = Mathf.Atan2(directionAngle.y, directionAngle.x) * Mathf.Rad2Deg;

        // Crie uma nova instância do laser na posição do objeto atual e com a rotação para o jogador
        this.transform.rotation = Quaternion.Euler(0, 0, angle + anglePlayer);
    }
    IEnumerator DelayStop()
    {
        yield return new WaitForSeconds(1.3f);
        canFollow = false;
        yield return new WaitForSeconds(10.5f);
        Destroy(gameObject);
    }
}
