using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticController : MonoBehaviour
{
    private GameController gameController;
    public TextMeshProUGUI text;
    public Button continueButton;
    public GameObject statistic;


    private GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory"); ;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        text.text = "Deaths: " + gameController.statisticSettings.numDeaths + "\n" +
                    "Dashes: " + gameController.statisticSettings.numDashs + "\n" +
                    "Attacks: " + gameController.statisticSettings.numAttacks + "\n" +
                    "Time to complete: " + gameController.statisticSettings.timeToCompleteGame / 60 + " minutes" + "\n" ;
        continueButton.onClick.AddListener(() =>
        {
            statistic.SetActive(false);
            GameObject.Find("Inventory"); 
        });

        SetInventory();
    }

    //fazer com que as cartas do inventario apareçam no objeto inventory, da esquerda para direita
    void Update()
    {

    }


    public void SetInventory()
    {
        List<Card> playerCards = gameController.playerSettings.inventory;
        float margin = -220;
        foreach (Card card in playerCards)
        {
            Sprite sprite = card.art;
            GameObject newCard = new GameObject();
            newCard.AddComponent<Image>();
            newCard.GetComponent<Image>().sprite = sprite;
            newCard.transform.SetParent(inventory.transform);
            //fazer os cards 250250
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 200);
            newCard.GetComponent<RectTransform>().localPosition = new Vector3(margin, 0, 0);
            newCard.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            margin += 50;


        }
    }

}
