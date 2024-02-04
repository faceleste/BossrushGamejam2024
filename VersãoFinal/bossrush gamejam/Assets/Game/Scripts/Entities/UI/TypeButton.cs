using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class TypeButton : MonoBehaviour, IPointerClickHandler
{
    public CardManager cardManager;
    public string type;
    public Image background;
    void Start()
    {
        background.color = new Color(1, 1, 0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        cardManager.type = type;
        cardManager.CallCardsType();
        if (type == "maestria")
        {
            background.color = new Color(0.6f, 0, 0.3f, 1f);
            
        }
        else if (type == "destreza")
        {
            background.color = new Color(1.2f, 0.6f, 0, 0.3f);
        }
        else if (type == "vigor")
        {
            background.color = new Color(1, 0, 0, 0.3f);
        }

    }
}