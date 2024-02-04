using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class CardUI : MonoBehaviour, IPointerClickHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private Coroutine clickCoroutine;
    private bool isClicked = false;
    private CardManager parent = new CardManager();
    private AudioSource audioSource;

    public List<AudioClip> audioClips = new List<AudioClip>();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    // Implementação do evento de clique
    public void OnPointerClick(PointerEventData eventData)
    {

        if (clickCoroutine != null)
            StopCoroutine(clickCoroutine);

        if (isClicked)
        {
            clickCoroutine = StartCoroutine(ClickEffectCoroutine(originalScale, originalPosition, 0.3f));
            isClicked = false;
        }
        else
        {
            clickCoroutine = StartCoroutine(ClickEffectCoroutine(originalScale * 1.1f, originalPosition - new Vector3(300f, 0f, 0f), 0.3f));
            isClicked = true;
        }

    }

    private IEnumerator ClickEffectCoroutine(Vector3 targetScale, Vector3 targetPosition, float duration)
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        transform.localScale = targetScale;
        transform.localPosition = targetPosition;
    }
}
