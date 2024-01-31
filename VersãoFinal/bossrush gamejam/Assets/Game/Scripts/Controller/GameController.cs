using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    [Header("Player")]
    public float speed;
    public float forceDash;
    public float hp;
    public float cooldownDash;
    public float dano;
    public int numEstagiosConcluidos;
    public float weaponSpeed;
    public float cdrAttack;
    public float speedDash;
    public float attackRange;
    public int qtdDash;
    public int qtdCubHead;
    public bool canMove = false;
    public bool canSecondAttack;
    public bool canAttackBlood;
    public bool canAttackFire;

    public List<Card> inventory = new List<Card>();
    public bool isInventoryChanged = false;
    public Player player;
    public PlayerAttack playerAttack;

    public bool isFirstTime;
    public void UpdateStatus()
    {
        player.playerSpeed = speed;
        player.forceDash = forceDash;
        player.hp = hp;
        player.cooldownDash = cooldownDash;

        playerAttack.attackDamage = dano;
        playerAttack.attackRange = attackRange;
        playerAttack.cooldownAtks = weaponSpeed;
    }

    public void AddToInventory(Card newCard)
    {
        string cardsInInventory = "";
        inventory.Add(newCard);
        Debug.Log("Card adicionado ao inventário");

        Debug.Log("Inventário: " + inventory.Count);
        for (int i = 0; i < inventory.Count; i++)
        {
            string cardTitle = inventory[i].title;
            cardsInInventory += cardTitle + " | ";
        }
        Debug.Log("Cartas no inventário: " + cardsInInventory); 

        isInventoryChanged = true;
    }
}

[System.Serializable]
public class TimeSettings
{
    [Header("Time")]
    public float fullTime;
    public float currentTime;
    public bool canCountTime;
}

public class SkillSettings : MonoBehaviour
{
    [Header("Skills")]
    public List<Skill> skills;
}

public class GameController : MonoBehaviour
{
    [Header("Instância")]
    public static GameController Instance;

    public PlayerSettings playerSettings;
    public TimeSettings timeSettings;

    private void Awake()
    {
        Debug.Log("Awakead");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            timeSettings.currentTime = timeSettings.fullTime;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if (timeSettings.canCountTime)
        {
            timeSettings.currentTime += Time.deltaTime;
        }
    }
}