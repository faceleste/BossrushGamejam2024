using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Player player;
    public float weaponSpeed;
    public float weaponDamage;
    public float speedDash;
    public int qtdDash;
    public int qtdCubHead;
    public bool canSecondAttack;
    public bool canAttackBlood;
    public bool canAttackFire;
    public List<Card> inventory = new List<Card>();
    public bool isInventoryChanged = false;
    public void UpdateStatus()
    {
        player.playerSpeed = speed;
        player.forceDash = forceDash;
        player.hp = hp;
        player.cooldownDash = cooldownDash;
    }

    public void AddToInventory(Card newCard)
    {
        Debug.Log("Adicionou");
        Debug.Log(inventory.Count());
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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            timeSettings.currentTime = timeSettings.fullTime;
            DontDestroyOnLoad(gameObject);
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