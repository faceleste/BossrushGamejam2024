using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class CardManager : MonoBehaviour
{




    public GameObject cartaPrefab;
    public GameObject confirmationDialogPrefab;
    public Transform contentPanel;

    public SkillManager skillManager;
    public string type;
    public GameController gameController;
    public List<Card> cards;

    public int cartasAtivas = 0;
    public CardBuilder builder;

    public AudioSource audioSource;
    public List<AudioClip> listaAudio = new List<AudioClip>();


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if (gameController.timeSettings.currentTime / 60 > 70)
        {
            DesativarCartas(cards);
        }
    }


    public void DesativarCartas(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (gameController.timeSettings.fullTime - (cards[i].timeRequired + gameController.timeSettings.currentTime / 60) < 0)
            {
                for (int j = 0; j < contentPanel.childCount; j++)
                {
                    if (contentPanel.GetChild(j).name == cards[i].title)
                    {
                        contentPanel.GetChild(j).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                        contentPanel.GetChild(j).transform.Find("Background").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                        contentPanel.GetChild(j).transform.Find("Button").gameObject.SetActive(false);
                    }
                }
            }
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
                for (int i = 0; i < gameController.playerSettings.inventory.Count; i++)
                {
                    if (card.id == gameController.playerSettings.inventory[i].id)
                    {
                        card.isCardUsed = true;

                    }
                }

                if (!card.isCardUsed)
                {

                    CriarCarta(card);
                }

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


        GameObject cartaObj = Instantiate(cartaPrefab, contentPanel);
        cartaObj.name = card.title;

        TextMeshProUGUI cost = cartaObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>();

        Image background = cartaObj.transform.Find("Background").GetComponent<Image>();

        Button button = cartaObj.transform.Find("Button").GetComponent<Button>();

        Image image = cartaObj.GetComponent<Image>();


        cost.text = "- " + card.timeRequired.ToString() + "min";
        background.sprite = card.art;


        button.onClick.AddListener(() =>
        {

            ShowConfirmationDialog(card, cartaObj);
        });



        RectTransform cartaRectTransform = cartaObj.GetComponent<RectTransform>();
        RectTransform previousCardRectTransform = contentPanel.childCount > 1 ? contentPanel.GetChild(contentPanel.childCount - 2).GetComponent<RectTransform>() : null;
        if (gameController.playerSettings.inventory.Count <= 2 && (card.id != 1 && card.id != 5))
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



    void PlaySound(Card card)
    {
        if (card.type == "destreza")
        {
            audioSource.clip = listaAudio[0];

        }
        else if (card.type == "maestria")
        {
            audioSource.clip = listaAudio[1];
        }
        else if (card.type == "vigor")
        {
            audioSource.clip = listaAudio[1]; //colocar aqui o 2, nao consegui importar corretamente. veja o isaac-apenas as 04:02 
        }
        audioSource.Play();
    }
    void ShowConfirmationDialog(Card card, GameObject cartaObj)
    {


        if (!gameController.optionSettings.canViewConfirmation)
        {
            PlaySound(card);
            skillManager.ActivateSkill(card);
            gameController.playerSettings.AddToInventory(card);
            card.isCardUsed = true;
            card.isAtivado = true;
            cartasAtivas++;
            Destroy(cartaObj);

        }
        else
        {



            string dialogTitle = card.title;
            string dialogDescription = card.description;


            GameObject confirmationDialog = Instantiate(confirmationDialogPrefab, contentPanel.transform.parent);
            TextMeshProUGUI titleText = confirmationDialog.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = confirmationDialog.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI costText = confirmationDialog.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
            Button confirmButton = confirmationDialog.transform.Find("ConfirmButton").GetComponent<Button>();
            Button cancelButton = confirmationDialog.transform.Find("CancelButton").GetComponent<Button>();
            Image cardSprite = confirmationDialog.transform.Find("CardSprite").GetComponent<Image>();


            titleText.text = dialogTitle;
            descriptionText.text = dialogDescription;
            costText.text = "Cost: " + card.timeRequired.ToString();
            cardSprite.sprite = card.art;

            confirmButton.onClick.AddListener(() =>
            {

                Destroy(confirmationDialog);

                PlaySound(card);
                skillManager.ActivateSkill(card);
                gameController.playerSettings.AddToInventory(card);
                card.isCardUsed = true;
                card.isAtivado = true;
                cartasAtivas++;
                Destroy(cartaObj);
            });


            cancelButton.onClick.AddListener(() =>
            {

                Destroy(confirmationDialog);

            });
        }
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