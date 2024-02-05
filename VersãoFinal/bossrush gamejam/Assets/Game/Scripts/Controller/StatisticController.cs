using System;
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

    public CardBuilder builder;
    private GameObject inventory;
   
    void Start()
    {
        inventory = GameObject.Find("Inventory"); ;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        float time = gameController.statisticSettings.timeToCompleteGame / 60;
        text.text = "Deaths: " + gameController.statisticSettings.numDeaths + "\n" +
                    "Dashes: " + gameController.statisticSettings.numDashs + "\n" +
                    "Attacks: " + gameController.statisticSettings.numAttacks + "\n" +
                    "Completed in: " + Math.Round(time, 2) + (time < 1 ? " sec" : " min") + "\n";
        continueButton.onClick.AddListener(() =>
        {
            statistic.SetActive(false);
        });

        SetInventory();
    }

    void Update()
    {
        List<int> inv = gameController.playerSettings.inventory;
        string strInv = "";

        foreach (int i in inv)
        {
            strInv += i + " ";
        }
        Debug.Log(strInv);
    }


    public void SetInventory()
    {
        List<int> inventoryCards = gameController.playerSettings.inventory;
        List<Card> cards = ListagemCards();
        float margin = -340;
        foreach (Card card in cards)
        {
            if (inventoryCards.Contains(card.id))
            {
                Sprite sprite = card.art;
                GameObject newCard = new GameObject();
                newCard.AddComponent<Image>();
                newCard.GetComponent<Image>().sprite = sprite;
                newCard.transform.SetParent(inventory.transform);
                newCard.GetComponent<RectTransform>().localPosition = new Vector3(margin, 0, 0);
                newCard.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
                margin += 150;
            }
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
