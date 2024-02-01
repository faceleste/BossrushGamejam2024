using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class CardManager : MonoBehaviour
{




    public GameObject cartaPrefab;
    public Transform contentPanel;

    public SkillManager skillManager;
    public string type;
    public GameController gameController;
    public List<Card> cards;

    public int cartasAtivas = 0;
    public CardBuilder builder;
    public void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        cards = ListagemCards();
        cartasAtivas = gameController.playerSettings.inventory.Count;
        CallCardsType();


    }

    public void Update()
    {

        if (cartasAtivas == 2)
        {
            AtivarBotaoTodasCartas();
        }
    }

    public void AtivarBotaoTodasCartas()
    {
        for (int i = 0; i < contentPanel.childCount; i++)
        {
            contentPanel.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            contentPanel.GetChild(i).transform.Find("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            contentPanel.GetChild(i).transform.Find("Button").gameObject.SetActive(true);
        }
    }
    public void CallCardsType()
    {
        LimparCartas();
        foreach (Card card in cards)
        {

            if (card.type == type && !card.isCardUsed)
            {

                CriarCarta(card);
                Debug.Log(card.title + " - isCardUsed" + card.isCardUsed + "- id" + card.id);
            }

        }
    }
    private void LimparCartas()
    {
        ResetarPosicoesCartas();
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }


    }

    private void ResetarPosicoesCartas()
    {



        for (int i = 0; i < contentPanel.childCount; i++)
        {
            RectTransform cartaRectTransform = contentPanel.GetChild(i).GetComponent<RectTransform>();
            Vector2 newPosition = cartaRectTransform.anchoredPosition;
            newPosition.y = 120f;
            cartaRectTransform.anchoredPosition = newPosition;
        }
    }

    private void CriarCarta(Card card)
    {

        if (VerificarInventario(card)) return;

        GameObject cartaObj = Instantiate(cartaPrefab, contentPanel);


        TextMeshProUGUI cost = cartaObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>();

        Image background = cartaObj.transform.Find("Background").GetComponent<Image>();

        Button button = cartaObj.transform.Find("Button").GetComponent<Button>();

        Image image = cartaObj.GetComponent<Image>();


        cost.text = card.timeRequired.ToString();
        background.sprite = card.art;


        button.onClick.AddListener(() =>
        {
            skillManager.ActivateSkill(card);
            gameController.playerSettings.AddToInventory(card);
            card.isCardUsed = true;
            card.isAtivado = true;
            cartasAtivas++;
            Destroy(cartaObj);


        });

        RectTransform cartaRectTransform = cartaObj.GetComponent<RectTransform>();
        RectTransform previousCardRectTransform = contentPanel.childCount > 1 ? contentPanel.GetChild(contentPanel.childCount - 2).GetComponent<RectTransform>() : null;
        if (card.id != 1 && card.id != 5)
        {
            cartaObj.transform.Find("Button").gameObject.SetActive(false);

            image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        }

        if (previousCardRectTransform != null)
        {
            float margin = 35f;

            Vector2 newPosition = cartaRectTransform.anchoredPosition;
            newPosition.y = previousCardRectTransform.anchoredPosition.y - margin;

            cartaRectTransform.anchoredPosition = newPosition;
        }
    }

    private bool VerificarInventario(Card card)
    {

        return gameController.playerSettings.inventory.Contains(card);
    }
    private List<Card> ListagemCards()
    {
        List<Card> cards = new List<Card>();
        cards.AddRange(builder.listagemDestreza(builder.destreza));
        cards.AddRange(builder.listagemMaestria(builder.maestria));
        cards.AddRange(builder.listagemVigor(builder.vigor));
        int id = 1;
        foreach (Card card in cards)
        {
            card.id = id;
            id++;
        }
        return cards;
    }
}