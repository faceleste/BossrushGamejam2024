using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Blink()
    {
        while (true)
        {
            text.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.5f);
            text.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
