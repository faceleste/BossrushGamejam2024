using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoScript : MonoBehaviour
{
    public string[] frases;
    public int currentFrase = 0;
    public TextMeshProUGUI textMesh;

    private bool isTyping = false;
    public float delayLetter = 0.01f;
    public float delayText = 2f;

    void Start()
    {
        StartCoroutine(TypeFrase(frases[currentFrase]));
    }

    // Update is called once per frame
    void Update()
    {
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                textMesh.text = frases[currentFrase];
                isTyping = false;
                StartCoroutine(NextFrase());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                StartCoroutine(TypeFrase(frases[currentFrase]));
            }
        }
    }

    IEnumerator TypeFrase(string frase)
    {
        isTyping = true;
        textMesh.text = "";
        foreach (char letter in frase.ToCharArray())
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(delayLetter);
            yield return null;
        }
        isTyping = false;
        yield return new WaitForSeconds(delayText);
        StartCoroutine(NextFrase());

    }

    IEnumerator NextFrase()
    {
        yield return new WaitForSeconds(0.01f);
        currentFrase++;
        if (currentFrase >= frases.Length)
        {
            currentFrase = 0;
        }
        StartCoroutine(TypeFrase(frases[currentFrase]));
    }
}
