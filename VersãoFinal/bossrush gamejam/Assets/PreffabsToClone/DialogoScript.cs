using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogoScript : MonoBehaviour
{
    public string[] frases;
    public int currentFrase = 0;
    public TextMeshProUGUI textMesh;

    private bool isTyping = false;
    public float delayLetter = 0.01f;
    public float delayText = 2f;
    private AudioSource audioSource;
    public List<AudioClip> sonsFala = new List<AudioClip>();
    public bool canWrite = true;
    public bool isWinScene = false;
    [Header("PREENCHA SOMENTE SE IS WIN SCENE FOR TRUE")]
    public Image background;
    public Sprite lastSprite;
    public AudioClip risadaFinal;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TypeFrase(frases[currentFrase]));
    }

    // Update is called once per frame
    void Update()
    {
        if (canWrite)
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
                    StopAllCoroutines();
                    textMesh.text = frases[currentFrase];
                    isTyping = false;
                    StartCoroutine(NextFrase());
                }
            }
        }

    }


    IEnumerator TypeFrase(string frase)
    {

        isTyping = true;
        textMesh.text = "";
        foreach (char letter in frase.ToCharArray())
        {
            audioSource.clip = sonsFala[UnityEngine.Random.Range(0, sonsFala.Count)];
            audioSource.Play();
            yield return new WaitForSeconds(delayLetter);

            textMesh.text += letter;
            yield return new WaitForSeconds(delayLetter);
            yield return null;
        }
        isTyping = false;
        yield return new WaitForSeconds(delayText);
        //StartCoroutine(NextFrase());

    }

    IEnumerator NextFrase()
    {
        yield return new WaitForSeconds(0.05f);
        currentFrase++;
        float timeToFinalize=2f;
        if (currentFrase >= frases.Length)
        {
            if (isWinScene)
            {



                audioSource.clip = risadaFinal;
                audioSource.pitch = 0.8f;
                audioSource.Play();
                timeToFinalize=5f;
            }

            canWrite = false;
            currentFrase = 0;
            this.GetComponent<Animator>().SetTrigger("Sumindo");

            yield return new WaitForSeconds(timeToFinalize);

            gameObject.SetActive(false);
        }
        else
        {
            if (isWinScene && currentFrase == frases.Length - 1)
            {

                textMesh.color = new Color(1, 0, 0, 0);




            }
            if (currentFrase == frases.Length - 2)
            {
                background.sprite = lastSprite;

            }

            StartCoroutine(TypeFrase(frases[currentFrase]));
        }

    }
}
