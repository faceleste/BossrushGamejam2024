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
    public bool isDeath;
    public bool isCheatMode;
    
    public float cooldownRecoverShield = 120;
    public float timeToRecoverShield = 120;
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

[System.Serializable]
public class OptionSettings
{
    public bool canViewConfirmation;

    public float volume;
    public bool isFullscreen;
    public int resolution;
    public bool isConfirmationViewed;
    public GameObject option;
    public void Reset()
    {
        canViewConfirmation = false;
        volume = 0.5f;
        isFullscreen = false;
        resolution = 0;
        isConfirmationViewed = false;
    }


    public void OpenMenuListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            option.SetActive(!option.activeSelf);
            Time.timeScale = option.activeSelf ? 0 : 1;

        }
    }
}

[System.Serializable]
public class StatisticSettings
{
    public int numDeaths;
    public int numDashs;
    public int numAttacks;
    public float timeToCompleteGame;
}

public class GameController : MonoBehaviour
{
    [Header("Instância")]
    public static GameController Instance;

    public PlayerSettings playerSettings;
    public TimeSettings timeSettings;
    public OptionSettings optionSettings;
    public StatisticSettings statisticSettings;

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
        playerSettings.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (playerSettings.hp <= 0)
        {
            playerSettings.hp = 1;
        }


        SceneManager.sceneLoaded += OnSceneLoaded;
        optionSettings.canViewConfirmation = true;
    }

    private void Update()
    {
        if(playerSettings.player.shields == 0)
        {
            playerSettings.cooldownRecoverShield -= Time.deltaTime;
        }
        if(playerSettings.cooldownRecoverShield <= 0)
        {
            playerSettings.player.shields = 1;
            playerSettings.cooldownRecoverShield = playerSettings.timeToRecoverShield;        
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
        if (timeSettings.canCountTime)
        {
            timeSettings.currentTime += Time.deltaTime;
        }


        if (playerSettings.isDeath)
        {
            playerSettings.isDeath = false;
            statisticSettings.numDeaths++;

        }

        if (playerSettings.numEstagiosConcluidos < 4)
        {
            statisticSettings.timeToCompleteGame += Time.deltaTime;
        }

 
        optionSettings.OpenMenuListener();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GameObject playerObject = GameObject.Find("Player");
        //findbytag
        GameObject optionObject = GameObject.FindWithTag("OptionMenu").gameObject;
        optionObject.SetActive(false);

        if (playerObject != null)
        {

            playerSettings.player = playerObject.GetComponent<Player>();
            playerSettings.playerAttack = playerObject.GetComponent<PlayerAttack>();
            optionSettings.option = optionObject;
        }
    }

    public void Reset()
    {
        playerSettings.Reset();
        timeSettings.Reset();
        optionSettings.Reset();

    }
}