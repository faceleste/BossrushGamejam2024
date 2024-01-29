using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDialogController : MonoBehaviour
{
    public GameObject dialogPlayer;
    public GameObject dialogBoss;
    public TextMeshProUGUI textmeshPlayer;
    public TextMeshProUGUI textmeshBoss;
    public string textBoss;
    public string TextPlayer;
    public Player player;
    public Cam camera;
    public float timeToStartDialog;
    public float timeToReturnToPlayer;
    public float timeToEndDialogs;
    public Transform bossTranform;
    public BossScript bossScript;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayDialog());
        //player = GameObject.FindGameObjectsWithTag("Player").GetComponent<Player>();
        camera = this.GetComponent<Cam>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayDialog()
    {
        player.canWalk = false;
        yield return new WaitForSeconds(timeToStartDialog);
        camera.player = bossTranform;
        yield return new WaitForSeconds(1f);
        dialogBoss.SetActive(true);
        yield return new WaitForSeconds(timeToReturnToPlayer);
        camera.player = camera.newPlayer;
        yield return new WaitForSeconds(1f);
        dialogPlayer.SetActive(true);
        yield return new WaitForSeconds(timeToEndDialogs);
        player.canWalk = true;
        dialogBoss.SetActive(false);
        dialogPlayer.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        bossScript.StartRoutineBoss();
    }
}
