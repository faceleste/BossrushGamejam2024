using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TypeButton : MonoBehaviour, IPointerClickHandler
{
    public CardManager cardManager;
    public string type;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        cardManager.type = type;
        cardManager.CallCardsType();
    }
}