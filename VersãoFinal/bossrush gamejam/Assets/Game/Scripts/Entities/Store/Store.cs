using UnityEngine;


public class Store : MonoBehaviour
{

    [Header("Store")]
    public GameController gameController;
    public Player player;

    public void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    public void AumentarHP()
    {
        gameController.playerSettings.hp += 1;
        player.UpdateData();
        Debug.Log("HP: " + gameController.playerSettings.hp);
    }

    public void DiminuirTempo()
    {
        gameController.timeSettings.fullTime -= 10;
        player.UpdateData();
        Debug.Log("Tempo: " + gameController.timeSettings.fullTime);
    }

    public void AumentarVelocidade()
    {
        gameController.playerSettings.speed += 1;
        player.UpdateData();
        Debug.Log("Velocidade: " + gameController.playerSettings.speed);
    }

    public void DiminuirCDR()
    {
        gameController.playerSettings.cooldownDash -= 1f;
        player.UpdateData();
        Debug.Log("CDR: " + gameController.playerSettings.cooldownDash);
    }

};