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

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        time = gameController.timeSettings.fullTime;
        //gameController.playerSettings.speed = 5f;
        InicializarDicionarioDeHabilidades();
    }

    void InicializarDicionarioDeHabilidades()
    {
        skillDictionary.Add(1, () => DestrezaSkills.WoodLegs(gameController));
        skillDictionary.Add(2, () => DestrezaSkills.SteelLegs(gameController));
        skillDictionary.Add(3, () => DestrezaSkills.BulletHell(gameController));
        skillDictionary.Add(4, () => DestrezaSkills.HellFist(gameController));
        skillDictionary.Add(5, () => DestrezaSkills.ThunderStomp(gameController));


        skillDictionary.Add(6, () => MaestriaSkills.DoubleDash(gameController));
        skillDictionary.Add(7, () => MaestriaSkills.WarriorOfDarkness(gameController));
        skillDictionary.Add(8, () => MaestriaSkills.DemonicInsults(gameController));
        skillDictionary.Add(9, () => MaestriaSkills.YouVsYou(gameController));
        skillDictionary.Add(10, () => MaestriaSkills.SpongyBoots(gameController));

        skillDictionary.Add(11, () => VigorSkills.BurningBack(gameController));
        skillDictionary.Add(12, () => VigorSkills.HardHitter(gameController));
        skillDictionary.Add(13, () => VigorSkills.IndomitableSprit(gameController));
        skillDictionary.Add(14, () => VigorSkills.Superman(gameController));
        skillDictionary.Add(15, () => VigorSkills.FireMetal(gameController));

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




    public static class DestrezaSkills
    {

        public static void WoodLegs(GameController gameController)
        {

            gameController.playerSettings.speed = 5f;
            gameController.playerSettings.canMove=true;
            gameController.playerSettings.UpdateStatus();
        }

        public static void SteelLegs(GameController gameController)
        {
            gameController.playerSettings.speed += gameController.playerSettings.speed * 0.50f;
            gameController.playerSettings.UpdateStatus();
        }

        public static void BulletHell(GameController gameController)
        {
            //faster dash cdr  (-30%)
            gameController.playerSettings.cooldownDash = gameController.playerSettings.cooldownDash * 0.70f;
            gameController.playerSettings.UpdateStatus();
        }

        public static void HellFist(GameController gameController)
        {
            //40% weapon speed and 20% weapon damage
            gameController.playerSettings.cdrAttack -= 0.40f;
            gameController.playerSettings.weaponSpeed -= 0.20f;
            gameController.playerSettings.UpdateStatus(); 
        }

        public static void ThunderStomp(GameController gameController)
        {
            //faster dash 
            gameController.playerSettings.forceDash += 25f;
            gameController.playerSettings.UpdateStatus();
        }


    }

    public static class MaestriaSkills
    {

        public static void DoubleDash(GameController gameController)
        {
            //+1 dash
            gameController.playerSettings.qtdDash += 1;
            gameController.playerSettings.UpdateStatus();
        }

        public static void WarriorOfDarkness(GameController gameController)
        {
            //unlocks short range attack
            gameController.playerSettings.canSecondAttack = true;
            gameController.playerSettings.UpdateStatus();
        }

        public static void DemonicInsults(GameController gameController)
        {
            //+30% damage weapon 
            gameController.playerSettings.dano += gameController.playerSettings.dano * 0.30f;
            gameController.playerSettings.UpdateStatus();

        }

        public static void YouVsYou(GameController gameController)
        {
            //+1 club head 
            gameController.playerSettings.qtdCubHead += 1;
        }

        public static void SpongyBoots(GameController gameController)
        {
            // só DEUS sabe 

        }
    }

    public static class VigorSkills
    {

        public static void BurningBack(GameController gameController)
        {
            //+1 hp 
            gameController.playerSettings.hp += 1;
            gameController.playerSettings.UpdateStatus();
        }

        public static void HardHitter(GameController gameController)
        {
            //teu golpe faz o cara sangrar '-
            gameController.playerSettings.canAttackBlood = true;
        }

        public static void IndomitableSprit(GameController gameController)
        {
            //+1 shield a cada 2 minutos 


        }

        public static void Superman(GameController gameController)
        {
            // +1 hp (regenera) a cada minuto 
        }

        public static void FireMetal(GameController gameController)
        {
            //+1 fogo (queima o boss (taporra meno ) )
            gameController.playerSettings.canAttackFire = true;
            gameController.playerSettings.UpdateStatus(); 
        }
    }



}
