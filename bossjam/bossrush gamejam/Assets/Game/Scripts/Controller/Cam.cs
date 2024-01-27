using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public Transform newPlayer;
    //public playerMove playerScript;
    //public Transform target; 
    public Vector3 offset;
    [Range(0, 10)]
    public Vector3 positionAl;
    public float SmoothFactor;
    public Vector3 minValues, maxValues; 
    private Vector3 mouse; 
    

    // Start is called before the first frame update
    void Start()
    {

        //anim =  GameObject.FindGameObjectWithTag("MainCamera").Animator;
        newPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        positionAl = player.transform.position;    
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = positionAl + offset;


        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        Vector3 SmoothPosition = Vector3.Lerp(transform.position, boundPosition, SmoothFactor*Time.fixedDeltaTime);
        transform.position = SmoothPosition;
        
    }
    public void Shake()
    {
        anim.SetTrigger("isShaking");
    }
}
