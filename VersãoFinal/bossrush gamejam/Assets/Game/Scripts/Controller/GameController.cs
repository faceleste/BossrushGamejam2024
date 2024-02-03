using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSettings
{
    [Header("Player")]
    public float speed;
    public float forceDash;
    public int hp;
    public int numShields;

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
    public bool canRecoverShield;
    public float damageBlood = 0.1f;

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
        player.shields = numShields;
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

    public void Reset()
    {
        speed = 0.6f;
        forceDash = 0;
        hp = 1;
        numShields = 0;
        cooldownDash = 2.8f;
        dano = 5;
        numEstagiosConcluidos = 0;
        weaponSpeed = 0;
        cdrAttack = 0;
        speedDash = 0;
        attackRange = 0;
        qtdDash = 1;
        qtdCubHead = 1;
        canMove = true;
        canSecondAttack = false;
        canAttackBlood = false;
        canAttackFire = false;
        canRecoverShield = false;
        damageBlood = 0.02f;
        inventory = new List<Card>();
    }

}

[System.Serializable]
public class TimeSettings
{
    [Header("Time")]
    public float fullTime;
    public float currentTime;
    public bool canCountTime;

    public void Reset()
    {
        currentTime = 0f;
        canCountTime = true;
        
    }
}

public class GameController : MonoBehaviour
{
    [Header("Instância")]
    public static GameController Instance;

    public PlayerSettings playerSettings;
    public TimeSettings timeSettings;

    private void Awake()
    {

        GameObject playerObject = GameObject.Find("Player");
        playerSettings.player = playerObject.GetComponent<Player>();
        playerSettings.playerAttack = playerObject.GetComponent<PlayerAttack>();
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


    private void Start()
    {
        if (playerSettings.hp <= 0)
        {
            playerSettings.hp = 1;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {


        if (Instance != this)
        {
            Destroy(gameObject);
        }
        if (timeSettings.canCountTime)
        {
            timeSettings.currentTime += Time.deltaTime;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {

            playerSettings.player = playerObject.GetComponent<Player>();
            playerSettings.playerAttack = playerObject.GetComponent<PlayerAttack>();
        }
    }

    public void Reset()
    {
        //resetar todos os atributos de t odas as subclasses da GameController 
        playerSettings.Reset();
        timeSettings.Reset();

    }
}