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
    public Vector3 originalMinValues, originalMaxValues;
    public Transform truePlayer;
    public Vector3 currentMinValue, currentMaxValues;
    
    public bool canLimitCam = true;
    public void resetValores(Transform t)
    {
        
        minValues = new Vector3(-100, -100, -10);
        maxValues = new Vector3(100, 100, -10);
        canLimitCam = false;
        player = t;
    }

    public void backValores()
    {
        canLimitCam = true;
        player = newPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //anim =  GameObject.FindGameObjectWithTag("MainCamera").Animator;
        truePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 mousePos = Input.mousePosition;
        newPlayer.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        player.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void Update()
    {
        if(canLimitCam == true)
        {

            if(this.transform.position.x > originalMinValues.x &&this.transform.position.y > originalMinValues.y  &&  this.transform.position.x < originalMaxValues.x &&  this.transform.position.y < originalMaxValues.y)
            {
                minValues = new Vector3(truePlayer.transform.position.x -1, truePlayer.transform.position.y -1f, -10);
                maxValues =new Vector3(truePlayer.transform.position.x +1, truePlayer.transform.position.y +1f, -10);
            }
            else
            {
                minValues = originalMinValues;
                maxValues = originalMaxValues;
            }

            Vector3 mousePos = Input.mousePosition;
            newPlayer.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            player.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
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
