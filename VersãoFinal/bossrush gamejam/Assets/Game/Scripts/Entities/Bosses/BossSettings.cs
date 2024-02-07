using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
public class BossSettings : MonoBehaviour
{
    [Header("Stats")]
    public float vida;
    public float currentVida;

    public GameObject splashSangue;
    public Transform centerBoss;
    public Player player;
    public ScriptBackLobby lobby;
    public float f;
    public GameController gameController;
    public PlayerAttack pAttack;
    public Image barraVida;

    public float speed = 1.0f; // velocidade de movimento
    public float width = 1.0f; // largura do movimento
    public float height = 1.0f; // altura do movimento

    [Header("Bool Ataques")]

    public bool atk01;
    public bool atk02;
    public bool atk03;


    [Header("Assets")]

    public Transform playerPosition;
    public Transform armaMaca;
    public Animator animCam;
    public Animator anim;
    public Vector3 lastPlayerPosition;

    [Header("Condições")]

    public bool isFire = false;
    public bool isBledding;

    private int bloodBossCount = 0;
    public int stacksBlood = 1;

    public GameObject sangueSangramento;
    public GameObject fogoSkill;
    public GameObject fogoSkillPlayer;
    public bool canAtkFogo = false;


    [Header("Audio")]
  
    public List<AudioClip> audioClips = new List<AudioClip>();
}