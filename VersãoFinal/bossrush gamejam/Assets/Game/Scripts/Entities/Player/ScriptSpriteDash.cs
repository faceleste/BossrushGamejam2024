using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpriteDash : MonoBehaviour
{
    public Sprite mainSprite;
    public SpriteRenderer sr;
    //public Animator anim;
    public float colorNum = 1.00f;
    public Color originalColor;
    public Color newColor;
    public bool canSumir = false;
    public float speedSumir = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
       
        originalColor = sr.color;
        StartCoroutine(DestroyObj());
        //anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = mainSprite;
        if(canSumir)
        {
            originalColor = Color.Lerp(originalColor, newColor, speedSumir);
            
        }
        sr.color = originalColor;
    }   

    public IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(0.7f);
        //anim.enabled = true;
        //anim.SetTrigger("sumindo");
        canSumir = true;
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }
}
