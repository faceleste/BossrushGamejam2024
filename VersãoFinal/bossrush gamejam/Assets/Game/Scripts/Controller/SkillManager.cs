using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillManager : MonoBehaviour
{
    private GameController gameController;

    private Dictionary<int, Action> skillDictionary = new Dictionary<int, Action>();
    private float time;
    private Card actualCard;

    [Header("Assets Skills")]
    public AudioClip spongeBob;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        time = gameController.timeSettings.fullTime;
        //gameController.playerSettings.speed = 5f;
        InicializarDicionarioDeHabilidades();
    }

    void InicializarDicionarioDeHabilidades()
    {
        skillDictionary.Add(1, () => DestrezaSkills.HellFist(gameController));
        skillDictionary.Add(2, () => DestrezaSkills.SteelLegs(gameController));
        skillDictionary.Add(3, () => DestrezaSkills.BulletHell(gameController));
        skillDictionary.Add(4, () => DestrezaSkills.WoodLegs(gameController));
        skillDictionary.Add(5, () => DestrezaSkills.ThunderStomp(gameController));


        skillDictionary.Add(6, () => MaestriaSkills.DoubleDash(gameController));
        skillDictionary.Add(7, () => MaestriaSkills.DemonicInsults(gameController));
        skillDictionary.Add(8, () => MaestriaSkills.SpongyBoots(gameController, spongeBob));

        skillDictionary.Add(10, () => VigorSkills.BurningBack(gameController));
        skillDictionary.Add(11, () => VigorSkills.HardHitter(gameController));
        skillDictionary.Add(12, () => VigorSkills.IndomitableSprit(gameController));
        skillDictionary.Add(13, () => VigorSkills.Superman(gameController));
        skillDictionary.Add(14, () => VigorSkills.FireMetal(gameController));

    }


    public void ActivateSkill(Card card)
    {

        int idSkill = card.id;
        actualCard = card;

        if (skillDictionary.ContainsKey(idSkill))
        {
            skillDictionary[idSkill]?.Invoke();
        }
        else
        {
            Debug.LogWarning("Unknown skill ID: " + idSkill);
        }


    }


    // DESTREZA
    // Wood Legs (Andar) = 30m
    // ThunderStomp (Dash) = 35m
    // Steel Legs (Andar + Rápido) = 5min
    // Bullet Hell (60% cdr dash) = 8min (buffei pra ser justo, era uma meio inutil) 
    // Hell First (40% weapon speed + 20% weapon damage)  = 5min


    // MAESTRIA
    // Double Dash  (Dois dashs) = 8min
    // WarriorOfDarkness (Ataque curto) = 5min
    // DemonicInsults (30% dano na arma) = 5min
    // YouVsYou (+1 cub head) = 10min

    // VIGOR
    // BurningBack (+1 hp) = 8min 
    // HardHitter (O cara sangra) = 8min
    // IndomitableSpirit (+1 shield a cada 2min ) = 5min
    // Superman (+1 hp a cada minuto não imediato = 8min
    // FireMetal (Fogo per hit / Queimar boss) = 8min 

    public static class DestrezaSkills
    {



        public static void WoodLegs(GameController gameController)
        {

            gameController.playerSettings.speed = 5f;
            gameController.playerSettings.canMove = true;
            gameController.timeSettings.currentTime += 30f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void SteelLegs(GameController gameController)
        {
            gameController.playerSettings.speed += gameController.playerSettings.speed * 0.50f;
            gameController.timeSettings.currentTime += 5f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void BulletHell(GameController gameController)
        {
            //faster dash cdr  (-60%)
            gameController.playerSettings.cooldownDash = gameController.playerSettings.cooldownDash * 0.40f;
            gameController.timeSettings.currentTime += 8f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void HellFist(GameController gameController)
        {
            //40% weapon speed and 20% weapon damage

            gameController.playerSettings.weaponSpeed += gameController.playerSettings.weaponSpeed * 0.40f;
            gameController.playerSettings.dano += gameController.playerSettings.dano * 0.20f;
            gameController.timeSettings.currentTime += 5f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void ThunderStomp(GameController gameController)
        {
            //faster dash 
            gameController.playerSettings.forceDash += 15f;
            gameController.timeSettings.currentTime += 35f * 60;
            gameController.playerSettings.UpdateStatus();
        }


    }

    public static class MaestriaSkills
    {

        public static void DoubleDash(GameController gameController)
        {
            //+1 dash
            gameController.playerSettings.qtdDash += 1;
            gameController.timeSettings.currentTime += 8f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void WarriorOfDarkness(GameController gameController)
        {
            //unlocks short range attack
            gameController.playerSettings.canSecondAttack = true;
            gameController.timeSettings.currentTime += 5f * 60;

            gameController.playerSettings.UpdateStatus();
        }

        public static void DemonicInsults(GameController gameController)
        {
            //+30% damage weapon 
            gameController.playerSettings.dano += gameController.playerSettings.dano * 0.30f;
            gameController.timeSettings.currentTime += 5f * 60;
            gameController.playerSettings.UpdateStatus();

        }



        public static void SpongyBoots(GameController gameController, AudioClip spongeBob)
        {
            //trocar o som do passo do player pelo som do bobsponja 
            gameController.playerSettings.isSpongeBob = true;



        }
    }

    public static class VigorSkills
    {

        public static void BurningBack(GameController gameController)
        {
            //+1 hp 
            gameController.playerSettings.hp += 1;
            gameController.timeSettings.currentTime += 8f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void HardHitter(GameController gameController)
        {
            //teu golpe faz o cara sangrar '-
            gameController.playerSettings.canAttackBlood = true;
            gameController.timeSettings.currentTime += 8f * 60;
        }

        public static void IndomitableSprit(GameController gameController)
        {
            //+1 shield a cada 2 minutos 
            gameController.playerSettings.numShields = 1;
            gameController.timeSettings.currentTime += 5f * 60;
            gameController.playerSettings.UpdateStatus();
        }

        public static void Superman(GameController gameController)
        {
            // +1 hp (regenera) a cada minuto 
            gameController.playerSettings.hp += 1;
            gameController.timeSettings.currentTime += 8f * 60;
        }

        public static void FireMetal(GameController gameController)
        {
            //+1 fogo (queima o boss (taporra meno ) )
            gameController.playerSettings.canAttackFire = true;
            gameController.timeSettings.currentTime += 8f * 60;
            gameController.playerSettings.UpdateStatus();
        }
    }



}
