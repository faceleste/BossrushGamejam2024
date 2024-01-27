using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryArea : MonoBehaviour
{
    private GameController gameController;
    private List<Card> listCard;
    public GameObject cartaPrefab;
    public Transform contentPanel;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }



    void Update()
    {
        if (gameController.playerSettings.isInventoryChanged)
        {
            Card card = gameController.playerSettings.inventory[gameController.playerSettings.inventory.Count - 1];
            if (card.type == type)
            {
                CriarCarta(card);
                gameController.playerSettings.isInventoryChanged = false;
            }
        }
    }

    void UpdateInventory()
    {
        if (gameController.playerSettings.inventory.Count > 0)
        {
            listCard = gameController.playerSettings.inventory;
            foreach (Card card in listCard)
            {
                if (card.type == type)
                {
                    CriarCarta(card);
                }
            }
        }
    }

    private void CriarCarta(Card card)
    {
        GameObject cartaObj = Instantiate(cartaPrefab, contentPanel);

        TextMeshProUGUI titleText = cartaObj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = cartaObj.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI typeText = cartaObj.transform.Find("Type").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cost = cartaObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI idText = cartaObj.transform.Find("Id").GetComponent<TextMeshProUGUI>();

        Button button = cartaObj.transform.Find("Button").GetComponent<Button>();
        Destroy(button.gameObject);
        Image image = cartaObj.GetComponent<Image>();

        titleText.text = card.title;
        descriptionText.text = card.description;
        typeText.text = card.type;
        cost.text = card.timeRequired.ToString();
        idText.text = card.id.ToString();





        RectTransform cartaRectTransform = cartaObj.GetComponent<RectTransform>();
        RectTransform previousCardRectTransform = contentPanel.childCount > 1 ? contentPanel.GetChild(contentPanel.childCount - 2).GetComponent<RectTransform>() : null;


        if (previousCardRectTransform != null)
        {
            float margin = 35f;

            Vector2 newPosition = cartaRectTransform.anchoredPosition;
            newPosition.y = previousCardRectTransform.anchoredPosition.y - margin;

            cartaRectTransform.anchoredPosition = newPosition;
        }


    }

}