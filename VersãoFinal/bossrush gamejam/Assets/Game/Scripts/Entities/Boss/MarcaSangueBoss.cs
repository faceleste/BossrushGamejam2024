using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcaSangueBoss : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer sr;
    public float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, sprites.Length);
        sr.sprite = sprites[r];

        int v = Random.Range(0, 360);
        Quaternion target = Quaternion.Euler(0, 0, v);
        transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
        /*if(v == 1)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
        }
        else if (v == 2)
        {
            Quaternion target = Quaternion.Euler(0, 0, 45);
            transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            transform.localScale = new Vector3(5, 5, 0);
        }
        else if (v == 3)
        {
            Quaternion target = Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            transform.localScale = new Vector3(6, 6, 0);
        }
        else if (v == 4)
        {
            Quaternion target = Quaternion.Euler(0, 0, -45);
            transform.rotation = Quaternion.Slerp(target, target,  Time.deltaTime * 20f);
            transform.localScale = new Vector3(7, 7, 0);
        }*/
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
