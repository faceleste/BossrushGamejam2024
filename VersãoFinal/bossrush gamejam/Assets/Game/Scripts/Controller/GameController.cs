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
    public List<Skill> skills ; 
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